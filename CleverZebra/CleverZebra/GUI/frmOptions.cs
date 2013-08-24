using CleverZebra.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CleverZebra
{
    class frmOptions : Form
    {
        private CheckBox cbShowInfo;
        private CheckBox cbGoSlow;
        private CheckBox cbUserHelp;
        private Label label1;
        private CZButton btnMainMenu;

        public frmOptions() {
            InitializeComponent();
            this.cbGoSlow.Checked = Controller.getInstance().goSlow || false;
            this.cbShowInfo.Checked = Controller.getInstance().reportRelations;
        }
        //Allow user to chose settings
        internal void InitializeComponent()
        {
            this.btnMainMenu = new CleverZebra.Resources.CZButton();
            this.cbShowInfo = new System.Windows.Forms.CheckBox();
            this.cbGoSlow = new System.Windows.Forms.CheckBox();
            this.cbUserHelp = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnMainMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnMainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainMenu.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnMainMenu.Location = new System.Drawing.Point(296, 217);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(130, 40);
            this.btnMainMenu.TabIndex = 1;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // cbShowInfo
            // 
            this.cbShowInfo.AutoSize = true;
            this.cbShowInfo.BackColor = System.Drawing.Color.Transparent;
            this.cbShowInfo.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbShowInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbShowInfo.Location = new System.Drawing.Point(35, 32);
            this.cbShowInfo.Name = "cbShowInfo";
            this.cbShowInfo.Size = new System.Drawing.Size(149, 19);
            this.cbShowInfo.TabIndex = 2;
            this.cbShowInfo.Text = "Show me your thinking";
            this.cbShowInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.cbShowInfo.UseVisualStyleBackColor = false;
            this.cbShowInfo.CheckedChanged += new System.EventHandler(this.cbShowInfo_CheckedChanged);
            // 
            // cbGoSlow
            // 
            this.cbGoSlow.AutoSize = true;
            this.cbGoSlow.BackColor = System.Drawing.Color.Transparent;
            this.cbGoSlow.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.cbGoSlow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGoSlow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbGoSlow.Location = new System.Drawing.Point(35, 66);
            this.cbGoSlow.Name = "cbGoSlow";
            this.cbGoSlow.Size = new System.Drawing.Size(68, 19);
            this.cbGoSlow.TabIndex = 2;
            this.cbGoSlow.Text = "Go slow";
            this.cbGoSlow.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.cbGoSlow.UseVisualStyleBackColor = false;
            this.cbGoSlow.CheckedChanged += new System.EventHandler(this.cbGoSlow_CheckedChanged);
            // 
            // cbUserHelp
            // 
            this.cbUserHelp.AutoSize = true;
            this.cbUserHelp.BackColor = System.Drawing.Color.Transparent;
            this.cbUserHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbUserHelp.Location = new System.Drawing.Point(35, 100);
            this.cbUserHelp.Name = "cbUserHelp";
            this.cbUserHelp.Size = new System.Drawing.Size(88, 19);
            this.cbUserHelp.TabIndex = 2;
            this.cbUserHelp.Text = "Let me help";
            this.cbUserHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.cbUserHelp.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(302, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 41);
            this.label1.TabIndex = 3;
            this.label1.Text = "Options";
            // 
            // frmOptions
            // 
            this.BackgroundImage = global::CleverZebra.Properties.Resources.CircleSparks;
            this.ClientSize = new System.Drawing.Size(439, 263);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbUserHelp);
            this.Controls.Add(this.cbGoSlow);
            this.Controls.Add(this.cbShowInfo);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "frmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void btnMainMenu_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void cbGoSlow_CheckedChanged(object sender, EventArgs e) {
            Controller.getInstance().goSlow = ((CheckBox)sender).Checked;
        }

        private void cbShowInfo_CheckedChanged(object sender, EventArgs e) {
            Controller.getInstance().reportRelations = cbShowInfo.Checked;
        }

    }
}
