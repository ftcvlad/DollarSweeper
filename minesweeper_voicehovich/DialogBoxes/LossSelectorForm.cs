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
    public partial class LossSelectorForm : Form {
        SweeperPanel spr;
        public LossSelectorForm(SweeperPanel spr) {
            this.spr = spr;
            InitializeComponent();
            MessageLabel.MaximumSize = new Size(211, 0);
            this.ControlBox = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

        }

        private void LossOkButton_Click(object sender, EventArgs e) {


            var checkedButton = groupBox1.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);

            spr.moneyLostPerGirl = Int32.Parse(checkedButton.Tag.ToString());

            this.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }
    }
}
