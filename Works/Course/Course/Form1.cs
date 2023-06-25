using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course;
using System.Reflection;

namespace Course
{
    public partial class Form1 : Form
    {
        static int maxWidth = 12;
        static int maxHeight = 12;
        static int[,] matrix = new int[maxHeight, maxWidth];
        static int[,] matrixCopy = new int[maxHeight, maxWidth];
        int[,] matrixPath = new int[1, 2];
        int xs = 1, ys = 1, xe = maxWidth - 2, ye = maxHeight - 2;
        int pathLength = 0;

        void Fill() // заполнение матрицы границами и 0
        {
            for (int i = 0; i <= maxHeight - 1; i++)
            {
                matrix[i, 0] = -1;
                matrix[i, maxWidth - 1] = -1;
            }
            for (int i = 1; i <= maxHeight - 2; i++)
                for (int j = 1; j <= maxWidth - 2; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i <= maxWidth - 1; i++)
            {
                matrix[0, i] = -1;
                matrix[maxHeight - 1, i] = -1;
            }
        }
        void Maze(int top, int down, int right, int left) // генерация лабиринта
        {
            if (down - top < 0 || right - left < 0)
                return;
            Random rnd = new Random();
            int vert = rnd.Next(left, right);
            int gor = rnd.Next(top, down);
            bool topLimit = false;
            while (matrix[gor, right + 2] == 0 || matrix[gor, left - 2] == 0)
            {
                if (gor - 1 >= top && topLimit == false)
                    gor--;
                else
                {
                    topLimit = true;
                    if (gor + 1 <= down)
                        gor++;
                    else return;
                }                    
            }
            topLimit = false;
            while (matrix[down + 2, vert] == 0 || matrix[top - 2, vert] == 0)
            {
                if (vert - 1 >= left && topLimit == false)
                    vert--;
                else
                {
                    topLimit = true;
                    if (vert + 1 <= right)
                        vert++;
                    else return;
                }                    
            }
            for (int i = top - 1; i <= down + 1; i++)
            {
                matrix[i, vert] = -1;
            }
            for (int i = left - 1; i <= right + 1; i++)
            {
                matrix[gor, i] = -1;
            }
            rnd = new Random();
            matrix[gor, rnd.Next(left - 1, vert - 1)] = 0;
            matrix[gor, rnd.Next(vert + 1, right + 1)] = 0;
            matrix[rnd.Next(top - 1, gor - 1), vert] = 0;
            matrix[rnd.Next(gor + 1, down + 1), vert] = 0;
            int block = rnd.Next(1, 4);
            switch (block)
            {
                case 1:
                    for (int i = left - 1; i <= vert - 1; i++)
                    {
                        matrix[gor, i] = -1;
                    }
                    break;
                case 2:
                    for (int i = vert + 1; i <= right + 1; i++)
                    {
                        matrix[gor, i] = -1;
                    }
                    break;
                case 3:
                    for (int i = top - 1; i <= gor - 1; i++)
                    {
                        matrix[i, vert] = -1;
                    }
                    break;
                case 4:
                    for (int i = gor + 1; i <= down + 1; i++)
                    {
                        matrix[i, vert] = -1;
                    }
                    break;
            }
            Maze(top, gor - 2, vert - 2, left); //левая верхняя
            Maze(top, gor - 2, right, vert + 2); //правая верхняя
            Maze(gor + 2, down, vert - 2, left); //левая нижняя
            Maze(gor + 2, down, right, vert + 2); //правая нижняя
        }
        new void Show() //отображение основы таблицы
        {
            dataGridView1.Rows.Clear();
            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = maxHeight * maxWidth;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            while (dataGridView1.ColumnCount < maxWidth)
            {
                dataGridView1.Columns.Add("", "");
                dataGridView1.Columns[dataGridView1.ColumnCount - 1].FillWeight = 1;
                progressBar1.PerformStep();
            }
            int cellSize = 50;
            if (maxHeight > 22 && maxWidth > 22)
            {
                dataGridView1.RowTemplate.MinimumHeight = 25;
                cellSize = 25;
            }
            for (int colCount = 0; colCount < maxWidth; colCount++)
            {
                dataGridView1.Columns[colCount].Width = cellSize;
                progressBar1.PerformStep();
            }
            dataGridView1.Rows.Add(maxHeight);
            for (int i = 0; i <= maxHeight - 1; i++)
            {
                for (int j = 0; j <= maxWidth - 1; j++)
                {
                    progressBar1.PerformStep();
                    if (matrix[i, j] == -1)
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Green;
                }
            }
            this.dataGridView1.CurrentCell = this.dataGridView1[0, 0]; this.dataGridView1.ClearSelection();
            dataGridView1.Rows[ys].Cells[xs].Style.BackColor = System.Drawing.Color.Blue;
            dataGridView1.Rows[ye].Cells[xe].Style.BackColor = System.Drawing.Color.Red;
            progressBar1.Visible = false;
        }

