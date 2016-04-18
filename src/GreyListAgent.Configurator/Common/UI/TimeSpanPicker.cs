//
// Copyright by Stephan Ruhland (stephan.ruhland@zeta-software.de)
//

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GreyListAgent.Configurator.Properties;

namespace GreyListAgent.Configurator.Common.UI
{
    public sealed class TimeSpanPicker : UserControl
    {
        private int _activeBox;
        private readonly RectangleF[] _brec;
        private readonly IContainer components = null;
        private readonly int[] _number;
        private readonly RectangleF[] _rec;
        private ToolTip _toolTip;
        private float _xSize;
        private float _ySize;


        public TimeSpanPicker()
        {
            MinimumSize = new Size(0x60, 20);
            _rec = new RectangleF[4];
            _brec = new RectangleF[3];
            _number = new[] { 0, 0, 0, 0 };
            InitializeComponent();
            LoadToolTip();
        }

        private bool IsVisualStyle =>
            VisualStyleInformation.DisplayName.Contains("Windows XP") &&
            VisualStyleInformation.Company.Contains("Microsoft");

        public bool ShowToolTip
        {
            get { return _toolTip.Active; }
            set { _toolTip.Active = value; }
        }

        public string ValueString
        {
            get
            {
                return _number[0].ToString("00") + "." + _number[1].ToString("00") + ":" + _number[2].ToString("00") + ":" +
                       _number[3].ToString("00");
            }
            set
            {
                try
                {
                    _number[0] = Convert.ToInt32(value.Substring(0, 2));
                    _number[1] = Convert.ToInt32(value.Substring(3, 2));
                    _number[2] = Convert.ToInt32(value.Substring(6, 2));
                    _number[3] = Convert.ToInt32(value.Substring(9, 2));
                    Refresh();
                }
                catch
                {
                    MessageBox.Show(
                        @"Error in data transfer to TimeSpan Picker! Operation has been canceled. string ""dd.hh:MM:SS"" DD - Days, HH - hours, MM - Minutes, SS - Seconds",
                        @"TimeSpanPicker Error", MessageBoxButtons.OK, MessageBoxIcon.Hand,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        public TimeSpan ValueTimeSpan
        {
            get { return new TimeSpan(_number[0], _number[1], _number[2], _number[3]); }
            set
            {
                try
                {
                    _number[0] = value.Days;
                    _number[1] = value.Hours;
                    _number[2] = value.Minutes;
                    _number[3] = value.Seconds;
                    Refresh();
                }
                catch
                {
                    MessageBox.Show(
                        @"Error in data transfer to TimeSpan Picker! Operation has been canceled. Timespan (DD, HH, MM, SS) DD - Days, HH - hours, MM - Minutes, SS - Seconds",
                        @"TimeSpanPicker Error", MessageBoxButtons.OK, MessageBoxIcon.Hand,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        public event EventHandler ValueChanged;

        private void AddOneToActiveBox()
        {
            if (_activeBox == 1)
            {
                if (_number[_activeBox - 1] < 0x63)
                {
                    _number[_activeBox - 1]++;
                    Refresh();
                }
            }
            else if (_activeBox == 2)
            {
                if (_number[_activeBox - 1] < 0x17)
                {
                    _number[_activeBox - 1]++;
                    Refresh();
                }
                else if ((_number[_activeBox - 1] == 0x17) && (_number[_activeBox - 2] < 0x63))
                {
                    _number[_activeBox - 2]++;
                    _number[_activeBox - 1] = 0;
                    Refresh();
                }
            }
            else if (_activeBox == 3)
            {
                if (_number[_activeBox - 1] < 0x3b)
                {
                    _number[_activeBox - 1]++;
                    Refresh();
                }
                else if ((_number[_activeBox - 1] == 0x3b) && (_number[_activeBox - 2] < 0x17))
                {
                    _number[_activeBox - 1] = 0;
                    _number[_activeBox - 2]++;
                    Refresh();
                }
            }
            else if (_activeBox == 4)
            {
                if (_number[_activeBox - 1] < 0x3b)
                {
                    _number[_activeBox - 1]++;
                    Refresh();
                }
                else if ((_number[_activeBox - 1] == 0x3b) && (_number[_activeBox - 2] < 0x3b))
                {
                    _number[_activeBox - 1] = 0;
                    _number[_activeBox - 2]++;
                    Refresh();
                }
            }
        }

        private void CheckAndAdjustUpperLimitNumbers()
        {
            if (_number[1] > 0x17)
            {
                _number[1] = 0;
            }
            if (_number[2] > 0x3b)
            {
                _number[2] = 0;
            }
            if (_number[3] > 0x3b)
            {
                _number[3] = 0;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawBorderOld(PaintEventArgs pea)
        {
            var color = Color.Gray;

            //  pea.Graphics.DrawLine(new Pen(Color.Black), 1, 1, 1, base.Height - 2);
            pea.Graphics.DrawLine(new Pen(color), 0, 0, 0, Height);
            //  pea.Graphics.DrawLine(new Pen(Color.Black), 1, 1, base.Width - 2, 1);
            pea.Graphics.DrawLine(new Pen(color), 0, 0, Width, 0);
            // pea.Graphics.DrawLine(new Pen(Color.White), 0, base.Height - 1, base.Width - 1, base.Height - 1);
            pea.Graphics.DrawLine(new Pen(color), 0, Height - 1, Width, Height - 1);
            //  pea.Graphics.DrawLine(new Pen(Color.White), base.Width - 1, 0, base.Width - 1, base.Height - 2);
            pea.Graphics.DrawLine(new Pen(color), Width - 1, 1, Width - 1, Height - 1);
        }

        private void DrawNumbers(PaintEventArgs pea)
        {
            for (var i = 0; i < 4; i++)
            {
                pea.Graphics.DrawString(_number[i].ToString("00"), Font,
                    _activeBox == i + 1
                        ? new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText))
                        : new SolidBrush(Color.FromKnownColor(KnownColor.Black)), _rec[i]);
            }
            pea.Graphics.DrawString(".", Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), _brec[0]);
            pea.Graphics.DrawString(":", Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), _brec[1]);
            pea.Graphics.DrawString(":", Font, new SolidBrush(Color.FromKnownColor(KnownColor.Black)), _brec[2]);
        }

        private void DrawRecs(PaintEventArgs pea)
        {
            for (var i = 1; i <= 4; i++)
            {
                pea.Graphics.FillRectangle(
                    _activeBox == i ? new SolidBrush(SystemColors.Highlight) : new SolidBrush(Color.White), _rec[i - 1]);
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // TimeSpanPicker
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            Font = new Font("Tahoma", 8.25F);
            MinimumSize = new Size(96, 20);
            Name = "TimeSpanPicker";
            Size = new Size(96, 20);
            Tag = "";
            ResumeLayout(false);
        }

        protected override bool IsInputKey(Keys keyData) =>
            (keyData == Keys.Up) || (keyData == Keys.Down) || (keyData == Keys.Left) || (keyData == Keys.Right) ||
            base.IsInputKey(keyData);

        private void LoadToolTip()
        {
            _toolTip = new ToolTip
            {
                AutoPopDelay = 0x1388,
                InitialDelay = 0x3e8,
                IsBalloon = IsVisualStyle,
                ReshowDelay = 500,
                ShowAlways = true
            };
            _toolTip.SetToolTip(this,
                "Enter a date in the format \"dd.hh:MM:SS\"\r\n with DD -day, HH - hours, MM - minutes, SS.");
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _activeBox = 1;
            Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var hasChanged = false;
            base.OnKeyDown(e);
            if (_activeBox > 0)
            {
                var keyData = e.KeyData;
                if (e.KeyData == Keys.Left)
                {
                    if (_activeBox > 1)
                    {
                        _activeBox--;
                        Refresh();
                    }
                    else if (_activeBox == 1)
                    {
                        _activeBox = 4;
                        Refresh();
                    }
                }
                else if (e.KeyData == Keys.Right)
                {
                    if (_activeBox < 4)
                    {
                        _activeBox++;
                        Refresh();
                    }
                    else if (_activeBox == 4)
                    {
                        _activeBox = 1;
                        Refresh();
                    }
                }
                else if (e.KeyData == Keys.Up)
                {
                    AddOneToActiveBox();
                }
                else if ((e.KeyData == Keys.Down) && (_number[_activeBox - 1] > 0))
                {
                    _number[_activeBox - 1]--;
                    Refresh();
                }
                if ((Keys.D0 == keyData) || (Keys.D1 == keyData) || (Keys.D2 == keyData) || (Keys.D3 == keyData) ||
                    (Keys.D4 == keyData) || (Keys.D5 == keyData) || (Keys.D6 == keyData) || (Keys.D7 == keyData) ||
                    (Keys.D8 == keyData) || (Keys.D9 == keyData))
                {
                    _number[_activeBox - 1] =
                        Convert.ToInt32(_number[_activeBox - 1].ToString("00").Substring(1, 1) +
                                        Convert.ToInt32(keyData.ToString().Substring(1, 1)));
                    Refresh();
                    hasChanged = true;
                }
                else if ((Keys.NumPad0 == keyData) || (Keys.NumPad1 == keyData) || (Keys.NumPad2 == keyData) ||
                         (Keys.NumPad3 == keyData) || (Keys.NumPad4 == keyData) || (Keys.NumPad5 == keyData) ||
                         (Keys.NumPad6 == keyData) || (Keys.NumPad7 == keyData) || (Keys.NumPad8 == keyData) ||
                         (Keys.NumPad9 == keyData))
                {
                    _number[_activeBox - 1] =
                        Convert.ToInt32(_number[_activeBox - 1].ToString("00").Substring(1, 1) +
                                        Convert.ToInt32(keyData.ToString().Substring(6, 1)));
                    Refresh();
                    hasChanged = true;
                }
                else if (keyData == Keys.Delete)
                {
                    _number[_activeBox - 1] = 0;
                    Refresh();
                }
                else if (keyData == Keys.Back)
                {
                    _number[_activeBox - 1] = 0;
                    if (_activeBox > 1)
                    {
                        _activeBox--;
                    }
                    Refresh();
                }

                if (hasChanged)
                    ValueChanged?.Invoke(this, new EventArgs());
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _activeBox = 0;
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.X > 2) && (e.X < 2f + _xSize))
            {
                _activeBox = 1;
                Refresh();
            }
            else if ((e.X > 2f + _xSize + _brec[0].Width) && (e.X < 2f + _xSize * 2f + _brec[0].Width))
            {
                _activeBox = 2;
                Refresh();
            }
            else if ((e.X > 2f + _xSize * 2f + _brec[0].Width + _brec[1].Width) &&
                     (e.X < 2f + _xSize * 3f + _brec[0].Width + _brec[1].Width))
            {
                _activeBox = 3;
                Refresh();
            }
            else if ((e.X > 2f + _xSize * 3f + _brec[0].Width + _brec[1].Width + _brec[2].Width) &&
                     (e.X < 2f + _xSize * 4f + _brec[0].Width + _brec[1].Width + _brec[2].Width))
            {
                _activeBox = 4;
                Refresh();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta >= 120)
            {
                AddOneToActiveBox();
            }
            else if ((e.Delta <= -120) && (_number[_activeBox - 1] > 0))
            {
                _number[_activeBox - 1]--;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _xSize = (Width - 1) / 5 - 2;
            _ySize = Height - 1 - 3 - 2;
            _rec[0] = new RectangleF(3f, 3f, _xSize, _ySize);
            _rec[1] = new RectangleF(2f + (_xSize + _xSize / 3f), 3f, _xSize, _ySize);
            _rec[2] = new RectangleF(2f + (_xSize * 2f + _xSize / 3f * 2f), 3f, _xSize, _ySize);
            _rec[3] = new RectangleF(2f + (_xSize * 3f + _xSize / 3f * 3f), 3f, _xSize, _ySize);
            _brec[0] = new RectangleF(_rec[0].X + _rec[0].Width, 2f, _rec[1].X - _rec[0].X - _rec[0].Width, _ySize);
            _brec[1] = new RectangleF(_rec[1].X + _rec[1].Width, 2f, _rec[2].X - _rec[1].X - _rec[1].Width, _ySize);
            _brec[2] = new RectangleF(_rec[2].X + _rec[2].Width, 2f, _rec[3].X - _rec[2].X - _rec[2].Width, _ySize);
            CheckAndAdjustUpperLimitNumbers();
            if (IsVisualStyle)
            {
                DrawRecs(e);
                e.Graphics.DrawRectangle(new Pen(VisualStyleInformation.TextControlBorder), 0, 0, Width - 1, Height - 1);
            }
            else
            {
                DrawRecs(e);
                DrawBorderOld(e);
            }
            DrawNumbers(e);
        }
    }
}