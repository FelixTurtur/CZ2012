using CleverZebra.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CleverZebra
{
    class frmSolvingWindow : Form
    {
        //displays puzzle as it is solved
        private Button btnMainMenu;

        private void InitializeComponent()
        {
            this.btnMainMenu = new CZButton("Main Menu");
            this.btnMainMenu.Location = new System.Drawing.Point(12, 12);

            this.SuspendLayout();
            // 
            // frmSolvingWindow
            // 
            this.ClientSize = new System.Drawing.Size(934, 527);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "frmSolvingWindow";
            this.ResumeLayout(false);

        }
    }
}
