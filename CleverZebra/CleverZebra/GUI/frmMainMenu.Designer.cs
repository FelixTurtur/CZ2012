using CleverZebra.Resources;
namespace CleverZebra
{
    partial class frmMainMenu
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
         System.Windows.Forms.Label lblTitle;
         this.btnExit = new CleverZebra.Resources.CZButton();
         this.btnSettings = new CleverZebra.Resources.CZButton();
         this.btnSolvePuzzle = new CleverZebra.Resources.CZButton();
         lblTitle = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // lblTitle
         // 
         lblTitle.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
         lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         lblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         lblTitle.ForeColor = System.Drawing.SystemColors.ControlLight;
         lblTitle.Location = new System.Drawing.Point(350, 38);
         lblTitle.Name = "lblTitle";
         lblTitle.Size = new System.Drawing.Size(125, 25);
         lblTitle.TabIndex = 0;
         lblTitle.Text = "Clever Zebra";
         lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         // 
         // btnExit
         // 
         this.btnExit.BackColor = System.Drawing.SystemColors.Desktop;
         this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
         this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnExit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnExit.ForeColor = System.Drawing.SystemColors.ControlLight;
         this.btnExit.Location = new System.Drawing.Point(349, 153);
         this.btnExit.Name = "btnExit";
         this.btnExit.Size = new System.Drawing.Size(130, 33);
         this.btnExit.TabIndex = 2;
         this.btnExit.Text = "Exit";
         this.btnExit.UseVisualStyleBackColor = true;
         this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
         // 
         // btnSettings
         // 
         this.btnSettings.BackColor = System.Drawing.SystemColors.Desktop;
         this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
         this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnSettings.ForeColor = System.Drawing.SystemColors.ControlLight;
         this.btnSettings.Location = new System.Drawing.Point(349, 114);
         this.btnSettings.Name = "btnSettings";
         this.btnSettings.Size = new System.Drawing.Size(130, 33);
         this.btnSettings.TabIndex = 2;
         this.btnSettings.Text = "Settings";
         this.btnSettings.UseVisualStyleBackColor = true;
         this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
         // 
         // btnSolvePuzzle
         // 
         this.btnSolvePuzzle.BackColor = System.Drawing.SystemColors.Desktop;
         this.btnSolvePuzzle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
         this.btnSolvePuzzle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnSolvePuzzle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnSolvePuzzle.ForeColor = System.Drawing.SystemColors.ControlLight;
         this.btnSolvePuzzle.Location = new System.Drawing.Point(349, 75);
         this.btnSolvePuzzle.Name = "btnSolvePuzzle";
         this.btnSolvePuzzle.Size = new System.Drawing.Size(130, 33);
         this.btnSolvePuzzle.TabIndex = 2;
         this.btnSolvePuzzle.Text = "Solve Puzzle";
         this.btnSolvePuzzle.UseVisualStyleBackColor = true;
         this.btnSolvePuzzle.Click += new System.EventHandler(this.btnSolvePuzzle_Click);
         // 
         // frmMainMenu
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackgroundImage = global::CleverZebra.Properties.Resources.CircleSparks;
         this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
         this.ClientSize = new System.Drawing.Size(511, 382);
         this.Controls.Add(this.btnExit);
         this.Controls.Add(this.btnSettings);
         this.Controls.Add(this.btnSolvePuzzle);
         this.Controls.Add(lblTitle);
         this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Name = "frmMainMenu";
         this.Text = "Clever Zebra";
         this.TransparencyKey = System.Drawing.Color.Transparent;
         this.ResumeLayout(false);

        }

        #endregion

        private CZButton btnSolvePuzzle;
        private CZButton btnSettings;
        private CZButton btnExit;
    }
}

