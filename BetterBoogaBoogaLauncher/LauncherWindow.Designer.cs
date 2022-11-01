namespace BetterBoogaBoogaLauncher
{
    partial class LauncherWindow
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
            this.components = new System.ComponentModel.Container();
            this.robloxTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.formBackground = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // robloxTimer
            // 
            this.robloxTimer.Enabled = true;
            this.robloxTimer.Tick += new System.EventHandler(this.RobloxTimer_Tick);
            // 
            // SuspendTimer
            // 
            this.SuspendTimer.Enabled = true;
            this.SuspendTimer.Interval = 500;
            this.SuspendTimer.Tick += new System.EventHandler(this.SuspendTimer_Tick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(15, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Starting Roblox .";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::BetterBoogaBoogaLauncher.Properties.Resources.robloxlogo;
            this.pictureBox2.Location = new System.Drawing.Point(170, 76);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(85, 85);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // formBackground
            // 
            this.formBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formBackground.Location = new System.Drawing.Point(0, 0);
            this.formBackground.Name = "formBackground";
            this.formBackground.Size = new System.Drawing.Size(430, 278);
            this.formBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.formBackground.TabIndex = 6;
            this.formBackground.TabStop = false;
            this.formBackground.Click += new System.EventHandler(this.formBackground_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(12, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Universe Name Here";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LauncherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(430, 278);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.formBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LauncherWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LauncherWindow";
            this.Load += new System.EventHandler(this.LauncherWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer robloxTimer;
        private System.Windows.Forms.Timer SuspendTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox formBackground;
        private System.Windows.Forms.Label label2;
    }
}