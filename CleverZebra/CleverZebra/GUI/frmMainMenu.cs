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

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e) {
           Application.Exit();
        }

        private void btnSettings_Click(object sender, EventArgs e) {
           frmOptions options = new frmOptions();
           this.AddOwnedForm(options);           
           options.InitializeComponent();
           options.Show();

        }

        private void btnSolvePuzzle_Click(object sender, EventArgs e) {
            frmPuzzles solveBox = new frmPuzzles();
            this.AddOwnedForm(solveBox);
            this.Hide();
            solveBox.Show();
        }
    }
}
