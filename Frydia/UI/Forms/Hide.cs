using Frank.Utils;
using System.Diagnostics;

namespace Frank
{
    public partial class Hide : Form
    {
        private TextStyle label;
        private int yPad = 100;

        public Hide(Rectangle bounds)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.SetLocation(bounds.X, bounds.Y);
            this.SetSize(bounds.Width, bounds.Height);
        }

        public void SetSize(int width, int height)
        {
            this.Size = new Size(width, height - this.yPad);
        }

        public void SetLocation(int x, int y)
        {
            this.Location = new Point(x, y + this.yPad);
        }

        private void Hide_Load(object sender, EventArgs e)
        {
            this.label = new TextStyle("Interdit !!", new FontFamily("Segoe UI"), (int)FontStyle.Bold, 48, new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2), Utils.Utils.GetCenterAlignment());
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Utils.Utils.DrawText(this.label, e);
        }

        private void Hide_Shown(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            this.label.position = new Point(this.ClientSize.Width /2, this.ClientSize.Height /2);
            this.pictureBox.Invalidate();
        }

        // Cache du alt + tab
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
    }
}
