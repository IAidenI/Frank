using Frank.Core;
using Frydia.Utils;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Frank
{
    public partial class Lydia : Form
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private Calculation _calcul;
        private Dictionary<IntPtr, Rectangle> _processCalc;
        private Dictionary<IntPtr, Hide> _hideForms;
        private HashSet<IntPtr> _pendingWindows;

        public Lydia()
        {
            InitializeComponent();
            this._calcul = new Calculation();
            this.tbCalcul.AutoSize = false;
            this._processCalc = new Dictionary<IntPtr, Rectangle>();
            this._hideForms = new Dictionary<IntPtr, Hide>();
            this._pendingWindows = new HashSet<IntPtr>();
        }

        private void Jeanne_Load(object sender, EventArgs e)
        {
            this.tbCalcul.Text = this._calcul.Generate();
            this.AutoResizeTextBox(this.tbCalcul);
            Debug.WriteLine(this.tbCalcul.Text);
            Debug.WriteLine(this._calcul.GetResult());
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

        private void timerSpy_Tick(object sender, EventArgs e)
        {
            var tasks = new List<Task>();
            var windows = ProcessUtils.GetAllWindows("CalculatorApp");

            foreach (var hWnd in windows)
            {
                if (!this._processCalc.ContainsKey(hWnd) && !this._pendingWindows.Contains(hWnd))
                {
                    this._pendingWindows.Add(hWnd);
                    tasks.Add(Task.Run(async () =>
                    {
                        Rectangle previous = ProcessUtils.GetBounds(hWnd);
                        Rectangle bounds = previous;
                        int retries = 20;
                        while (bounds == previous && retries-- > 0)
                        {
                            await Task.Delay(500);
                            bounds = ProcessUtils.GetBounds(hWnd);
                        }
                        this.Invoke(() =>
                        {
                            this._processCalc[hWnd] = bounds;
                            this._pendingWindows.Remove(hWnd);
                            HideProcess();
                        });
                    }));
                }
            }

            var alivePids = Process.GetProcessesByName("CalculatorApp").Select(p => p.Id).ToHashSet();

            var closed = this._processCalc.Keys
                .Where(h =>
                {
                    ProcessUtils.GetWindowThreadProcessId(h, out uint pid);
                    return !alivePids.Contains((int)pid);
                }).ToList();

            if (closed.Any())
            {
                closed.ForEach(h => this._processCalc.Remove(h));
                this.ClearHideProcess();
            }
        }

        private void HideProcess()
        {
            foreach (var (hWnd, rect) in this._processCalc)
            {
                if (!this._hideForms.ContainsKey(hWnd))
                {
                    var hideForm = new Hide(rect);
                    this._hideForms[hWnd] = hideForm;
                    hideForm.Show();
                }
            }
        }

        private void ClearHideProcess()
        {
            var toClose = this._hideForms.Keys.Where(h => !_processCalc.ContainsKey(h)).ToList();
            foreach (var hWnd in toClose)
            {
                this._hideForms[hWnd].Close();
                this._hideForms.Remove(hWnd);
            }
        }

        private void timerMove_Tick(object sender, EventArgs e)
        {
            foreach (var (hWnd, hideForm) in this._hideForms)
            {
                Rectangle bounds = ProcessUtils.GetBounds(hWnd);
                if (hideForm.Location != new Point(bounds.X, bounds.Y) || hideForm.Size != new Size(bounds.Width, bounds.Height))
                {
                    hideForm.SetLocation(bounds.X, bounds.Y);
                    hideForm.SetSize(bounds.Width, bounds.Height);
                }
            }
        }
    }
}
