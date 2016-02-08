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
    public partial class Form1 : Form {
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
        Bitmap six = Resources.five;
        Bitmap seven = Resources.five;
        Bitmap eight = Resources.five;
        Bitmap empty = Resources.empty;
        Bitmap flag = Resources.flag;
        Bitmap bomb = Resources.bomb;
        Bitmap bombBad = Resources.bombBad;
        Bitmap bombWrong = Resources.bombWrong;

        Bitmap whileHovered = Resources.whileHovered;
        Bitmap whilePressed = Resources.whilePressed;

        TableLayoutPanel tableLayoutPanel1;
        PictureBox previousCursorOver = null;
        public Form1() {

            InitializeComponent();

            initializeTable(9,9, 10);

            button1.Click += new EventHandler(newGame);//test
            this.MouseMove += new MouseEventHandler(mouseTableAndFramelMove);
        }

    

        private void initializeTable(int r, int c, int n) {
            this.rows = r;
            this.cols = c;
            this.nOfBombs = n;
            int cellSize = 17;

            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            
            
           

            tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(43, 74);
            tableLayoutPanel1.Size = new Size(cols * (cellSize+1) + 1, rows * (cellSize + 1)+1);//take 1px border into account
            tableLayoutPanel1.ColumnCount = cols;
            tableLayoutPanel1.RowCount = rows;
           

            for (int i = 0; i < rows; i++) {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, cellSize));
            }

            for (int j = 0; j < cols; j++) {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellSize));
            }

            tableLayoutPanel1.MouseMove += new MouseEventHandler(mouseTableAndFramelMove);
            

            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {

                    PictureBox pb = new PictureBox();


                    pb.Margin = new Padding(0);


                    pb.MouseMove += new MouseEventHandler(mousePictureboxMove);

                    pb.MouseUp += new MouseEventHandler(digitEventHandler);
                    pb.MouseDown += new MouseEventHandler(mdHandler);


                    
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
                }
                else if (((PictureBox)sender).Image == flag) {
                    ((PictureBox)sender).Image = null;
                    bombsMarked--;

                }
                killMyBrain = true;

            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (((PictureBox)sender).Image != flag) {
                    ((PictureBox)sender).Image = whilePressed;
                }
            }



               
        }

        private void mouseTableAndFramelMove(object sender, MouseEventArgs e) {//when leave cells, do dehighlight animation
            if (previousCursorOver != null) {//when pictureBox is disabled, table starts capturing events
                if (previousCursorOver.Image!= flag) {
                    previousCursorOver.Image = null;
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
                //Console.WriteLine("hehe");
                if (previousCursorOver != null) {
                    if (previousCursorOver.Image != flag) {
                        previousCursorOver.Image = null;
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
                if (previousCursorOver != null && previousCursorOver.Image!=flag) {
                    previousCursorOver.Image = null;
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

                }

                if (grid[r][c] > 0) {
                    openDigits(r, c, source);
                    
                }
                else if (grid[r][c] == -1) {
                    source.BackgroundImage = bombBad;

                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < cols; j++) {
                            PictureBox nextCell = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i );
                            nextCell.Enabled = false;
                            Console.WriteLine("1");
                            if (grid[i][j]==-1 && nextCell.BackgroundImage == null) {
                                Console.WriteLine("2");
                                nextCell.BackgroundImage = bomb;
                                continue;
                            }

                            if ((grid[i][j] != -1 && nextCell.Image == flag)) {
                                nextCell.BackgroundImage = bombWrong;
                            }
                                

                        }
                    }

                    //stop timer
                           
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
                                green.BackgroundImage = empty;
                                //System.Threading.Thread.Sleep(100);
                            }//end of if ==-2
                        }
                    }

                    // DrawingControl.ResumeDrawing(this);
                }

               
                if (demined == rows * cols - nOfBombs) {
                    Console.WriteLine("Wiiiiiin");

                    for (int i = 0; i < rows; i++) {//mark unmarked bombs
                        for (int j = 0; j < cols; j++) {

                            if (grid[i][j] == -1) {
                                ((PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Image = flag;
                            }
                        }
                    }

                    //stop timer
                    //record results, top score
                }
               
            }

        }


        private void openDigits(int r, int c, PictureBox source) {
            demined++;
            if (grid[r][c] == 1) {
                source.BackgroundImage = one;
            }
            else if (grid[r][c] == 2) {
                source.BackgroundImage = two;
            }
            else if (grid[r][c] == 3) {
                source.BackgroundImage = three;
            }
            else if (grid[r][c] == 4) {
                source.BackgroundImage = four;
            }
            else if (grid[r][c] == 5) {
                source.BackgroundImage = five;
            }
            else if (grid[r][c] == 6) {
                source.BackgroundImage = six;
            }
            else if (grid[r][c] == 7) {
                source.BackgroundImage = seven;
            }
            else if (grid[r][c] == 8) {
                source.BackgroundImage = eight;
            }
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
            startGame();
        }

        private void startGame() {

            firstTouch = true;
            demined = 0;
            bombsMarked = 0;
            grid = new List<List<int>>();
            for (int i = 0; i < rows; i++) {
                List<int> nextRow = new List<int>();
                for (int j = 0; j < cols; j++) {
                    PictureBox con = (PictureBox)tableLayoutPanel1.GetControlFromPosition(j, i);
                    con.Enabled = true;
                    con.BackgroundImage = null;

                    nextRow.Add(0);

                }

                grid.Add(nextRow);
            }
           

        }

        private void placeBombs(int r, int c) {
            int numOfCells = rows * cols;

           // Random rnd = new Random((int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 1000));
            Random rnd = new Random(1);

            List<int> allCells = new List<int>();//to avoid repeated random numbers
            for (int i = 0; i < numOfCells; i++) {
                allCells.Add(i);
            }

            //cells around 1st touch cannot contain bombs!
            for (int dr = -1; dr <= 1; dr++) {
                for (int dc = -1; dc <= 1; dc++) {

                    if (r + dr >= 0 && c + dc >= 0 && r + dr < rows && c + dc < cols ) {
                        allCells.RemoveAt(allCells.IndexOf((r + dr) * cols + c + dc)); 
                    }
                }
            }

            //bombs
            int index;
            for (int i=0;i< nOfBombs; i++) { 
                index = rnd.Next(0, allCells.Count);
               
                grid[allCells[index] / cols][allCells[index] % cols] = -1;

                //hide this
                //Button con = (Button)tableLayoutPanel1.GetControlFromPosition(allCells[index] % cols, allCells[index] / cols);
               // con.BackgroundBackgroundImage = bomb;
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

        

        private void intermediate16x1640MinesToolStripMenuItem_Click(object sender, EventArgs e) {
       
            tableLayoutPanel1.Dispose();
            initializeTable(16,16,40);
            startGame();
        }

        private void beginner9x916MinesToolStripMenuItem_Click(object sender, EventArgs e) {
            tableLayoutPanel1.Dispose();
            initializeTable(9, 9, 10);
            startGame();
        }
    }
}

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