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
        private System.Windows.Forms.FlowLayoutPanel flpClues;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.Label lPreamble;
        private System.Windows.Forms.Label lClue1;
        private System.Windows.Forms.FlowLayoutPanel flpSolution;
        private Resources.CZButton btnSolve;

        #region Constructor and Initialise
        public frmSolver() {
            InitializeComponent();
        }

        internal void InitializeComponent() {
            this.flpClues = new System.Windows.Forms.FlowLayoutPanel();
            this.lTitle = new System.Windows.Forms.Label();
            this.lPreamble = new System.Windows.Forms.Label();
            this.lClue1 = new System.Windows.Forms.Label();
            this.flpSolution = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSolve = new CleverZebra.Resources.CZButton();
            this.btnBack = new CleverZebra.Resources.CZButton();
            this.flpClues.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpClues
            // 
            this.flpClues.BackColor = System.Drawing.Color.Transparent;
            this.flpClues.Controls.Add(this.lTitle);
            this.flpClues.Controls.Add(this.lPreamble);
            this.flpClues.Controls.Add(this.lClue1);
            this.flpClues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpClues.Location = new System.Drawing.Point(12, 67);
            this.flpClues.Name = "flpClues";
            this.flpClues.Size = new System.Drawing.Size(351, 448);
            this.flpClues.TabIndex = 2;
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.ForeColor = System.Drawing.Color.DarkOrange;
            this.lTitle.Location = new System.Drawing.Point(3, 0);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(34, 19);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "Title";
            // 
            // lPreamble
            // 
            this.lPreamble.AutoSize = true;
            this.lPreamble.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPreamble.ForeColor = System.Drawing.Color.DarkOrange;
            this.lPreamble.Location = new System.Drawing.Point(3, 24);
            this.lPreamble.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lPreamble.Name = "lPreamble";
            this.lPreamble.Size = new System.Drawing.Size(54, 13);
            this.lPreamble.TabIndex = 1;
            this.lPreamble.Text = "Preamble";
            // 
            // lClue1
            // 
            this.lClue1.AutoSize = true;
            this.lClue1.BackColor = System.Drawing.Color.Black;
            this.lClue1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lClue1.ForeColor = System.Drawing.Color.DarkOrange;
            this.lClue1.Location = new System.Drawing.Point(3, 42);
            this.lClue1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lClue1.Name = "lClue1";
            this.lClue1.Size = new System.Drawing.Size(30, 13);
            this.lClue1.TabIndex = 1;
            this.lClue1.Text = "Clue";
            // 
            // flpSolution
            // 
            this.flpSolution.BackColor = System.Drawing.Color.Transparent;
            this.flpSolution.Location = new System.Drawing.Point(383, 12);
            this.flpSolution.Name = "flpSolution";
            this.flpSolution.Size = new System.Drawing.Size(539, 187);
            this.flpSolution.TabIndex = 3;
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
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.flpSolution);
            this.Controls.Add(this.flpClues);
            this.Controls.Add(this.btnBack);
            this.Name = "frmSolver";
            this.Text = "frmSolver";
            this.flpClues.ResumeLayout(false);
            this.flpClues.PerformLayout();
            this.ResumeLayout(false);

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

        }

    }
}
