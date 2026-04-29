using System.ComponentModel;

namespace CustomControls.RJControls
{
    [DefaultEvent("_TextChanged")]
    public partial class RJTextBox : UserControl
    {
        //Fields
        private Color _borderColor = Color.MediumSlateBlue;
        private int _borderSize = 2;
        private bool _underlinedStyle = false;
        private Color _borderFocusColor = Color.HotPink;
        private bool _isFocused = false;

        //Constructor
        public RJTextBox()
        {
            InitializeComponent();
        }

        //Events
        public event EventHandler _TextChanged;

        //Properties
        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderSize
        {
            get { return _borderSize; }
            set
            {
                this._borderSize = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool UnderlinedStyle
        {
            get { return this._underlinedStyle; }
            set
            {
                this._underlinedStyle = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool PasswordChar
        {
            get { return this.textBox1.UseSystemPasswordChar; }
            set { this.textBox1.UseSystemPasswordChar = value; }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Multiline
        {
            get { return this.textBox1.Multiline; }
            set { this.textBox1.Multiline = value; }
        }

        [Category("RJ Code Advance")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.textBox1.BackColor = value;
            }
        }

        [Category("RJ Code Advance")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                this.textBox1.ForeColor = value;
            }
        }

        [Category("RJ Code Advance")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                this.textBox1.Font = value;
                if (this.DesignMode) this.UpdateControlHeight();
            }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderFocusColor
        {
            get { return this._borderFocusColor; }
            set { this._borderFocusColor = value; }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PlacehorderText
        {
            get => this.textBox1.PlaceholderText;
            set => this.textBox1.PlaceholderText = value;
        }


        //Overridden methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            //Draw border
            using (Pen penBorder = new Pen(this._borderColor, this._borderSize))
            {
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                if (this._isFocused) penBorder.Color = this._borderFocusColor;

                if (this._underlinedStyle) graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                else graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode) this.UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.UpdateControlHeight();
        }

        //Private methods
        private void UpdateControlHeight()
        {
            if (this.textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                this.textBox1.Multiline = true;
                this.textBox1.MinimumSize = new Size(0, txtHeight);
                this.textBox1.Multiline = false;

                this.Height = this.textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        //TextBox events
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this._TextChanged != null) this._TextChanged.Invoke(sender, e);
        }

        private void textBox1_Click(object sender, EventArgs e) => this.OnClick(e);

        private void textBox1_MouseEnter(object sender, EventArgs e) => this.OnMouseEnter(e);

        private void textBox1_MouseLeave(object sender, EventArgs e) => this.OnMouseLeave(e);

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this._isFocused = true;
            this.Invalidate();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this._isFocused = false;
            this.Invalidate();
        }

    }
}
