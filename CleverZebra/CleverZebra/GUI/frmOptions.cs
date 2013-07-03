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
        private CZButton btnMainMenu;
        //Allow user to chose settings
        private void InitializeComponent()
        {
            this.btnMainMenu = new CleverZebra.Resources.CZButton();
            this.SuspendLayout();
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnMainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainMenu.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnMainMenu.Location = new System.Drawing.Point(296, 217);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(130, 40);
            this.btnMainMenu.TabIndex = 1;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.ClientSize = new System.Drawing.Size(439, 263);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "frmOptions";
            this.ResumeLayout(false);

        }
    }
}
