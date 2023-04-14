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
            this.btnConnect = new System.Windows.Forms.Button();
            this.picEthernetConnect = new System.Windows.Forms.PictureBox();
            this.btnDisConnected = new System.Windows.Forms.Button();
            this.palEthernetSetting = new System.Windows.Forms.Panel();
            this.radbtnActive = new System.Windows.Forms.RadioButton();
            this.radbtnPassive = new System.Windows.Forms.RadioButton();
            this.picRed = new System.Windows.Forms.PictureBox();
            this.picGreen = new System.Windows.Forms.PictureBox();
            this.picGray = new System.Windows.Forms.PictureBox();
            this.picYellow = new System.Windows.Forms.PictureBox();
            this.txtSendMsg = new System.Windows.Forms.TextBox();
            this.txtReceiveMsg = new System.Windows.Forms.TextBox();
            this.groBoxEthernetSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEthernetConnect)).BeginInit();
            this.palEthernetSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picYellow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(1073, 526);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(261, 116);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(16, 99);
            this.txtRemoteIP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(260, 29);
            this.txtRemoteIP.TabIndex = 1;
            this.txtRemoteIP.Text = "127.0.0.1";
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(16, 166);
            this.txtRemotePort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(260, 29);
            this.txtRemotePort.TabIndex = 1;
            this.txtRemotePort.Text = "6000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1074, 671);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(261, 116);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(16, 36);
            this.txtDeviceID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(259, 29);
            this.txtDeviceID.TabIndex = 3;
            this.txtDeviceID.Text = "0";
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(16, 287);
            this.txtLocalPort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(259, 29);
            this.txtLocalPort.TabIndex = 1;
            this.txtLocalPort.Text = "5000";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(17, 227);
            this.txtLocalIP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Size = new System.Drawing.Size(260, 29);
            this.txtLocalIP.TabIndex = 1;
            this.txtLocalIP.Text = "127.0.0.1";
            // 
            // labRemoteIP
            // 
            this.labRemoteIP.AutoSize = true;
            this.labRemoteIP.Location = new System.Drawing.Point(12, 71);
            this.labRemoteIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labRemoteIP.Name = "labRemoteIP";
            this.labRemoteIP.Size = new System.Drawing.Size(81, 18);
            this.labRemoteIP.TabIndex = 4;
            this.labRemoteIP.Text = "Remote IP";
            // 
            // labRemotePort
            // 
            this.labRemotePort.AutoSize = true;
            this.labRemotePort.Location = new System.Drawing.Point(14, 146);
            this.labRemotePort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labRemotePort.Name = "labRemotePort";
            this.labRemotePort.Size = new System.Drawing.Size(94, 18);
            this.labRemotePort.TabIndex = 4;
            this.labRemotePort.Text = "Remote Port";
            // 
            // labDeviceID
            // 
            this.labDeviceID.AutoSize = true;
            this.labDeviceID.Location = new System.Drawing.Point(15, 15);
            this.labDeviceID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labDeviceID.Name = "labDeviceID";
            this.labDeviceID.Size = new System.Drawing.Size(75, 18);
            this.labDeviceID.TabIndex = 4;
            this.labDeviceID.Text = "DeviceID";
            // 
            // labLocalIP
            // 
            this.labLocalIP.AutoSize = true;
            this.labLocalIP.Location = new System.Drawing.Point(15, 207);
            this.labLocalIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labLocalIP.Name = "labLocalIP";
            this.labLocalIP.Size = new System.Drawing.Size(67, 18);
            this.labLocalIP.TabIndex = 5;
            this.labLocalIP.Text = "Local IP";
            // 
            // labLocalPort
            // 
            this.labLocalPort.AutoSize = true;
            this.labLocalPort.Location = new System.Drawing.Point(12, 267);
            this.labLocalPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labLocalPort.Name = "labLocalPort";
            this.labLocalPort.Size = new System.Drawing.Size(80, 18);
            this.labLocalPort.TabIndex = 5;
            this.labLocalPort.Text = "Local Port";
            // 
            // groBoxEthernetSetting
            // 
            this.groBoxEthernetSetting.BackColor = System.Drawing.Color.LightBlue;
            this.groBoxEthernetSetting.Controls.Add(this.btnConnect);
            this.groBoxEthernetSetting.Controls.Add(this.picEthernetConnect);
            this.groBoxEthernetSetting.Controls.Add(this.btnDisConnected);
            this.groBoxEthernetSetting.Controls.Add(this.palEthernetSetting);
            this.groBoxEthernetSetting.Location = new System.Drawing.Point(1043, 12);
            this.groBoxEthernetSetting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groBoxEthernetSetting.Name = "groBoxEthernetSetting";
            this.groBoxEthernetSetting.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groBoxEthernetSetting.Size = new System.Drawing.Size(320, 485);
            this.groBoxEthernetSetting.TabIndex = 6;
            this.groBoxEthernetSetting.TabStop = false;
            this.groBoxEthernetSetting.Text = "Ethernet Setting";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.PaleGreen;
            this.btnConnect.Location = new System.Drawing.Point(30, 411);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(124, 53);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // picEthernetConnect
            // 
            this.picEthernetConnect.Image = global::WindowsFormsApp1.Properties.Resources.gray;
            this.picEthernetConnect.Location = new System.Drawing.Point(256, 0);
            this.picEthernetConnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picEthernetConnect.Name = "picEthernetConnect";
            this.picEthernetConnect.Size = new System.Drawing.Size(47, 55);
            this.picEthernetConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEthernetConnect.TabIndex = 8;
            this.picEthernetConnect.TabStop = false;
            // 
            // btnDisConnected
            // 
            this.btnDisConnected.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnDisConnected.Location = new System.Drawing.Point(167, 411);
            this.btnDisConnected.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDisConnected.Name = "btnDisConnected";
            this.btnDisConnected.Size = new System.Drawing.Size(124, 50);
            this.btnDisConnected.TabIndex = 7;
            this.btnDisConnected.Text = "DisConnected";
            this.btnDisConnected.UseVisualStyleBackColor = false;
            this.btnDisConnected.Click += new System.EventHandler(this.btnDisConnected_Click);
            // 
            // palEthernetSetting
            // 
            this.palEthernetSetting.Controls.Add(this.radbtnActive);
            this.palEthernetSetting.Controls.Add(this.radbtnPassive);
            this.palEthernetSetting.Controls.Add(this.txtDeviceID);
            this.palEthernetSetting.Controls.Add(this.labLocalPort);
            this.palEthernetSetting.Controls.Add(this.labDeviceID);
            this.palEthernetSetting.Controls.Add(this.txtLocalPort);
            this.palEthernetSetting.Controls.Add(this.labLocalIP);
            this.palEthernetSetting.Controls.Add(this.txtRemoteIP);
            this.palEthernetSetting.Controls.Add(this.labRemotePort);
            this.palEthernetSetting.Controls.Add(this.txtLocalIP);
            this.palEthernetSetting.Controls.Add(this.labRemoteIP);
            this.palEthernetSetting.Controls.Add(this.txtRemotePort);
            this.palEthernetSetting.Location = new System.Drawing.Point(16, 39);
            this.palEthernetSetting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.palEthernetSetting.Name = "palEthernetSetting";
            this.palEthernetSetting.Size = new System.Drawing.Size(291, 379);
            this.palEthernetSetting.TabIndex = 6;
            // 
            // radbtnActive
            // 
            this.radbtnActive.AutoSize = true;
            this.radbtnActive.Location = new System.Drawing.Point(174, 341);
            this.radbtnActive.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radbtnActive.Name = "radbtnActive";
            this.radbtnActive.Size = new System.Drawing.Size(79, 22);
            this.radbtnActive.TabIndex = 6;
            this.radbtnActive.TabStop = true;
            this.radbtnActive.Text = "Active";
            this.radbtnActive.UseVisualStyleBackColor = true;
            // 
            // radbtnPassive
            // 
            this.radbtnPassive.AutoSize = true;
            this.radbtnPassive.Checked = true;
            this.radbtnPassive.Location = new System.Drawing.Point(47, 341);
            this.radbtnPassive.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radbtnPassive.Name = "radbtnPassive";
            this.radbtnPassive.Size = new System.Drawing.Size(85, 22);
            this.radbtnPassive.TabIndex = 6;
            this.radbtnPassive.TabStop = true;
            this.radbtnPassive.Text = "Passive";
            this.radbtnPassive.UseVisualStyleBackColor = true;
            // 
            // picRed
            // 
            this.picRed.Image = global::WindowsFormsApp1.Properties.Resources.red;
            this.picRed.Location = new System.Drawing.Point(1009, -10);
            this.picRed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picRed.Name = "picRed";
            this.picRed.Size = new System.Drawing.Size(47, 55);
            this.picRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRed.TabIndex = 7;
            this.picRed.TabStop = false;
            this.picRed.Visible = false;
            // 
            // picGreen
            // 
            this.picGreen.Image = global::WindowsFormsApp1.Properties.Resources.green;
            this.picGreen.Location = new System.Drawing.Point(1071, -10);
            this.picGreen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picGreen.Name = "picGreen";
            this.picGreen.Size = new System.Drawing.Size(47, 55);
            this.picGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGreen.TabIndex = 8;
            this.picGreen.TabStop = false;
            this.picGreen.Visible = false;
            // 
            // picGray
            // 
            this.picGray.Image = global::WindowsFormsApp1.Properties.Resources.gray;
            this.picGray.Location = new System.Drawing.Point(1136, -10);
            this.picGray.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picGray.Name = "picGray";
            this.picGray.Size = new System.Drawing.Size(47, 55);
            this.picGray.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGray.TabIndex = 8;
            this.picGray.TabStop = false;
            this.picGray.Visible = false;
            // 
            // picYellow
            // 
            this.picYellow.Image = global::WindowsFormsApp1.Properties.Resources.yellow;
            this.picYellow.Location = new System.Drawing.Point(1204, -10);
            this.picYellow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picYellow.Name = "picYellow";
            this.picYellow.Size = new System.Drawing.Size(47, 55);
            this.picYellow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picYellow.TabIndex = 8;
            this.picYellow.TabStop = false;
            this.picYellow.Visible = false;
            // 
            // txtSendMsg
            // 
            this.txtSendMsg.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtSendMsg.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSendMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSendMsg.Location = new System.Drawing.Point(41, 323);
            this.txtSendMsg.Multiline = true;
            this.txtSendMsg.Name = "txtSendMsg";
            this.txtSendMsg.Size = new System.Drawing.Size(518, 150);
            this.txtSendMsg.TabIndex = 9;
            // 
            // txtReceiveMsg
            // 
            this.txtReceiveMsg.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtReceiveMsg.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtReceiveMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtReceiveMsg.Location = new System.Drawing.Point(41, 585);
            this.txtReceiveMsg.Multiline = true;
            this.txtReceiveMsg.Name = "txtReceiveMsg";
            this.txtReceiveMsg.Size = new System.Drawing.Size(518, 150);
            this.txtReceiveMsg.TabIndex = 9;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1396, 968);
            this.Controls.Add(this.txtReceiveMsg);
            this.Controls.Add(this.txtSendMsg);
            this.Controls.Add(this.picYellow);
            this.Controls.Add(this.picGray);
            this.Controls.Add(this.picGreen);
            this.Controls.Add(this.picRed);
            this.Controls.Add(this.groBoxEthernetSetting);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTest);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groBoxEthernetSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEthernetConnect)).EndInit();
            this.palEthernetSetting.ResumeLayout(false);
            this.palEthernetSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picYellow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button btnDisConnected;
        private System.Windows.Forms.Panel palEthernetSetting;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.RadioButton radbtnActive;
        private System.Windows.Forms.RadioButton radbtnPassive;
        private System.Windows.Forms.PictureBox picRed;
        private System.Windows.Forms.PictureBox picGreen;
        private System.Windows.Forms.PictureBox picGray;
        private System.Windows.Forms.PictureBox picYellow;
        private System.Windows.Forms.PictureBox picEthernetConnect;
        private System.Windows.Forms.TextBox txtSendMsg;
        private System.Windows.Forms.TextBox txtReceiveMsg;
    }
}

