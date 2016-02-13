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

    public partial class SweeperPanel : TableLayoutPanel {


        Bitmap one = Resources.one;
        Bitmap two = Resources.two;
        Bitmap three = Resources.three;
        Bitmap four = Resources.four;
        Bitmap five = Resources.five;
        Bitmap six = Resources.six;
        Bitmap seven = Resources.seven;
        Bitmap eight = Resources.eight;
        Bitmap empty = Resources.empty;
        Bitmap flag = Resources.flag;
        Bitmap bomb = Resources.girl3;
        Bitmap bombBad = Resources.happyGirl;
        Bitmap bombWrong = Resources.girlWrong2;


        Bitmap whileHovered = Resources.whileHovered;
        Bitmap whilePressed = Resources.whilePressed;
        Bitmap unopened = Resources.unopened;


        MainForm refer = null;

        public SweeperPanel(MainForm refer) {
            this.refer = refer;

            Bitmap[] allImages = { empty, one, two, three, four, five, six, seven, eight, flag, bomb, bombBad, bombWrong, whileHovered, whilePressed, unopened };
            for (int i = 0; i < allImages.Length; i++) {
                DrawLineInt(allImages[i]);
            }
            CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        }

        public void DrawLineInt(Bitmap bmp) {
            Pen blackPen = new Pen(Color.DarkSeaGreen, 1);


            using (var graphics = Graphics.FromImage(bmp)) {
                graphics.DrawLine(blackPen, 0, 0, 0, 16);
                graphics.DrawLine(blackPen, 0, 0, 16, 0);
                graphics.DrawLine(blackPen, 16, 0, 16, 16);
                graphics.DrawLine(blackPen, 0, 16, 16, 16);
            }
        }


        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            //draw grid contained info
            for (int r = 0; r < refer.rows; r++) {
                for (int c = 0; c < refer.cols; c++) {

                    Point topLeftLocation = new Point(c * refer.cellSize, r * refer.cellSize);


                    if (refer.grid[r][c] == -1) {
                        e.Graphics.DrawImage(one, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -2) {
                        e.Graphics.DrawImage(two, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -3) {
                        e.Graphics.DrawImage(three, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -4) {
                        e.Graphics.DrawImage(four, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -5) {
                        e.Graphics.DrawImage(five, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -6) {
                        e.Graphics.DrawImage(six, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -7) {
                        e.Graphics.DrawImage(seven, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -8) {
                        e.Graphics.DrawImage(eight, topLeftLocation);
                    }

                    else if (refer.grid[r][c] == -10) {
                        e.Graphics.DrawImage(empty, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -13) {
                        e.Graphics.DrawImage(bombBad, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -14) {
                        e.Graphics.DrawImage(bombWrong, topLeftLocation);
                    }
                    else if (refer.grid[r][c] == -15) {
                        e.Graphics.DrawImage(bomb, topLeftLocation);
                    }
                    else if (refer.grid[r][c] >= 100) {
                        e.Graphics.DrawImage(flag, topLeftLocation);
                    }
                    else if (refer.grid[r][c] >=0 && refer.grid[r][c]<=8 || refer.grid[r][c]==13) {
                         e.Graphics.DrawImage(unopened, topLeftLocation);
                       
                        //Pen pen = new Pen(Color.DarkSeaGreen, 1);
                        //e.Graphics.DrawRectangle(pen, new Rectangle(topLeftLocation.X, topLeftLocation.Y, 17, 17));
                    }


                }
            }

            //draw hovered, dragged cells
            base.OnPaint(e);
        }



    }
}






namespace minesweeper_voicehovich {
    public partial class MainForm : Form {
        public int rows;
        public int cols;
        public List<List<int>> grid;
        public int cellSize;

        int nOfBombs;
        bool firstTouch;
        int demined;
        int bombsMarked;
        bool gameFinished;



        Bitmap manOrd = Resources.man;
        Bitmap manSad = Resources.sad;
        Bitmap manHap = Resources.happy;
        Bitmap manExcited = Resources.excited;

   


        SweeperPanel tableLayoutPanel1;
       
        Timer MyTimer = new Timer();
        int timeElapsed = 0;
        int dollarsCollected = 0;
        public int moneyLostPerGirl = 10000;
       




        public String currentDifficulty = "beginner";

        counterPanel bombCounter = new counterPanel();
        counterPanel timeCounter = new counterPanel();
        serializableArrays bestScores;

        //!!! move rows , cols, cellSize to SweeperPanel

        public MainForm() {//contains initializations happening once

           
           
           
            

            InitializeComponent();

            initializeTable(9,9, 10);

            button1.Anchor = AnchorStyles.None;
            button1.FlatAppearance.BorderSize = 0;
            button1.Click += new EventHandler(newGame);
        


            button1.Size = new Size(60,60);

            tableLayoutPanel2.Location = new System.Drawing.Point(50, 50);
            tableLayoutPanel2.Controls.Add(bombCounter);
            tableLayoutPanel2.Controls.Add(button1);
            tableLayoutPanel2.Controls.Add(timeCounter);

            dollarsCollectedLabel.Text = "$ 0";
            Controls.Add(dollarsCollectedLabel);

            this.FormBorderStyle= FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
         //   this.MouseMove += new MouseEventHandler(mouseTableAndFrameMove);

          
            MyTimer.Interval = (1000);
            MyTimer.Tick += new EventHandler(timerTicked);


            bestScores = serializableArrays.Load();

        }

        private void timerTicked(object sender, EventArgs e) {
            timeElapsed++;
            timeCounter.setNumber(timeElapsed);
            if (timeElapsed == 999) {
                MyTimer.Stop();
            }
        }

        private void initializeTable(int r, int c, int n) {


            button1.Image = manOrd;

            this.rows = r;
            this.cols = c;
            this.nOfBombs = n;
             cellSize = 17;

            int sideOffset = 50;
            int tableLayoutPanel2Width = Math.Max(270, cols * (cellSize + 1) + 1);
            int tableLayoutPanel2Height = 50;
            int tableLayoutPanel1Height = rows * (cellSize + 1) + 1;

            //http://stackoverflow.com/questions/2022660/how-to-get-the-size-of-a-winforms-form-titlebar-height
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;


            //------------tableLayoutPanel2 ------------

            tableLayoutPanel2.Size = new Size(tableLayoutPanel2Width, tableLayoutPanel2Height+2);//2 to make image fit...
            bombCounter.setNumber(nOfBombs);

            //------MainForm //

            this.Size = new Size(tableLayoutPanel2Width + sideOffset * 2+7*2, sideOffset*2 + Math.Max(tableLayoutPanel1Height, 160) + 20 + tableLayoutPanel2Height+ titleHeight);


            //--dollarLabel






            dollarsCollectedLabel.Location = new System.Drawing.Point(sideOffset, sideOffset  + Math.Max(tableLayoutPanel1Height, 160) + 20 + tableLayoutPanel2Height + titleHeight-20);
           
           
          

            //------------tableLayoutPanel1 ------------
            tableLayoutPanel1 = new SweeperPanel(this);
            tableLayoutPanel1.SuspendLayout();




            
            this.tableLayoutPanel1.Location = new System.Drawing.Point(sideOffset, 120);
            tableLayoutPanel1.Size = new Size(cols *cellSize, rows *cellSize);
          

            //tableLayoutPanel1.MouseMove += new MouseEventHandler(mouseTableAndFrameMove);

            tableLayoutPanel1.MouseUp += new MouseEventHandler(mouseUpEventHandler);
            tableLayoutPanel1.MouseDown += new MouseEventHandler(mdHandler);


            tableLayoutPanel1.ResumeLayout();
            this.Controls.Add(tableLayoutPanel1);

          


        }




        private void mdHandler(object sender, MouseEventArgs e) {//when mouseDown, do current cell animation

            

            Point cursorTableRelative = tableLayoutPanel1.PointToClient(Cursor.Position);

            int c = cursorTableRelative.X / cellSize;
            int r = cursorTableRelative.Y / cellSize;

            if (grid[r][c] < 0 || gameFinished) {
                return;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right) {

                if (grid[r][c] < 100) {//add flag

                    grid[r][c] += 100;
                    bombsMarked++;
                    bombCounter.setNumber(nOfBombs - bombsMarked);
                }
                else if (grid[r][c] >= 100) {
                    grid[r][c] -= 100;

                    bombsMarked--;
                    bombCounter.setNumber(nOfBombs - bombsMarked);
                   
                }
                tableLayoutPanel1.Invalidate(new Rectangle(c*cellSize,r*cellSize,cellSize,cellSize));//+++
                

            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (grid[r][c] < 100 && grid[r][c] >=0) {
                   // ((PictureBox)sender).Image = whilePressed;//change previousCellPosition Point instead
                    button1.Image = manExcited;
                    
                    tableLayoutPanel1.Invalidate(new Rectangle(c*cellSize,r*cellSize,cellSize,cellSize));//+++
                }

            }


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







        private void mouseUpEventHandler(object sender, MouseEventArgs e) {

            if (gameFinished == true) {
                return;//if game finished
            }

            button1.Image = manOrd;




            Point cursorTableRelative = tableLayoutPanel1.PointToClient(Cursor.Position);
            //Console.WriteLine(cursorTableRelative.X + "   " + cursorTableRelative.Y);

            int x = cursorTableRelative.X;
            int y = cursorTableRelative.Y;
            if (x<0 || y<0 || x>cols*cellSize-1 || y> rows * cellSize - 1) {
                return;//if clicked outside SweeperPanel
            }
           
           

            int r = y / cellSize;
            int c = x / cellSize;

            if (grid[r][c] < 0) {
                return;//if cell already opened
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (grid[r][c] >=100) {
                    return;//if leftclicked flag
                }


                if (firstTouch == true) {
                    firstTouch = false;
                    placeBombs(r, c);

                    MyTimer.Start();
                }

                if (grid[r][c] >= 1 && grid[r][c]<=8) {
                   
                    openDigits(r, c);
                    grid[r][c] *= -1;
                    tableLayoutPanel1.Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));//+++


                }
                else if (grid[r][c] == 13) {

                    dollarsCollected = dollarsCollected - moneyLostPerGirl;
                    dollarsCollectedLabel.Text = "$ " + dollarsCollected;
                    grid[r][c] = -13;
                    tableLayoutPanel1.Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));//+++

                    if (dollarsCollected<0) {

                        gameFinished = true;
                        MyTimer.Stop();
                        button1.Image = manSad;
                        

                        for (int i = 0; i < rows; i++) {
                            for (int j = 0; j < cols; j++) {
                             
                                if (grid[i][j] == 13) {
                                    grid[i][j] = -15;
                                    tableLayoutPanel1.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
                                    continue;
                                }

                                else if ((grid[i][j] >=100 && grid[i][j]!=113)) {
                                    grid[i][j] = -14;
                                    tableLayoutPanel1.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
                                }
                               


                            }
                        }

                    }
                 
                }
                else if (grid[r][c] == 0) {

                    grid[r][c] = 10;
                    markEmptyRecursively(r, c);

                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < cols; j++) {

                            //open empty cell and digits around it
                            if (grid[i][j] == 10) {
                                grid[i][j] = -10;
                                tableLayoutPanel1.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
                                                        
                                for (int dr = -1; dr <= 1; dr++) {
                                    for (int dc = -1; dc <= 1; dc++) {
                                        if (i + dr >= 0 && j + dc >= 0 && i + dr < rows && j + dc < cols && grid[i + dr][j + dc]>=1 && grid[i + dr][j + dc] <= 8) {
                                          
                                            openDigits(i + dr, j + dc);
                                            grid[i + dr][j + dc] *= -1;
                                            tableLayoutPanel1.Invalidate(new Rectangle((j + dc) * cellSize,(i + dr) * cellSize, cellSize, cellSize));//+++                                
                                        }
                                    }
                                }
                                demined++;
                                
                                
                            }
                        }
                    }

                    
                }

               
                if (demined == rows * cols - nOfBombs) {
                    MyTimer.Stop();
                    gameFinished = true;
                    button1.Image = manHap;
                    Console.WriteLine("Wiiiiiin");

                    for (int i = 0; i < rows; i++) {//mark unmarked bombs
                        for (int j = 0; j < cols; j++) {

                            if (grid[i][j] == 13) {
                                grid[i][j] += 100;
                                tableLayoutPanel1.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++

                            }
                        }
                    }

                    if (moneyLostPerGirl == 10000) {//record only 1 life games
                        bestScores.manageScores(currentDifficulty, timeElapsed);
                    }
                    
                    bombCounter.setNumber(0);
                }

                //tableLayoutPanel1.Invalidate();
            }

        }


        private void openDigits(int r, int c) {
            demined++;
            if (grid[r][c] == 1) {
              
                dollarsCollected++;
            }
            else if (grid[r][c] == 2) {
                
                dollarsCollected += 2;
            }
            else if (grid[r][c] == 3) {
              
                dollarsCollected += 3;
            }
            else if (grid[r][c] == 4) {
               
                dollarsCollected += 4;
            }
            else if (grid[r][c] == 5) {
               
                dollarsCollected += 5;
            }
            else if (grid[r][c] == 6) {
                
                dollarsCollected += 6;
            }
            else if (grid[r][c] == 7) {
               
                dollarsCollected += 7;
            }
            else if (grid[r][c] == 8) {
              
                dollarsCollected += 8;
            }
            dollarsCollectedLabel.Text = "$ " + dollarsCollected;
            


        }

    
        private void markEmptyRecursively(int row, int col) {

            if (row - 1 >= 0 && grid[row - 1][col] == 0 ) {
                    grid[row - 1][col] = 10;
                    markEmptyRecursively(row - 1, col);
            }
            if (row + 1 < rows && grid[row + 1][col] == 0) {
                    grid[row + 1][col] = 10;
                    markEmptyRecursively(row + 1, col);
            }
            if (col - 1 >= 0 && grid[row][col-1] == 0) {
                    grid[row][col - 1] = 10;
                    markEmptyRecursively(row, col - 1);
            }
            if (col + 1 < cols && grid[row ][col+1] == 0) {
                    grid[row][col + 1] = 10;
                    markEmptyRecursively(row, col + 1);
            }
        }

        private void newGame(object sender, EventArgs e) {
            button1.Image = manOrd;
            startGame(false);
        }
        //zzz
        private void startGame(bool newDimensions ) {

            firstTouch = true;
            demined = 0;
            gameFinished = false;
            dollarsCollected = 0;
            dollarsCollectedLabel.Text = "$ 0";
            bombsMarked = 0;
            timeElapsed = 0;
            timeCounter.setNumber(0);
            MyTimer.Stop();
            bombCounter.setNumber(nOfBombs);

            if (!newDimensions) {//1st time or when grid resized, draw all grid
               
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        if ((grid[i][j] < 0 || grid[i][j]>=100) ) {
                            tableLayoutPanel1.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
                        }
                    }
                }
            }
            else {
                tableLayoutPanel1.Invalidate();//+++
            }

            grid = new List<List<int>>();
            for (int i = 0; i < rows; i++) {
                List<int> nextRow = new List<int>();
                for (int j = 0; j < cols; j++) {

                    nextRow.Add(0);

                }

                grid.Add(nextRow);
            }
           // tableLayoutPanel1.Invalidate();



        }

        private void placeBombs(int r, int c) {
            int numOfCells = rows * cols;

            Random rnd = new Random((int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 1000));
          

            List<int> allCells = new List<int>();//to avoid repeated random numbers
            for (int i = 0; i < numOfCells; i++) {
                allCells.Add(i);
            }

            //cells around 1st touch cannot contain bombs!
            if (numOfCells - 9 >= nOfBombs) {//make sure enough space to put 

                for (int dr = -1; dr <= 1; dr++) {
                    for (int dc = -1; dc <= 1; dc++) {

                        if (r + dr >= 0 && c + dc >= 0 && r + dr < rows && c + dc < cols) {
                            allCells.RemoveAt(allCells.IndexOf((r + dr) * cols + c + dc));
                        }
                    }
                }
            }
            //bombs
            int index;
            for (int i=0;i< nOfBombs; i++) { 
                index = rnd.Next(0, allCells.Count);
               
                grid[allCells[index] / cols][allCells[index] % cols]+= 13;//if user puts flag before 1st touch, it remains there! thus +13
                allCells.RemoveAt(index);
            }

            //numbers
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (grid[i][j] != 13 && grid[i][j] != 113) {
                        int nOfBombsNextDoor = 0;

                        for (int dr = -1; dr <= 1; dr++) {
                            for (int dc = -1; dc <= 1; dc++) {

                                if (i+dr>=0 && j+dc>=0 && i+dr<rows && j + dc < cols && grid[i + dr][j + dc] %100==13) {
                                    nOfBombsNextDoor++;
                                }
                            }
                        }

                        grid[i][j] += nOfBombsNextDoor;
                        
                    }

                }
            }

        }

        private void Form1_Load(object sender, EventArgs e) {

            startGame(true);
        }

        

       

        public  void createNewSizeGrid(int width, int height, int mines) {
            tableLayoutPanel1.Dispose();
            initializeTable(height, width, mines);//change width and height order, as I screwed i,j order
            startGame(true);
        }

    

        private void button1_Click(object sender, EventArgs e) {

        }

        private void beginner9x910MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            currentDifficulty = "beginner";
            createNewSizeGrid(9, 9, 10);
        }

        private void intermediate16x1640MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            currentDifficulty = "intermediate";
            createNewSizeGrid(16, 16, 40);
        }

        private void advanced30x2499MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            currentDifficulty = "advanced";
            createNewSizeGrid(30, 16, 99);
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
            LossSelectorForm form = new LossSelectorForm(this);
            form.ShowDialog();
        }

        private void aboutDollarSweeperToolStripMenuItem_Click(object sender, EventArgs e) {


            AboutForm form = new AboutForm();
            form.ShowDialog();
        }
    }
}

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




