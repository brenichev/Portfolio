using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_2
{
    public partial class Form1 : Form
    {
        int userChoice, userChoice2;
        string[] Num;

        int ReadNum(string number)
        {
            int numberInt = 0;
            bool ok = int.TryParse(number, out numberInt);
            if (!ok)
                textBox2.Text += "Возможно введено слишком большое число" + Environment.NewLine;
            return numberInt;
        }

        void ChangeColor(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    button1.BackColor = Color.White;
                    break;
                case 2:
                    button2.BackColor = Color.White;
                    break;
                case 3:
                    button3.BackColor = Color.White;
                    break;
                case 4:
                    button4.BackColor = Color.White;
                    break;
                case 5:
                    button5.BackColor = Color.White;
                    break;
            }
        }

        void WriteArray(int[] a, int size) //Вывод массива на экран
        {
            if (Data.size == 0)
                textBox1.Text = textBox1.Text + "Массив пустой";
            else
            {
                textBox1.Text = textBox1.Text + "Одномерный массив: ";
                textBox1.Text = textBox1.Text + Environment.NewLine;
                for (int i = 0; i <= Data.size - 1; i++)
                    textBox1.Text = textBox1.Text + Data.a[i] + " ";
            }
            textBox1.Text = textBox1.Text + Environment.NewLine;
        }

        void WriteArray(int[,] table, int row, int col) //Вывод массива на экран
        {
            if (Data.row == 0 || Data.col == 0)
                textBox1.Text = textBox1.Text + "Двумерный массив пустой";
            else
            {
                textBox1.AppendText("Двумерный массив: ");
                textBox1.AppendText(Environment.NewLine);
                for (int i = 0; i <= Data.row - 1; i++)
                {
                    for (int j = 0; j <= Data.col - 1; j++)
                        textBox1.AppendText(String.Format("{0,4}", Data.table[i, j]) + " ");
                    textBox1.AppendText(Environment.NewLine);
                }
            }
            textBox1.AppendText(Environment.NewLine);
        }

        void SearchArifm(int[] a, int size) //удалить элемент равный среднему арифметическому элементов массива
        {
            if (Data.size == 0)
            {
                textBox1.Text = "Одномерный массив пустой" + Environment.NewLine;
                return;
            }

            int sum = 0;
            for (int j = 0; j <= size - 1; j++)
            {
                sum = sum + a[j];
            }

            int srArifm = sum / Data.size;
            int number = 0;
            int found = -1;
            while (number <= (Data.size - 1) && Data.a[number] != srArifm)
            {

                if (Data.a[number] == srArifm)
                    found = number;
                number++;

            }

            if (Data.a[0] == srArifm)
                found = 0;
            int k = found;
            if (found != -1)
            {
                int[] temp = new int[size - 1];
                textBox1.Text = String.Format("Элемент с номером " + (found + 1) + " равен среднему арифметическому элементов массива = " + Data.a[found] + Environment.NewLine);
                for (int i = 0; i <= found - 1; i++)
                {
                    temp[i] = Data.a[i];
                }
                for (int i = found + 1; i <= size - 1; i++)
                {
                    temp[k] = Data.a[i];
                    k++;
                }
                Data.a = temp;
                Data.size = Data.size - 1;
                WriteArray(Data.a, Data.size);
            }
            else
            {
                textBox1.Text = "Такого элемента нет";
            }
        }

        void AddElemToArr(int[,] table, int row, int col) //Добавить столбец в конец матрицы
        {
            int[,] temp = new int[Data.row, Data.col];
            string[] Num = textBox2.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int newcol = Data.col;
            if (Num.Length < (Data.row) && Data.userChoice3 == 2)
                newcol = Data.col - 1;
            if (Data.EmptyArr != 1 && Data.table != null)
            {
                newcol = newcol + 1;
                temp = new int[Data.row, newcol];
                for (int i = 0; i <= Data.row - 1; i++)
                {
                    for (int j = 0; j <= Data.col - 1; j++)
                        temp[i, j] = Data.table[i, j];
                }
                Data.col = newcol;
            }

            switch (Data.userChoice3)
            {
                case 1:
                    Random r = new Random();
                    for (int i = 0; i <= Data.row - 1; i++)
                    {
                        temp[i, Data.col - 1] = r.Next(-100, 100);
                    }
                    Data.EmptyArr = 0;
                    break;

                case 2:
                    if (Num.Length < (Data.row))
                        textBox1.Text = "Введено меньше элементов чем указано " + Environment.NewLine;
                    else
                    {
                        for (int i = 0; i <= Data.row - 1; i++)
                        {
                            temp[i, Data.col - 1] = ReadNum(Num[i]);
                        }
                        Data.EmptyArr = 0;
                    }
                    break;
            }
            Data.table = temp;
            textBox2.Text = "";
            WriteArray(Data.table, Data.row, Data.col);
            return;
        }

        void DelFromArray(int[][] jag_arr, int row2) //Удалить строки, где есть число K
        {
            if (Data.jag_arr.Length == 0)
            {
                textBox1.Text += "Массив пустой";
                return;
            }
            int k = ReadNum(textBox2.Text);
            int foundk = -1;
            int m = 0;
            int[][] temp = new int[Data.row2][];
            for (int i = 0; i <= Data.row2 - 1; i++)
            {
                for (int j = 0; j <= Data.jag_arr[i].Length - 1; j++)
                {
                    if (Data.jag_arr[i][j] == k)
                    {
                        foundk = i;
                    }

                }
                if (foundk == -1)
                {
                    temp[m] = new int[Data.jag_arr[i].Length];
                    for (int j = 0; j < Data.jag_arr[i].Length; j++)
                        temp[m][j] = Data.jag_arr[i][j];
                    m++;
                }

                foundk = -1;
            }
            if (Data.row2 == m)
                textBox1.Text += "Такого элемента нет" + Environment.NewLine;
            Data.jag_arr = temp;
            Data.row2 = m;
            if (jag_arr.Length == 0)
                textBox1.Text += "Массив пустой" + Environment.NewLine;
            else
                WriteJagArray(Data.jag_arr, Data.row2);
            return;
        }

        void WriteJagArray(int[][] jag_arr, int row2)
        {
            if (Data.row2 == 0)
                textBox1.Text += "Рваный массив пустой " + Environment.NewLine;
            else
            {
                textBox1.Text += "Рваный массив";
                textBox1.Text += Environment.NewLine; //Переход на новую строку
                for (int i = 0; i < Data.row2; i++)
                {
                    for (int j = 0; j < Data.jag_arr[i].Length; j++)
                        textBox1.Text += String.Format("{0,4}", Data.jag_arr[i][j] + " ");
                    textBox1.Text += Environment.NewLine;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            button8.Location = new Point(410, button1.Location.Y); //расположение кнопок
            button9.Location = new Point(410, button2.Location.Y);
            button10.Location = new Point(410, button3.Location.Y);
            button6.Location = new Point(410, button1.Location.Y);
            button7.Location = new Point(410, button2.Location.Y);
            button11.Hide();
            button12.Hide();
            button8.Show();
            button9.Show();
            button10.Show();
            button16.Hide();
            button17.Hide();
            button18.Hide();
            textBox2.Hide();
            label1.Hide();
            button13.Hide();
            button14.Hide();
            button1.BackColor = Color.FromArgb(128, 255, 255); //Смена цвета активной операции
            if(userChoice!=1)
            ChangeColor(userChoice);
            userChoice = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button16.Location = new Point(410, button2.Location.Y); //расположение кнопок
            button17.Location = new Point(410, button3.Location.Y);
            button18.Location = new Point(410, button4.Location.Y);
            button6.Location = new Point(410, button2.Location.Y);
            button7.Location = new Point(410, button3.Location.Y);
            button11.Hide();
            button12.Hide();
            button16.Show();
            button17.Show();
            button18.Show();
            button8.Hide();
            button9.Hide();
            button10.Hide();
            textBox2.Hide();
            label1.Hide();
            button13.Hide();
            button14.Hide();
            button2.BackColor = Color.FromArgb(128, 255, 255);//Смена цвета активной операции
            if (userChoice != 2)
                ChangeColor(userChoice);
            userChoice = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button12.Hide();
            button16.Hide();
            button17.Hide();
            button18.Hide();
            textBox2.Hide();
            label1.Hide();
            button13.Hide();
            button14.Hide();
            button3.BackColor = Color.FromArgb(128, 255, 255);//Смена цвета активной операции
            if (userChoice != 3)
                ChangeColor(userChoice);
            SearchArifm(Data.a, Data.size);
            userChoice = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button16.Hide();
            button17.Hide();
            button18.Hide();
            textBox2.Hide();
            label1.Hide();
            button13.Hide();
            button14.Hide();
            button11.Location = new Point(410, button4.Location.Y);
            button12.Location = new Point(410, button5.Location.Y);
            button11.Show();
            button12.Show();
            button4.BackColor = Color.FromArgb(128, 255, 255);//Смена цвета активной операции
            if (userChoice != 4)
                ChangeColor(userChoice);
            userChoice = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button12.Hide();
            button16.Hide();
            button17.Hide();
            button18.Hide();            
            button5.BackColor = Color.FromArgb(128, 255, 255);//Смена цвета активной операции
            if (userChoice != 5)
                ChangeColor(userChoice);            
            if (Data.row2 == 0)
                textBox1.Text = "Массив пустой" + Environment.NewLine;
            else
            {
                WriteJagArray(Data.jag_arr, Data.row2);
                button14.Location = button13.Location;
                button14.Show();
                label1.Text = "Введите k";
                label1.Show();
                textBox2.Text = "";
                textBox2.Show();
                textBox2.Focus();
            }
            userChoice = 5;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button6.Show();
            button7.Show();
            userChoice2 = 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button7.Hide();
            button6.Hide();
            Data.userChoice3 = 1;
            switch (userChoice2)
            {
                case 1:
                    Form2 form2 = new Form2();
                    form2.Show();
                    break;
                case 2:
                    Form3 form3 = new Form3();
                    form3.Show();
                    break;
                case 3:
                    Form4 form4 = new Form4();
                    form4.Show();
                    break;
            }
            Hide();
            button1.BackColor = Color.White;
            userChoice = -1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Hide();
            button6.Hide();
            Data.userChoice3 = 2;
            switch (userChoice2)
            {
                case 1:
                    Form2 form2 = new Form2();
                    form2.Show();
                    break;
                case 2:
                    Form3 form3 = new Form3();
                    form3.Show();
                    break;
                case 3:
                    Form4 form4 = new Form4();
                    form4.Show();
                    break;
            }
            Hide();
            button1.BackColor = Color.White;
            userChoice = -1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button6.Show();
            button7.Show();
            userChoice2 = 2;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button6.Show();
            button7.Show();
            userChoice2 = 3;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Hide();
            button12.Hide();
            Data.userChoice3 = 1;
            if (Data.row == 0 || Data.col == 0)
            {
                Data.EmptyArr = 1;
                textBox2.Text = "";
                textBox2.Show();
                label1.Text = "Введите количество строк";
                label1.Show();
                button13.Show();
                textBox2.Focus();
            }
            else
                AddElemToArr(Data.table, Data.row, Data.col);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Data.EmptyArr == 1)
            {
                string[] Num = textBox2.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                Data.row = int.Parse(Num[0]);
                Data.col = 1;
                if (Data.userChoice3 == 2)
                {
                    Data.EmptyArr = 0;
                    label1.Text = "Введите столбец массива(сверху вниз, " + Data.row + " элементов)";
                    textBox2.Text = "";
                }
                else
                {
                    textBox2.Hide();
                    label1.Hide();
                    button13.Hide();
                    AddElemToArr(Data.table, Data.row, Data.col);
                }

            }
            else
            {
                AddElemToArr(Data.table, Data.row, Data.col);
                textBox2.Hide();
                label1.Hide();
                button13.Hide();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button11.Hide();
            button12.Hide();
            Data.userChoice3 = 2;
            if (Data.row == 0 || Data.col == 0)
            {
                Data.EmptyArr = 1;
                textBox2.Text = "";
                textBox2.Show();
                label1.Text = "Введите количество строк";
                label1.Show();
                button13.Show();
            }
            else
            {
                label1.Text = "Введите столбец массива(сверху вниз, " + Data.row + " элементов)";
                label1.Show();
                button13.Show();
                textBox2.Show();
            }
            textBox2.Focus();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DelFromArray(Data.jag_arr, Data.row2);
        }

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            button6.Hide();
            button7.Hide();
            button8.Hide();
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button12.Hide();
            textBox2.Hide();
            label1.Hide();
            button13.Hide();
            button14.Hide();
            button16.Hide();
            button17.Hide();
            button18.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            WriteArray(Data.a, Data.size);
            button16.Hide();
            button17.Hide();
            button18.Hide();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            WriteArray(Data.table, Data.row, Data.col);
            button16.Hide();
            button17.Hide();
            button18.Hide();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            WriteJagArray(Data.jag_arr, Data.row2);
            button16.Hide();
            button17.Hide();
            button18.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == ' ') && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == '\r'))
                e.Handled = true;
        }
    }
}
