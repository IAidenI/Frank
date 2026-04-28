using Frank.Core;
using System.Diagnostics;

namespace Frank
{
    public partial class Lydia : Form
    {
        private Calculation _calcul;
        public Lydia()
        {
            InitializeComponent();
            this._calcul = new Calculation();
            this.tbCalcul.AutoSize = false;
        }

        private void Jeanne_Load(object sender, EventArgs e)
        {
            this.tbCalcul.Text = this._calcul.Generate();
            this.AutoResizeTextBox(this.tbCalcul);
            Debug.WriteLine(this.tbCalcul.Text);
            Debug.WriteLine(this._calcul.GetResult());

            /*Process[] pname = Process.GetProcessesByName("CalculatorApp");
            if (pname.Length == 0)
                MessageBox.Show("nothing");
            else
                MessageBox.Show("run");

            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                Debug.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
            }*/
        }

        private bool IsInvalidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return true;

            bool hasDigit = false;
            bool hasDot = false;

            for (int i = 0; i < input.Length; i++)
            {
                int c = (int)input[i];

                // Chiffre
                if (char.IsDigit((char)c))
                {
                    hasDigit = true;
                    continue;
                }

                // Signe moins uniquement en première position
                if (c == 0x2d)
                {
                    if (i != 0) return true; // invalide
                    continue;
                }

                // Virgule
                if (c == 0x2c)
                {
                    if (hasDot) return true; // deuxième point interdit

                    // Point sans chiffre avant ou après => invalide (gère ".0", "12.")
                    if (i == 0 || i == input.Length - 1) return true;

                    hasDot = true;
                    continue;
                }

                // Caractère interdit
                return true;
            }

            return !hasDigit;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (this.IsInvalidInput(this.tbUser.Text))
            {
                MessageBox.Show("Caractère invalide");
                return;
            }

            if (this._calcul.CheckResult(decimal.Parse(this.tbUser.Text)))
            {
                MessageBox.Show("Bravo");
                this.Close();
            }
            else
            {
                MessageBox.Show("Loser");
            }
        }

        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnValidate.PerformClick();
            }
        }
        
        private void tbCalcul_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText("Et non tu ne pensais pas que ça serait si simple quand même :)");
                e.SuppressKeyPress = true;
            }
        }

        private void AutoResizeTextBox(TextBox textBox)
        {
            using (Graphics g = textBox.CreateGraphics())
            {
                Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);

                textBox.Width = size.Width + 10;
            }
        }
    }
}
