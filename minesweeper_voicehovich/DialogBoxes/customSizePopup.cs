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
    public partial class CustomSizeForm : Form {
        
        MainForm mfRef;
        bool validInput = true;
        ToolTip hint = null;



        public CustomSizeForm( MainForm mfRef) {
           
            this.mfRef = mfRef;
           
            InitializeComponent();


            tbWidth.LostFocus += new EventHandler(TbWidth_LostFocus);
            tbHeight.LostFocus += new EventHandler(TbWidth_LostFocus);
            tbMines.LostFocus += new EventHandler(TbWidth_LostFocus);

            this.LocationChanged += new EventHandler(formChangedLoc);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.CenterToScreen();
        }

        private void formChangedLoc(object sender, EventArgs e) {
            if (hint != null) {
                hint.Dispose();
                hint = null;
            }
        }

        private void TbWidth_LostFocus(object sender, EventArgs e) {

          
            TextBox source = (TextBox)sender;
            validInput = true;
            if (hint != null) {
                hint.Dispose();
                hint = null;
            }
           
            if (source.Name.Equals("tbWidth")) {
                checkValidity( 60, source);
            }
            else if (source.Name.Equals("tbHeight")) {
                checkValidity( 40, source);
            }
            if (source.Name.Equals("tbMines")) {
                checkValidity( 400, source);
            }
        }

        private void checkValidity(int maxValue, TextBox source) {
            if (source.Text.Length == 0) {
                makeBalloon("You should provide value between 1 and "+ maxValue, source,"info");
                validInput = false;
                source.Text = source.Tag.ToString();
                return;
            }

            String currStr = source.Text;
            for (int i=0; i < currStr.Length; i++) {

                if (currStr[i] < '0' || currStr[i] > '9') {
                    makeBalloon("Only digits allowed!", source, "error");
                    validInput = false;
                    source.Text = source.Tag.ToString();
                    return;
                }      
            }

               
            

            int number = Int32.Parse(source.Text);
            if (number < 1 || number > maxValue) {
                makeBalloon("You should provide value between 1 and "+maxValue, source, "info");
                validInput = false;
                source.Text = source.Tag.ToString();
            }



        }

        private void makeBalloon(String str, TextBox source, String iconName) {
            hint = new ToolTip();
            hint.IsBalloon = true;

            hint.ToolTipTitle = "Incorrect input";
            if (iconName.Equals("info")) {
                hint.ToolTipIcon = ToolTipIcon.Info;
            }
            else {
                hint.ToolTipIcon = ToolTipIcon.Error;
            }
            
         
            //bug with ToolTip having top left corner at specified position (instead of arrow)
            //showing 1st baloon for 0 seconds sometimes solves
            hint.Show(String.Empty, source,0);
            hint.Show(str, source,40,10);
        }

        private void button1_Click(object sender, EventArgs e) {
         

            if (validInput) {

                


                int width = Int32.Parse(tbWidth.Text);
                int height = Int32.Parse(tbHeight.Text);
                int mines = Int32.Parse(tbMines.Text);

                //check if bombs can fit created field
                if (width * height < mines) {
                    if (hint == null) {
                      
                        makeBalloon("Specified area cannot hold that many mines!", tbMines, "info");
                    }
                   
                    return;
                }

                string diffic;
                if (width==9 && height==9 && mines == 10) {
                    diffic = "beginner";
                }
                else if (width == 16 && height == 16 && mines == 40) {
                    diffic = "intermediate";
                }
                else if (width == 30 && height == 16 && mines == 99) {
                    diffic = "intermediate";
                }
                else {
                    diffic = "custom";//do nothing with best scores
                }

                mfRef.createNewSizeGrid(width, height, mines, diffic);
                this.Dispose();
            }
            else {
                validInput = true;

            } 

          
            

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Dispose();
           
        }

        private void tbWidth_TextChanged(object sender, EventArgs e) {
           
        }

        private void CustomSize_Load(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }
    }
}
