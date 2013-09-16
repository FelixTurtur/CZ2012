using CleverZebra.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CleverZebra
{
    //displays puzzle as it is solved
    class frmPuzzles : Form
    {
        private ListBox lbPuzzles;
        private Label lSelectPuzzle;
        private Label lTitle;
        private Label lIntro;
        private FlowLayoutPanel flowLayoutPanel1;
        private CZButton btnBegin;
        private CZButton btnMainMenu;

        public frmPuzzles() {
            Controller.getInstance().loadPuzzles();
            this.InitializeComponent();
            this.lbPuzzles.DataSource = Controller.getInstance().getPuzzleTitles();
            this.lSelectPuzzle.Visible = true;
            this.lbPuzzles.Visible = true;
            this.FormClosing += frmPuzzles_FormClosing;
        }

        void frmPuzzles_FormClosing(object sender, FormClosingEventArgs e) {
            this.Owner.Show();
            this.FormClosing -= frmPuzzles_FormClosing;
            this.Close();
        }
        
        #region initialiser
        internal void InitializeComponent()
        {
            this.lbPuzzles = new System.Windows.Forms.ListBox();
            this.lSelectPuzzle = new System.Windows.Forms.Label();
            this.lTitle = new System.Windows.Forms.Label();
            this.lIntro = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBegin = new CleverZebra.Resources.CZButton();
            this.btnMainMenu = new CleverZebra.Resources.CZButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPuzzles
            // 
            this.lbPuzzles.BackColor = System.Drawing.Color.White;
            this.lbPuzzles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPuzzles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPuzzles.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbPuzzles.FormattingEnabled = true;
            this.lbPuzzles.ItemHeight = 17;
            this.lbPuzzles.Location = new System.Drawing.Point(12, 111);
            this.lbPuzzles.Name = "lbPuzzles";
            this.lbPuzzles.Size = new System.Drawing.Size(240, 327);
            this.lbPuzzles.TabIndex = 2;
            this.lbPuzzles.SelectedIndexChanged += new System.EventHandler(this.lbPuzzles_SelectedIndexChanged);
            // 
            // lSelectPuzzle
            // 
            this.lSelectPuzzle.AutoSize = true;
            this.lSelectPuzzle.BackColor = System.Drawing.Color.Transparent;
            this.lSelectPuzzle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSelectPuzzle.ForeColor = System.Drawing.Color.DarkOrange;
            this.lSelectPuzzle.Location = new System.Drawing.Point(7, 79);
            this.lSelectPuzzle.Name = "lSelectPuzzle";
            this.lSelectPuzzle.Size = new System.Drawing.Size(137, 25);
            this.lSelectPuzzle.TabIndex = 3;
            this.lSelectPuzzle.Text = "Select a Puzzle";
            // 
            // lTitle
            // 
            this.lTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lTitle.AutoSize = true;
            this.lTitle.BackColor = System.Drawing.Color.Transparent;
            this.lTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lTitle.Location = new System.Drawing.Point(1, 0);
            this.lTitle.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.lTitle.MaximumSize = new System.Drawing.Size(500, 30);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(48, 25);
            this.lTitle.TabIndex = 4;
            this.lTitle.Text = "Title";
            // 
            // lIntro
            // 
            this.lIntro.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lIntro.AutoSize = true;
            this.lIntro.BackColor = System.Drawing.SystemColors.InfoText;
            this.lIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lIntro.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lIntro.Location = new System.Drawing.Point(5, 35);
            this.lIntro.Margin = new System.Windows.Forms.Padding(5, 10, 3, 10);
            this.lIntro.MaximumSize = new System.Drawing.Size(500, 300);
            this.lIntro.Name = "lIntro";
            this.lIntro.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lIntro.Size = new System.Drawing.Size(30, 18);
            this.lIntro.TabIndex = 5;
            this.lIntro.Text = "Title";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.lTitle);
            this.flowLayoutPanel1.Controls.Add(this.lIntro);
            this.flowLayoutPanel1.Controls.Add(this.btnBegin);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(311, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(576, 426);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btnBegin
            // 
            this.btnBegin.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnBegin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBegin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBegin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnBegin.Location = new System.Drawing.Point(3, 66);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(130, 40);
            this.btnBegin.TabIndex = 1;
            this.btnBegin.Text = "Begin";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnMainMenu.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange;
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
            // frmPuzzles
            // 
            this.BackgroundImage = global::CleverZebra.Properties.Resources.CircleSparks;
            this.ClientSize = new System.Drawing.Size(784, 582);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lSelectPuzzle);
            this.Controls.Add(this.lbPuzzles);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "frmPuzzles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clever Zebra";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private List<string> getPuzzlesList() {
            List<string> puzzles = new List<string> {"puzzle1, puzzle2"};
            return puzzles;
        }

        private void btnMainMenu_Click(object sender, EventArgs e) {
           this.Owner.Show();
           this.FormClosing -= frmPuzzles_FormClosing;
           this.Close();
        }

        private void lbPuzzles_SelectedIndexChanged(object sender, EventArgs e) {
            loadPuzzle(lbPuzzles.SelectedIndex);
        }

        private void loadPuzzle(int p) {
            Controller.getInstance().setActivePuzzle(p);
            lTitle.Text = lbPuzzles.SelectedItem.ToString();
            lIntro.Text = Controller.getInstance().getPreamble();
        }

        private void btnBegin_Click(object sender, EventArgs e) {
            this.Hide();
            frmSolver solver = new frmSolver();
            this.AddOwnedForm(solver);
            solver.Show();
        }

    }
}
