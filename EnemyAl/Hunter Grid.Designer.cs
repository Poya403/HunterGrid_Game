namespace EnemyAl
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            gameTimer = new System.Windows.Forms.Timer(components);
            btnRestart = new Button();
            lblScore = new Label();
            SuspendLayout();
            // 
            // gameTimer
            // 
            gameTimer.Tick += gameTimer_Tick;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(22, 57);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(102, 43);
            btnRestart.TabIndex = 0;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblScore.Location = new Point(22, 9);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(99, 31);
            lblScore.TabIndex = 1;
            lblScore.Text = "Score : 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background_Game1;
            ClientSize = new Size(898, 869);
            Controls.Add(lblScore);
            Controls.Add(btnRestart);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            ImeMode = ImeMode.Off;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hunter Grid";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer gameTimer;
        private Button btnRestart;
        private Label lblScore;
    }
}
