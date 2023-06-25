using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        int userChoice, userChoice2, userChoice3;
        public int[] a;
        int[,] table = new int[0, 0];
        string[] Num;
        int row;
        int col;

        int ReadNum(string message, int MinNum, int MaxNum) //Ввод числа и проверка на правильность ввода
        {
            bool ok = false;
            int number = 0;
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                ok = int.TryParse(input, out number);
                if (!ok)
                    Console.WriteLine("Введено не целое число ");
                else
                    if (number < MinNum)
                {
                    Console.WriteLine("Число должно быть больше {0} ", MinNum - 1);
                    ok = false;
                }
                else
                        if (number > MaxNum)
                {
                    Console.WriteLine("Число должно быть меньше {0} ", MaxNum + 1);
                    ok = false;
                }
            } while (!ok);

            return number;
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

        int[] CreateArray(int size, int userChoice3) //Выбор варианта формирования массива
        {
            int[] a = new int[size];
            switch (userChoice3)
            {
                case 1:
                    a = CreateRandomArray(size);
                    break;

                case 2:
                    a = ReadArray(size);
                    break;
            }
            return a;
        }

        int[,] CreateArray(int row, int col, int userChoice3) //Выбор варианта формирования массива
        {
            int[,] table = new int[row, col];
            switch (userChoice3)
            {
                case 1:
                    table = CreateRandomArray(row, col);
                    break;

                case 2:
                    table = ReadArray(row, col);
                    break;
            }
            return table;
        }

        int[] ReadArray(int size) //Ввод массива с клавиатуры
        {
            string[] Num = richTextBox1.Text.Split(' ');            
            for (int i = 0; i <= size - 1; i++)
            {
                Data.a[i] = int.Parse(Num[i]);
            }
            WriteArray(Data.a, size);
            return a;
        }

        int[,] ReadArray(int row, int col) //Ввод массива с клавиатуры
        {
            int[,] table = new int[row, col];
            if (row > 0 && col > 0)
                Console.WriteLine("Введите массив(по элементу в строке) ");
            string[] Num = richTextBox1.Text.Split(' ');
            for (int i = 0; i <= row - 1; i++)
            {
                for (int j = 0; j <= col - 1; j++)
                    table[i, j] = int.Parse(Num[i+j]);
                Console.WriteLine("Следующая строка ");
            }
            WriteArray(table, row, col);
            return table;
        }

        int[] CreateRandomArray(int size) //Формирование массива ДСЧ
        {
            int[] a = new int[size];
            Random r = new Random();
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = r.Next(-100, 100);
            }
            WriteArray(a, size);
            return a;
        }

        int[,] CreateRandomArray(int row, int col) //Формирование массива ДСЧ
        {

            int[,] table = new int[row, col];
            Random r = new Random();
            for (int i = 0; i <= row - 1; i++)
            {
                for (int j = 0; j <= col - 1; j++)
                    table[i, j] = r.Next(-100, 100);
            }
            WriteArray(table, row, col);
            return table;
        }

        void WriteArray(int[] a, int size) //Вывод массива на экран
        {
            if (a.Length == 0)
                richTextBox2.Text = richTextBox2.Text + "Массив пустой";
            else
            {
                richTextBox2.Text = "Одномерный массив: ";
                richTextBox2.Text = richTextBox2.Text + Environment.NewLine;
                for (int i = 0; i <= size - 1; i++)
                    richTextBox2.Text = richTextBox2.Text + a[i] + " ";
            }
            richTextBox2.Text = richTextBox2.Text + Environment.NewLine;
        }

        void WriteArray(int[,] table, int row, int col) //Вывод массива на экран
        {
            if (table.Length == 0)
                richTextBox2.Text = richTextBox2.Text + "Двумерный массив пустой";
            else
            {
                richTextBox2.Text = "Двумерный массив: ";
                richTextBox2.Text = richTextBox2.Text + Environment.NewLine;
                for (int i = 0; i <= row - 1; i++)
                {
                    for (int j = 0; j <= col - 1; j++)
                        richTextBox2.Text += String.Format("{0,4}", table[i, j]) + " ";
                    richTextBox2.Text = richTextBox2.Text + Environment.NewLine;
                }
            }                
            richTextBox2.Text = richTextBox2.Text + Environment.NewLine;
        }

        /* static int[,] CreateArray(int row, int col) //Выбор варианта формирования массива
         {
             int userChoice = 1;
             int[,] table = new int[row, col];           
             switch (userChoice)
             {
                 case 1:
                     table = CreateRandomArray(row, col);
                     break;

                 case 2:
                     table = ReadArray(row, col);
                     break;
             }
             return table;
         }*/
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            label1.Text = "";            
            button7.Hide();
            button6.Hide();
            button9.Hide();
            button10.Hide();
            button11.Hide();
        }

        private void richTextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
         {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == ' '))
                e.Handled = true;
        }

        private void richTextBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
              e.Handled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button9.Location = new Point(292, button1.Location.Y);
            button10.Location = new Point(292, button2.Location.Y);
            button11.Location = new Point(292, button3.Location.Y);
            button9.Show();
            button10.Show();
            button11.Show();
            richTextBox1.Text = "";
            button1.BackColor = Color.FromArgb(128, 255, 255);
            ChangeColor(userChoice);
            userChoice = 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button9.Location = new Point(292, button2.Location.Y);
            button10.Location = new Point(292, button3.Location.Y);
            button11.Location = new Point(292, button3.Location.Y + 36);
            button9.Show();
            button10.Show();
            button11.Show();
            richTextBox1.Text = "";
            button2.BackColor = Color.FromArgb(128, 255, 255);
            ChangeColor(userChoice);
            userChoice = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(128, 255, 255);
            ChangeColor(userChoice);
            userChoice = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(128, 255, 255);
            ChangeColor(userChoice);
            userChoice = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(128, 255, 255);
            ChangeColor(userChoice);
            userChoice = 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button7.Hide();
            button6.Hide();
            label1.Text = "Введите количество элементов: ";
            userChoice3 = 1;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button6.Location = new Point(292, button1.Location.Y);
            button7.Location = new Point(292, button2.Location.Y);
            button6.Show();
            button7.Show();            
            label1.Text = "Введите количество элементов: ";
            userChoice2 = 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button6.Location = new Point(292, button1.Location.Y);
            button7.Location = new Point(292, button2.Location.Y);
            button6.Show();
            button7.Show();            
            label1.Text = "Введите количество элементов: ";
            userChoice2 = 2;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button6.Location = new Point(292, button1.Location.Y);
            button7.Location = new Point(292, button2.Location.Y);
            button6.Show();
            button7.Show();            
            label1.Text = "Введите количество элементов: ";
            userChoice2 = 3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Hide();
            button6.Hide();
            label1.Text = "Введите количество элементов: ";
            userChoice3 = 2;
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            switch (userChoice)
            {
                case 1:
                    switch (userChoice2)
                    {
                        case 1:
                            switch(userChoice3)
                            {
                                case 1:
                                    Data.size = int.Parse(richTextBox1.Text);
                                    Data.a = new int[Data.size];
                                    Data.a = CreateArray(Data.size, userChoice3);
                                    label1.Text = "";
                                    richTextBox1.Text = "";
                                    break;

                                case 2:
                                    Data.size = int.Parse(richTextBox1.Text);
                                    label1.Text = "Введите массив ";
                                    richTextBox1.Text = "";
                                    userChoice = 7;
                                    break;
                            }
                            break;

                        case 2:
                            switch (userChoice3)
                            {
                                case 1:
                                    Num = richTextBox1.Text.Split(' ');
                                    row = int.Parse(Num[0]);
                                    col = int.Parse(Num[1]);
                                    table = new int[row, col];
                                    table = CreateArray(row, col, userChoice3);                                    
                                    label1.Text = "";
                                    richTextBox1.Text = "";
                                    break;

                                case 2:
                                    Num = richTextBox1.Text.Split(' ');
                                    row = int.Parse(Num[0]);
                                    col = int.Parse(Num[1]);
                                    table = new int[row, col];
                                    label1.Text = "Введите массив ";
                                    richTextBox1.Text = "";
                                    userChoice = 8;
                                    break;
                            }
                            

                            break;
                    }
                break;

                case 7:
                    Data.a = new int[Data.size];
                    Data.a = CreateArray(Data.size, userChoice3);
                    label1.Text = "";
                    richTextBox1.Text = "";
                    break;
            }
        }
    }

    static class Data
    {
        static public int[] a { get; set; }
        static public int size { get; set; }
    }
}
