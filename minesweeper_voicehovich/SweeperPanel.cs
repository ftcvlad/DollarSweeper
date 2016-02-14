using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using minesweeper_voicehovich.Properties;//for resources

namespace minesweeper_voicehovich {

    public class SweeperPanel : TableLayoutPanel {

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


        MainForm mfReference = null;



        public int rows;
        public int cols;
        public List<List<int>> grid;
        public int cellSize;
        int nOfBombs;
        bool firstTouch;
        int demined;
        int bombsMarked;
        bool gameFinished;
        Timer MyTimer = new Timer();
        int timeElapsed = 0;
        int dollarsCollected = 0;
        public int moneyLostPerGirl = 10000;
        public String currentDifficulty;
        bool pressed;

        public Tuple<int, int> previousCell = null;

        public SweeperPanel(MainForm refer, int cellSize) {
            this.cellSize = cellSize;
            this.mfReference = refer;

            Bitmap[] allImages = { empty, one, two, three, four, five, six, seven, eight, flag, bomb, bombBad, bombWrong, whileHovered, whilePressed, unopened };
            for (int i = 0; i < allImages.Length; i++) {
                DrawLineInt(allImages[i]);
            }
            CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
            MyTimer.Interval = (1000);
            MyTimer.Tick += new EventHandler(timerTicked);
            MouseUp += new MouseEventHandler(mouseUpEventHandler);
            MouseDown += new MouseEventHandler(mdHandler);
            MouseMove += new MouseEventHandler(mousePictureboxMove);

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
            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {   
                 
                    Point topLeftLocation = new Point(c * cellSize, r * cellSize);


                    if (grid[r][c] == -1) {
                        e.Graphics.DrawImage(one, topLeftLocation);
                    }
                    else if (grid[r][c] == -2) {
                        e.Graphics.DrawImage(two, topLeftLocation);
                    }
                    else if (grid[r][c] == -3) {
                        e.Graphics.DrawImage(three, topLeftLocation);
                    }
                    else if (grid[r][c] == -4) {
                        e.Graphics.DrawImage(four, topLeftLocation);
                    }
                    else if (grid[r][c] == -5) {
                        e.Graphics.DrawImage(five, topLeftLocation);
                    }
                    else if (grid[r][c] == -6) {
                        e.Graphics.DrawImage(six, topLeftLocation);
                    }
                    else if (grid[r][c] == -7) {
                        e.Graphics.DrawImage(seven, topLeftLocation);
                    }
                    else if (grid[r][c] == -8) {
                        e.Graphics.DrawImage(eight, topLeftLocation);
                    }

                    else if (grid[r][c] == -10) {
                        e.Graphics.DrawImage(empty, topLeftLocation);
                    }
                    else if (grid[r][c] == -13) {
                        e.Graphics.DrawImage(bombBad, topLeftLocation);
                    }
                    else if (grid[r][c] == -14) {
                        e.Graphics.DrawImage(bombWrong, topLeftLocation);
                    }
                    else if (grid[r][c] == -15) {
                        e.Graphics.DrawImage(bomb, topLeftLocation);
                    }
                    else if (grid[r][c] >= 100) {
                        e.Graphics.DrawImage(flag, topLeftLocation);
                    }
                    else if (grid[r][c] >= 0 && grid[r][c] <= 8 || grid[r][c] == 13) {
                        e.Graphics.DrawImage(unopened, topLeftLocation);

                        //Pen pen = new Pen(Color.DarkSeaGreen, 1);
                        //e.Graphics.DrawRectangle(pen, new Rectangle(topLeftLocation.X, topLeftLocation.Y, 17, 17));
                    }


                }
            }

            //here previousCell is actually a current cell

            if (previousCell != null) {
               
                Point tlloc = new Point(previousCell.Item2 * cellSize, previousCell.Item1 * cellSize);
                if (!pressed) {//hovered
                   
                    e.Graphics.DrawImage(whileHovered, tlloc);
                }
                else {//pressed
                    e.Graphics.DrawImage(whilePressed, tlloc);
                }
            }
            

            //draw hovered, dragged cells
            base.OnPaint(e);
        }


        private void timerTicked(object sender, EventArgs e) {
            timeElapsed++;
            mfReference.timeCounter.setNumber(timeElapsed);
            if (timeElapsed == 999) {
                MyTimer.Stop();
            }
        }

        public void initializeSweeperPanel(int r, int c, int n, string difficulty) {
            this.currentDifficulty = difficulty;
            this.rows = r;
            this.cols = c;
            this.nOfBombs = n;

        }

