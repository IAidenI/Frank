namespace Frank
{
    partial class Lydia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            tbCalcul = new TextBox();
            timerSpy = new System.Windows.Forms.Timer(components);
            timerMove = new System.Windows.Forms.Timer(components);
            timerClipboard = new System.Windows.Forms.Timer(components);
            rbtnValidate = new Frydia.UI.Controls.RoundedButton.RoundedButton();
            frameCalul = new Frydia.UI.Controls.RoundedButton.RoundControl();
            frameInfo = new Frydia.UI.Controls.RoundedButton.RoundControl();
            timerInfo = new System.Windows.Forms.Timer(components);
            lblInstructions = new Label();
            frameInstructions = new Frydia.UI.Controls.RoundedButton.RoundControl();
            lblInfo = new Label();
            rjUser = new CustomControls.RJControls.RJTextBox();
            iconInstructions = new Frydia.UI.Controls.CircleIcon();
            rbtnKillExcel = new Frydia.UI.Controls.RoundedButton.RoundedButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(157, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(526, 37);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Punition : Vous devez résoudre ce calcul";
            // 
            // tbCalcul
            // 
            tbCalcul.BackColor = Color.White;
            tbCalcul.BorderStyle = BorderStyle.None;
            tbCalcul.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbCalcul.Location = new Point(362, 149);
            tbCalcul.Name = "tbCalcul";
            tbCalcul.ReadOnly = true;
            tbCalcul.Size = new Size(78, 20);
            tbCalcul.TabIndex = 4;
            tbCalcul.Text = "Calcul";
            tbCalcul.TextAlign = HorizontalAlignment.Center;
            // 
            // timerSpy
            // 
            timerSpy.Enabled = true;
            timerSpy.Tick += timerSpy_Tick;
            // 
            // timerMove
            // 
            timerMove.Enabled = true;
            timerMove.Tick += timerMove_Tick;
            // 
            // timerClipboard
            // 
            timerClipboard.Enabled = true;
            timerClipboard.Tick += timerClipboard_Tick;
            // 
            // rbtnValidate
            // 
            rbtnValidate.BorderColor = Color.Transparent;
            rbtnValidate.BorderWidth = 0F;
            rbtnValidate.ButtonColor = Color.FromArgb(30, 111, 255);
            rbtnValidate.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rbtnValidate.ForeColor = Color.White;
            rbtnValidate.Location = new Point(473, 216);
            rbtnValidate.MouseOverBackColor = Color.FromArgb(20, 95, 200);
            rbtnValidate.Name = "rbtnValidate";
            rbtnValidate.Radius = 7;
            rbtnValidate.Size = new Size(93, 37);
            rbtnValidate.TabIndex = 7;
            rbtnValidate.Text = "✓ Valider";
            rbtnValidate.Click += rbtnValidate_Click;
            // 
            // frameCalul
            // 
            frameCalul.BackColor = Color.Transparent;
            frameCalul.BorderColor = Color.White;
            frameCalul.BorderWidth = 1F;
            frameCalul.Location = new Point(344, 133);
            frameCalul.Name = "frameCalul";
            frameCalul.Radius = 5;
            frameCalul.Size = new Size(115, 52);
            frameCalul.TabIndex = 8;
            // 
            // frameInfo
            // 
            frameInfo.BackColor = Color.Transparent;
            frameInfo.BorderColor = SystemColors.Control;
            frameInfo.BorderWidth = 1F;
            frameInfo.Location = new Point(0, 0);
            frameInfo.Name = "frameInfo";
            frameInfo.Radius = 5;
            frameInfo.Size = new Size(201, 52);
            frameInfo.TabIndex = 9;
            // 
            // timerInfo
            // 
            timerInfo.Interval = 3500;
            timerInfo.Tick += timerInfo_Tick;
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.BackColor = Color.Transparent;
            lblInstructions.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInstructions.Location = new Point(251, 334);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(360, 80);
            lblInstructions.TabIndex = 10;
            lblInstructions.Text = "Consignes :\r\n• Votre réponse doit obligatoirement être un nombre\r\n• Si vous utilisez une virgule, il s'agit de ',' pas '.'\r\n• Le résultat peut être négatif";
            // 
            // frameInstructions
            // 
            frameInstructions.BackColor = Color.Transparent;
            frameInstructions.BorderColor = SystemColors.Control;
            frameInstructions.BorderWidth = 1F;
            frameInstructions.Location = new Point(204, 311);
            frameInstructions.Name = "frameInstructions";
            frameInstructions.Radius = 7;
            frameInstructions.Size = new Size(428, 125);
            frameInstructions.TabIndex = 11;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfo.Location = new Point(20, 17);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(96, 20);
            lblInfo.TabIndex = 12;
            lblInfo.Text = "Informations";
            // 
            // rjUser
            // 
            rjUser.BackColor = SystemColors.Window;
            rjUser.BorderColor = Color.MediumSlateBlue;
            rjUser.BorderFocusColor = Color.HotPink;
            rjUser.BorderSize = 2;
            rjUser.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rjUser.ForeColor = Color.DimGray;
            rjUser.Location = new Point(216, 216);
            rjUser.Margin = new Padding(4);
            rjUser.Multiline = false;
            rjUser.Name = "rjUser";
            rjUser.Padding = new Padding(7);
            rjUser.PasswordChar = false;
            rjUser.PlacehorderText = "Saisir le résultat";
            rjUser.Size = new Size(250, 36);
            rjUser.TabIndex = 13;
            rjUser.UnderlinedStyle = false;
            rjUser.KeyDown += rjUser_KeyDown;
            // 
            // iconInstructions
            // 
            iconInstructions.BackColor = Color.Transparent;
            iconInstructions.Location = new Point(221, 334);
            iconInstructions.Name = "iconInstructions";
            iconInstructions.Size = new Size(24, 24);
            iconInstructions.TabIndex = 16;
            iconInstructions.Text = "i";
            // 
            // rbtnKillExcel
            // 
            rbtnKillExcel.BorderColor = Color.Transparent;
            rbtnKillExcel.BorderWidth = 0F;
            rbtnKillExcel.ButtonColor = Color.FromArgb(30, 111, 255);
            rbtnKillExcel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbtnKillExcel.ForeColor = Color.White;
            rbtnKillExcel.Location = new Point(130, 14);
            rbtnKillExcel.MouseOverBackColor = Color.FromArgb(20, 95, 200);
            rbtnKillExcel.Name = "rbtnKillExcel";
            rbtnKillExcel.Radius = 5;
            rbtnKillExcel.Size = new Size(51, 25);
            rbtnKillExcel.TabIndex = 17;
            rbtnKillExcel.Text = "Stop";
            rbtnKillExcel.Click += rbtnKillExcel_Click;
            // 
            // Lydia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(rbtnKillExcel);
            Controls.Add(iconInstructions);
            Controls.Add(lblInfo);
            Controls.Add(frameInfo);
            Controls.Add(rjUser);
            Controls.Add(lblInstructions);
            Controls.Add(frameInstructions);
            Controls.Add(tbCalcul);
            Controls.Add(frameCalul);
            Controls.Add(rbtnValidate);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Lydia";
            ShowInTaskbar = false;
            Text = "Lydia";
            TopMost = true;
            FormClosing += Lydia_FormClosing;
            FormClosed += Lydia_FormClosed;
            Load += Lydia_Load;
            Shown += Lydia_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitle;
        private TextBox tbCalcul;
        private System.Windows.Forms.Timer timerSpy;
        private System.Windows.Forms.Timer timerMove;
        private System.Windows.Forms.Timer timerClipboard;
        private Frydia.UI.Controls.RoundedButton.RoundedButton rbtnValidate;
        private Frydia.UI.Controls.RoundedButton.RoundControl frameCalul;
        private Frydia.UI.Controls.RoundedButton.RoundControl frameInfo;
        private System.Windows.Forms.Timer timerInfo;
        private Label lblInstructions;
        private Frydia.UI.Controls.RoundedButton.RoundControl frameInstructions;
        private Label lblInfo;
        private CustomControls.RJControls.RJTextBox rjUser;
        private Frydia.UI.Controls.CircleIcon iconInstructions;
        private Frydia.UI.Controls.RoundedButton.RoundedButton rbtnKillExcel;
    }
}