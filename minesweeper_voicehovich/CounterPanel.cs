using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using minesweeper_voicehovich.Properties;

namespace minesweeper_voicehovich {
    public class counterPanel : TableLayoutPanel {


        Bitmap[] pics = { Resources._zero, Resources._one,Resources._two, Resources._three, Resources._four, Resources._five,
                    Resources._six,Resources._seven, Resources._eight, Resources._nine};
        Bitmap _minus = Resources._minus;

        public counterPanel() {

            ColumnCount = 3;
            RowCount = 1;

            RowStyles.Add(new RowStyle(SizeType.Percent, 100F));


            for (int j = 0; j < 3; j++) {
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

                PictureBox p = new PictureBox();
                p.Image = pics[0];
                p.Margin = new Padding(0);
                p.Anchor = AnchorStyles.None;
                p.Size = p.Image.Size;
                Controls.Add(p);
            }

            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            this.Margin = new Padding(0);
        }

        public void setNumber(int num) {

            String str = num + "";
            if (num >= 0) {

                if (str.Length == 1) {
                    str = "00" + str;
                }
                else if (str.Length == 2) {
                    str = "0" + str;
                }

                for (int i = 0; i < 3; i++) {
                    int index = (int)Char.GetNumericValue(str[i]);
                    PictureBox nextPic = (PictureBox)this.GetControlFromPosition(i, 0);

                    nextPic.Image = pics[index];
                }
            }
            else {
                if (str.Length == 2) {
                    ((PictureBox)this.GetControlFromPosition(0, 0)).Image = pics[0];
                    ((PictureBox)this.GetControlFromPosition(1, 0)).Image = _minus;
                    ((PictureBox)this.GetControlFromPosition(2, 0)).Image = pics[(int)Char.GetNumericValue(str[1])];
                }
                else if (str.Length == 3) {
                    ((PictureBox)this.GetControlFromPosition(0, 0)).Image = _minus;
                    ((PictureBox)this.GetControlFromPosition(1, 0)).Image = pics[(int)Char.GetNumericValue(str[1])];
                    ((PictureBox)this.GetControlFromPosition(2, 0)).Image = pics[(int)Char.GetNumericValue(str[2])];
                }
            }

        }
    }
}