        private void mdHandler(object sender, MouseEventArgs e) {//when mouseDown, do current cell animation



            Point cursorTableRelative = this.PointToClient(Cursor.Position);

            int c = cursorTableRelative.X / cellSize;
            int r = cursorTableRelative.Y / cellSize;

            if (grid[r][c] < 0 || gameFinished) {
                return;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right) {

                if (grid[r][c] < 100) {//add flag

                    grid[r][c] += 100;
                    bombsMarked++;
                    mfReference.bombCounter.setNumber(nOfBombs - bombsMarked);
                    previousCell = null;
                }
                else if (grid[r][c] >= 100) {
                    grid[r][c] -= 100;

                    bombsMarked--;
                    mfReference.bombCounter.setNumber(nOfBombs - bombsMarked);

                }
                this.Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));//+++


            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (grid[r][c] < 100 && grid[r][c] >= 0) {
                    pressed = true;//to distinguish between hovered and pressed
                    mfReference.updateButton("excited");

                    this.Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));//+++
                }

            }


        }


        private void mouseUpEventHandler(object sender, MouseEventArgs e) {

            if (gameFinished == true) {
                return;//if game finished
            }

            mfReference.updateButton("ordinary");




            Point cursorTableRelative = this.PointToClient(Cursor.Position);


            int x = cursorTableRelative.X;
            int y = cursorTableRelative.Y;
            if (x < 0 || y < 0 || x > cols * cellSize - 1 || y > rows * cellSize - 1) {
                return;//if clicked outside SweeperPanel
            }



            int r = y / cellSize;
            int c = x / cellSize;

            if (grid[r][c] < 0) {
                return;//if cell already opened
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (grid[r][c] >= 100) {
                    return;//if leftclicked flag
                }


                if (firstTouch == true) {
                    firstTouch = false;
                    placeBombs(r, c);

                    MyTimer.Start();
                }

                if (grid[r][c] >= 1 && grid[r][c] <= 8) {

                    openDigits(r, c);
                    grid[r][c] *= -1;
                    this.Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));//+++


                }
                else if (grid[r][c] == 13) {

                    dollarsCollected = dollarsCollected - moneyLostPerGirl;
                    mfReference.setDollarsLabel("$ " + dollarsCollected);

                    grid[r][c] = -13;
                    this.Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));//+++

                    if (dollarsCollected < 0) {

                        gameFinished = true;
                        MyTimer.Stop();
                        mfReference.updateButton("lost");


                        for (int i = 0; i < rows; i++) {
                            for (int j = 0; j < cols; j++) {

                                if (grid[i][j] == 13) {
                                    grid[i][j] = -15;
                                    this.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
                                    continue;
                                }

                                else if ((grid[i][j] >= 100 && grid[i][j] != 113)) {
                                    grid[i][j] = -14;
                                    this.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
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
                                this.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++

                                for (int dr = -1; dr <= 1; dr++) {
                                    for (int dc = -1; dc <= 1; dc++) {
                                        if (i + dr >= 0 && j + dc >= 0 && i + dr < rows && j + dc < cols && grid[i + dr][j + dc] >= 1 && grid[i + dr][j + dc] <= 8) {

                                            openDigits(i + dr, j + dc);
                                            grid[i + dr][j + dc] *= -1;
                                            this.Invalidate(new Rectangle((j + dc) * cellSize, (i + dr) * cellSize, cellSize, cellSize));//+++                                
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
                    mfReference.updateButton("win");
                    Console.WriteLine("Wiiiiiin");

                    for (int i = 0; i < rows; i++) {//mark unmarked bombs
                        for (int j = 0; j < cols; j++) {

                            if (grid[i][j] == 13) {
                                grid[i][j] += 100;
                                this.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++

                            }
                        }
                    }

                    if (moneyLostPerGirl == 10000) {//record only 1 life games
                        mfReference.bestScores.manageScores(currentDifficulty, timeElapsed);
                    }

                    mfReference.bombCounter.setNumber(0);
                }

                
            }

        }



        private void openDigits(int r, int c) {
            demined++;
            dollarsCollected += grid[r][c];
            mfReference.setDollarsLabel("$ " + dollarsCollected);

        }


        private void markEmptyRecursively(int row, int col) {

            if (row - 1 >= 0 && grid[row - 1][col] == 0) {
                grid[row - 1][col] = 10;
                markEmptyRecursively(row - 1, col);
            }
            if (row + 1 < rows && grid[row + 1][col] == 0) {
                grid[row + 1][col] = 10;
                markEmptyRecursively(row + 1, col);
            }
            if (col - 1 >= 0 && grid[row][col - 1] == 0) {
                grid[row][col - 1] = 10;
                markEmptyRecursively(row, col - 1);
            }
            if (col + 1 < cols && grid[row][col + 1] == 0) {
                grid[row][col + 1] = 10;
                markEmptyRecursively(row, col + 1);
            }
        }

        public void startGame(bool newDimensions) {

            firstTouch = true;
            demined = 0;
            gameFinished = false;
            dollarsCollected = 0;
            mfReference.setDollarsLabel("$ 0");
            bombsMarked = 0;
            timeElapsed = 0;
            mfReference.timeCounter.setNumber(0);
            MyTimer.Stop();
            mfReference.bombCounter.setNumber(nOfBombs);

            if (!newDimensions) {//1st time or when grid resized, draw all grid

                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        if ((grid[i][j] < 0 || grid[i][j] >= 100)) {
                            this.Invalidate(new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize));//+++
                        }
                    }
                }
            }
            else {
                this.Invalidate();//+++
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
            for (int i = 0; i < nOfBombs; i++) {
                index = rnd.Next(0, allCells.Count);

                grid[allCells[index] / cols][allCells[index] % cols] += 13;//if user puts flag before 1st touch, it remains there! thus +13
                allCells.RemoveAt(index);
            }

            //numbers
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (grid[i][j] != 13 && grid[i][j] != 113) {
                        int nOfBombsNextDoor = 0;

                        for (int dr = -1; dr <= 1; dr++) {
                            for (int dc = -1; dc <= 1; dc++) {

                                if (i + dr >= 0 && j + dc >= 0 && i + dr < rows && j + dc < cols && grid[i + dr][j + dc] % 100 == 13) {
                                    nOfBombsNextDoor++;
                                }
                            }
                        }

                        grid[i][j] += nOfBombsNextDoor;

                    }

                }
            }

        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // SweeperPanel
            // 
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ResumeLayout(false);

        }















        //3 CASES
        //1) mouse unpressed, leave cells => table/form mouseMove event
        //2) mouse pressed, leave cells => part A in pictureBox'es mouseMove event (as mousepressed, old pb continues to send events)
        //3) move from 1 cell to another=> part B in pictureBox'es mouseMove event
        //also mark cell when first clicked; also use current cell (not sender) in mouseUp

        ////???
        private void mousePictureboxMove(object sender, MouseEventArgs e) {



            Point cursorTableRelative = this.PointToClient(Cursor.Position);


            int x = cursorTableRelative.X;
            int y = cursorTableRelative.Y;
            int r = y / cellSize;//if r or c invalid indexes, leaves in next if
            int c = x / cellSize;

            //1) entered revealed cell 2) left grid while pressed (sender remains SweeperPanel)
            if (x < 0 || y < 0 || x > cols * cellSize - 1 || y > rows * cellSize - 1 || grid[r][c] < 0) {
                if (previousCell != null) {
                    if (grid[previousCell.Item1][previousCell.Item2] < 100 && grid[previousCell.Item1][previousCell.Item2] >= 0) {//&& previousCursorOver.Enabled == true
                        Invalidate(new Rectangle(previousCell.Item2 * cellSize, previousCell.Item1 * cellSize, cellSize, cellSize));
                    }
                    previousCell = null;
                }
                return;
            }






            //part B
            //if (killMyBrain) {//nubas, taki udalil eto
            //    return;
            //}
            if (previousCell == null || (r != previousCell.Item1 || c != previousCell.Item2)) {//current cell is certainly not null
                if (previousCell != null && grid[previousCell.Item1][previousCell.Item2] < 100 && grid[previousCell.Item1][previousCell.Item2] >= 0) {
                    Invalidate(new Rectangle(previousCell.Item2 * cellSize, previousCell.Item1 * cellSize, cellSize, cellSize));
                }

                if (grid[r][c] >=100) {
                    previousCell = null;
                    return;
                }
                previousCell = new Tuple<int, int>(r, c);
                Invalidate(new Rectangle(c * cellSize, r * cellSize, cellSize, cellSize));

                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
                  //  pressed = true;//??
                }
                else if (e.Button == MouseButtons.None) {

                    pressed = false;
                }
            }




        } 

        





    }
}
