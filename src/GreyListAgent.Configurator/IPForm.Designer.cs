namespace GreyListAgent.Configurator
{
    partial class IPForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.lblNetmask = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.edtMask = new IPAddressControlLib.IPAddressControl();
            this.edtIP = new IPAddressControlLib.IPAddressControl();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(122, 119);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(203, 119);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.lblNetmask);
            this.gbInfo.Controls.Add(this.lblIP);
            this.gbInfo.Controls.Add(this.edtMask);
            this.gbInfo.Controls.Add(this.edtIP);
            this.gbInfo.Location = new System.Drawing.Point(12, 12);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(266, 91);
            this.gbInfo.TabIndex = 0;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Enter IP address or subnet";
            // 
            // lblNetmask
            // 
            this.lblNetmask.AutoSize = true;
            this.lblNetmask.Location = new System.Drawing.Point(17, 60);
            this.lblNetmask.Name = "lblNetmask";
            this.lblNetmask.Size = new System.Drawing.Size(48, 13);
            this.lblNetmask.TabIndex = 9;
            this.lblNetmask.Text = "Netmask";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(17, 30);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(46, 13);
            this.lblIP.TabIndex = 8;
            this.lblIP.Text = "Address";
            // 
            // edtMask
            // 
            this.edtMask.AllowInternalTab = false;
            this.edtMask.AutoHeight = true;
            this.edtMask.BackColor = System.Drawing.SystemColors.Window;
            this.edtMask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.edtMask.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtMask.Location = new System.Drawing.Point(138, 53);
            this.edtMask.MinimumSize = new System.Drawing.Size(87, 21);
            this.edtMask.Name = "edtMask";
            this.edtMask.ReadOnly = false;
            this.edtMask.Size = new System.Drawing.Size(105, 21);
            this.edtMask.TabIndex = 1;
            this.edtMask.Text = "...";
            // 
            // edtIP
            // 
            this.edtIP.AllowInternalTab = false;
            this.edtIP.AutoHeight = true;
            this.edtIP.BackColor = System.Drawing.SystemColors.Window;
            this.edtIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.edtIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtIP.Location = new System.Drawing.Point(138, 27);
            this.edtIP.MinimumSize = new System.Drawing.Size(87, 21);
            this.edtIP.Name = "edtIP";
            this.edtIP.ReadOnly = false;
            this.edtIP.Size = new System.Drawing.Size(105, 21);
            this.edtIP.TabIndex = 0;
            this.edtIP.Text = "...";
            // 
            // IPForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(292, 152);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IPForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label lblNetmask;
        private System.Windows.Forms.Label lblIP;
        private IPAddressControlLib.IPAddressControl edtMask;
        private IPAddressControlLib.IPAddressControl edtIP;
    }
}