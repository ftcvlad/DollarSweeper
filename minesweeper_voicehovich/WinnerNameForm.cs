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
    public partial class WinnerNameForm : Form {
        Label aLittleCase=null;
        public WinnerNameForm(Label name) {
            aLittleCase = name;
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!textBox1.Text.Equals("")) {
                aLittleCase.Text = textBox1.Text;
                this.Dispose();
            }
        }

        private void WinnerNameForm_Load(object sender, EventArgs e) {

        }
    }
}
