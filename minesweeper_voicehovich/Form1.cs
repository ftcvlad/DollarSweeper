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

            //   this.MouseMove += new MouseEventHandler(mouseTableAndFrameMove);


            //http://stackoverflow.com/questions/2022660/how-to-get-the-size-of-a-winforms-form-titlebar-height
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            titleHeight = screenRectangle.Top - this.Top;

            tableLayoutPanel1 = new SweeperPanel(this, cellSize);
            this.Controls.Add(tableLayoutPanel1);

            initializeMfFlexiblecomponents(9, 9, 10, "beginner");

            bestScores = serializableArrays.Load();
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

       

       

     









        //pb.MouseMove += new MouseEventHandler(mousePictureboxMove);

        //pb.MouseUp += new MouseEventHandler(mouseUpEventHandler);
        //pb.MouseDown += new MouseEventHandler(mdHandler);


        ////???
        //private void mouseTableAndFrameMove(object sender, MouseEventArgs e) {//when leave cells, do dehighlight animation
        //    if (previousCursorOver != null) {//when pictureBox is disabled, table starts capturing events
        //        if (previousCursorOver.Image!= flag && previousCursorOver.Enabled==true) {
        //            previousCursorOver.Image = unopened;
        //        }

        //        previousCursorOver = null;
        //    }


        //}

        //3 CASES
        //1) mouse unpressed, leave cells => table/form mouseMove event
        //2) mouse pressed, leave cells => part A in pictureBox'es mouseMove event (as mousepressed, old pb continues to send events)
        //3) move from 1 cell to another (can jump between unopened cells not hitting cell borders (table)) => part B in pictureBox'es mouseMove event
        //also mark cell when first clicked; also use current cell (not sender) in mouseUp

        //???
        //private void mousePictureboxMove(object sender, MouseEventArgs e) {
        //    //sender is always same => calculate source differently

        //    //Point cursorTableRelative = tableLayoutPanel1.PointToClient(Cursor.Position);
        //   // PictureBox source = (PictureBox)tableLayoutPanel1.GetChildAtPoint(cursorTableRelative);

        //    Point cursorTableRelative = tableLayoutPanel1.PointToClient(Cursor.Position);


        //    int x = cursorTableRelative.X;
        //    int y = cursorTableRelative.Y;
        //    if (x < 0 || y < 0 || x > cols * cellSize - 1 || y > rows * cellSize - 1) {
        //        return;
        //    }



        //    int r = y / cellSize;
        //    int c = x / cellSize;

        //    if (grid[r][c] < 0) {
        //        return;
        //    }




        //    //part A
        //    if (source == null || source.Enabled == false) {

        //        if (previousCursorOver != null) {
        //            if (previousCursorOver.Image != flag && previousCursorOver.Enabled == true) {
        //                previousCursorOver.Image = unopened;
        //            }
        //            previousCursorOver = null;
        //        }
        //        return;
        //    }

        //    //part B
        //    if (killMyBrain) {
        //        return;
        //    }
        //    if (source!= previousCursorOver) {
        //        if (previousCursorOver != null && previousCursorOver.Image!=flag && previousCursorOver.Enabled == true) {
        //            previousCursorOver.Image = unopened;
        //        }

        //        if (source.Image==flag ) {
        //            previousCursorOver = null;
        //            return;
        //        }
        //        previousCursorOver = source;

        //        if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
        //            source.Image = whilePressed;
        //        }
        //        else if (e.Button == MouseButtons.None) {

        //            source.Image = whileHovered;
        //        }
        //    }

        //}


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


