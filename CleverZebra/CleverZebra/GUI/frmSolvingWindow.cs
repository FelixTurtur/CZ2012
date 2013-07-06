using CleverZebra.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CleverZebra
{
    //displays puzzle as it is solved
    class frmSolvingWindow : Form
    {
       private CZButton btnMainMenu;

        internal void InitializeComponent()
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
         this.btnMainMenu.Location = new System.Drawing.Point(12, 12);
         this.btnMainMenu.Name = "btnMainMenu";
         this.btnMainMenu.Size = new System.Drawing.Size(130, 40);
         this.btnMainMenu.TabIndex = 1;
         this.btnMainMenu.Text = "Main Menu";
         this.btnMainMenu.UseVisualStyleBackColor = true;
         this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
         // 
         // frmSolvingWindow
         // 
         this.BackgroundImage = global::CleverZebra.Properties.Resources.CircleSparks;
         this.ClientSize = new System.Drawing.Size(934, 527);
         this.Controls.Add(this.btnMainMenu);
         this.Name = "frmSolvingWindow";
         this.ResumeLayout(false);

        }

        private void btnMainMenu_Click(object sender, EventArgs e) {
           this.Owner.Show();
           this.Close();
        }


    }
}
