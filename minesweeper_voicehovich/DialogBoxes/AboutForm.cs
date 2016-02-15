using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper_voicehovich {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
            AboutLabel.MaximumSize = new Size(250,230);
            this.CenterToScreen();
        }

        private void AboutOkButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void AboutForm_Load(object sender, EventArgs e) {

        }
    }
}
