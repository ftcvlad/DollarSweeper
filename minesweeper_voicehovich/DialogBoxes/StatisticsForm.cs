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
    public partial class StatisticsForm : Form {
        serializableArrays data;
        public StatisticsForm(serializableArrays data) {
            this.data = data;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            this.CenterToScreen();
        }

        private void StatisticsForm_Load(object sender, EventArgs e) {

           
            for (int i = 1; i < 11; i++) {
                Label[] allLabels = { new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label() };

                allLabels[0].Text = i + "";
                allLabels[1].Text = data.begBestScores[i - 1].Item2 + "";
                allLabels[2].Text = data.begBestScores[i - 1].Item1 + "";
                allLabels[3].Text = data.interBestScores[i - 1].Item2 + "";
                allLabels[4].Text = data.interBestScores[i - 1].Item1 + "";
                allLabels[5].Text = data.advBestScores[i - 1].Item2 + "";
                allLabels[6].Text = data.advBestScores[i - 1].Item1 + "";

                for (int j = 0; j < 7; j++) {
                    allLabels[j].Anchor = AnchorStyles.None;
                    allLabels[j].AutoSize = true;
                    scoreTable.Controls.Add(allLabels[j], j, i);
                }

            }

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            this.Dispose();
        }
    }
}
