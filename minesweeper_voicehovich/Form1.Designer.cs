namespace minesweeper_voicehovich {
    partial class MainForm {
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
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginner9x910MinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intermediate16x1640MinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advanced30x2499MinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setMoneyLostlivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutDollarSweeperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dollarsCollectedLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(197, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 45);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(50, 107);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(293, 36);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginner9x910MinesToolStripMenuItem,
            this.intermediate16x1640MinesToolStripMenuItem,
            this.advanced30x2499MinesToolStripMenuItem,
            this.customSizeToolStripMenuItem,
            this.toolStripSeparator1,
            this.statisticsToolStripMenuItem,
            this.toolStripSeparator2,
            this.setMoneyLostlivesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // beginner9x910MinesToolStripMenuItem
            // 
            this.beginner9x910MinesToolStripMenuItem.Name = "beginner9x910MinesToolStripMenuItem";
            this.beginner9x910MinesToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.beginner9x910MinesToolStripMenuItem.Text = "Beginner (9x9, 10 mines)";
            this.beginner9x910MinesToolStripMenuItem.Click += new System.EventHandler(this.beginner9x910MinesToolStripMenuItem_Click);
            // 
            // intermediate16x1640MinesToolStripMenuItem
            // 
            this.intermediate16x1640MinesToolStripMenuItem.Name = "intermediate16x1640MinesToolStripMenuItem";
            this.intermediate16x1640MinesToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.intermediate16x1640MinesToolStripMenuItem.Text = "Intermediate (16x16,40 mines)";
            this.intermediate16x1640MinesToolStripMenuItem.Click += new System.EventHandler(this.intermediate16x1640MinesToolStripMenuItem_Click);
            // 
            // advanced30x2499MinesToolStripMenuItem
            // 
            this.advanced30x2499MinesToolStripMenuItem.Name = "advanced30x2499MinesToolStripMenuItem";
            this.advanced30x2499MinesToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.advanced30x2499MinesToolStripMenuItem.Text = "Advanced (30x16, 99 mines)";
            this.advanced30x2499MinesToolStripMenuItem.Click += new System.EventHandler(this.advanced30x2499MinesToolStripMenuItem_Click);
            // 
            // customSizeToolStripMenuItem
            // 
            this.customSizeToolStripMenuItem.Name = "customSizeToolStripMenuItem";
            this.customSizeToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.customSizeToolStripMenuItem.Text = "CustomSize";
            this.customSizeToolStripMenuItem.Click += new System.EventHandler(this.customSizeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            this.statisticsToolStripMenuItem.Click += new System.EventHandler(this.statisticsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // setMoneyLostlivesToolStripMenuItem
            // 
            this.setMoneyLostlivesToolStripMenuItem.Name = "setMoneyLostlivesToolStripMenuItem";
            this.setMoneyLostlivesToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.setMoneyLostlivesToolStripMenuItem.Text = "Set money lost per girl";
            this.setMoneyLostlivesToolStripMenuItem.Click += new System.EventHandler(this.setMoneyLostlivesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutDollarSweeperToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutDollarSweeperToolStripMenuItem
            // 
            this.aboutDollarSweeperToolStripMenuItem.Name = "aboutDollarSweeperToolStripMenuItem";
            this.aboutDollarSweeperToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.aboutDollarSweeperToolStripMenuItem.Text = "About DollarSweeper";
            this.aboutDollarSweeperToolStripMenuItem.Click += new System.EventHandler(this.aboutDollarSweeperToolStripMenuItem_Click);
            // 
            // dollarsCollectedLabel
            // 
            this.dollarsCollectedLabel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dollarsCollectedLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dollarsCollectedLabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dollarsCollectedLabel.Location = new System.Drawing.Point(307, 247);
            this.dollarsCollectedLabel.Name = "dollarsCollectedLabel";
            this.dollarsCollectedLabel.Size = new System.Drawing.Size(99, 24);
            this.dollarsCollectedLabel.TabIndex = 7;
            this.dollarsCollectedLabel.Text = "$ 123";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 382);
            this.Controls.Add(this.dollarsCollectedLabel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DollarSweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem beginner9x910MinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advanced30x2499MinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intermediate16x1640MinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setMoneyLostlivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutDollarSweeperToolStripMenuItem;
        private System.Windows.Forms.Label dollarsCollectedLabel;
    }
}

