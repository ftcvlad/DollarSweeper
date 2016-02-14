using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using minesweeper_voicehovich.Properties;//for resources



namespace minesweeper_voicehovich {
    public partial class MainForm : Form {
       

        Bitmap manOrd = Resources.man;
        Bitmap manSad = Resources.sad;
        Bitmap manHap = Resources.happy;
        Bitmap manExcited = Resources.excited;

   
        SweeperPanel tableLayoutPanel1;

        public counterPanel bombCounter = new counterPanel();
        public counterPanel timeCounter = new counterPanel();

        public serializableArrays bestScores;
        int cellSize = 17;
        int sideOffset = 50;
        int tableLayoutPanel2Height = 52;
        int titleHeight;



        public MainForm() {


            InitializeComponent();

            button1.Anchor = AnchorStyles.None;
            button1.FlatAppearance.BorderSize = 0;
            button1.Click += new EventHandler(newGame);
            button1.Size = new Size(60, 60);

            tableLayoutPanel2.Location = new System.Drawing.Point(50, 50);
            tableLayoutPanel2.Controls.Add(bombCounter);
            tableLayoutPanel2.Controls.Add(button1);
            tableLayoutPanel2.Controls.Add(timeCounter);

            dollarsCollectedLabel.Text = "$ 0";
            Controls.Add(dollarsCollectedLabel);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.MouseMove += new MouseEventHandler(mouseMfMove);


            //http://stackoverflow.com/questions/2022660/how-to-get-the-size-of-a-winforms-form-titlebar-height
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            titleHeight = screenRectangle.Top - this.Top;

            tableLayoutPanel1 = new SweeperPanel(this, cellSize);
            this.Controls.Add(tableLayoutPanel1);

            initializeMfFlexiblecomponents(9, 9, 10, "beginner");

            bestScores = serializableArrays.Load();
        }

        
        //left grid while unpressed
        private void mouseMfMove(object sender, MouseEventArgs e) {
            if (tableLayoutPanel1.previousCell != null) {
                if (tableLayoutPanel1.grid[tableLayoutPanel1.previousCell.Item1][tableLayoutPanel1.previousCell.Item2] < 100 &&
                    tableLayoutPanel1.grid[tableLayoutPanel1.previousCell.Item1][tableLayoutPanel1.previousCell.Item2] >= 0) {//&& previousCursorOver.Enabled == true
                    tableLayoutPanel1.Invalidate(new Rectangle(tableLayoutPanel1.previousCell.Item2 * cellSize, tableLayoutPanel1.previousCell.Item1 * cellSize, cellSize, cellSize));
                }
                tableLayoutPanel1.previousCell = null;
            }
            return;


        }


        public void setDollarsLabel(string text) {
            dollarsCollectedLabel.Text = text;
        }

        public void updateButton(string look) {
            if (look.Equals("ordinary")) {
                button1.Image = manOrd;
            }
            else if (look.Equals("lost")) {
                button1.Image = manSad;
            }
            else if (look.Equals("excited")) {
                button1.Image = manExcited;
            }
            else if (look.Equals("won")) {
                button1.Image = manHap;
            }
        }


        private void initializeMfFlexiblecomponents(int rows, int cols, int nOfBombs, string difficulty) {

            button1.Image = manOrd;

            int tableLayoutPanel2Width = Math.Max(270, cols * cellSize);
            tableLayoutPanel2.Size = new Size(tableLayoutPanel2Width, tableLayoutPanel2Height);
            Size = new Size(tableLayoutPanel2Width + sideOffset * 2 + 7 * 2, sideOffset * 2 + Math.Max(rows * cellSize, 160) + 20 + tableLayoutPanel2Height + titleHeight);

            bombCounter.setNumber(nOfBombs);
          
            dollarsCollectedLabel.Location = new System.Drawing.Point(sideOffset, sideOffset + Math.Max(rows * cellSize, 160) + 20 + tableLayoutPanel2Height + titleHeight - 20);


            //------------tableLayoutPanel1 ------------
            
            this.tableLayoutPanel1.Location = new System.Drawing.Point(sideOffset, 120);
            tableLayoutPanel1.Size = new Size(cols * cellSize, rows * cellSize);
            tableLayoutPanel1.initializeSweeperPanel(rows, cols, nOfBombs, difficulty);
           

        }

       

       

     









        private void newGame(object sender, EventArgs e) {
            button1.Image = manOrd;
            tableLayoutPanel1.startGame(false);
        }
        
       
        private void Form1_Load(object sender, EventArgs e) {
            tableLayoutPanel1.startGame(true);
        }

        
        public  void createNewSizeGrid(int width, int height, int mines, string difficulty) {

            initializeMfFlexiblecomponents(height, width, mines, difficulty);//change width and height order, as I screwed i,j order
            tableLayoutPanel1.startGame(true);
        }



        private void button1_Click(object sender, EventArgs e) {

        }

        private void beginner9x910MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            
            createNewSizeGrid(9, 9, 10, "beginner");
        }

        private void intermediate16x1640MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            
            createNewSizeGrid(16, 16, 40, "intermediate");
        }

        private void advanced30x2499MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            
            createNewSizeGrid(30, 16, 99, "advanced");
        }

        private void customSizeToolStripMenuItem_Click(object sender, EventArgs e) {

            CustomSizeForm form = new CustomSizeForm(this);
            form.ShowDialog();
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e) {
            StatisticsForm form = new StatisticsForm(bestScores);
            form.ShowDialog();
        }

        private void setMoneyLostlivesToolStripMenuItem_Click(object sender, EventArgs e) {
            LossSelectorForm form = new LossSelectorForm(tableLayoutPanel1);
            form.ShowDialog();
        }

        private void aboutDollarSweeperToolStripMenuItem_Click(object sender, EventArgs e) {


            AboutForm form = new AboutForm();
            form.ShowDialog();
        }
    }
}


