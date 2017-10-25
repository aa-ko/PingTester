namespace PingTester
{
    partial class Form1
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
            this.btnActive = new System.Windows.Forms.Button();
            this.lblCurrentPing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnActive
            // 
            this.btnActive.BackColor = System.Drawing.SystemColors.Control;
            this.btnActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActive.Location = new System.Drawing.Point(12, 219);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(87, 68);
            this.btnActive.TabIndex = 0;
            this.btnActive.Text = "Aktiv";
            this.btnActive.UseVisualStyleBackColor = false;
            this.btnActive.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCurrentPing
            // 
            this.lblCurrentPing.AutoSize = true;
            this.lblCurrentPing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentPing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPing.Location = new System.Drawing.Point(155, 243);
            this.lblCurrentPing.Name = "lblCurrentPing";
            this.lblCurrentPing.Size = new System.Drawing.Size(59, 22);
            this.lblCurrentPing.TabIndex = 1;
            this.lblCurrentPing.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 299);
            this.Controls.Add(this.lblCurrentPing);
            this.Controls.Add(this.btnActive);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.Label lblCurrentPing;
    }
}