namespace minesweeper_voicehovich {
    [Serializable()]
    public class serializableArrays {

        public Tuple<int, string>[] begBestScores = new Tuple<int, string>[10];
        public Tuple<int, string>[] interBestScores = new Tuple<int, string>[10];
        public Tuple<int, string>[] advBestScores = new Tuple<int, string>[10];




      

        private static String filePath = "bestScores.txt";

        public serializableArrays() {

            for (int i = 0; i < 10; i++) {
                begBestScores[i] = new Tuple<int, string>(999, "unknown");
                interBestScores[i] = new Tuple<int, string>(999, "unknown");
                advBestScores[i] = new Tuple<int, string>(999, "unknown");
            }

        }


        public static void Save(serializableArrays obj) {

            System.IO.Stream stream = System.IO.File.Open(filePath, System.IO.FileMode.Create);
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);



        }

        public static serializableArrays Load() {
            try {
                System.IO.Stream stream = System.IO.File.Open(filePath, System.IO.FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (serializableArrays)binaryFormatter.Deserialize(stream);
            }
            catch(System.Runtime.Serialization.SerializationException se) {
                serializableArrays sa = new serializableArrays();
                return sa;
            }
            catch (System.IO.FileNotFoundException e) {
                serializableArrays sa = new serializableArrays();
                return sa;
            }
        }



        public void manageScores(String difficulty, int score) {
            if (difficulty.Equals("beginner")) {
                updateScores(begBestScores, score);
            }
            else if (difficulty.Equals("intermediate")) {
                updateScores(interBestScores, score);
            }
            else if (difficulty.Equals("advanced")) {
                updateScores(advBestScores, score);
            }
        }

        public void updateScores(Tuple<int, string>[] arrToUpdate, int score) {

            for (int i = 0; i < 10; i++) {
                if (arrToUpdate[i].Item1 > score) {

                    for (int j = 9; j > i; j--) {
                        arrToUpdate[j] = arrToUpdate[j - 1];
                    }
                    

                    Label aCaseForName=new Label();
                    WinnerNameForm form = new WinnerNameForm(aCaseForName);
                    form.ShowDialog();

                    arrToUpdate[i] = new Tuple<int, string>(score, aCaseForName.Text);
                    serializableArrays.Save(this);
                    return;
                }
            }
        }

    }
}


