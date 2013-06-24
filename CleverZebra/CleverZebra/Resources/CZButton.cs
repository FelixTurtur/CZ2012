using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Resources
{
    class CZButton : System.Windows.Forms.Button
    {
        public CZButton(string name)
        {
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "btn" + name.Replace(" ", "");
            this.Size = new System.Drawing.Size(130, 40);
            this.TabIndex = 1;
            this.Text = name;
            this.UseVisualStyleBackColor = true;
        }

        public CZButton()
        {
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Size = new System.Drawing.Size(130, 40);
            this.TabIndex = 1;
            this.UseVisualStyleBackColor = true;

        }
    }
}
