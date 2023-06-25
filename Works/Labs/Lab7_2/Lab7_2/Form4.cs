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
    public partial class Form4 : Form
    {
        int ReadNum(string number)
        {
            int numberInt = 0;
            bool ok = int.TryParse(number, out numberInt);
            if (!ok)
                textBox2.Text += "Возможно введено слишком большое число" + Environment.NewLine;
            return numberInt;
        }

        int[][] CreateJagArray(int row2) //Формирование рваного массива
        {            
            Data.jag_arr = new int[Data.row2][];
            Random r = new Random();
            int col2 = 0;
            string[] Num = textBox3.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] Num2 = textBox1.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int k = 0;        
            if (int.Parse(numericUpDown1.Text) > Num.Length)
                textBox2.Text += "Введено элементов меньше чем указано" + Environment.NewLine;
            else
            switch (Data.userChoice3)
            {
                case 1:                    
                    for (int i = 0; i <= Data.row2 - 1; i++)
                    {
                        col2 = ReadNum(Num[i]);
                        Data.jag_arr[i] = new int[col2];
                        for (int j = 0; j <= col2 - 1; j++)
                            Data.jag_arr[i][j] = r.Next(-100, 100);
                    }
                    WriteJagArray(Data.jag_arr, Data.row2);
                    break;

                case 2:                    
                    {
                        for (int i = 0; i <= Data.row2 - 1; i++)
                        {
                            col2 = ReadNum(Num[i]);
                            Data.jag_arr[i] = new int[col2];

                            for (int j = 0; j <= col2 - 1; j++)
                            {
                                Data.jag_arr[i][j] = ReadNum(Num2[k]);
                                k++;
                            }

                        }
                        WriteJagArray(Data.jag_arr, Data.row2);
                    }                    
                    break;
            }            
            return Data.jag_arr;
        }

        void WriteJagArray(int[][] jag_arr, int row2)
        {
            if (Data.jag_arr.Length == 0)
                textBox2.Text += "Массив пустой ";
            else
            {
                textBox2.Text += "Рваный массив";
                textBox2.Text += Environment.NewLine;
                for (int i = 0; i < Data.row2; i++)
                {
                    for (int j = 0; j < Data.jag_arr[i].Length; j++)
                        textBox2.Text += String.Format("{0,4}", Data.jag_arr[i][j] + " ");
                    textBox2.Text += Environment.NewLine;
                }
            }
                
        }

        public Form4()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            numericUpDown1.Text = "";
            if (Data.userChoice3 == 1)
            {
                textBox1.Hide();
                label3.Hide();
                button1.Location = new Point(button1.Location.X, textBox1.Location.Y + 20);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.row2 = ReadNum(numericUpDown1.Text);
            textBox2.Text = "";
            CreateJagArray(Data.row2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
                Form1 form1 = new Form1();
                form1.Show();
                Hide();            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == ' ') && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == '\r') && !(e.KeyChar == '-'))
                e.Handled = true;
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar == ' ') && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == '\r'))
                e.Handled = true;
        }
    }
}
