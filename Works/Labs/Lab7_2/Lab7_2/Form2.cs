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
    public partial class Form2 : Form
    {
        int ReadNum(string number)
        {
            int numberInt = 0;
            bool ok = int.TryParse(number, out numberInt);
            if (!ok)
                textBox2.Text += "Возможно введено слишком большое число" + Environment.NewLine;
            return numberInt;
        }

        void ReadArray(int size) //Ввод массива с клавиатуры
        {
            Data.a = new int[Data.size];
            string[] Num = textBox1.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            if (Num.Length < Data.size)
                textBox2.Text = "Введено меньше элементов чем указано";
            else
            {
                for (int i = 0; i <= size - 1; i++)
                {
                    Data.a[i] = ReadNum(Num[i]);
                }
                WriteArray(Data.a, size);
            }                       
        }

        void CreateRandomArray(int size) //Формирование массива ДСЧ
        {
            int[] a = new int[size];
            Random r = new Random();
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = r.Next(-100, 100);
            }
            Data.a = a;
            WriteArray(Data.a, Data.size);            
        }
        /*
        int[,] ReadArray(int row, int col) //Ввод массива с клавиатуры
        {
            int[,] table = new int[row, col];
            if (row > 0 && col > 0)
                Console.WriteLine("Введите массив(по элементу в строке) ");
            string[] Num = textBox1.Text.Split(' ');
            for (int i = 0; i <= row - 1; i++)
            {
                for (int j = 0; j <= col - 1; j++)
                    table[i, j] = int.Parse(Num[i + j]);
                Console.WriteLine("Следующая строка ");
            }
            WriteArray(table, row, col);
            return table;
        }*/

        void WriteArray(int[] a, int size) //Вывод массива на экран
        {
            if (a.Length == 0)
                textBox2.Text = textBox2.Text + "Массив пустой";
            else
            {
                textBox2.Text += "Одномерный массив: ";
                textBox2.Text = textBox2.Text + Environment.NewLine;
                for (int i = 0; i <= size - 1; i++)
                    textBox2.Text = textBox2.Text + a[i] + " ";
            }
            textBox2.Text = textBox2.Text + Environment.NewLine;
        }

        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (Data.userChoice3 == 1)
            {
                textBox1.Hide();
                label2.Hide();
                button1.Location = new Point(button1.Location.X, textBox1.Location.Y + 20);
            }
            textBox1.Text = "";
            numericUpDown1.Text = "";
            textBox2.Text = "";
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Data.size = ReadNum(numericUpDown1.Text);
            textBox2.Text = "";
            switch(Data.userChoice3)
            {
                case 1:
                    CreateRandomArray(Data.size);
                    break;
                case 2:
                    ReadArray(Data.size);
                    break;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == ' ') && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == '-'))
                e.Handled = true;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
