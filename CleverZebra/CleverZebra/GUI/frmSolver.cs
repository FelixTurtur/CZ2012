using Representation;
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
    public class frmSolver : Form
    {
        private Resources.CZButton btnBack;
        private FlowLayoutPanel flpText;
        private Label lTitle;
        private Label lGridTitle;
        private FlowLayoutPanel flpSolution;
        private GroupBox gbClues;
        private FlowLayoutPanel flpClues;
        private DataGridView dgvSolution;
        private Resources.CZButton btnSolve;
        private FlowLayoutPanel flpResults;
        private ProgressBar pbSolving;
        private DataTable data;

        #region Constructor and Initialise
        public frmSolver() {
            InitializeComponent();
            setupPuzzle();
        }

        internal void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flpText = new System.Windows.Forms.FlowLayoutPanel();
            this.lTitle = new System.Windows.Forms.Label();
            this.gbClues = new System.Windows.Forms.GroupBox();
            this.flpClues = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSolution = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvSolution = new System.Windows.Forms.DataGridView();
            this.flpResults = new System.Windows.Forms.FlowLayoutPanel();
            this.pbSolving = new System.Windows.Forms.ProgressBar();
            this.btnSolve = new CleverZebra.Resources.CZButton();
            this.btnBack = new CleverZebra.Resources.CZButton();
            this.flpText.SuspendLayout();
            this.gbClues.SuspendLayout();
            this.flpSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolution)).BeginInit();
            this.SuspendLayout();
            // 
            // flpText
            // 
            this.flpText.BackColor = System.Drawing.Color.Transparent;
            this.flpText.Controls.Add(this.lTitle);
            this.flpText.Controls.Add(this.gbClues);
            this.flpText.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpText.Location = new System.Drawing.Point(12, 67);
            this.flpText.MaximumSize = new System.Drawing.Size(350, 450);
            this.flpText.Name = "flpText";
            this.flpText.Size = new System.Drawing.Size(350, 450);
            this.flpText.TabIndex = 2;
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.ForeColor = System.Drawing.Color.DarkOrange;
            this.lTitle.Location = new System.Drawing.Point(3, 0);
            this.lTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lTitle.MaximumSize = new System.Drawing.Size(330, 30);
            this.lTitle.MinimumSize = new System.Drawing.Size(330, 0);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(330, 25);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "Select";
            // 
            // gbClues
            // 
            this.gbClues.AutoSize = true;
            this.gbClues.BackColor = System.Drawing.Color.Black;
            this.gbClues.Controls.Add(this.flpClues);
            this.gbClues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbClues.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbClues.ForeColor = System.Drawing.Color.DarkOrange;
            this.gbClues.Location = new System.Drawing.Point(3, 33);
            this.gbClues.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.gbClues.MaximumSize = new System.Drawing.Size(330, 400);
            this.gbClues.MinimumSize = new System.Drawing.Size(330, 100);
            this.gbClues.Name = "gbClues";
            this.gbClues.Size = new System.Drawing.Size(330, 100);
            this.gbClues.TabIndex = 2;
            this.gbClues.TabStop = false;
            this.gbClues.Text = "Clues";
            // 
            // flpClues
            // 
            this.flpClues.AutoSize = true;
            this.flpClues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpClues.Location = new System.Drawing.Point(3, 18);
            this.flpClues.MaximumSize = new System.Drawing.Size(320, 380);
            this.flpClues.Name = "flpClues";
            this.flpClues.Size = new System.Drawing.Size(300, 0);
            this.flpClues.TabIndex = 0;
            // 
            // flpSolution
            // 
            this.flpSolution.BackColor = System.Drawing.Color.Transparent;
            this.flpSolution.Controls.Add(this.dgvSolution);
            this.flpSolution.Location = new System.Drawing.Point(383, 12);
            this.flpSolution.Name = "flpSolution";
            this.flpSolution.Size = new System.Drawing.Size(539, 187);
            this.flpSolution.TabIndex = 3;
            // 
            // dgvSolution
            // 
            this.dgvSolution.AllowUserToAddRows = false;
            this.dgvSolution.AllowUserToDeleteRows = false;
            this.dgvSolution.AllowUserToResizeRows = false;
            this.dgvSolution.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSolution.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvSolution.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSolution.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSolution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolution.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSolution.EnableHeadersVisualStyles = false;
            this.dgvSolution.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.Location = new System.Drawing.Point(0, 0);
            this.dgvSolution.Margin = new System.Windows.Forms.Padding(0);
            this.dgvSolution.Name = "dgvSolution";
            this.dgvSolution.ReadOnly = true;
            this.dgvSolution.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSolution.RowHeadersVisible = false;
            this.dgvSolution.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSolution.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
            this.dgvSolution.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DarkOrange;
            this.dgvSolution.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSolution.RowTemplate.ReadOnly = true;
            this.dgvSolution.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSolution.ShowCellErrors = false;
            this.dgvSolution.ShowCellToolTips = false;
            this.dgvSolution.ShowEditingIcon = false;
            this.dgvSolution.ShowRowErrors = false;
            this.dgvSolution.Size = new System.Drawing.Size(536, 90);
            this.dgvSolution.StandardTab = true;
            this.dgvSolution.TabIndex = 4;
            this.dgvSolution.TabStop = false;
            this.dgvSolution.Visible = false;
            // 
            // flpResults
            // 
            this.flpResults.BackColor = System.Drawing.Color.Transparent;
            this.flpResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpResults.Location = new System.Drawing.Point(383, 230);
            this.flpResults.Name = "flpResults";
            this.flpResults.Size = new System.Drawing.Size(539, 287);
            this.flpResults.TabIndex = 4;
            this.flpResults.Visible = false;
            // 
            // pbSolving
            // 
            this.pbSolving.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pbSolving.ForeColor = System.Drawing.Color.DarkOrange;
            this.pbSolving.Location = new System.Drawing.Point(383, 105);
            this.pbSolving.Name = "pbSolving";
            this.pbSolving.Size = new System.Drawing.Size(100, 23);
            this.pbSolving.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSolving.TabIndex = 5;
            this.pbSolving.Visible = false;
            // 
            // btnSolve
            // 
            this.btnSolve.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnSolve.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolve.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnSolve.Location = new System.Drawing.Point(233, 12);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(130, 40);
            this.btnSolve.TabIndex = 2;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(130, 40);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CleverZebra.Properties.Resources.CircleSparks;
            this.ClientSize = new System.Drawing.Size(934, 527);
            this.Controls.Add(this.pbSolving);
            this.Controls.Add(this.flpResults);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.flpSolution);
            this.Controls.Add(this.flpText);
            this.Controls.Add(this.btnBack);
            this.Name = "frmSolver";
            this.Text = "frmSolver";
            this.flpText.ResumeLayout(false);
            this.flpText.PerformLayout();
            this.gbClues.ResumeLayout(false);
            this.gbClues.PerformLayout();
            this.flpSolution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolution)).EndInit();
            this.ResumeLayout(false);

        }

        void setupPuzzle() {
            this.flpClues.SuspendLayout();
            this.SuspendLayout();
            this.lTitle.Text = Controller.getInstance().getActiveTitle();
            List<string> clues = Controller.getInstance().getActiveClues();
            foreach (string clue in clues) {
                Label lClue = makeCZClue();
                lClue.Text = clue;
                lClue.Margin = new Padding(0, 10, 0, 0);
                this.flpClues.Controls.Add(lClue);
            }
            this.flpClues.ResumeLayout(false);
            this.flpClues.PerformLayout();
            this.ResumeLayout(false);
        }

        private Label makeCZClue() {
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.BackColor = System.Drawing.Color.Black;
            newLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newLabel.ForeColor = System.Drawing.Color.DarkOrange;
            newLabel.Location = new System.Drawing.Point(3, 42);
            newLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            newLabel.Size = new System.Drawing.Size(30, 13);
            newLabel.TabIndex = 1;
            return newLabel;
        }
        #endregion
        #region Windows Form Designer generated code
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        private void btnBack_Click(object sender, EventArgs e) {
            this.Owner.Show();
            this.Close();
        }

        private void btnSolve_Click(object sender, EventArgs e) {
            Controller.getInstance().Updater += controller_update;
            SetupSolutionArea();
            try {
                Controller.getInstance().Solve();
            }
            catch (CZParser.ParserException p) {
                //announce Parser error
                pbSolving.Hide();
                pbSolving.Value = 0;
            }
            catch (Logix.LogicException l) {
                //announc Logix error
                pbSolving.Hide();
                pbSolving.Value = 0;
            }
            catch (Exception ex) {
                //unexpected error
                pbSolving.Hide();
                pbSolving.Value = 0;
            }
        }

        private void SetupSolutionArea() {
            lGridTitle = new Label();
            lGridTitle = makeTitleLabel();
            lGridTitle.Text = "Solution";
            flpSolution.Controls.Add(lGridTitle);
            data = new DataTable();
            int height = Controller.getInstance().getActiveHeight();
            int width = Controller.getInstance().getActiveWidth();
            if (dgvSolution.Columns.Count == 0) {
                List<string> catTitles = Controller.getInstance().getCategoryTitles();
                for (int i = 0; i < catTitles.Count; i++) {
                    data.Columns.Add(catTitles[i]);
                }
            }
            object[] rowItems = new object[width];
            for (int i = 0; i < height;i++ ) {
                for (int j = 0; j < width; j++) {
                    rowItems[j] = "?";
                }
                data.Rows.Add(rowItems);
            }

            dgvSolution.DataSource = data;
            dgvSolution.Height = (int)Math.Floor(24.4 * (height + 1));
            dgvSolution.Visible = true;
            flpSolution.Controls.Add(dgvSolution);
            pbSolving.Step = 100 / (height * width);
            pbSolving.Show();
        }

        private Label makeTitleLabel() {
            Label title = new Label();
            title.AutoSize = false;
            title.BackColor = System.Drawing.Color.Transparent;
            title.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title.ForeColor = System.Drawing.Color.DarkOrange;
            title.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            title.MinimumSize = new System.Drawing.Size(this.dgvSolution.Width, 0);
            title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            return title;
        }


        internal void controller_update(Controller c, SolutionBoxEventArgs e) {
            //show Update
            this.dgvSolution.Rows[e.line].Cells[e.catIndex].Value = e.item;
            this.pbSolving.PerformStep();
        }

        private void czButton1_Click(object sender, EventArgs e) {
            dgvSolution.Rows[0].Cells[2].Value = "Crunchy";
            dgvSolution.UpdateCellValue(2,0);
        }

        private void czButton2_Click(object sender, EventArgs e) {
            dgvSolution.Rows[1].Cells[2].Value = "Crispy";
            dgvSolution.UpdateCellValue(2, 0);
        }
    }
}
