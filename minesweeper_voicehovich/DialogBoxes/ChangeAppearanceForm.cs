using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using minesweeper_voicehovich.Properties;

namespace minesweeper_voicehovich {
    public partial class ChangeAppearanceForm : Form {


        Bitmap look0 = Resources.look0;
        Bitmap look1 = Resources.look1;
        Bitmap look2 = Resources.look2;

        Image grad0 = Resources.grad0;
        Image grad1 = Resources.grad1;
        Image grad2 = Resources.grad2;

        SweeperPanel tlpRef;

        

        public ChangeAppearanceForm(SweeperPanel tlpRef) {
            this.tlpRef = tlpRef;
            InitializeComponent();

            listView1.View = View.LargeIcon;
            listView1.MultiSelect = false;

            ListViewItem item1 = new ListViewItem("No foreground", 0);
            ListViewItem item2 = new ListViewItem("Red", 1);
            ListViewItem item3 = new ListViewItem("Copper", 2);
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });


            ImageList imageList = new ImageList();

            imageList.Images.Add(look0);
            imageList.Images.Add(look1);
            imageList.Images.Add(look2);

            imageList.ImageSize = new Size(85, 85);
            imageList.ColorDepth = ColorDepth.Depth24Bit;
            listView1.LargeImageList = imageList;

            this.MaximizeBox = false;
            this.Controls.Add(listView1);
             
            this.CenterToScreen();

        
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void okButton_Click(object sender, EventArgs e) {

            if (listView1.SelectedItems.Count != 0) {
                string itemSelected = listView1.SelectedItems[0].Text;
                if (itemSelected.Equals("No foreground")) {
                    tlpRef.changeForegroundFromDialog(grad0);
                }
                else if (itemSelected.Equals("Red")) {
                    tlpRef.changeForegroundFromDialog(grad1);
                }
                else if (itemSelected.Equals("Copper")) {
                    tlpRef.changeForegroundFromDialog(grad2);
                }
            }

            this.Dispose();

        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void ChangeAppearanceForm_Load(object sender, EventArgs e) {

        }


    }
}
