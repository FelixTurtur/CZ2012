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
        private ProgressBar pbSolving;
        private GroupBox gbStats;
        private Label lbStats;
        private FlowLayoutPanel flpStats;
        private FlowLayoutPanel frameRelations;
        private GroupBox gbRelations;
        private FlowLayoutPanel flpRelations;
        private DataGridView dgvCategories;
        private FlowLayoutPanel flpCategories;
        private Label lbCategories;
        private Label lbSolution;
        private DataTable data;

        #region Constructor and Initialise
        public frmSolver() {
            InitializeComponent();
            setupPuzzle();
            this.FormClosing += frmSolver_FormClosing;
        }

        void frmSolver_FormClosing(object sender, FormClosingEventArgs e) {
            this.FormClosing -= frmSolver_FormClosing;
            this.Owner.Show();
        }

        internal void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flpText = new System.Windows.Forms.FlowLayoutPanel();
            this.lTitle = new System.Windows.Forms.Label();
            this.gbClues = new System.Windows.Forms.GroupBox();
            this.flpClues = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSolution = new System.Windows.Forms.FlowLayoutPanel();
            this.lbSolution = new System.Windows.Forms.Label();
            this.dgvSolution = new System.Windows.Forms.DataGridView();
            this.pbSolving = new System.Windows.Forms.ProgressBar();
            this.gbStats = new System.Windows.Forms.GroupBox();
            this.lbStats = new System.Windows.Forms.Label();
            this.flpStats = new System.Windows.Forms.FlowLayoutPanel();
            this.frameRelations = new System.Windows.Forms.FlowLayoutPanel();
            this.gbRelations = new System.Windows.Forms.GroupBox();
            this.flpRelations = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.flpCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.lbCategories = new System.Windows.Forms.Label();
            this.btnSolve = new CleverZebra.Resources.CZButton();
            this.btnBack = new CleverZebra.Resources.CZButton();
            this.flpText.SuspendLayout();
            this.gbClues.SuspendLayout();
            this.flpSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolution)).BeginInit();
            this.gbStats.SuspendLayout();
            this.frameRelations.SuspendLayout();
            this.gbRelations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.flpCategories.SuspendLayout();
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
            this.lTitle.Text = "Title";
            // 
            // gbClues
            // 
            this.gbClues.AutoSize = true;
            this.gbClues.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.flpSolution.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flpSolution.BackColor = System.Drawing.Color.Transparent;
            this.flpSolution.Controls.Add(this.lbSolution);
            this.flpSolution.Controls.Add(this.dgvSolution);
            this.flpSolution.Controls.Add(this.pbSolving);
            this.flpSolution.Location = new System.Drawing.Point(380, 248);
            this.flpSolution.Name = "flpSolution";
            this.flpSolution.Size = new System.Drawing.Size(539, 269);
            this.flpSolution.TabIndex = 3;
            // 
            // lbSolution
            // 
            this.lbSolution.AutoSize = true;
            this.lbSolution.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSolution.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbSolution.Location = new System.Drawing.Point(3, 0);
            this.lbSolution.Name = "lbSolution";
            this.lbSolution.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lbSolution.Size = new System.Drawing.Size(82, 30);
            this.lbSolution.TabIndex = 6;
            this.lbSolution.Text = "Solution";
            this.lbSolution.Visible = false;
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
            this.dgvSolution.EnableHeadersVisualStyles = false;
            this.dgvSolution.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.Location = new System.Drawing.Point(0, 30);
            this.dgvSolution.Margin = new System.Windows.Forms.Padding(0);
            this.dgvSolution.MultiSelect = false;
            this.dgvSolution.Name = "dgvSolution";
            this.dgvSolution.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSolution.RowHeadersVisible = false;
            this.dgvSolution.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSolution.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Orange;
            this.dgvSolution.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvSolution.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Orange;
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
            // pbSolving
            // 
            this.pbSolving.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pbSolving.ForeColor = System.Drawing.Color.DarkOrange;
            this.pbSolving.Location = new System.Drawing.Point(3, 123);
            this.pbSolving.Name = "pbSolving";
            this.pbSolving.Size = new System.Drawing.Size(100, 23);
            this.pbSolving.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSolving.TabIndex = 5;
            this.pbSolving.Visible = false;
            // 
            // gbStats
            // 
            this.gbStats.BackColor = System.Drawing.Color.Black;
            this.gbStats.Controls.Add(this.lbStats);
            this.gbStats.Controls.Add(this.flpStats);
            this.gbStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbStats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStats.ForeColor = System.Drawing.Color.DarkOrange;
            this.gbStats.Location = new System.Drawing.Point(3, 233);
            this.gbStats.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.gbStats.MaximumSize = new System.Drawing.Size(270, 280);
            this.gbStats.MinimumSize = new System.Drawing.Size(245, 100);
            this.gbStats.Name = "gbStats";
            this.gbStats.Size = new System.Drawing.Size(247, 108);
            this.gbStats.TabIndex = 7;
            this.gbStats.TabStop = false;
            this.gbStats.Text = "Stats";
            this.gbStats.Visible = false;
            // 
            // lbStats
            // 
            this.lbStats.AutoSize = true;
            this.lbStats.Location = new System.Drawing.Point(7, 19);
            this.lbStats.Name = "lbStats";
            this.lbStats.Size = new System.Drawing.Size(143, 15);
            this.lbStats.TabIndex = 2;
            this.lbStats.Text = "Puzzle solved successfully";
            // 
            // flpStats
            // 
            this.flpStats.AutoSize = true;
            this.flpStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpStats.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpStats.Location = new System.Drawing.Point(3, 19);
            this.flpStats.MaximumSize = new System.Drawing.Size(250, 280);
            this.flpStats.Name = "flpStats";
            this.flpStats.Size = new System.Drawing.Size(241, 0);
            this.flpStats.TabIndex = 1;
            // 
            // frameRelations
            // 
            this.frameRelations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.frameRelations.BackColor = System.Drawing.Color.Transparent;
            this.frameRelations.Controls.Add(this.gbRelations);
            this.frameRelations.Controls.Add(this.gbStats);
            this.frameRelations.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.frameRelations.Location = new System.Drawing.Point(925, 12);
            this.frameRelations.Name = "frameRelations";
            this.frameRelations.Size = new System.Drawing.Size(250, 647);
            this.frameRelations.TabIndex = 5;
            this.frameRelations.Visible = false;
            // 
            // gbRelations
            // 
            this.gbRelations.AutoSize = true;
            this.gbRelations.BackColor = System.Drawing.Color.Black;
            this.gbRelations.Controls.Add(this.flpRelations);
            this.gbRelations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbRelations.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRelations.ForeColor = System.Drawing.Color.DarkOrange;
            this.gbRelations.Location = new System.Drawing.Point(3, 5);
            this.gbRelations.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.gbRelations.MaximumSize = new System.Drawing.Size(270, 350);
            this.gbRelations.MinimumSize = new System.Drawing.Size(245, 220);
            this.gbRelations.Name = "gbRelations";
            this.gbRelations.Size = new System.Drawing.Size(245, 220);
            this.gbRelations.TabIndex = 3;
            this.gbRelations.TabStop = false;
            this.gbRelations.Text = "Relations Found";
            this.gbRelations.Visible = false;
            // 
            // flpRelations
            // 
            this.flpRelations.AutoSize = true;
            this.flpRelations.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpRelations.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRelations.Location = new System.Drawing.Point(3, 19);
            this.flpRelations.MaximumSize = new System.Drawing.Size(320, 380);
            this.flpRelations.Name = "flpRelations";
            this.flpRelations.Size = new System.Drawing.Size(239, 0);
            this.flpRelations.TabIndex = 6;
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.AllowUserToResizeRows = false;
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvCategories.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCategories.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.EnableHeadersVisualStyles = false;
            this.dgvCategories.Location = new System.Drawing.Point(0, 30);
            this.dgvCategories.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCategories.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.RowHeadersWidth = 60;
            this.dgvCategories.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvCategories.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvCategories.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Orange;
            this.dgvCategories.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvCategories.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Orange;
            this.dgvCategories.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCategories.RowTemplate.DividerHeight = 1;
            this.dgvCategories.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCategories.ShowCellErrors = false;
            this.dgvCategories.ShowCellToolTips = false;
            this.dgvCategories.ShowEditingIcon = false;
            this.dgvCategories.ShowRowErrors = false;
            this.dgvCategories.Size = new System.Drawing.Size(536, 138);
            this.dgvCategories.StandardTab = true;
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.TabStop = false;
            // 
            // flpCategories
            // 
            this.flpCategories.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flpCategories.BackColor = System.Drawing.Color.Transparent;
            this.flpCategories.Controls.Add(this.lbCategories);
            this.flpCategories.Controls.Add(this.dgvCategories);
            this.flpCategories.Location = new System.Drawing.Point(380, 12);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Size = new System.Drawing.Size(539, 230);
            this.flpCategories.TabIndex = 7;
            // 
            // lbCategories
            // 
            this.lbCategories.AutoSize = true;
            this.lbCategories.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategories.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbCategories.Location = new System.Drawing.Point(3, 0);
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lbCategories.Size = new System.Drawing.Size(102, 30);
            this.lbCategories.TabIndex = 1;
            this.lbCategories.Text = "Categories";
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
            this.ClientSize = new System.Drawing.Size(1184, 750);
            this.Controls.Add(this.flpCategories);
            this.Controls.Add(this.frameRelations);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.flpSolution);
            this.Controls.Add(this.flpText);
            this.Controls.Add(this.btnBack);
            this.Name = "frmSolver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSolver";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.flpText.ResumeLayout(false);
            this.flpText.PerformLayout();
            this.gbClues.ResumeLayout(false);
            this.gbClues.PerformLayout();
            this.flpSolution.ResumeLayout(false);
            this.flpSolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolution)).EndInit();
            this.gbStats.ResumeLayout(false);
            this.gbStats.PerformLayout();
            this.frameRelations.ResumeLayout(false);
            this.frameRelations.PerformLayout();
            this.gbRelations.ResumeLayout(false);
            this.gbRelations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.flpCategories.ResumeLayout(false);
            this.flpCategories.PerformLayout();
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
            showCategories();
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
            Controller.getInstance().Updater -= controller_update;
            Controller.getInstance().Completer -= puzzle_complete;
            this.FormClosing -= frmSolver_FormClosing;
            this.Dispose();
        }

        private void btnSolve_Click(object sender, EventArgs e) {
            Controller.getInstance().Completer = puzzle_complete;
            Controller.getInstance().Updater = controller_update;
            SetupSolutionArea();
            try {
                List<string> relations = Controller.getInstance().ParseClues();
                if (Controller.getInstance().reportRelations) 
                    displayRelations(relations);
            }
            catch (Exception p) {
                displayParserError(p);
                return;
            }
            try {
                Controller.getInstance().SolveProblem();
            }
            catch (Exception l) {
                displayLogixError(l);
            }
        }

        private void displayLogixError(Exception l) {
            this.lbStats.Text = "Puzzle solution not found.\n";
            this.lbStats.Text += l.Message;
            this.lbStats.Text += "\n";
            this.lbStats.Text += l.InnerException == null ? "" : l.InnerException.Message;
            this.gbStats.Visible = true;
            pbSolving.Hide();
            pbSolving.Value = 0;
        }

        private void displayParserError(Exception p) {
            var tbRelations = makeRelationsBox();
            tbRelations.Text = "Unable to parse problem clues.\n";
            tbRelations.Text += p.Message;
            tbRelations.Text += "\n";
            tbRelations.Text += p.InnerException == null ? "" : p.InnerException.Message;
            tbRelations.ReadOnly = true;
            this.flpRelations.Controls.Add(tbRelations);
            this.frameRelations.Visible = true;
            this.gbRelations.Visible = true;
            pbSolving.Hide();
            pbSolving.Value = 0;
        }

        private RichTextBox makeRelationsBox() {
            var tbRelations = new RichTextBox();
            tbRelations.BackColor = System.Drawing.Color.Black;
            tbRelations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbRelations.ForeColor = System.Drawing.Color.DarkOrange;
            tbRelations.Location = new System.Drawing.Point(3, 3);
            tbRelations.MaximumSize = new System.Drawing.Size(250, 250);
            tbRelations.Name = "tbRelations";
            tbRelations.ReadOnly = false;
            tbRelations.TabIndex = 0;
            tbRelations.Text = "";
            tbRelations.Width = 230;
            return tbRelations;
        }

        private void displayRelations(List<string> relations) {
            var tbRelations = makeRelationsBox();
            tbRelations.Lines = relations.ToArray();
            tbRelations.Height = tbRelations.Lines.Count() * 15;
            tbRelations.ResumeLayout();
            tbRelations.Visible = true;
            tbRelations.ReadOnly = true;
            this.flpRelations.Controls.Add(tbRelations);
            this.frameRelations.Visible = true;
            this.gbRelations.Visible = true;
        }

        private void SetupSolutionArea() {
            this.lbSolution.Visible = true;
            flpSolution.Controls.Add(this.lbSolution);
            int height = Controller.getInstance().getActiveHeight();
            int width = Controller.getInstance().getActiveWidth();
            SetupDataGrid(height, width);
            pbSolving.Step = 100 / (height * width);
            pbSolving.Show();
        }

        private void showCategories() {
            data = new DataTable();
            List<string> catTitles = Controller.getInstance().getCategoryTitles();
            List<string> items = Controller.getInstance().getCategoryItems();
            int width = catTitles.Count;
            int height = items.Count / width;            
            for (int i = 0; i <= height; i++) {
                if (i == 0) 
                    data.Columns.Add("Title");
                else
                    data.Columns.Add(i.ToString());
            }
            string[] rowItems = new string[height + 1];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j <= height; j++) {
                    if (j == 0)
                        rowItems[j] = catTitles[i];
                    else 
                        rowItems[j] = items[(i*height)+j-1];
                }
                data.Rows.Add(rowItems);
            }
            dgvCategories.DataSource = data;
            dgvCategories.Height = (int)Math.Floor(24.4 * (width + 1));
            dgvCategories.Visible = true;
            flpCategories.Controls.Add(dgvCategories);
        }

        private void SetupDataGrid(int height, int width) {
            data = new DataTable();
            List<string> catTitles = Controller.getInstance().getCategoryTitles();
            for (int i = 0; i < catTitles.Count; i++) {
                data.Columns.Add(catTitles[i]);
            }
            object[] rowItems = new object[width];
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    rowItems[j] = "?";
                }
                if (data.Rows.Count < i + 1) {
                    data.Rows.Add(rowItems);
                }
                else {
                    data.Rows[i].ItemArray = rowItems;
                }
            }
            dgvSolution.DataSource = data;
            dgvSolution.Height = (int)Math.Floor(24.4 * (height + 1));
            dgvSolution.Visible = true;
            flpSolution.Controls.Add(dgvSolution);
        }

        private void puzzle_complete(Controller c, SolutionReachedArgs e) {
            pbSolving.Hide();
            for (int r = 0; r < e.solution.Count; r++) {
                for (int i = 0; i < e.solution[r].Count; i++) {
                    dgvSolution.Rows[r].Cells[i].Value = e.solution[r][i];
                }
            }
            this.lbStats.Text = e.success ? "Puzzle successfully solved!" : "Puzzle solution not found.";
            if (e.success) {
                this.lbStats.Text += "\n";
                this.lbStats.Text += "Solution found in " + e.numTurns + " steps";
                this.lbStats.Text += "\n";
                this.lbStats.Text += "Time taken: " + e.solutionTime.TotalMilliseconds + "ms";
            }
            this.gbStats.Visible = true;
        }

        internal void controller_update(Controller c, SolutionBoxEventArgs e) {
            try {
                dgvSolution.Rows[e.line].Cells[e.catIndex].Value = e.item;
                dgvSolution.UpdateCellValue(e.catIndex, e.line);
                this.pbSolving.PerformStep();
            }
            catch (Exception ex) {
                throw new Exception("Controller Update failure", ex);
            }
        }

    }
}
