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
            this.edtNetmask = new IPAddressControlLib.IPAddressControl();
            this.edtCleanRowCount = new System.Windows.Forms.NumericUpDown();
            this.lblIpNetmask = new System.Windows.Forms.Label();
            this.lblUnconfirmedMaxAge = new System.Windows.Forms.Label();
            this.lblConfirmedMaxAge = new System.Windows.Forms.Label();
            this.lblGreyListingPeriod = new System.Windows.Forms.Label();
            this.lblCleanRowCount = new System.Windows.Forms.Label();
            this.lblCopyrightRight = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.LinkLabel();
            this.lblCopyrightLeft = new System.Windows.Forms.Label();
            this.lblHomePage = new System.Windows.Forms.LinkLabel();
            this.tpIP = new System.Windows.Forms.TabPage();
            this.btnDeleteIP = new System.Windows.Forms.Button();
            this.btnEditIP = new System.Windows.Forms.Button();
            this.btnAddIP = new System.Windows.Forms.Button();
            this.lbIPList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tpClients = new System.Windows.Forms.TabPage();
            this.btnDeleteClient = new System.Windows.Forms.Button();
            this.btnEditClient = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.lbClientList = new System.Windows.Forms.ListBox();
            this.cmClients = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.eventLog = new System.Diagnostics.EventLog();
            this.alMain = new Crad.Windows.Forms.Actions.ActionList();
            this.action1 = new Crad.Windows.Forms.Actions.Action();
            this.edtGreyListPeriod = new Zeta.TimeSpanPicker();
            this.edtMaxAgeConfirmed = new Zeta.TimeSpanPicker();
            this.edtMaxAgeUnConfirmed = new Zeta.TimeSpanPicker();
            this.tcMain.SuspendLayout();
            this.tpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtCleanRowCount)).BeginInit();
            this.tpIP.SuspendLayout();
            this.tpClients.SuspendLayout();
            this.cmClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alMain)).BeginInit();
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
            this.tcMain.Location = new System.Drawing.Point(9, 9);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(424, 461);
            this.tcMain.TabIndex = 0;
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.edtGreyListPeriod);
            this.tpOptions.Controls.Add(this.edtMaxAgeConfirmed);
            this.tpOptions.Controls.Add(this.edtNetmask);
            this.tpOptions.Controls.Add(this.edtMaxAgeUnConfirmed);
            this.tpOptions.Controls.Add(this.edtCleanRowCount);
            this.tpOptions.Controls.Add(this.lblIpNetmask);
            this.tpOptions.Controls.Add(this.lblUnconfirmedMaxAge);
            this.tpOptions.Controls.Add(this.lblConfirmedMaxAge);
            this.tpOptions.Controls.Add(this.lblGreyListingPeriod);
            this.tpOptions.Controls.Add(this.lblCleanRowCount);
            this.tpOptions.Controls.Add(this.lblCopyrightRight);
            this.tpOptions.Controls.Add(this.lblAuthor);
            this.tpOptions.Controls.Add(this.lblCopyrightLeft);
            this.tpOptions.Controls.Add(this.lblHomePage);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(416, 435);
            this.tpOptions.TabIndex = 0;
            this.tpOptions.Text = "Preferences";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // edtNetmask
            // 
            this.edtNetmask.AllowInternalTab = false;
            this.edtNetmask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtNetmask.AutoHeight = true;
            this.edtNetmask.BackColor = System.Drawing.SystemColors.Window;
            this.edtNetmask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.edtNetmask.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtNetmask.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtNetmask.Location = new System.Drawing.Point(308, 127);
            this.edtNetmask.MinimumSize = new System.Drawing.Size(87, 21);
            this.edtNetmask.Name = "edtNetmask";
            this.edtNetmask.ReadOnly = false;
            this.edtNetmask.Size = new System.Drawing.Size(96, 21);
            this.edtNetmask.TabIndex = 5;
            this.edtNetmask.Text = "...";
            // 
            // edtCleanRowCount
            // 
            this.edtCleanRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtCleanRowCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtCleanRowCount.Location = new System.Drawing.Point(308, 19);
            this.edtCleanRowCount.Name = "edtCleanRowCount";
            this.edtCleanRowCount.Size = new System.Drawing.Size(96, 21);
            this.edtCleanRowCount.TabIndex = 1;
            // 
            // lblIpNetmask
            // 
            this.lblIpNetmask.AutoSize = true;
            this.lblIpNetmask.Location = new System.Drawing.Point(16, 131);
            this.lblIpNetmask.Name = "lblIpNetmask";
            this.lblIpNetmask.Size = new System.Drawing.Size(274, 13);
            this.lblIpNetmask.TabIndex = 10;
            this.lblIpNetmask.Text = "IP Netmask to apply to source IPs when hashing triplets";
            // 
            // lblUnconfirmedMaxAge
            // 
            this.lblUnconfirmedMaxAge.AutoSize = true;
            this.lblUnconfirmedMaxAge.Location = new System.Drawing.Point(16, 105);
            this.lblUnconfirmedMaxAge.Name = "lblUnconfirmedMaxAge";
            this.lblUnconfirmedMaxAge.Size = new System.Drawing.Size(183, 13);
            this.lblUnconfirmedMaxAge.TabIndex = 9;
            this.lblUnconfirmedMaxAge.Text = "Maximum age of unconfirmed triplets";
            // 
            // lblConfirmedMaxAge
            // 
            this.lblConfirmedMaxAge.AutoSize = true;
            this.lblConfirmedMaxAge.Location = new System.Drawing.Point(16, 77);
            this.lblConfirmedMaxAge.Name = "lblConfirmedMaxAge";
            this.lblConfirmedMaxAge.Size = new System.Drawing.Size(171, 13);
            this.lblConfirmedMaxAge.TabIndex = 8;
            this.lblConfirmedMaxAge.Text = "Maximum age of confirmed triplets";
            // 
            // lblGreyListingPeriod
            // 
            this.lblGreyListingPeriod.AutoSize = true;
            this.lblGreyListingPeriod.Location = new System.Drawing.Point(16, 50);
            this.lblGreyListingPeriod.Name = "lblGreyListingPeriod";
            this.lblGreyListingPeriod.Size = new System.Drawing.Size(90, 13);
            this.lblGreyListingPeriod.TabIndex = 7;
            this.lblGreyListingPeriod.Text = "Greylisting period";
            // 
            // lblCleanRowCount
            // 
            this.lblCleanRowCount.AutoSize = true;
            this.lblCleanRowCount.Location = new System.Drawing.Point(16, 21);
            this.lblCleanRowCount.Name = "lblCleanRowCount";
            this.lblCleanRowCount.Size = new System.Drawing.Size(203, 13);
            this.lblCleanRowCount.TabIndex = 6;
            this.lblCleanRowCount.Text = "Number of rows to clean on each recieve";
            // 
            // lblCopyrightRight
            // 
            this.lblCopyrightRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lblAuthor.TabIndex = 6;
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
            this.lblHomePage.Location = new System.Drawing.Point(314, 408);
            this.lblHomePage.Name = "lblHomePage";
            this.lblHomePage.Size = new System.Drawing.Size(90, 13);
            this.lblHomePage.TabIndex = 7;
            this.lblHomePage.TabStop = true;
            this.lblHomePage.Text = "Visit project\'s site";
            this.lblHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblHomePage_LinkClicked);
            // 
            // tpIP
            // 
            this.tpIP.Controls.Add(this.btnDeleteIP);
            this.tpIP.Controls.Add(this.btnEditIP);
            this.tpIP.Controls.Add(this.btnAddIP);
            this.tpIP.Controls.Add(this.lbIPList);
            this.tpIP.Location = new System.Drawing.Point(4, 22);
            this.tpIP.Name = "tpIP";
            this.tpIP.Padding = new System.Windows.Forms.Padding(3);
            this.tpIP.Size = new System.Drawing.Size(416, 435);
            this.tpIP.TabIndex = 1;
            this.tpIP.Text = "IP Whitelist";
            this.tpIP.UseVisualStyleBackColor = true;
            // 
            // btnDeleteIP
            // 
            this.btnDeleteIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteIP.Location = new System.Drawing.Point(326, 395);
            this.btnDeleteIP.Name = "btnDeleteIP";
            this.btnDeleteIP.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteIP.TabIndex = 3;
            this.btnDeleteIP.Text = "Delete";
            this.btnDeleteIP.UseVisualStyleBackColor = true;
            // 
            // btnEditIP
            // 
            this.btnEditIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditIP.Location = new System.Drawing.Point(245, 395);
            this.btnEditIP.Name = "btnEditIP";
            this.btnEditIP.Size = new System.Drawing.Size(75, 23);
            this.btnEditIP.TabIndex = 2;
            this.btnEditIP.Text = "Edit...";
            this.btnEditIP.UseVisualStyleBackColor = true;
            // 
            // btnAddIP
            // 
            this.btnAddIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIP.Location = new System.Drawing.Point(164, 395);
            this.btnAddIP.Name = "btnAddIP";
            this.btnAddIP.Size = new System.Drawing.Size(75, 23);
            this.btnAddIP.TabIndex = 1;
            this.btnAddIP.Text = "Add...";
            this.btnAddIP.UseVisualStyleBackColor = true;
            // 
            // lbIPList
            // 
            this.lbIPList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIPList.ContextMenuStrip = this.contextMenuStrip1;
            this.lbIPList.FormattingEnabled = true;
            this.lbIPList.Location = new System.Drawing.Point(14, 16);
            this.lbIPList.Name = "lbIPList";
            this.lbIPList.Size = new System.Drawing.Size(387, 368);
            this.lbIPList.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tpClients
            // 
            this.tpClients.Controls.Add(this.btnDeleteClient);
            this.tpClients.Controls.Add(this.btnEditClient);
            this.tpClients.Controls.Add(this.btnAddClient);
            this.tpClients.Controls.Add(this.lbClientList);
            this.tpClients.Location = new System.Drawing.Point(4, 22);
            this.tpClients.Name = "tpClients";
            this.tpClients.Padding = new System.Windows.Forms.Padding(3);
            this.tpClients.Size = new System.Drawing.Size(416, 435);
            this.tpClients.TabIndex = 2;
            this.tpClients.Text = "Client Whitelist";
            this.tpClients.UseVisualStyleBackColor = true;
            // 
            // btnDeleteClient
            // 
            this.btnDeleteClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteClient.Location = new System.Drawing.Point(324, 395);
            this.btnDeleteClient.Name = "btnDeleteClient";
            this.btnDeleteClient.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteClient.TabIndex = 7;
            this.btnDeleteClient.Text = "Delete";
            this.btnDeleteClient.UseVisualStyleBackColor = true;
            // 
            // btnEditClient
            // 
            this.btnEditClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditClient.Location = new System.Drawing.Point(243, 395);
            this.btnEditClient.Name = "btnEditClient";
            this.btnEditClient.Size = new System.Drawing.Size(75, 23);
            this.btnEditClient.TabIndex = 6;
            this.btnEditClient.Text = "Edit...";
            this.btnEditClient.UseVisualStyleBackColor = true;
            // 
            // btnAddClient
            // 
            this.btnAddClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddClient.Location = new System.Drawing.Point(162, 395);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(75, 23);
            this.btnAddClient.TabIndex = 5;
            this.btnAddClient.Text = "Add...";
            this.btnAddClient.UseVisualStyleBackColor = true;
            // 
            // lbClientList
            // 
            this.lbClientList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClientList.ContextMenuStrip = this.cmClients;
            this.lbClientList.FormattingEnabled = true;
            this.lbClientList.Location = new System.Drawing.Point(14, 16);
            this.lbClientList.Name = "lbClientList";
            this.lbClientList.Size = new System.Drawing.Size(385, 368);
            this.lbClientList.TabIndex = 4;
            // 
            // cmClients
            // 
            this.cmClients.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmClients.Name = "contextMenuStrip2";
            this.cmClients.Size = new System.Drawing.Size(108, 70);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem.Text = "Add...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editToolStripMenuItem.Text = "Edit...";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
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
            this.btnOk.Location = new System.Drawing.Point(273, 476);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(354, 476);
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
            // alMain
            // 
            this.alMain.Actions.Add(this.action1);
            this.alMain.ContainerControl = this;
            // 
            // edtGreyListPeriod
            // 
            this.edtGreyListPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtGreyListPeriod.BackColor = System.Drawing.SystemColors.Window;
            this.edtGreyListPeriod.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtGreyListPeriod.Location = new System.Drawing.Point(308, 46);
            this.edtGreyListPeriod.MinimumSize = new System.Drawing.Size(96, 20);
            this.edtGreyListPeriod.Name = "edtGreyListPeriod";
            this.edtGreyListPeriod.ShowToolTip = true;
            this.edtGreyListPeriod.Size = new System.Drawing.Size(96, 21);
            this.edtGreyListPeriod.TabIndex = 2;
            this.edtGreyListPeriod.Tag = "";
            this.edtGreyListPeriod.ValueString = "00.00:00:00";
            this.edtGreyListPeriod.ValueTimeSpan = System.TimeSpan.Parse("00:00:00");
            // 
            // edtMaxAgeConfirmed
            // 
            this.edtMaxAgeConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMaxAgeConfirmed.BackColor = System.Drawing.SystemColors.Window;
            this.edtMaxAgeConfirmed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtMaxAgeConfirmed.Location = new System.Drawing.Point(308, 73);
            this.edtMaxAgeConfirmed.MinimumSize = new System.Drawing.Size(96, 20);
            this.edtMaxAgeConfirmed.Name = "edtMaxAgeConfirmed";
            this.edtMaxAgeConfirmed.ShowToolTip = true;
            this.edtMaxAgeConfirmed.Size = new System.Drawing.Size(96, 21);
            this.edtMaxAgeConfirmed.TabIndex = 3;
            this.edtMaxAgeConfirmed.Tag = "";
            this.edtMaxAgeConfirmed.ValueString = "00.00:00:00";
            this.edtMaxAgeConfirmed.ValueTimeSpan = System.TimeSpan.Parse("00:00:00");
            // 
            // edtMaxAgeUnConfirmed
            // 
            this.edtMaxAgeUnConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMaxAgeUnConfirmed.BackColor = System.Drawing.SystemColors.Window;
            this.edtMaxAgeUnConfirmed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtMaxAgeUnConfirmed.Location = new System.Drawing.Point(308, 100);
            this.edtMaxAgeUnConfirmed.MinimumSize = new System.Drawing.Size(96, 20);
            this.edtMaxAgeUnConfirmed.Name = "edtMaxAgeUnConfirmed";
            this.edtMaxAgeUnConfirmed.ShowToolTip = true;
            this.edtMaxAgeUnConfirmed.Size = new System.Drawing.Size(96, 21);
            this.edtMaxAgeUnConfirmed.TabIndex = 4;
            this.edtMaxAgeUnConfirmed.Tag = "";
            this.edtMaxAgeUnConfirmed.ValueString = "00.00:00:00";
            this.edtMaxAgeUnConfirmed.ValueTimeSpan = System.TimeSpan.Parse("00:00:00");
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(441, 511);
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
            ((System.ComponentModel.ISupportInitialize)(this.edtCleanRowCount)).EndInit();
            this.tpIP.ResumeLayout(false);
            this.tpClients.ResumeLayout(false);
            this.cmClients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.TabPage tpIP;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tpClients;
        private System.Windows.Forms.ListBox lbIPList;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Diagnostics.EventLog eventLog;
        private System.Windows.Forms.LinkLabel lblHomePage;
        private System.Windows.Forms.LinkLabel lblAuthor;
        private System.Windows.Forms.Label lblCopyrightLeft;
        private System.Windows.Forms.Label lblCopyrightRight;
        private System.Windows.Forms.Label lblCleanRowCount;
        private System.Windows.Forms.Label lblGreyListingPeriod;
        private System.Windows.Forms.Label lblConfirmedMaxAge;
        private System.Windows.Forms.Label lblUnconfirmedMaxAge;
        private System.Windows.Forms.NumericUpDown edtCleanRowCount;
        private System.Windows.Forms.Label lblIpNetmask;
        private Zeta.TimeSpanPicker edtMaxAgeUnConfirmed;
        private IPAddressControlLib.IPAddressControl edtNetmask;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Zeta.TimeSpanPicker edtGreyListPeriod;
        private Zeta.TimeSpanPicker edtMaxAgeConfirmed;
        private System.Windows.Forms.Button btnDeleteIP;
        private System.Windows.Forms.Button btnEditIP;
        private System.Windows.Forms.Button btnAddIP;
        private System.Windows.Forms.Button btnDeleteClient;
        private System.Windows.Forms.Button btnEditClient;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.ListBox lbClientList;
        private System.Windows.Forms.ContextMenuStrip cmClients;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private Crad.Windows.Forms.Actions.ActionList alMain;
        private Crad.Windows.Forms.Actions.Action action1;
    }
}

