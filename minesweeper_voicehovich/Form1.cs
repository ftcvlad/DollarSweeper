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
using System.Runtime.InteropServices;//for dllimport

namespace minesweeper_voicehovich {
    public partial class MainForm : Form {
        int rows;
        int cols;
        int nOfBombs;
        List<List<int>> grid;
        bool firstTouch;
        int demined;
        int bombsMarked;
        bool killMyBrain = false;

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

        Bitmap manOrd = Resources.man;
        Bitmap manSad = Resources.sad;
        Bitmap manHap = Resources.happy;
        Bitmap manExcited = Resources.excited;

        Bitmap whileHovered = Resources.whileHovered;
        Bitmap whilePressed = Resources.whilePressed;

        Bitmap unopened = null;
       // Bitmap unopened = Resources.unopened;//*** no borders in between, but slower. Use rectangle?


        TableLayoutPanel tableLayoutPanel1;
        PictureBox previousCursorOver = null;
        Timer MyTimer = new Timer();
        int timeElapsed = 0;
        int dollarsCollected = 0;
        public int moneyLostPerGirl = 10000;

        
      

        public String currentDifficulty = "beginner";

        counterPanel bombCounter = new counterPanel();
        counterPanel timeCounter = new counterPanel();
        serializableArrays bestScores;



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
            
            this.MouseMove += new MouseEventHandler(mouseTableAndFrameMove);

          
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
            int cellSize = 17;

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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();




           // tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;//***
            tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(sideOffset, 120);
            tableLayoutPanel1.Size = new Size(cols * (cellSize+1) + 1, rows * (cellSize + 1)+1);//take 1px border into account
            tableLayoutPanel1.ColumnCount = cols;
            tableLayoutPanel1.RowCount = rows;
           

