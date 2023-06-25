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
    public partial class Form3 : Form
    {
        int ReadNum(string number)
        {
            int numberInt = 0;
            bool ok = int.TryParse(number, out numberInt);
            if (!ok)
                textBox2.Text += "Возможно введено слишком большое число" + Environment.NewLine;
            return numberInt;
        }

        void CreateRandomArray(int row, int col) //Формирование массива ДСЧ
        {

            Data.table = new int[Data.row, Data.col];
            Random r = new Random();
            for (int i = 0; i <= Data.row - 1; i++)
            {
                for (int j = 0; j <= Data.col - 1; j++)
                    Data.table[i, j] = r.Next(-100, 100);
            }
            WriteArray(Data.table, Data.row, Data.col);            
        }

        void ReadArray(int row, int col) //Ввод массива с клавиатуры
        {
            Data.table = new int[Data.row, Data.col];
            int k = 0;
            string[] Num = textBox1.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            sum = Data.row * Data.col;
            if (sum > Num.Length)
                textBox2.Text += "Введено элементов меньше чем указано";
            else
            {
                for (int i = 0; i <= Data.row - 1; i++)
                {
                    for (int j = 0; j <= Data.col - 1; j++)
                    {
                        Data.table[i, j] = ReadNum(Num[k]);
                        k++;
                    }

                }
                WriteArray(Data.table, Data.row, Data.col);
            }                               
        }

        void WriteArray(int[,] table, int row, int col) //Вывод массива на экран
        {
            if (table.Length == 0)
                textBox2.Text = textBox2.Text + "Двумерный массив пустой";
            else
            {
                textBox2.Text = textBox2.Text + "Двумерный массив: ";
                textBox2.Text = textBox2.Text + Environment.NewLine;
                for (int i = 0; i <= Data.row - 1; i++)
                {
                    for (int j = 0; j <= Data.col - 1; j++)
                        textBox2.Text += String.Format("{0,4}", table[i, j]) + " ";
                    textBox2.Text = textBox2.Text + Environment.NewLine;
                }
            }
            textBox2.Text = textBox2.Text + Environment.NewLine;
        }

        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (Data.userChoice3 == 1)
            {
                textBox1.Hide();
                label3.Hide();
                button1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + 20);
            }
            numericUpDown1.Text = "";
            numericUpDown2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.row = ReadNum(numericUpDown1.Text);
            Data.col = ReadNum(numericUpDown2.Text);
            textBox2.Text = "";
            switch (Data.userChoice3)
            {
                case 1:
                    CreateRandomArray(Data.row, Data.col);
                    break;
                case 2:
                    ReadArray(Data.row, Data.col);
                    break;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == ' ') && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == '\r') && !(e.KeyChar == '-'))
                e.Handled = true;
        }
    }
}
