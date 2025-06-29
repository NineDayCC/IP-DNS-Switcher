namespace IP_DNS_Switcher
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxSubnet = new System.Windows.Forms.TextBox();
            this.textBoxGateway = new System.Windows.Forms.TextBox();
            this.textBoxDNS1 = new System.Windows.Forms.TextBox();
            this.textBoxDNS2 = new System.Windows.Forms.TextBox();
            this.buttonApplyStatic = new System.Windows.Forms.Button();
            this.buttonSetDHCP = new System.Windows.Forms.Button();
            this.buttonSaveProfile = new System.Windows.Forms.Button();
            this.buttonDeleteProfile = new System.Windows.Forms.Button();
            this.labelProfile = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelSubnet = new System.Windows.Forms.Label();
            this.labelGateway = new System.Windows.Forms.Label();
            this.labelDNS1 = new System.Windows.Forms.Label();
            this.labelDNS2 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxAdapters = new System.Windows.Forms.ComboBox();
            this.labelAdapter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(120, 60);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(200, 28);
            this.comboBoxProfiles.TabIndex = 2;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(120, 100);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(200, 27);
            this.textBoxIP.TabIndex = 3;
            // 
            // textBoxSubnet
            // 
            this.textBoxSubnet.Location = new System.Drawing.Point(120, 140);
            this.textBoxSubnet.Name = "textBoxSubnet";
            this.textBoxSubnet.Size = new System.Drawing.Size(200, 27);
            this.textBoxSubnet.TabIndex = 4;
            // 
            // textBoxGateway
            // 
            this.textBoxGateway.Location = new System.Drawing.Point(120, 180);
            this.textBoxGateway.Name = "textBoxGateway";
            this.textBoxGateway.Size = new System.Drawing.Size(200, 27);
            this.textBoxGateway.TabIndex = 5;
            // 
            // textBoxDNS1
            // 
            this.textBoxDNS1.Location = new System.Drawing.Point(120, 220);
            this.textBoxDNS1.Name = "textBoxDNS1";
            this.textBoxDNS1.Size = new System.Drawing.Size(200, 27);
            this.textBoxDNS1.TabIndex = 6;
            // 
            // textBoxDNS2
            // 
            this.textBoxDNS2.Location = new System.Drawing.Point(120, 260);
            this.textBoxDNS2.Name = "textBoxDNS2";
            this.textBoxDNS2.Size = new System.Drawing.Size(200, 27);
            this.textBoxDNS2.TabIndex = 7;
            // 
            // buttonApplyStatic
            // 
            this.buttonApplyStatic.Location = new System.Drawing.Point(230, 304);
            this.buttonApplyStatic.Name = "buttonApplyStatic";
            this.buttonApplyStatic.Size = new System.Drawing.Size(90, 30);
            this.buttonApplyStatic.TabIndex = 8;
            this.buttonApplyStatic.Text = "应用静态配置";
            this.buttonApplyStatic.UseVisualStyleBackColor = true;
            // 
            // buttonSetDHCP
            // 
            this.buttonSetDHCP.Location = new System.Drawing.Point(230, 340);
            this.buttonSetDHCP.Name = "buttonSetDHCP";
            this.buttonSetDHCP.Size = new System.Drawing.Size(90, 30);
            this.buttonSetDHCP.TabIndex = 9;
            this.buttonSetDHCP.Text = "自动获取";
            this.buttonSetDHCP.UseVisualStyleBackColor = true;
            // 
            // buttonSaveProfile
            // 
            this.buttonSaveProfile.Location = new System.Drawing.Point(44, 304);
            this.buttonSaveProfile.Name = "buttonSaveProfile";
            this.buttonSaveProfile.Size = new System.Drawing.Size(90, 30);
            this.buttonSaveProfile.TabIndex = 10;
            this.buttonSaveProfile.Text = "保存方案";
            this.buttonSaveProfile.UseVisualStyleBackColor = true;
            this.buttonSaveProfile.Click += new System.EventHandler(this.buttonSaveProfile_Click);
            // 
            // buttonDeleteProfile
            // 
            this.buttonDeleteProfile.Location = new System.Drawing.Point(44, 340);
            this.buttonDeleteProfile.Name = "buttonDeleteProfile";
            this.buttonDeleteProfile.Size = new System.Drawing.Size(90, 30);
            this.buttonDeleteProfile.TabIndex = 11;
            this.buttonDeleteProfile.Text = "删除方案";
            this.buttonDeleteProfile.UseVisualStyleBackColor = true;
            this.buttonDeleteProfile.Click += new System.EventHandler(this.buttonDeleteProfile_Click);
            // 
            // labelProfile
            // 
            this.labelProfile.AutoSize = true;
            this.labelProfile.Location = new System.Drawing.Point(40, 63);
            this.labelProfile.Name = "labelProfile";
            this.labelProfile.Size = new System.Drawing.Size(69, 20);
            this.labelProfile.TabIndex = 12;
            this.labelProfile.Text = "配置方案";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(40, 103);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(52, 20);
            this.labelIP.TabIndex = 13;
            this.labelIP.Text = "IP地址";
            // 
            // labelSubnet
            // 
            this.labelSubnet.AutoSize = true;
            this.labelSubnet.Location = new System.Drawing.Point(40, 143);
            this.labelSubnet.Name = "labelSubnet";
            this.labelSubnet.Size = new System.Drawing.Size(69, 20);
            this.labelSubnet.TabIndex = 14;
            this.labelSubnet.Text = "子网掩码";
            // 
            // labelGateway
            // 
            this.labelGateway.AutoSize = true;
            this.labelGateway.Location = new System.Drawing.Point(40, 183);
            this.labelGateway.Name = "labelGateway";
            this.labelGateway.Size = new System.Drawing.Size(69, 20);
            this.labelGateway.TabIndex = 15;
            this.labelGateway.Text = "默认网关";
            // 
            // labelDNS1
            // 
            this.labelDNS1.AutoSize = true;
            this.labelDNS1.Location = new System.Drawing.Point(40, 223);
            this.labelDNS1.Name = "labelDNS1";
            this.labelDNS1.Size = new System.Drawing.Size(86, 20);
            this.labelDNS1.TabIndex = 16;
            this.labelDNS1.Text = "主DNS地址";
            // 
            // labelDNS2
            // 
            this.labelDNS2.AutoSize = true;
            this.labelDNS2.Location = new System.Drawing.Point(40, 263);
            this.labelDNS2.Name = "labelDNS2";
            this.labelDNS2.Size = new System.Drawing.Size(86, 20);
            this.labelDNS2.TabIndex = 17;
            this.labelDNS2.Text = "备DNS地址";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelStatus.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStatus.Location = new System.Drawing.Point(40, 390);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(144, 24);
            this.labelStatus.TabIndex = 18;
            this.labelStatus.Text = "网络状态检测中...";
            // 
            // comboBoxAdapters
            // 
            this.comboBoxAdapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdapters.FormattingEnabled = true;
            this.comboBoxAdapters.Location = new System.Drawing.Point(120, 23);
            this.comboBoxAdapters.Name = "comboBoxAdapters";
            this.comboBoxAdapters.Size = new System.Drawing.Size(200, 28);
            this.comboBoxAdapters.TabIndex = 1;
            // 
            // labelAdapter
            // 
            this.labelAdapter.AutoSize = true;
            this.labelAdapter.Location = new System.Drawing.Point(40, 26);
            this.labelAdapter.Name = "labelAdapter";
            this.labelAdapter.Size = new System.Drawing.Size(69, 20);
            this.labelAdapter.TabIndex = 0;
            this.labelAdapter.Text = "选择网卡";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(367, 513);
            this.Controls.Add(this.labelAdapter);
            this.Controls.Add(this.comboBoxAdapters);
            this.Controls.Add(this.comboBoxProfiles);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxSubnet);
            this.Controls.Add(this.textBoxGateway);
            this.Controls.Add(this.textBoxDNS1);
            this.Controls.Add(this.textBoxDNS2);
            this.Controls.Add(this.buttonApplyStatic);
            this.Controls.Add(this.buttonSetDHCP);
            this.Controls.Add(this.buttonSaveProfile);
            this.Controls.Add(this.buttonDeleteProfile);
            this.Controls.Add(this.labelProfile);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.labelSubnet);
            this.Controls.Add(this.labelGateway);
            this.Controls.Add(this.labelDNS1);
            this.Controls.Add(this.labelDNS2);
            this.Controls.Add(this.labelStatus);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "Form1";
            this.Text = "IP/DNS一键切换";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxSubnet;
        private System.Windows.Forms.TextBox textBoxGateway;
        private System.Windows.Forms.TextBox textBoxDNS1;
        private System.Windows.Forms.TextBox textBoxDNS2;
        private System.Windows.Forms.Button buttonApplyStatic;
        private System.Windows.Forms.Button buttonSetDHCP;
        private System.Windows.Forms.Button buttonSaveProfile;
        private System.Windows.Forms.Button buttonDeleteProfile;
        private System.Windows.Forms.Label labelProfile;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelSubnet;
        private System.Windows.Forms.Label labelGateway;
        private System.Windows.Forms.Label labelDNS1;
        private System.Windows.Forms.Label labelDNS2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxAdapters;
        private System.Windows.Forms.Label labelAdapter;

        #endregion
    }
}

