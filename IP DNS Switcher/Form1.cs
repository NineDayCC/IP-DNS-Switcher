using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management; // 添加WMI命名空间
using System.Security.Principal;
using System.IO;
using Newtonsoft.Json;

namespace IP_DNS_Switcher
{
    public partial class Form1 : Form
    {
        // 简单的配置结构体
        private class NetProfile
        {
            public string Name { get; set; }
            public string IP { get; set; }
            public string Subnet { get; set; }
            public string Gateway { get; set; }
            public string DNS1 { get; set; }
            public string DNS2 { get; set; }
            public override string ToString() => Name;
        }

        // 配置文件结构体，包含方案列表和上次选择
        private class ProfileData
        {
            public List<NetProfile> Profiles { get; set; } = new List<NetProfile>();
            public string LastProfileName { get; set; }
        }

        private ProfileData profileData = new ProfileData();
        private bool isAdmin = false;
        private string profileFile = "profiles.json";

        // 方案名长度限制
        private const int MaxProfileNameLength = 32;

        public Form1()
        {
            // 窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            isAdmin = IsRunAsAdmin();
            if (!isAdmin)
            {
                MessageBox.Show("请以管理员身份运行本程序，否则网络设置将无法生效！", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            InitAdapters();
            comboBoxAdapters.SelectedIndexChanged += ComboBoxAdapters_SelectedIndexChanged;
            InitProfiles();
            comboBoxProfiles.SelectedIndexChanged += ComboBoxProfiles_SelectedIndexChanged;
            buttonApplyStatic.Click += ButtonApplyStatic_Click;
            buttonSetDHCP.Click += ButtonSetDHCP_Click;
            // buttonSaveProfile.Click += buttonSaveProfile_Click; // 移除，避免重复绑定
            ComboBoxProfiles_SelectedIndexChanged(null, null);
            ShowCurrentNetworkStatus();
        }

        // 检查是否以管理员权限运行
        private bool IsRunAsAdmin()
        {
            try
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch { return false; }
        }

        private void InitProfiles()
        {
            profileData = new ProfileData();
            comboBoxProfiles.Items.Clear();
            // 读取配置文件
            if (File.Exists(profileFile))
            {
                try
                {
                    var json = File.ReadAllText(profileFile);
                    var loaded = JsonConvert.DeserializeObject<ProfileData>(json);
                    if (loaded != null)
                    {
                        // 过滤非法方案名和IP
                        loaded.Profiles = loaded.Profiles.Where(p => !string.IsNullOrWhiteSpace(p.Name)
                            && p.Name.Length <= MaxProfileNameLength
                            && IsValidIP(p.IP)
                            && IsValidIP(p.Subnet)
                            && IsValidIP(p.Gateway)
                            && IsValidIP(p.DNS1)
                            && (string.IsNullOrWhiteSpace(p.DNS2) || IsValidIP(p.DNS2))
                        ).ToList();
                        profileData = loaded;
                    }
                }
                catch { }
            }
            // 如果没有配置则加一个默认
            if (profileData.Profiles.Count == 0)
            {
                profileData.Profiles.Add(new NetProfile { Name = "NAS 旁路由", IP = "192.168.50.10", Subnet = "255.255.255.0", Gateway = "192.168.50.6", DNS1 = "192.168.50.6", DNS2 = "114.114.114.114" });
            }
            comboBoxProfiles.Items.AddRange(profileData.Profiles.ToArray());
            // 记住上次选择的方案
            int idx = 0;
            if (!string.IsNullOrEmpty(profileData.LastProfileName))
            {
                int found = profileData.Profiles.FindIndex(p => p.Name == profileData.LastProfileName);
                if (found >= 0) idx = found;
            }
            if (profileData.Profiles.Count > 0) comboBoxProfiles.SelectedIndex = idx;
        }

        // 保存配置到文件
        private void SaveProfiles()
        {
            try
            {
                var json = JsonConvert.SerializeObject(profileData, Formatting.Indented);
                File.WriteAllText(profileFile, json);
            }
            catch { }
        }

        private void ComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var p = comboBoxProfiles.SelectedItem as NetProfile;
            if (p != null)
            {
                textBoxIP.Text = p.IP;
                textBoxSubnet.Text = p.Subnet;
                textBoxGateway.Text = p.Gateway;
                textBoxDNS1.Text = p.DNS1;
                textBoxDNS2.Text = p.DNS2;
                // 保存当前选择
                profileData.LastProfileName = p.Name;
                SaveProfiles();
            }
            ShowCurrentNetworkStatus(); // 新增
        }

        private void ButtonApplyStatic_Click(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                MessageBox.Show("请以管理员身份运行本程序，否则无法设置网络！", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string nic = GetSelectedAdapterName();
            if (string.IsNullOrEmpty(nic)) { MessageBox.Show("未找到有线网卡"); return; }
            bool ok = SetStaticIP(nic, textBoxIP.Text, textBoxSubnet.Text, textBoxGateway.Text);
            bool ok2 = SetDNS(nic, textBoxDNS1.Text, textBoxDNS2.Text);
            if (ok && ok2)
                MessageBox.Show("已应用静态配置");
            else
                MessageBox.Show("应用静态配置失败，请以管理员身份运行或检查参数！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ShowCurrentNetworkStatus(); // 新增
        }

        private void ButtonSetDHCP_Click(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                MessageBox.Show("请以管理员身份运行本程序，否则无法设置网络！", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string nic = GetSelectedAdapterName();
            if (string.IsNullOrEmpty(nic)) { MessageBox.Show("未找到有线网卡"); return; }
            bool ok = SetDHCP(nic);
            bool ok2 = SetDNSDHCP(nic);
            if (ok && ok2)
                MessageBox.Show("已切换为自动获取");
            else
                MessageBox.Show("切换为自动获取失败，请以管理员身份运行！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ShowCurrentNetworkStatus(); // 新增
        }

        // 保存网卡名和Index的映射
        private Dictionary<string, uint> adapterNameToIndex = new Dictionary<string, uint>();

        // 新增：初始化网卡下拉框
        private void InitAdapters()
        {
            comboBoxAdapters.Items.Clear();
            adapterNameToIndex.Clear();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2 AND AdapterTypeID=0");
            foreach (ManagementObject mo in searcher.Get())
            {
                var name = mo["NetConnectionID"]?.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    comboBoxAdapters.Items.Add(name);
                    adapterNameToIndex[name] = (uint)mo["Index"];
                }
            }
            if (comboBoxAdapters.Items.Count > 0)
                comboBoxAdapters.SelectedIndex = 0;
        }

        // 新增：切换网卡时刷新状态
        private void ComboBoxAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCurrentNetworkStatus();
        }

        // 修改：获取当前选中网卡名称
        private string GetSelectedAdapterName()
        {
            return comboBoxAdapters.SelectedItem as string;
        }

        // 获取当前选中网卡的Index
        private uint? GetSelectedAdapterIndex()
        {
            string selectedName = comboBoxAdapters.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedName)) return null;
            if (adapterNameToIndex.TryGetValue(selectedName, out uint idx))
                return idx;
            return null;
        }

        // 设置静态IP
        private bool SetStaticIP(string nic, string ip, string mask, string gateway)
        {
            uint? idx = GetSelectedAdapterIndex();
            if (idx == null) return false;
            var mos = new ManagementObjectSearcher($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled=TRUE AND Index={idx}");
            foreach (ManagementObject mo in mos.Get())
            {
                var res1 = mo.InvokeMethod("EnableStatic", new object[] { new string[] { ip }, new string[] { mask } });
                var res2 = mo.InvokeMethod("SetGateways", new object[] { new string[] { gateway }, new ushort[] { 1 } });
                return (res1 != null && (uint)res1 == 0) && (res2 != null && (uint)res2 == 0);
            }
            return false;
        }
        // 设置DNS
        private bool SetDNS(string nic, string dns1, string dns2)
        {
            uint? idx = GetSelectedAdapterIndex();
            if (idx == null) return false;
            var mos = new ManagementObjectSearcher($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled=TRUE AND Index={idx}");
            foreach (ManagementObject mo in mos.Get())
            {
                var res = mo.InvokeMethod("SetDNSServerSearchOrder", new object[] { new string[] { dns1, dns2 } });
                return (res != null && (uint)res == 0);
            }
            return false;
        }
        // 切换为DHCP
        private bool SetDHCP(string nic)
        {
            uint? idx = GetSelectedAdapterIndex();
            if (idx == null) return false;
            var mos = new ManagementObjectSearcher($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled=TRUE AND Index={idx}");
            foreach (ManagementObject mo in mos.Get())
            {
                var res = mo.InvokeMethod("EnableDHCP", null);
                return (res != null && (uint)res == 0);
            }
            return false;
        }
        // DNS自动获取
        private bool SetDNSDHCP(string nic)
        {
            uint? idx = GetSelectedAdapterIndex();
            if (idx == null) return false;
            var mos = new ManagementObjectSearcher($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled=TRUE AND Index={idx}");
            foreach (ManagementObject mo in mos.Get())
            {
                var res = mo.InvokeMethod("SetDNSServerSearchOrder", null);
                return (res != null && (uint)res == 0);
            }
            return false;
        }

        // 新增：显示当前网络连接状态（自动/手动）及当前IP和DNS
        private void ShowCurrentNetworkStatus()
        {
            uint? idx = GetSelectedAdapterIndex();
            if (idx == null)
            {
                labelStatus.Text = "未找到有线网卡";
                return;
            }
            var mos = new ManagementObjectSearcher($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled=TRUE AND Index={idx}");
            foreach (ManagementObject mo in mos.Get())
            {
                bool isDHCP = (bool)(mo["DHCPEnabled"] ?? false);
                string[] ips = mo["IPAddress"] as string[];
                string[] masks = mo["IPSubnet"] as string[];
                string[] gateways = mo["DefaultIPGateway"] as string[];
                string[] dns = mo["DNSServerSearchOrder"] as string[];
                string ip = (ips != null && ips.Length > 0) ? ips[0] : "-";
                string mask = (masks != null && masks.Length > 0) ? masks[0] : "-";
                string gateway = (gateways != null && gateways.Length > 0) ? gateways[0] : "-";
                string dns1 = (dns != null && dns.Length > 0) ? dns[0] : "-";
                string dns2 = (dns != null && dns.Length > 1) ? dns[1] : "-";
                // 格式化对齐
                string info =
                    (isDHCP ? "当前为自动获取(DHCP)\n" : "当前为手动设置\n") +
                    $"IP地址      : {ip}\n" +
                    $"子网掩码   : {mask}\n" +
                    $"网关          : {gateway}\n" +
                    $"主DNS      : {dns1}\n" +
                    $"备DNS      : {dns2}";
                labelStatus.Text = info;
                // 建议在Designer中将labelStatus.Font设为Consolas等宽字体
                return;
            }
            labelStatus.Text = "未检测到网络配置";
        }

        // 新增：保存当前输入为新方案
        private void buttonSaveProfile_Click(object sender, EventArgs e)
        {
            string name = PromptForProfileName();
            if (string.IsNullOrWhiteSpace(name)) return;
            if (name.Length > MaxProfileNameLength)
            {
                MessageBox.Show($"方案名不能超过{MaxProfileNameLength}个字符！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 检查IP合法性
            if (!IsValidIP(textBoxIP.Text.Trim()) || !IsValidIP(textBoxSubnet.Text.Trim()) || !IsValidIP(textBoxGateway.Text.Trim()) || !IsValidIP(textBoxDNS1.Text.Trim()) || (!string.IsNullOrWhiteSpace(textBoxDNS2.Text.Trim()) && !IsValidIP(textBoxDNS2.Text.Trim())))
            {
                MessageBox.Show("IP、子网掩码、网关、DNS地址格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var p = new NetProfile
            {
                Name = name.Trim(),
                IP = textBoxIP.Text.Trim(),
                Subnet = textBoxSubnet.Text.Trim(),
                Gateway = textBoxGateway.Text.Trim(),
                DNS1 = textBoxDNS1.Text.Trim(),
                DNS2 = textBoxDNS2.Text.Trim()
            };
            profileData.Profiles.Add(p);
            profileData.LastProfileName = p.Name;
            SaveProfiles();
            InitProfiles();
            int idx = profileData.Profiles.FindIndex(x => x.Name == p.Name);
            if (idx >= 0) comboBoxProfiles.SelectedIndex = idx;
            MessageBox.Show("方案已保存！");
        }

        // 删除当前选中方案
        private void buttonDeleteProfile_Click(object sender, EventArgs e)
        {
            int idx = comboBoxProfiles.SelectedIndex;
            if (idx < 0 || idx >= profileData.Profiles.Count)
            {
                MessageBox.Show("请选择要删除的方案。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var p = profileData.Profiles[idx];
            var result = MessageBox.Show($"确定要删除方案：{p.Name} 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                profileData.Profiles.RemoveAt(idx);
                // 如果删除的是当前记忆的，清空LastProfileName
                if (profileData.LastProfileName == p.Name)
                    profileData.LastProfileName = profileData.Profiles.Count > 0 ? profileData.Profiles[0].Name : null;
                SaveProfiles();
                InitProfiles();
                MessageBox.Show("方案已删除！");
            }
        }

        // 通用输入框
        private string PromptForProfileName()
        {
            string defaultName = "";
            var current = comboBoxProfiles.SelectedItem as NetProfile;
            if (current != null)
                defaultName = current.Name;
            using (var form = new Form())
            using (var label = new Label() { Left = 10, Top = 15, Text = "请输入方案名称：" })
            using (var textBox = new TextBox() { Left = 10, Top = 40, Width = 260, Text = defaultName })
            using (var buttonOk = new Button() { Text = "确定", Left = 110, Width = 60, Top = 70, DialogResult = DialogResult.OK })
            {
                form.Text = "保存方案";
                form.ClientSize = new Size(280, 110);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk });
                form.AcceptButton = buttonOk;
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(this) == DialogResult.OK)
                    return textBox.Text;
                return null;
            }
        }

        // IP合法性检查
        private bool IsValidIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip)) return false;
            System.Net.IPAddress addr;
            return System.Net.IPAddress.TryParse(ip, out addr);
        }
    }
}
