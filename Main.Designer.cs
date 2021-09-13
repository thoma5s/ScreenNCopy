using System.Drawing;

namespace Screen_N_Copy
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button = new System.Windows.Forms.Button();
            this.autocorrect = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Button
            // 
            this.Button.BackColor = System.Drawing.Color.Gray;
            this.Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button.FlatAppearance.BorderSize = 0;
            this.Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button.ForeColor = System.Drawing.Color.White;
            this.Button.Location = new System.Drawing.Point(0, 23);
            this.Button.Name = "Button";
            this.Button.Size = new System.Drawing.Size(101, 28);
            this.Button.TabIndex = 0;
            this.Button.Text = "Screen N Copy";
            this.Button.UseVisualStyleBackColor = false;
            this.Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // autocorrect
            // 
            this.autocorrect.AutoSize = true;
            this.autocorrect.Checked = true;
            this.autocorrect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autocorrect.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.autocorrect.ForeColor = System.Drawing.Color.DarkGray;
            this.autocorrect.Location = new System.Drawing.Point(9, 4);
            this.autocorrect.Name = "autocorrect";
            this.autocorrect.Size = new System.Drawing.Size(85, 16);
            this.autocorrect.TabIndex = 2;
            this.autocorrect.Text = "Auto-Correct";
            this.autocorrect.UseVisualStyleBackColor = true;
            this.autocorrect.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(101, 51);
            this.Controls.Add(this.Button);
            this.Controls.Add(this.autocorrect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Opacity = 0.9D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🍆🍑";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Close);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button;
        private System.Windows.Forms.CheckBox autocorrect;
    }
}

