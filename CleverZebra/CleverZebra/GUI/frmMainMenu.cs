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
            this.FormClosing += frmMainMenu_FormClosing;
        }

        void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e) {
            Exit(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Exit(sender, new FormClosingEventArgs(CloseReason.ApplicationExitCall, false));
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
            this.FormClosing -= frmMainMenu_FormClosing;
            solveBox.Show();
        }

        internal void Exit(object sender, FormClosingEventArgs e) {
            var result = MessageBox.Show("Are you sure you want to quit?", "Confirm exit", MessageBoxButtons.OKCancel);
            if (result == System.Windows.Forms.DialogResult.OK) {
                this.FormClosing -= frmMainMenu_FormClosing;
                Application.Exit();
            }
            else {
                e.Cancel = true;
            }
        }

    }
}
