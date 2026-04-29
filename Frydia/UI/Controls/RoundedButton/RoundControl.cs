using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Frydia.UI.Controls.RoundedButton
{
    public partial class RoundControl : UserControl
    {
        public RoundControl()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true
            );
        }

        private int _radius = 10;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Radius
        {
            get { return _radius; }
            set 
            {
                _radius = value;
                this.Invalidate();
            }
        }

        private Color _backgroundColor = SystemColors.Control;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                this._backgroundColor = value;
                this.Invalidate();
            }
        }

        private Color _borderColor = SystemColors.Control;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                this._borderColor = value;
                this.Invalidate();
            }
        }

        private float _borderWidth = 1.0f;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float BorderWidth
        {
            get { return this._borderWidth; }
            set
            {
                this._borderWidth = value;
                this.Invalidate();
            }
        }

        private void drawBorder(Graphics g)
        {
            using var pen = new Pen(this._borderColor, this._borderWidth);
            g.DrawRoundedRectangle(pen, 10, 10, this.Width - 20, this.Height - 20, this._radius);
        }
        private void drawBackground(Graphics g)
        {
            using var brush = new SolidBrush(this._backgroundColor);
            g.FillRoundedRectangle(brush, 10, 10, this.Width - 20, this.Height - 20, this._radius);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            this.drawBackground(g);
            this.drawBorder(g);
        }
    }
}
