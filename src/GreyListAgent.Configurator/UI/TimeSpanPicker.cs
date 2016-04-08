//
// Copyright by Stephan Ruhland (stephan.ruhland@zeta-software.de)
//

namespace Zeta
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class TimeSpanPicker : UserControl
    {
        private int activeBox = 0;
        private RectangleF[] brec;
        private IContainer components = null;
        private int[] number;
        private RectangleF[] rec;
        private System.Windows.Forms.ToolTip toolTip;
        private float xSize;
        private float ySize;

        public TimeSpanPicker()
        {
            this.MinimumSize = new Size(0x60, 20);
            this.rec = new RectangleF[4];
            this.brec = new RectangleF[3];
            this.number = new int[] { 0, 0, 0, 0 };
            this.InitializeComponent();
            this.LoadToolTip();
        }

        private void AddOneToActiveBox()
        {
            if (this.activeBox == 1)
            {
                if (this.number[this.activeBox - 1] < 0x63)
                {
                    this.number[this.activeBox - 1]++;
                    this.Refresh();
                }
            }
            else if (this.activeBox == 2)
            {
                if (this.number[this.activeBox - 1] < 0x17)
                {
                    this.number[this.activeBox - 1]++;
                    this.Refresh();
                }
                else if ((this.number[this.activeBox - 1] == 0x17) && (this.number[this.activeBox - 2] < 0x63))
                {
                    this.number[this.activeBox - 2]++;
                    this.number[this.activeBox - 1] = 0;
                    this.Refresh();
                }
            }
            else if (this.activeBox == 3)
            {
                if (this.number[this.activeBox - 1] < 0x3b)
                {
                    this.number[this.activeBox - 1]++;
                    this.Refresh();
                }
                else if ((this.number[this.activeBox - 1] == 0x3b) && (this.number[this.activeBox - 2] < 0x17))
                {
                    this.number[this.activeBox - 1] = 0;
                    this.number[this.activeBox - 2]++;
                    this.Refresh();
                }
            }
            else if (this.activeBox == 4)
            {
                if (this.number[this.activeBox - 1] < 0x3b)
                {
                    this.number[this.activeBox - 1]++;
                    this.Refresh();
                }
                else if ((this.number[this.activeBox - 1] == 0x3b) && (this.number[this.activeBox - 2] < 0x3b))
                {
                    this.number[this.activeBox - 1] = 0;
                    this.number[this.activeBox - 2]++;
                    this.Refresh();
                }
            }
        }

        private void CheckAndAdjustUpperLimitNumbers()
        {
            if (this.number[1] > 0x17)
            {
                this.number[1] = 0;
            }
            if (this.number[2] > 0x3b)
            {
                this.number[2] = 0;
            }
            if (this.number[3] > 0x3b)
            {
                this.number[3] = 0;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawBorderOld(PaintEventArgs pea)
        {
            Color color = Color.Gray;
            
          //  pea.Graphics.DrawLine(new Pen(Color.Black), 1, 1, 1, base.Height - 2);
            pea.Graphics.DrawLine(new Pen(color), 0, 0, 0, base.Height);
          //  pea.Graphics.DrawLine(new Pen(Color.Black), 1, 1, base.Width - 2, 1);
            pea.Graphics.DrawLine(new Pen(color), 0, 0, base.Width, 0);
           // pea.Graphics.DrawLine(new Pen(Color.White), 0, base.Height - 1, base.Width - 1, base.Height - 1);
            pea.Graphics.DrawLine(new Pen(color), 0, base.Height-1, base.Width, base.Height-1);
          //  pea.Graphics.DrawLine(new Pen(Color.White), base.Width - 1, 0, base.Width - 1, base.Height - 2);
            pea.Graphics.DrawLine(new Pen(color), base.Width - 1, 1, base.Width - 1, base.Height - 1);
        }

        private void DrawNumbers(PaintEventArgs pea)
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.activeBox == (i + 1))
                {
                    pea.Graphics.DrawString(this.number[i].ToString("00"), Font, new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), this.rec[i]);
                }
                else
                {
                    pea.Graphics.DrawString(this.number[i].ToString("00"), Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), this.rec[i]);
                }
            }
            pea.Graphics.DrawString(".", Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), this.brec[0]);
            pea.Graphics.DrawString(":", Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), this.brec[1]);
            pea.Graphics.DrawString(":", Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), this.brec[2]);
        }

        private void DrawRecs(PaintEventArgs pea)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (this.activeBox == i)
                {
                    pea.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), this.rec[i - 1]);
                }
                else
                {
                    pea.Graphics.FillRectangle(new SolidBrush(Color.White), this.rec[i - 1]);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TimeSpanPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MinimumSize = new System.Drawing.Size(96, 20);
            this.Name = "TimeSpanPicker";
            this.Size = new System.Drawing.Size(96, 20);
            this.Tag = "";
            this.ResumeLayout(false);

        }

        protected override bool IsInputKey(Keys keyData) => 
            (((((keyData == Keys.Up) || (keyData == Keys.Down)) || (keyData == Keys.Left)) || (keyData == Keys.Right)) || base.IsInputKey(keyData));

        private void LoadToolTip()
        {
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.toolTip.AutoPopDelay = 0x1388;
            this.toolTip.InitialDelay = 0x3e8;
            this.toolTip.IsBalloon = this.IsVisualStyle;
            this.toolTip.ReshowDelay = 500;
            this.toolTip.ShowAlways = true;
            this.toolTip.SetToolTip(this, "Geben Sie ein Datum im Format \"DD.HH:MM:SS\" mit\r\nDD - Tage, HH - Stunden, MM - Minuten, SS - Sekunden ein.");
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.activeBox = 1;
            this.Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.activeBox > 0)
            {
                Keys keyData = e.KeyData;
                if (e.KeyData == Keys.Left)
                {
                    if (this.activeBox > 1)
                    {
                        this.activeBox--;
                        this.Refresh();
                    }
                    else if (this.activeBox == 1)
                    {
                        this.activeBox = 4;
                        this.Refresh();
                    }
                }
                else if (e.KeyData == Keys.Right)
                {
                    if (this.activeBox < 4)
                    {
                        this.activeBox++;
                        this.Refresh();
                    }
                    else if (this.activeBox == 4)
                    {
                        this.activeBox = 1;
                        this.Refresh();
                    }
                }
                else if (e.KeyData == Keys.Up)
                {
                    this.AddOneToActiveBox();
                }
                else if ((e.KeyData == Keys.Down) && (this.number[this.activeBox - 1] > 0))
                {
                    this.number[this.activeBox - 1]--;
                    this.Refresh();
                }
                if ((((((Keys.D0 == keyData) || (Keys.D1 == keyData)) || ((Keys.D2 == keyData) || (Keys.D3 == keyData))) || (((Keys.D4 == keyData) || (Keys.D5 == keyData)) || ((Keys.D6 == keyData) || (Keys.D7 == keyData)))) || (Keys.D8 == keyData)) || (Keys.D9 == keyData))
                {
                    this.number[this.activeBox - 1] = Convert.ToInt32(this.number[this.activeBox - 1].ToString("00").Substring(1, 1) + Convert.ToInt32(keyData.ToString().Substring(1, 1)).ToString());
                    this.Refresh();
                }
                else if ((((((Keys.NumPad0 == keyData) || (Keys.NumPad1 == keyData)) || ((Keys.NumPad2 == keyData) || (Keys.NumPad3 == keyData))) || (((Keys.NumPad4 == keyData) || (Keys.NumPad5 == keyData)) || ((Keys.NumPad6 == keyData) || (Keys.NumPad7 == keyData)))) || (Keys.NumPad8 == keyData)) || (Keys.NumPad9 == keyData))
                {
                    this.number[this.activeBox - 1] = Convert.ToInt32(this.number[this.activeBox - 1].ToString("00").Substring(1, 1) + Convert.ToInt32(keyData.ToString().Substring(6, 1)).ToString());
                    this.Refresh();
                }
                else if (keyData == Keys.Delete)
                {
                    this.number[this.activeBox - 1] = 0;
                    this.Refresh();
                }
                else if (keyData == Keys.Back)
                {
                    this.number[this.activeBox - 1] = 0;
                    if (this.activeBox > 1)
                    {
                        this.activeBox--;
                    }
                    this.Refresh();
                }
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.activeBox = 0;
            this.Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.X > 2) && (e.X < (2f + this.xSize)))
            {
                this.activeBox = 1;
                this.Refresh();
            }
            else if ((e.X > ((2f + this.xSize) + this.brec[0].Width)) && (e.X < ((2f + (this.xSize * 2f)) + this.brec[0].Width)))
            {
                this.activeBox = 2;
                this.Refresh();
            }
            else if ((e.X > (((2f + (this.xSize * 2f)) + this.brec[0].Width) + this.brec[1].Width)) && (e.X < (((2f + (this.xSize * 3f)) + this.brec[0].Width) + this.brec[1].Width)))
            {
                this.activeBox = 3;
                this.Refresh();
            }
            else if ((e.X > ((((2f + (this.xSize * 3f)) + this.brec[0].Width) + this.brec[1].Width) + this.brec[2].Width)) && (e.X < ((((2f + (this.xSize * 4f)) + this.brec[0].Width) + this.brec[1].Width) + this.brec[2].Width)))
            {
                this.activeBox = 4;
                this.Refresh();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta >= 120)
            {
                this.AddOneToActiveBox();
            }
            else if ((e.Delta <= -120) && (this.number[this.activeBox - 1] > 0))
            {
                this.number[this.activeBox - 1]--;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.xSize = ((base.Width - 1) / 5) - 2;
            this.ySize = ((base.Height - 1) - 3) - 2;
            this.rec[0] = new RectangleF(3f, 3f, this.xSize, this.ySize);
            this.rec[1] = new RectangleF(2f + (this.xSize + (this.xSize / 3f)), 3f, this.xSize, this.ySize);
            this.rec[2] = new RectangleF(2f + ((this.xSize * 2f) + ((this.xSize / 3f) * 2f)), 3f, this.xSize, this.ySize);
            this.rec[3] = new RectangleF(2f + ((this.xSize * 3f) + ((this.xSize / 3f) * 3f)), 3f, this.xSize, this.ySize);
            this.brec[0] = new RectangleF(this.rec[0].X + this.rec[0].Width, 2f, (this.rec[1].X - this.rec[0].X) - this.rec[0].Width, this.ySize);
            this.brec[1] = new RectangleF(this.rec[1].X + this.rec[1].Width, 2f, (this.rec[2].X - this.rec[1].X) - this.rec[1].Width, this.ySize);
            this.brec[2] = new RectangleF(this.rec[2].X + this.rec[2].Width, 2f, (this.rec[3].X - this.rec[2].X) - this.rec[2].Width, this.ySize);
            this.CheckAndAdjustUpperLimitNumbers();
            if (this.IsVisualStyle)
            {
                this.DrawRecs(e);
                e.Graphics.DrawRectangle(new Pen(VisualStyleInformation.TextControlBorder), 0, 0, base.Width - 1, base.Height - 1);
            }
            else
            {
                this.DrawRecs(e);
                this.DrawBorderOld(e);
            }
            this.DrawNumbers(e);
        }

        private bool IsVisualStyle =>
            (VisualStyleInformation.DisplayName.Contains("Windows XP") && VisualStyleInformation.Company.Contains("Microsoft"));

        public bool ShowToolTip
        {
            get
            {
                return this.toolTip.Active;
            }
            set
            {
                this.toolTip.Active = value;
            }
        }

        public string ValueString
        {
            get
            {
               return (this.number[0].ToString("00") + "." + this.number[1].ToString("00") + ":" + this.number[2].ToString("00") + ":" + this.number[3].ToString("00"));
            }
                set
            {
                try
                {
                    this.number[0] = Convert.ToInt32(value.Substring(0, 2));
                    this.number[1] = Convert.ToInt32(value.Substring(3, 2));
                    this.number[2] = Convert.ToInt32(value.Substring(6, 2));
                    this.number[3] = Convert.ToInt32(value.Substring(9, 2));
                    this.Refresh();
                }
                catch
                {
                    MessageBox.Show("Fehler bei Daten\x00fcbergabe an TimeSpanPicker!\r\nVorgang wurde abgebrochen.\r\nstring \"DD.HH:MM:SS\"\r\nDD - Days, HH - Hours, MM - Minutes, SS - Seconds", "TimeSpanPicker Fehler", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
        }

        public TimeSpan ValueTimeSpan
        {
            get
            {
               return new TimeSpan(this.number[0], this.number[1], this.number[2], this.number[3]);
            }
            set
            {
                try
                {
                    this.number[0] = value.Days;
                    this.number[1] = value.Hours;
                    this.number[2] = value.Minutes;
                    this.number[3] = value.Seconds;
                    this.Refresh();
                }
                catch
                {
                    MessageBox.Show("Fehler bei Daten\x00fcbergabe an TimeSpanPicker!\r\nVorgang wurde abgebrochen.\r\nTimeSpan (DD,HH,MM,SS)\r\nDD - Days, HH - Hours, MM - Minutes, SS - Seconds", "TimeSpanPicker Fehler", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
        }
    }
}

