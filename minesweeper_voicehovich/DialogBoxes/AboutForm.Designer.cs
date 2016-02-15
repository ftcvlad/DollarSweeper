namespace minesweeper_voicehovich {
    partial class AboutForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.AboutLabel = new System.Windows.Forms.Label();
            this.AboutOkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AboutLabel
            // 
            this.AboutLabel.AutoSize = true;
            this.AboutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutLabel.Location = new System.Drawing.Point(12, 9);
            this.AboutLabel.Name = "AboutLabel";
            this.AboutLabel.Size = new System.Drawing.Size(2820, 16);
            this.AboutLabel.TabIndex = 0;
            this.AboutLabel.Text = resources.GetString("AboutLabel.Text");
            // 
            // AboutOkButton
            // 
            this.AboutOkButton.Location = new System.Drawing.Point(96, 226);
            this.AboutOkButton.Name = "AboutOkButton";
            this.AboutOkButton.Size = new System.Drawing.Size(80, 32);
            this.AboutOkButton.TabIndex = 1;
            this.AboutOkButton.Text = "Ok";
            this.AboutOkButton.UseVisualStyleBackColor = true;
            this.AboutOkButton.Click += new System.EventHandler(this.AboutOkButton_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 270);
            this.Controls.Add(this.AboutOkButton);
            this.Controls.Add(this.AboutLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.Text = "About DollarSweeper";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AboutLabel;
        private System.Windows.Forms.Button AboutOkButton;
    }
}