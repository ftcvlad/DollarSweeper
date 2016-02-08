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

        TableLayoutPanel tableLayoutPanel1;

        public Form1() {

            InitializeComponent();

            initializeTable(9,9, 10);

            button1.Click += new EventHandler(newGame);//test
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

            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {

                    Button b = new Button();

                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;

                   
                    b.Margin = new Padding(0, 0, 0, 0);
                    b.MouseUp += new MouseEventHandler(digitEventHandler);
                  
                    b.Tag = i * c + j;
                    tableLayoutPanel1.Controls.Add(b, j, i);
                   
                }
            }
            tableLayoutPanel1.ResumeLayout();
            this.Controls.Add(tableLayoutPanel1);


           
           
        }








        //protected override void OnMouseDown(MouseEventArgs e) {
        //    mouseDown = false;

        //    SetSelectionRect();
        //    Invalidate();

        //    GetSelectedTextBoxes();
        //}








        private void digitEventHandler(object sender, MouseEventArgs e) {
            Button source = (Button)sender;
            int loc = Int32.Parse(source.Tag.ToString());
            int r = loc / cols;
            int c = loc % cols;

            if (e.Button == System.Windows.Forms.MouseButtons.Right) {


                if (source.BackgroundImage ==null ) {//add flag
                    source.BackgroundImage = flag;
                    bombsMarked++;
                }
                else if (source.BackgroundImage == flag) {
                    source.BackgroundImage = null;
                    bombsMarked--;

                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left) {

                if (source.BackgroundImage == flag) {
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
                            Button nextCell = (Button)tableLayoutPanel1.GetControlFromPosition(j, i );
                            nextCell.Enabled = false;
                            if (grid[i][j]==-1 && nextCell.BackgroundImage == null) {
                                nextCell.BackgroundImage = bomb;
                                continue;
                            }

                            if ((grid[i][j] != -1 && nextCell.BackgroundImage == flag)) {
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

                                        if (i + dr >= 0 && j + dc >= 0 && i + dr < rows && j + dc < cols && grid[i + dr][j + dc] != -2 && grid[i + dr][j + dc] != -3 && grid[i + dr][j + dc] != 0 && ((Button)tableLayoutPanel1.GetControlFromPosition(j+dc, i+dr)).BackgroundImage != flag) {//!=0 is some bug case!

                                            Button borderDigit = (Button)tableLayoutPanel1.GetControlFromPosition(j + dc, i + dr);
                                            openDigits(i + dr, j + dc, borderDigit);


                                            grid[i + dr][j + dc] = -3;
                                            borderDigit.Enabled = false;
                                           
                                        }


                                    }
                                }
                              


                                Button green = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                                demined++;
                                green.BackgroundImage = empty;
                                //System.Threading.Thread.Sleep(100);
                            }//end of if ==-2
                        }
                    }

                    // DrawingControl.ResumeDrawing(this);
                }

                Console.WriteLine("+"+demined);
                if (demined == rows * cols - nOfBombs) {
                    Console.WriteLine("Wiiiiiin");

                    for (int i = 0; i < rows; i++) {//mark unmarked bombs
                        for (int j = 0; j < cols; j++) {

                            if (grid[i][j] == -1) {
                                ((Button)tableLayoutPanel1.GetControlFromPosition(j, i)).BackgroundImage = flag;
                            }
                        }
                    }

                    //stop timer
                    //record results, top score
                }
               
            }



            

        }


        private void openDigits(int r, int c, Button source) {
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
                if (((Button)tableLayoutPanel1.GetControlFromPosition(col, row-1)).BackgroundImage != flag) {
                    grid[row - 1][col] = -2;
                    markEmptyRecursively(row - 1, col);
                }
                
            }
            if (row + 1 < rows && grid[row + 1][col] == 0) {
                if (((Button)tableLayoutPanel1.GetControlFromPosition(col, row+1)).BackgroundImage != flag) {
                    grid[row + 1][col] = -2;
                    markEmptyRecursively(row + 1, col);
                }
               
            }
            if (col - 1 >= 0 && grid[row][col-1] == 0) {
                if (((Button)tableLayoutPanel1.GetControlFromPosition(col-1, row)).BackgroundImage != flag) {
                    grid[row][col - 1] = -2;
                    markEmptyRecursively(row, col - 1);
                }
                
            }
            if (col + 1 < cols && grid[row ][col+1] == 0) {
                if (((Button)tableLayoutPanel1.GetControlFromPosition(col+1, row)).BackgroundImage != flag) {
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
                    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
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
               // con.BackgroundImage = bomb;
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
                        //    con.BackgroundImage = one;
                        //}
                        //else if (nOfBombsNextDoor == 2) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundImage = two;
                        //}
                        //else if (nOfBombsNextDoor == 3) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundImage = three;
                        //}
                        //else if (nOfBombsNextDoor == 4) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundImage = four;
                        //}
                        //else if (nOfBombsNextDoor == 5) {
                        //    Button con = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                        //    con.BackgroundImage = five;
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