        void ShowDig() // отображение числовых значений
        {
            for (int i = 0; i <= maxHeight - 1; i++)
            {
                for (int j = 0; j <= maxWidth - 1; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
            this.dataGridView1.CurrentCell = this.dataGridView1[0, 0]; this.dataGridView1.ClearSelection();
        }

        void Wave(int[,] matrix, int xs, int ys, int xe, int ye) //распространение волны
        {
            matrix[ys, xs] = 1;
            int k = 1;
            int xc = -1;
            int yc = -1;
            bool end = true;
            while (end)
            {
                end = false;
                int ya = 1;
                while (ya < maxHeight - 1)
                {
                    int xa = 1;
                    while (xa < maxWidth - 1)
                    {
                        if (matrix[ya, xa] == k)
                        {
                            yc = -1;
                            while (yc < 2)
                            {
                                xc = -1;
                                while (xc < 2)
                                {
                                    if (matrix[ya + yc, xa + xc] == 0)
                                    {
                                        if (matrix[ya, xa + xc] != -1 && matrix[ya + yc, xa] != -1) // проверка на случай, если стены расположены диагонально  0 -1
                                                                                                    //                                                чтобы путь не проходил через них                       -1  0
                                            matrix[ya + yc, xa + xc] = k + 1;
                                        end = true;
                                    }
                                    xc++;
                                }
                                yc++;
                            }
                        }
                        xa++;
                    }
                    ya++;
                }
                if (matrix[ye, xe] > 0)
                    end = false;
                else
                    k++;
            }
        }

        void Path() // Восстановление пути
        {
            int newP = 0;
            matrixPath = new int[pathLength, 2];
            matrixPath[0, 0] = ye;
            matrixPath[0, 1] = xe;
            int k = pathLength - 1;
            int xa = xe;
            int ya = ye;
            bool found = false;
            while (k > 0)
            {
                if (matrix[ya - 1, xa] == k) //Всегда прямой путь, а не диагональный, если это возможно
                {
                    ya = ya - 1;
                }
                else
                if (matrix[ya, xa + 1] == k)
                {
                    xa = xa + 1;
                }
                else
                if (matrix[ya + 1, xa] == k)
                {
                    ya = ya + 1;
                }
                else
                if (matrix[ya, xa - 1] == k)
                {
                    xa = xa - 1;
                }
                else
                if (matrix[ya - 1, xa + 1] == k)
                {
                    xa = xa + 1;
                    ya = ya - 1;
                }
                else
                if (matrix[ya + 1, xa + 1] == k)
                {
                    xa = xa + 1;
                    ya = ya + 1;
                }
                else
                if (matrix[ya + 1, xa - 1] == k)
                {
                    xa = xa - 1;
                    ya = ya + 1;
                }
                else
                if (matrix[ya - 1, xa - 1] == k)
                {
                    xa = xa - 1;
                    ya = ya - 1;
                }
                newP++;
                matrixPath[newP, 0] = ya;
                matrixPath[newP, 1] = xa;
                k--;
                /*int yc = 0;
                while (yc < 2)
                {
                    int xc = 0;
                    while (xc < 2)
                    {
                        if (matrix[ya + yc, xa + xc] == k)
                        {
                            xa = xa + xc;
                            ya = ya + yc;
                            newP++;
                            matrixPath[newP, 0] = ya;
                            matrixPath[newP, 1] = xa;
                            yc = 2;
                            xc = 2;
                            k--;
                        }
                        if (xc == 1)
                            xc++;
                        else
                        if (xc == -1)
                            xc = 1;
                        else
                            xc--;
                    }
                    if (yc == 1)
                        yc++;
                    else
                        if (yc == -1)
                        yc = 1;
                    else
                        yc--;
                }*/
            }
        }

        private void button3_Click(object sender, EventArgs e) // выбор точки А
        {
            dataGridView1.Rows[ys].Cells[xs].Style.BackColor = System.Drawing.Color.White;
            xs = dataGridView1.CurrentCell.ColumnIndex;
            ys = dataGridView1.CurrentCell.RowIndex;
            if (matrixCopy[ys, xs] == -1)
            {
                MessageBox.Show("Нельзя поставить точку. Стена.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dataGridView1.CurrentCell.Style.BackColor = System.Drawing.Color.Blue;
                dataGridView1.Rows[ye].Cells[xe].Style.BackColor = System.Drawing.Color.Red;
            }

            this.dataGridView1.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e) // выбор точки Б
        {
            dataGridView1.Rows[ye].Cells[xe].Style.BackColor = System.Drawing.Color.White;
            xe = dataGridView1.CurrentCell.ColumnIndex;
            ye = dataGridView1.CurrentCell.RowIndex;
            if (matrixCopy[ye, xe] == -1)
            {
                MessageBox.Show("Нельзя поставить точку. Стена.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dataGridView1.CurrentCell.Style.BackColor = System.Drawing.Color.Red;
                dataGridView1.Rows[ys].Cells[xs].Style.BackColor = System.Drawing.Color.Blue;
            }
            this.dataGridView1.ClearSelection();
        }

        private void button5_Click(object sender, EventArgs e) // поиск пути
        {
            for (int i = 1; i <= pathLength - 2; i++) //очистка предыдущего пути
            {
                int x = matrixPath[i, 1];
                int y = matrixPath[i, 0];
                if (matrixCopy[y, x] != -1)
                    dataGridView1.Rows[y].Cells[x].Style.BackColor = System.Drawing.Color.White;
            }
            matrix = new int[maxHeight, maxWidth];
            matrix = (int[,])matrixCopy.Clone();
            //Show(); //???
            Wave(matrix, xs, ys, xe, ye);
            //ShowDig(); //отображение волн
            if (xs == xe && ys == ye)
                MessageBox.Show("Начальная и конечная точки совпадают", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            if (matrix[ye, xe] > 0)
            {
                pathLength = matrix[ye, xe];
                matrixPath = new int[pathLength, 2];
                Path();
                ShowPath(matrixPath);
            }
            else
            {
                MessageBox.Show("Пути не существует", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // добавление преград
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridView1.ClearSelection();
                int clickx = e.ColumnIndex;
                int clicky = e.RowIndex;
                if (matrixCopy[clicky, clickx] == 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Green;
                    matrixCopy[clicky, clickx] = -1;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.White;
                    matrixCopy[clicky, clickx] = 0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fill();
            Maze(2, maxHeight - 3, maxWidth - 3, 2);
            matrixCopy = (int[,])matrix.Clone();
            Show();
        }

        void ShowPath(int[,] matrixPath) //отображение пути в DGV
        {
            dataGridView1.Rows[ys].Cells[xs].Style.BackColor = System.Drawing.Color.Blue;
            dataGridView1.Rows[ye].Cells[xe].Style.BackColor = System.Drawing.Color.Red;
            for (int i = 1; i <= pathLength - 2; i++)
            {
                int x = matrixPath[i, 1];
                int y = matrixPath[i, 0];
                dataGridView1.Rows[y].Cells[x].Style.BackColor = System.Drawing.Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e) //создание матрицы по введенным данным
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;
            dataGridView1.RowTemplate.MinimumHeight = 50; //установка размера ячеек на 50(высота)
            maxHeight = int.Parse(numericUpDown1.Text) + 2; //+2 - ограничение поля стенами
            maxWidth = int.Parse(numericUpDown2.Text) + 2;
            xs = 1; ys = 1; xe = maxWidth - 2; ye = maxHeight - 2;
            matrix = new int[maxHeight, maxWidth];
            Fill();
            matrixCopy = (int[,])matrix.Clone();
            int columnsCount = dataGridView1.Columns.Count;
            for (int i = columnsCount - 1; i >= maxWidth; i--) //удаление лишних столбов после таблиц большего размера
            {
                dataGridView1.Columns.Remove(dataGridView1.Columns[i]);
            }
            Show();
        }


        public Form1()
        {
            InitializeComponent();
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dataGridView1, new object[] { true }); // ускорение отрисовки DGV
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;
            dataGridView1.RowTemplate.MinimumHeight = 50;
            matrix = new int[maxHeight, maxWidth];
            matrixCopy = new int[maxHeight, maxWidth];
            Fill();
            matrixCopy = (int[,])matrix.Clone();
            Show();
        }
    }
}