            for (int i = 0; i < rows; i++) {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, cellSize));
            }

            for (int j = 0; j < cols; j++) {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellSize));
            }

            tableLayoutPanel1.MouseMove += new MouseEventHandler(mouseTableAndFrameMove);
            

            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {

                    PictureBox pb = new PictureBox();


                    pb.Margin = new Padding(0);


                    pb.MouseMove += new MouseEventHandler(mousePictureboxMove);

                    pb.MouseUp += new MouseEventHandler(digitEventHandler);
                    pb.MouseDown += new MouseEventHandler(mdHandler);


                    

                    pb.Image = unopened;
                  
                    pb.Tag = i * c + j;
                    tableLayoutPanel1.Controls.Add(pb, j, i);
                   
                }
            }
            tableLayoutPanel1.ResumeLayout();
            this.Controls.Add(tableLayoutPanel1);


           
           
        }


        

        private void mdHandler(object sender, MouseEventArgs e) {//when mouseDown, do current cell animation

           
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {

                if (((PictureBox)sender).Image == whileHovered) {//add flag
                    ((PictureBox)sender).Image = flag;
                    bombsMarked++;
                    bombCounter.setNumber(nOfBombs - bombsMarked);
                }
                else if (((PictureBox)sender).Image == flag) {
                    ((PictureBox)sender).Image = unopened;
                    bombsMarked--;
                    bombCounter.setNumber(nOfBombs - bombsMarked);

                }
                killMyBrain = true;

            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (((PictureBox)sender).Image != flag) {
                    ((PictureBox)sender).Image = whilePressed;
                }
                button1.Image = manExcited;
            }



               
        }

        private void mouseTableAndFrameMove(object sender, MouseEventArgs e) {//when leave cells, do dehighlight animation
            if (previousCursorOver != null) {//when pictureBox is disabled, table starts capturing events
                if (previousCursorOver.Image!= flag && previousCursorOver.Enabled==true) {
                    previousCursorOver.Image = unopened;
                }
                    
                previousCursorOver = null;
            }

            
        }

        //3 CASES
        //1) mouse unpressed, leave cells => table/form mouseMove event
        //2) mouse pressed, leave cells => part A in pictureBox'es mouseMove event (as mousepressed, old pb continues to send events)
        //3) move from 1 cell to another (can jump between unopened cells not hitting cell borders (table)) => part B in pictureBox'es mouseMove event
        //also mark cell when first clicked; also use current cell (not sender) in mouseUp

        private void mousePictureboxMove(object sender, MouseEventArgs e) {
            //sender is always same => calculate source differently

            Point cursorTableRelative = tableLayoutPanel1.PointToClient(Cursor.Position);
            PictureBox source = (PictureBox)tableLayoutPanel1.GetChildAtPoint(cursorTableRelative);
            
            //part A
            if (source == null || source.Enabled == false) {
               
                if (previousCursorOver != null) {
                    if (previousCursorOver.Image != flag && previousCursorOver.Enabled == true) {
                        previousCursorOver.Image = unopened;
                    }
                    previousCursorOver = null;
                }
                return;
            }
           
            //part B
            if (killMyBrain) {
                return;
            }
            if (source!= previousCursorOver) {
                if (previousCursorOver != null && previousCursorOver.Image!=flag && previousCursorOver.Enabled == true) {
                    previousCursorOver.Image = unopened;
                }
            
                if (source.Image==flag ) {
                    previousCursorOver = null;
                    return;
                }
                previousCursorOver = source;
               
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
                    source.Image = whilePressed;
                }
                else if (e.Button == MouseButtons.None) {

                    source.Image = whileHovered;
                }
            }

        }







        private void digitEventHandler(object sender, MouseEventArgs e) {

            button1.Image = manOrd;

            killMyBrain = false;

            Point cursorTableRelative = tableLayoutPanel1.PointToClient(Cursor.Position);
            PictureBox source = (PictureBox)tableLayoutPanel1.GetChildAtPoint(cursorTableRelative);
            if (source == null) {//if press up outside cells
                return;
            }
           
          
            int loc = Int32.Parse(source.Tag.ToString());
            int r = loc / cols;
            int c = loc % cols;

            
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (source.Image == flag) {
                    return;
                }

                source.Enabled = false;
                

                if (firstTouch == true) {
                    firstTouch = false;
                    placeBombs(r, c);

                    MyTimer.Start();
                }

                if (grid[r][c] > 0) {
                    openDigits(r, c, source);
                    
                }
                else if (grid[r][c] == -1) {

                    dollarsCollected = dollarsCollected - moneyLostPerGirl;
                    dollarsCollectedLabel.Text = "$ " + dollarsCollected;
                    source.Image = bombBad;
                    
                    if (dollarsCollected<0) {

                        MyTimer.Stop();
                        button1.Image = manSad;
                        

                        for (int i = 0; i < rows; i++) {
                            for (int j = 0; j < cols; j++) {
                                PictureBox nextCell = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);
                                nextCell.Enabled = false;

                                if (grid[i][j] == -1 && nextCell.Image == unopened) {

                                    nextCell.Image = bomb;
                                    continue;
                                }

                                if ((grid[i][j] != -1 && nextCell.Image == flag)) {
                                    nextCell.Image = bombWrong;
                                }


                            }
                        }



                        //stop timer
                        return;
                    }
                    else {
                        source.Enabled = false;
                    }
                }
                else if (grid[r][c] == 0) {


                    //  DrawingControl.SuspendDrawing(this);
                    grid[r][c] = -2;
                    markEmptyRecursively(r, c);

                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < cols; j++) {

                            if (grid[i][j] == -2) {
                                grid[i][j] = -3;
                                for (int dr = -1; dr <= 1; dr++) {
                                    for (int dc = -1; dc <= 1; dc++) {

                                        if (i + dr >= 0 && j + dc >= 0 && i + dr < rows && j + dc < cols && grid[i + dr][j + dc] != -2 && grid[i + dr][j + dc] != -3 && grid[i + dr][j + dc] != 0 && ((PictureBox)tableLayoutPanel1.GetControlFromPosition(j+dc, i+dr)).Image != flag) {//!=0 is some bug case!

                                            PictureBox borderDigit = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j + dc, i + dr);
                                            openDigits(i + dr, j + dc, borderDigit);


                                            grid[i + dr][j + dc] = -3;
                                            borderDigit.Enabled = false;
                                           
                                        }


                                    }
                                }



                                PictureBox green = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);
                                green.Enabled = false;
                                demined++;
                                green.Image = empty;
                                //System.Threading.Thread.Sleep(100);
                            }//end of if ==-2
                        }
                    }

                    // DrawingControl.ResumeDrawing(this);
                }

               
                if (demined == rows * cols - nOfBombs) {
                    MyTimer.Stop();

                    button1.Image = manHap;
                    Console.WriteLine("Wiiiiiin");

                    for (int i = 0; i < rows; i++) {//mark unmarked bombs
                        for (int j = 0; j < cols; j++) {

                            if (grid[i][j] == -1) {
                                ((PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Image = flag;
                            }
                        }
                    }

                    if (moneyLostPerGirl == 10000) {//record only 1 life games
                        bestScores.manageScores(currentDifficulty, timeElapsed);
                    }
                    //stop timer
                    //record results, top score
                    bombCounter.setNumber(0);
                }
               
            }

        }


        private void openDigits(int r, int c, PictureBox source) {
            demined++;
            if (grid[r][c] == 1) {
                source.Image = one;
                dollarsCollected++;
            }
            else if (grid[r][c] == 2) {
                source.Image = two;
                dollarsCollected += 2;
            }
            else if (grid[r][c] == 3) {
                source.Image = three;
                dollarsCollected += 3;
            }
            else if (grid[r][c] == 4) {
                source.Image = four;
                dollarsCollected += 4;
            }
            else if (grid[r][c] == 5) {
                source.Image = five;
                dollarsCollected += 5;
            }
            else if (grid[r][c] == 6) {
                source.Image = six;
                dollarsCollected += 6;
            }
            else if (grid[r][c] == 7) {
                source.Image = seven;
                dollarsCollected += 7;
            }
            else if (grid[r][c] == 8) {
                source.Image = eight;
                dollarsCollected += 8;
            }
            dollarsCollectedLabel.Text = "$ " + dollarsCollected;
            grid[r][c] = -3;


        }

    
        private void markEmptyRecursively(int row, int col) {

            if (row - 1 >= 0 && grid[row - 1][col] == 0 ) {
                if (((PictureBox)tableLayoutPanel1.GetControlFromPosition(col, row-1)).Image != flag) {
                    grid[row - 1][col] = -2;
                    markEmptyRecursively(row - 1, col);
                }
                
            }
            if (row + 1 < rows && grid[row + 1][col] == 0) {
                if (((PictureBox)tableLayoutPanel1.GetControlFromPosition(col, row+1)).Image != flag) {
                    grid[row + 1][col] = -2;
                    markEmptyRecursively(row + 1, col);
                }
               
            }
            if (col - 1 >= 0 && grid[row][col-1] == 0) {
                if (((PictureBox)tableLayoutPanel1.GetControlFromPosition(col-1, row)).Image != flag) {
                    grid[row][col - 1] = -2;
                    markEmptyRecursively(row, col - 1);
                }
                
            }
            if (col + 1 < cols && grid[row ][col+1] == 0) {
                if (((PictureBox)tableLayoutPanel1.GetControlFromPosition(col+1, row)).Image != flag) {
                    grid[row][col + 1] = -2;
                    markEmptyRecursively(row, col + 1);
                }
               
            }
        }

        private void newGame(object sender, EventArgs e) {
            button1.Image = manOrd;
            startGame();
        }

        private void startGame() {

            firstTouch = true;
            demined = 0;
            dollarsCollected = 0;
            dollarsCollectedLabel.Text = "$ 0";
            bombsMarked = 0;
            timeElapsed = 0;
            timeCounter.setNumber(0);
            MyTimer.Stop();
            bombCounter.setNumber(nOfBombs);
            grid = new List<List<int>>();
            for (int i = 0; i < rows; i++) {
                List<int> nextRow = new List<int>();
                for (int j = 0; j < cols; j++) {
                    PictureBox con = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);
                    con.Enabled = true;
                    con.Image = unopened;
                    //if (con.Image == flag) {
                    //    con.Image = null;
                    //}
                    nextRow.Add(0);

                }

                grid.Add(nextRow);
            }
           

        }

        private void placeBombs(int r, int c) {
            int numOfCells = rows * cols;

            Random rnd = new Random((int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 1000));
           // Random rnd = new Random(1);

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
               
                grid[allCells[index] / cols][allCells[index] % cols] = -1;

                //hide this
                //PictureBox  con = (PictureBox)tableLayoutPanel1.GetControlFromPosition(allCells[index] % cols, allCells[index] / cols);
                //con.BackgroundImage = bomb;
                //--------

                allCells.RemoveAt(index);
            }

            //numbers
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (grid[i][j] != -1) {
                        int nOfBombsNextDoor = 0;

                        for (int dr = -1; dr <= 1; dr++) {
                            for (int dc = -1; dc <= 1; dc++) {

                                if (i+dr>=0 && j+dc>=0 && i+dr<rows && j + dc < cols && grid[i + dr][j + dc] == -1) {
                                    nOfBombsNextDoor++;
                                }
                            }
                        }

                        grid[i][j] = nOfBombsNextDoor;
                        //hide this
                        //if (nOfBombsNextDoor == 1) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundBackgroundImage = one;
                        //}
                        //else if (nOfBombsNextDoor == 2) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundBackgroundImage = two;
                        //}
                        //else if (nOfBombsNextDoor == 3) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundBackgroundImage = three;
                        //}
                        //else if (nOfBombsNextDoor == 4) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundBackgroundImage = four;
                        //}
                        //else if (nOfBombsNextDoor == 5) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundBackgroundImage = five;
                        //}
                    }

                }
            }

        }

        private void Form1_Load(object sender, EventArgs e) {

            startGame();
        }

        

       

        public  void createNewSizeGrid(int width, int height, int mines) {
            tableLayoutPanel1.Dispose();
            initializeTable(height, width, mines);//change width and height order, as I screwed i,j order
            startGame();
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




        Tuple<string, int> t = new Tuple<string, int>("Hello", 4);

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
                    //make enter name frame

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





////do yuo wooooork?a




// http://stackoverflow.com/questions/13194674/foreach-loop-to-create-100-buttons-painting-all-buttons-at-same-time-as-to-prev
//class DrawingControl {
//    [DllImport("user32.dll")]
//    public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

//    private const int WM_SETREDRAW = 11;

//    public static void SuspendDrawing(Control parent) {
//        SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
//    }

//    public static void ResumeDrawing(Control parent) {
//        SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
//        parent.Refresh();
//    }
//}