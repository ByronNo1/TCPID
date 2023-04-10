namespace TCPIP
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.labRemoteIP = new System.Windows.Forms.Label();
            this.labRemotePort = new System.Windows.Forms.Label();
            this.labDeviceID = new System.Windows.Forms.Label();
            this.labLocalIP = new System.Windows.Forms.Label();
            this.labLocalPort = new System.Windows.Forms.Label();
            this.groBoxEthernetSetting = new System.Windows.Forms.GroupBox();
            this.groBoxEthernetSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(1159, 463);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(464, 192);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(22, 138);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(317, 33);
            this.txtRemoteIP.TabIndex = 1;
            this.txtRemoteIP.Text = "127.0.0.1";
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(23, 216);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(317, 33);
            this.txtRemotePort.TabIndex = 1;
            this.txtRemotePort.Text = "5000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1158, 721);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(365, 160);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(23, 64);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(316, 33);
            this.txtDeviceID.TabIndex = 3;
            this.txtDeviceID.Text = "0";
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(25, 368);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(316, 33);
            this.txtLocalPort.TabIndex = 1;
            this.txtLocalPort.Text = "5000";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(24, 287);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Size = new System.Drawing.Size(317, 33);
            this.txtLocalIP.TabIndex = 1;
            this.txtLocalIP.Text = "127.0.0.1";
            // 
            // labRemoteIP
            // 
            this.labRemoteIP.AutoSize = true;
            this.labRemoteIP.Location = new System.Drawing.Point(18, 105);
            this.labRemoteIP.Name = "labRemoteIP";
            this.labRemoteIP.Size = new System.Drawing.Size(94, 21);
            this.labRemoteIP.TabIndex = 4;
            this.labRemoteIP.Text = "Remote IP";
            // 
            // labRemotePort
            // 
            this.labRemotePort.AutoSize = true;
            this.labRemotePort.Location = new System.Drawing.Point(20, 192);
            this.labRemotePort.Name = "labRemotePort";
            this.labRemotePort.Size = new System.Drawing.Size(109, 21);
            this.labRemotePort.TabIndex = 4;
            this.labRemotePort.Text = "Remote Port";
            // 
            // labDeviceID
            // 
            this.labDeviceID.AutoSize = true;
            this.labDeviceID.Location = new System.Drawing.Point(21, 40);
            this.labDeviceID.Name = "labDeviceID";
            this.labDeviceID.Size = new System.Drawing.Size(87, 21);
            this.labDeviceID.TabIndex = 4;
            this.labDeviceID.Text = "DeviceID";
            // 
            // labLocalIP
            // 
            this.labLocalIP.AutoSize = true;
            this.labLocalIP.Location = new System.Drawing.Point(21, 263);
            this.labLocalIP.Name = "labLocalIP";
            this.labLocalIP.Size = new System.Drawing.Size(78, 21);
            this.labLocalIP.TabIndex = 5;
            this.labLocalIP.Text = "Local IP";
            // 
            // labLocalPort
            // 
            this.labLocalPort.AutoSize = true;
            this.labLocalPort.Location = new System.Drawing.Point(21, 344);
            this.labLocalPort.Name = "labLocalPort";
            this.labLocalPort.Size = new System.Drawing.Size(93, 21);
            this.labLocalPort.TabIndex = 5;
            this.labLocalPort.Text = "Local Port";
            // 
            // groBoxEthernetSetting
            // 
            this.groBoxEthernetSetting.Controls.Add(this.txtDeviceID);
            this.groBoxEthernetSetting.Controls.Add(this.labLocalPort);
            this.groBoxEthernetSetting.Controls.Add(this.labDeviceID);
            this.groBoxEthernetSetting.Controls.Add(this.labLocalIP);
            this.groBoxEthernetSetting.Controls.Add(this.txtLocalPort);
            this.groBoxEthernetSetting.Controls.Add(this.labRemotePort);
            this.groBoxEthernetSetting.Controls.Add(this.txtRemoteIP);
            this.groBoxEthernetSetting.Controls.Add(this.labRemoteIP);
            this.groBoxEthernetSetting.Controls.Add(this.txtLocalIP);
            this.groBoxEthernetSetting.Controls.Add(this.txtRemotePort);
            this.groBoxEthernetSetting.Location = new System.Drawing.Point(199, 71);
            this.groBoxEthernetSetting.Name = "groBoxEthernetSetting";
            this.groBoxEthernetSetting.Size = new System.Drawing.Size(406, 478);
            this.groBoxEthernetSetting.TabIndex = 6;
            this.groBoxEthernetSetting.TabStop = false;
            this.groBoxEthernetSetting.Text = "Ethernet Setting";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1706, 1129);
            this.Controls.Add(this.groBoxEthernetSetting);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTest);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groBoxEthernetSetting.ResumeLayout(false);
            this.groBoxEthernetSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.TextBox txtLocalIP;
        private System.Windows.Forms.Label labRemoteIP;
        private System.Windows.Forms.Label labRemotePort;
        private System.Windows.Forms.Label labDeviceID;
        private System.Windows.Forms.Label labLocalIP;
        private System.Windows.Forms.Label labLocalPort;
        private System.Windows.Forms.GroupBox groBoxEthernetSetting;
    }
}

