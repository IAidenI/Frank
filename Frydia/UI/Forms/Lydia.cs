using Frank.Core;
using Frydia.Utils;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Frank
{
    public partial class Lydia : Form
    {
        [DllImport("user32.dll", SetLastError = true)] private static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
        private const uint _WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        [DllImport("user32.dll")] private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private Calculation _calcul;
        private bool _excelFound = false;
        private bool _snippingToolFound = false;
        private bool _closing = false;
        private string _lastClipboard = "";

        private Dictionary<IntPtr, Hide> _hideForms = new();
        private List<ProcessWatcher> _watchers;

        private Taskmanager _taskmanager;

        private Point _frameCalculPad = new Point(15, 15);
        private Point _frameInfoPad = new Point(15, 15);
        private Point _frameInstructionsPad = new Point(15, 15);

        private const int MaxExcelTries = 2;
        private int _excelTriesCounter = MaxExcelTries;

        public Lydia()
        {
            InitializeComponent();
            this._calcul = new Calculation();

            this.lblInstructions.Parent = this.frameInstructions;
            this.iconInstructions.Parent = this.frameInstructions;
            this.lblInstructions.BringToFront();
            this.iconInstructions.BringToFront();

            this.lblInfo.Parent = this.frameInfo;
            this.rbtnKillExcel.Parent = this.frameInfo;
            this.lblInfo.BringToFront();
            this.rbtnKillExcel.BringToFront();

            this.tbCalcul.AutoSize = false;
            this.lblInfo.Visible = false;
            this.frameInfo.Visible = false;
            this.rbtnKillExcel.Visible = false;

            this.BackColor = Color.FromArgb(248, 250, 253);

            this.lblInfo.ForeColor = Color.FromArgb(168, 127, 29, 29);

            this.frameCalul.BackgroundColor = Color.White;
            this.frameCalul.BorderColor = Color.FromArgb(220, 225, 230);

            this.frameInfo.BackgroundColor = Color.FromArgb(200, 245, 249, 255);
            this.frameInfo.BorderColor = Color.FromArgb(190, 215, 255);

            this.rbtnKillExcel.ButtonColor = Color.FromArgb(220, 53, 69);
            this.rbtnKillExcel.MouseOverBackColor = Color.FromArgb(200, 35, 51);
            this.rbtnKillExcel.ForeColor = Color.White;

            this.frameInstructions.BackgroundColor = Color.FromArgb(200, 245, 249, 255);
            this.frameInstructions.BorderColor = Color.FromArgb(190, 215, 255);

            this.frameInfo.BackgroundColor = Color.FromArgb(220, 255, 235, 238);
            this.frameInfo.BorderColor = Color.FromArgb(200, 220, 38, 38);

            this.rjUser.BorderColor = Color.FromArgb(200, 200, 200);
            this.rjUser.BorderFocusColor = Color.FromArgb(74, 131, 255);
            this.iconInstructions.IconColor = Color.FromArgb(19, 115, 241);


            this._watchers = new List<ProcessWatcher>
            {
                new ProcessWatcher("CalculatorApp",
                    onFound: (hWnd, bounds) => {
                        var hideForm = new Hide(bounds);
                        this._hideForms[hWnd] = hideForm;
                        hideForm.Show();
                        this.BringToFront();
                    },
                    onLost: (hWnd) => {
                        if (this._hideForms.TryGetValue(hWnd, out var f))
                        {
                            f.Close();
                            this._hideForms.Remove(hWnd);
                        }
                    }
                ),
                new ProcessWatcher("excel",
                    onFound: (hWnd, bounds) => this._excelFound = true,
                    onLost: (hWnd) => {}
                ),
                new ProcessWatcher("SnippingTool",
                    onFound: (hWnd, bounds) => this._snippingToolFound = true,
                    onLost: (hWnd) => {}
                ),
                new ProcessWatcher("ScreenClippingHost",
                    onFound: (hWnd, bounds) => this._snippingToolFound = true,
                    onLost: (hWnd) => {}
                ),
                new ProcessWatcher("SnipAndSketch",
                    onFound: (hWnd, bounds) => this._snippingToolFound = true,
                    onLost: (hWnd) => {}
                )
            };

            this._taskmanager = new Taskmanager();
        }

        private void Lydia_Shown(object sender, EventArgs e)
        {
            this.lblTitle.Focus();
        }

        private void Lydia_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            this.LoadCalcul();

            // Centre les composents en X
            this.lblTitle.Location = new Point((this.ClientSize.Width - this.lblTitle.Width) / 2, this.lblTitle.Location.Y);
            this.LoadInfo();

            this.frameInstructions.Size = new Size(this.lblInstructions.Size.Width + this.iconInstructions.Width + 3 * this._frameInstructionsPad.X, this.lblInstructions.Size.Height + 2 * this._frameInstructionsPad.Y);
            this.frameInstructions.Location = new Point((this.ClientSize.Width - this.frameInstructions.Size.Width) / 2, this.frameInstructions.Location.Y);
            this.iconInstructions.Location = new Point(this._frameInstructionsPad.X, this._frameInstructionsPad.Y);
            this.lblInstructions.Location = new Point(this.iconInstructions.Width + 2 * this._frameInstructionsPad.X, this._frameInstructionsPad.Y);

            int spacing = 10;
            int groupWidth = this.rjUser.Width + spacing + this.rbtnValidate.Width;
            int startX = (this.ClientSize.Width - groupWidth) / 2;
            this.rjUser.Location = new Point(startX, this.rjUser.Location.Y);
            this.rbtnValidate.Location = new Point(this.rjUser.Right + spacing, this.rbtnValidate.Location.Y);
        }

        private void LoadCalcul()
        {
            // Resets
            this._excelFound = false;
            this._snippingToolFound = false;

            this.tbCalcul.Text = this._calcul.Generate();
            this.AutoResizeTextBox(this.tbCalcul);
            Debug.WriteLine(this.tbCalcul.Text);
            Debug.WriteLine(this._calcul.GetResult());
            this.tbCalcul.Location = new Point((this.ClientSize.Width - this.tbCalcul.Width) / 2, this.tbCalcul.Location.Y);
            this.frameCalul.Location = new Point(this.tbCalcul.Location.X - this._frameCalculPad.X, this.tbCalcul.Location.Y - this._frameCalculPad.Y);
            this.frameCalul.Size = new Size(this.tbCalcul.Size.Width + 2 * this._frameCalculPad.X, this.tbCalcul.Height + 2 * this._frameCalculPad.Y);

            foreach (var watcher in this._watchers)
            {
                if (watcher.ProcessName.Equals("excel", StringComparison.OrdinalIgnoreCase) ||
                    watcher.ProcessName.Equals("SnippingTool", StringComparison.OrdinalIgnoreCase) ||
                    watcher.ProcessName.Equals("ScreenClippingHost", StringComparison.OrdinalIgnoreCase) ||
                    watcher.ProcessName.Equals("SnipAndSketch", StringComparison.OrdinalIgnoreCase))
                {
                    watcher.AcknowledgeCurrentWindows();
                }
            }
        }

        private void LoadInfo(bool button = false)
        {
            int width = this.lblInfo.Width + 2 * this._frameInfoPad.X;
            int height = this.lblInfo.Height + 2 * this._frameInfoPad.Y;

            if (button) width += this.rbtnKillExcel.Width + 2 * this._frameCalculPad.X;

            this.frameInfo.Size = new Size(width, height);

            this.frameInfo.Location = new Point(this.ClientRectangle.X, this.ClientRectangle.Y);
            this.lblInfo.Location = new Point(this._frameInfoPad.X, this._frameInfoPad.Y);

            if (button) this.rbtnKillExcel.Location = new Point(this.lblInfo.Right + this._frameCalculPad.X, this._frameInfoPad.Y + (this.lblInfo.Height - this.rbtnKillExcel.Height) / 2);
        }

        private void ShowInfo(string message, bool button = false)
        {
            this.timerInfo.Stop();
            this.lblInfo.Visible = false;
            this.frameInfo.Visible = false;
            this.rbtnKillExcel.Visible = false;

            this.lblInfo.Text = message;
            this.lblInfo.AutoSize = true;
            this.lblInfo.Update();

            this.LoadInfo(button);

            this.lblInfo.Visible = true;
            this.frameInfo.Visible = true;
            this.rbtnKillExcel.Visible = button;

            this.timerInfo.Start();
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

        private void rbtnValidate_Click(object sender, EventArgs e)
        {
            if (this.IsInvalidInput(this.rjUser.Text))
            {
                this.ShowInfo("Caractère invalide.");
                return;
            }

            if (!long.TryParse(this.rjUser.Text, out var input))
            {
                this.ShowInfo("Nombre invalide.");
                return;
            }

            if (this._calcul.CheckEmergency(input))
            {
                MessageBox.Show("Tu triches !!\nMais bon, je l'accepte.", "TRICHEUR !!!");
                this.CleanClose();
                return;
            }

            if (this._excelFound)
            {
                if (this._excelTriesCounter == 0)
                {
                    this.ShowInfo("Excel à été ouvert --> nouvelle formule\nVoulez vous arrêter excel ?", true);
                }
                else
                {
                    this._excelTriesCounter--;
                    this.ShowInfo("Excel à été ouvert --> nouvelle formule");
                }
                this.LoadCalcul();
                return;
            }

            if (this._calcul.CheckResult(decimal.Parse(this.rjUser.Text)))
            {
                MessageBox.Show("C'est le bon résultat, tu es libre.", "Bravo");
                this.CleanClose();
            }
            else
            {
                this.ShowInfo("Mauvais résultat, recommence.");
            }
        }

        private void rjUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rbtnValidate.PerformClick();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                this.rjUser.Text = "Oukilé le texte ?";
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
            // Gestionnaire des tâches
            this._taskmanager.Monitor();
            if (this._taskmanager.Acknowledge)
            {
                this.ShowInfo("Un gestionnaire des tâches...\nTu pensais que je ne le verrais pas ?");
                this._taskmanager.Acknowledge = false;
            }

            // Snipping Tool
            if (this._snippingToolFound)
            {
                this.ShowInfo("Et non, pas de Google Lens pour aujourd'hui");
                this._snippingToolFound = false;
            }

            // Autres process
            foreach (var watcher in this._watchers)
            {
                var windows = ProcessUtils.GetAllWindows(watcher.ProcessName);

                foreach (var hWnd in windows)
                {
                    if (!watcher.KnownWindows.ContainsKey(hWnd) && !watcher.PendingWindows.Contains(hWnd))
                    {
                        watcher.PendingWindows.Add(hWnd);

                        Task.Run(async () =>
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
                                watcher.KnownWindows[hWnd] = bounds;
                                watcher.PendingWindows.Remove(hWnd);
                                watcher.OnWindowFound(hWnd, bounds);
                            });
                        });
                    }
                }

                var alivePids = Process.GetProcessesByName(watcher.ProcessName).Select(p => p.Id).ToHashSet();

                var closed = watcher.KnownWindows.Keys
                    .Where(h =>
                    {
                        ProcessUtils.GetWindowThreadProcessId(h, out uint pid);
                        return !alivePids.Contains((int)pid);
                    })
                    .ToList();

                foreach (var h in closed)
                {
                    watcher.KnownWindows.Remove(h);
                    watcher.OnWindowLost(h);
                }
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
        private void CleanClose()
        {
            this.timerSpy.Stop();
            this.timerMove.Stop();
            this.timerClipboard.Stop();
            this.timerInfo.Stop();

            this._closing = true;
            this.Close();
        }

        private void Lydia_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (!this._closing)
            {
                this.ShowInfo("Vraiment ?\nTu pensais juste pouvoir quitter comme ça ?");
                e.Cancel = true;
            }
        }

        private void Lydia_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this._closing) Application.ExitThread();
        }

        // Empêche le déplacement de la fenêtre
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 0x2;

            if (m.Msg == WM_NCLBUTTONDOWN && (int)m.WParam == HTCAPTION)
                return;

            base.WndProc(ref m);
        }

        // Empêche la capture d'écran
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SetWindowDisplayAffinity(this.Handle, _WDA_EXCLUDEFROMCAPTURE);
        }

        private void timerClipboard_Tick(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText()) return;

            string current = Clipboard.GetText();

            if (current != this._lastClipboard)
            {
                this._lastClipboard = current;
                Clipboard.SetText("Tu ne pensais pas que ça serait si simple quand même :)");
            }
        }

        private void timerInfo_Tick(object sender, EventArgs e)
        {
            this.lblInfo.Visible = false;
            this.frameInfo.Visible = false;
            this.timerInfo.Stop();
        }

        private void rbtnKillExcel_Click(object sender, EventArgs e)
        {
            var excels = Process.GetProcessesByName("excel");
            foreach (var excel in excels)
            {
                try
                {
                    excel.Kill();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            this._excelTriesCounter = MaxExcelTries;
        }
    }
}
