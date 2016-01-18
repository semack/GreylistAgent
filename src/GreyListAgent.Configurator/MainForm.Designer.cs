namespace GreyListAgent.Configurator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.edtCleanRowCount = new System.Windows.Forms.NumericUpDown();
            this.lblIpNetmask = new System.Windows.Forms.Label();
            this.lblUnconfirmedMaxAge = new System.Windows.Forms.Label();
            this.lblConfirmedMaxAge = new System.Windows.Forms.Label();
            this.lblGreyListingPeriod = new System.Windows.Forms.Label();
            this.lblCleanRowCount = new System.Windows.Forms.Label();
            this.gbTransportService = new System.Windows.Forms.GroupBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblCopyrightRight = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.LinkLabel();
            this.lblCopyrightLeft = new System.Windows.Forms.Label();
            this.lblHomePage = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tpIP = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tpClients = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lvLog = new System.Windows.Forms.ListView();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.eventLog = new System.Diagnostics.EventLog();
            this.tcMain.SuspendLayout();
            this.tpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCleanRowCount)).BeginInit();
            this.gbTransportService.SuspendLayout();
            this.tpIP.SuspendLayout();
            this.tbLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpOptions);
            this.tcMain.Controls.Add(this.tpIP);
            this.tcMain.Controls.Add(this.tpClients);
            this.tcMain.Controls.Add(this.tbLog);
            this.tcMain.Location = new System.Drawing.Point(9, 9);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(467, 461);
            this.tcMain.TabIndex = 0;
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.numericUpDown1);
            this.tpOptions.Controls.Add(this.edtCleanRowCount);
            this.tpOptions.Controls.Add(this.lblIpNetmask);
            this.tpOptions.Controls.Add(this.lblUnconfirmedMaxAge);
            this.tpOptions.Controls.Add(this.lblConfirmedMaxAge);
            this.tpOptions.Controls.Add(this.lblGreyListingPeriod);
            this.tpOptions.Controls.Add(this.lblCleanRowCount);
            this.tpOptions.Controls.Add(this.gbTransportService);
            this.tpOptions.Controls.Add(this.lblCopyrightRight);
            this.tpOptions.Controls.Add(this.lblAuthor);
            this.tpOptions.Controls.Add(this.lblCopyrightLeft);
            this.tpOptions.Controls.Add(this.lblHomePage);
            this.tpOptions.Controls.Add(this.label2);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(459, 435);
            this.tpOptions.TabIndex = 0;
            this.tpOptions.Text = "Preferences";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(326, 211);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 12;
            // 
            // edtCleanRowCount
            // 
            this.edtCleanRowCount.Location = new System.Drawing.Point(326, 92);
            this.edtCleanRowCount.Name = "edtCleanRowCount";
            this.edtCleanRowCount.Size = new System.Drawing.Size(120, 21);
            this.edtCleanRowCount.TabIndex = 11;
            // 
            // lblIpNetmask
            // 
            this.lblIpNetmask.AutoSize = true;
            this.lblIpNetmask.Location = new System.Drawing.Point(15, 213);
            this.lblIpNetmask.Name = "lblIpNetmask";
            this.lblIpNetmask.Size = new System.Drawing.Size(274, 13);
            this.lblIpNetmask.TabIndex = 10;
            this.lblIpNetmask.Text = "IP Netmask to apply to source IPs when hashing triplets";
            // 
            // lblUnconfirmedMaxAge
            // 
            this.lblUnconfirmedMaxAge.AutoSize = true;
            this.lblUnconfirmedMaxAge.Location = new System.Drawing.Point(15, 186);
            this.lblUnconfirmedMaxAge.Name = "lblUnconfirmedMaxAge";
            this.lblUnconfirmedMaxAge.Size = new System.Drawing.Size(310, 13);
            this.lblUnconfirmedMaxAge.TabIndex = 9;
            this.lblUnconfirmedMaxAge.Text = "Maximum age of unconfirmed triplets before getting cleaned up";
            // 
            // lblConfirmedMaxAge
            // 
            this.lblConfirmedMaxAge.AutoSize = true;
            this.lblConfirmedMaxAge.Location = new System.Drawing.Point(15, 160);
            this.lblConfirmedMaxAge.Name = "lblConfirmedMaxAge";
            this.lblConfirmedMaxAge.Size = new System.Drawing.Size(438, 13);
            this.lblConfirmedMaxAge.TabIndex = 8;
            this.lblConfirmedMaxAge.Text = "Maximum age of confirmed triplets before requring re-confirmation and getting cle" +
    "aned up";
            // 
            // lblGreyListingPeriod
            // 
            this.lblGreyListingPeriod.AutoSize = true;
            this.lblGreyListingPeriod.Location = new System.Drawing.Point(15, 132);
            this.lblGreyListingPeriod.Name = "lblGreyListingPeriod";
            this.lblGreyListingPeriod.Size = new System.Drawing.Size(310, 13);
            this.lblGreyListingPeriod.TabIndex = 7;
            this.lblGreyListingPeriod.Text = "Maximum age of unconfirmed triplets before getting cleaned up";
            // 
            // lblCleanRowCount
            // 
            this.lblCleanRowCount.AutoSize = true;
            this.lblCleanRowCount.Location = new System.Drawing.Point(15, 94);
            this.lblCleanRowCount.Name = "lblCleanRowCount";
            this.lblCleanRowCount.Size = new System.Drawing.Size(203, 13);
            this.lblCleanRowCount.TabIndex = 6;
            this.lblCleanRowCount.Text = "Number of rows to clean on each recieve";
            // 
            // gbTransportService
            // 
            this.gbTransportService.Controls.Add(this.btnRestart);
            this.gbTransportService.Controls.Add(this.btnStop);
            this.gbTransportService.Controls.Add(this.btnStart);
            this.gbTransportService.Location = new System.Drawing.Point(14, 13);
            this.gbTransportService.Name = "gbTransportService";
            this.gbTransportService.Size = new System.Drawing.Size(432, 74);
            this.gbTransportService.TabIndex = 5;
            this.gbTransportService.TabStop = false;
            this.gbTransportService.Text = "Transport Service Control";
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(300, 32);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(110, 23);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(159, 32);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(110, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(18, 32);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(110, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // lblCopyrightRight
            // 
            this.lblCopyrightRight.AutoSize = true;
            this.lblCopyrightRight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyrightRight.Location = new System.Drawing.Point(158, 408);
            this.lblCopyrightRight.Name = "lblCopyrightRight";
            this.lblCopyrightRight.Size = new System.Drawing.Size(72, 13);
            this.lblCopyrightRight.TabIndex = 4;
            this.lblCopyrightRight.Text = "© 2015-2016";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(64, 408);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(95, 13);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.TabStop = true;
            this.lblAuthor.Text = "James DeVincentis";
            this.lblAuthor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAuthor_LinkClicked);
            // 
            // lblCopyrightLeft
            // 
            this.lblCopyrightLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyrightLeft.AutoSize = true;
            this.lblCopyrightLeft.Location = new System.Drawing.Point(12, 408);
            this.lblCopyrightLeft.Name = "lblCopyrightLeft";
            this.lblCopyrightLeft.Size = new System.Drawing.Size(54, 13);
            this.lblCopyrightLeft.TabIndex = 2;
            this.lblCopyrightLeft.Text = "Copyright";
            // 
            // lblHomePage
            // 
            this.lblHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHomePage.AutoSize = true;
            this.lblHomePage.Location = new System.Drawing.Point(357, 408);
            this.lblHomePage.Name = "lblHomePage";
            this.lblHomePage.Size = new System.Drawing.Size(90, 13);
            this.lblHomePage.TabIndex = 1;
            this.lblHomePage.TabStop = true;
            this.lblHomePage.Text = "Visit project\'s site";
            this.lblHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblHomePage_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // tpIP
            // 
            this.tpIP.Controls.Add(this.label1);
            this.tpIP.Controls.Add(this.listBox1);
            this.tpIP.Location = new System.Drawing.Point(4, 22);
            this.tpIP.Name = "tpIP";
            this.tpIP.Padding = new System.Windows.Forms.Padding(3);
            this.tpIP.Size = new System.Drawing.Size(459, 435);
            this.tpIP.TabIndex = 1;
            this.tpIP.Text = "IP Whitelist";
            this.tpIP.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(14, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(430, 342);
            this.listBox1.TabIndex = 0;
            // 
            // tpClients
            // 
            this.tpClients.Location = new System.Drawing.Point(4, 22);
            this.tpClients.Name = "tpClients";
            this.tpClients.Padding = new System.Windows.Forms.Padding(3);
            this.tpClients.Size = new System.Drawing.Size(459, 435);
            this.tpClients.TabIndex = 2;
            this.tpClients.Text = "Client Whitelist";
            this.tpClients.UseVisualStyleBackColor = true;
            // 
            // tbLog
            // 
            this.tbLog.Controls.Add(this.btnRefresh);
            this.tbLog.Controls.Add(this.lvLog);
            this.tbLog.Location = new System.Drawing.Point(4, 22);
            this.tbLog.Name = "tbLog";
            this.tbLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbLog.Size = new System.Drawing.Size(459, 435);
            this.tbLog.TabIndex = 3;
            this.tbLog.Text = "Logs";
            this.tbLog.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(378, 406);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lvLog
            // 
            this.lvLog.FullRowSelect = true;
            this.lvLog.GridLines = true;
            this.lvLog.Location = new System.Drawing.Point(6, 6);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(447, 394);
            this.lvLog.SmallImageList = this.ilIcons;
            this.lvLog.TabIndex = 0;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.List;
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "error.png");
            this.ilIcons.Images.SetKeyName(1, "information.png");
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(316, 476);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(397, 476);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // eventLog
            // 
            this.eventLog.SynchronizingObject = this;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(484, 511);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tcMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exchange Graylist Configuration";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcMain.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.tpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCleanRowCount)).EndInit();
            this.gbTransportService.ResumeLayout(false);
            this.tpIP.ResumeLayout(false);
            this.tpIP.PerformLayout();
            this.tbLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.TabPage tpIP;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tpClients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tbLog;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Diagnostics.EventLog eventLog;
        private System.Windows.Forms.LinkLabel lblHomePage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblAuthor;
        private System.Windows.Forms.Label lblCopyrightLeft;
        private System.Windows.Forms.Label lblCopyrightRight;
        private System.Windows.Forms.GroupBox gbTransportService;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblCleanRowCount;
        private System.Windows.Forms.Label lblGreyListingPeriod;
        private System.Windows.Forms.Label lblConfirmedMaxAge;
        private System.Windows.Forms.Label lblUnconfirmedMaxAge;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown edtCleanRowCount;
        private System.Windows.Forms.Label lblIpNetmask;
    }
}

