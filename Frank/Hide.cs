namespace Frank
{
    public partial class Hide : Form
    {
        private TextStyle label;

        public Hide()
        {
            InitializeComponent();
        }

        private void Hide_Load(object sender, EventArgs e)
        {
            this.label = new TextStyle("Nonon interit !!", new FontFamily("Segoe UI"), (int)FontStyle.Bold, 48, new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2), Utils.GetCenterAlignment());
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Utils.DrawText(this.label, e);
        }

        private void Hide_Shown(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
    }
}
