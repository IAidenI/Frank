using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Frydia.UI.Controls
{
    public partial class CircleIcon : Control
    {
        private string _text = string.Empty;
        private Color _iconColor    = Color.Black;
        private float _iconFontSize = 13f;

        public CircleIcon()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true
            );

            this.Cursor = Cursors.Default;
            this.UpdateSize();
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => this._text;
            set
            {
                this._text = value;
                this.Invalidate();
            }
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color IconColor
        {
            get => this._iconColor;
            set
            {
                this._iconColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float IconFontSize
        {
            get => this._iconFontSize;
            set
            {
                this._iconFontSize = value;
                this.UpdateSize();
                this.Invalidate();
            }
        }

        private void UpdateSize()
        {
            int size = (int)Math.Ceiling(this._iconFontSize * 1.8f);
            this.Size = new Size(size, size);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int pad = 2;
            Rectangle rect = new Rectangle(pad, pad, this.Size.Width - 2 * pad, this.Size.Width - 2 * pad);

            using var pen = new Pen(this.IconColor, Math.Max(1.5f, this.IconFontSize / 5f));
            e.Graphics.DrawEllipse(pen, rect);

            using var font  = new Font(this.Font.FontFamily, this.IconFontSize, this.Font.Style);
            using var brush = new SolidBrush(this.IconColor);

            SizeF textSize = e.Graphics.MeasureString(this.Text, font);

            e.Graphics.DrawString(this.Text, font, brush, (this.Size.Width - textSize.Width) / 2f, (this.Size.Height - textSize.Height) / 2f);
        }
    }
}
