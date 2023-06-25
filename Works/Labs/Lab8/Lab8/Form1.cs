using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    struct Library
    {
        static public string author;
        public string name;
        public int age;
        public string publisher;

        static public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { if (value > 0 && value < 2021) age = value; else age = 0; }
        }

        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        public Library(string A, string N, int Age, string P)
        {
            author = A; name = N; age = Age; publisher = P;
        }

        public void Input()
        {
            Console.Write("Автор:"); author = Console.ReadLine();
            Console.Write("Название"); name = Console.ReadLine();
            Console.Write("Год издания:"); age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Издательство:"); publisher = Console.ReadLine();

        }


    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Library p1 = new Library(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text), textBox4.Text);
            textBox5.Text = Library.Author;
        }
    }
}
