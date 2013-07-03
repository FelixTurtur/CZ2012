using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleverZebra
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Application.Run(new frmSolvingWindow());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Application.Run(new frmOptions());
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
