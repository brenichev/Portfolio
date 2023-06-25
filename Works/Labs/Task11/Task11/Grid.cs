using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    public class Grid
    {
        const int n = 10;
        byte[,] grid = new byte[n, n];
        string str = "";
        string[,] matrix = new string[n, n];

        [ExcludeFromCodeCoverage]
        public Grid(byte[,] Grid, string Str)
        {
            grid = Grid;
            this.str = Str;
        }

        public Grid()
        {
            grid = new byte[n, n];
            this.str = "";
        }

        public string Str
        {
            get { return str; }
            set { str = value; }
        }

        public void Rotate(ref byte[,] gridTemp, int n)
        {
            byte[,] temp = new byte[n, n];
            temp = gridTemp;
            gridTemp = new byte[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    gridTemp[j, n - i - 1] = temp[i, j];
        }

        public void Encode()
        {
            byte[,] temp = new byte[n, n];
            temp = this.grid;
            int count = 0;
            while (count < str.Length)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (temp[i, j] == 1)
                        {
                            this.matrix[i, j] = str[count].ToString();
                            count++;
                        }
                Rotate(ref temp, n);
            }
            this.str = "";
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    this.str = this.str + matrix[i, j];
        }

        public void Decode()
        {
            byte[,] temp = new byte[n, n];
            temp = this.grid;
            string strTemp = "";
            while (strTemp.Length < n * n)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (temp[i, j] == 1)
                        {
                            strTemp = strTemp + matrix[i, j];
                        }
                Rotate(ref temp, n);
            }
            this.str = strTemp;
        }

        [ExcludeFromCodeCoverage]
        public static void ReadString()
        {
            Console.WriteLine("Введите последовательность из 100 букв для зашифровки");
            string str = "";
            do
            {
                str = Console.ReadLine();
                if (str.Length != 100)
                    Console.WriteLine("Последовательность должна состоять из 100 букв, введено = " + str.Length);
            } while (str.Length != 100);
        }

        [ExcludeFromCodeCoverage]
        public static Grid Read()
        {
            Console.WriteLine("Введите последовательность из 100 букв для зашифровки");
            string str = "";
            do
            {
                str = Console.ReadLine();
                if (str.Length != 100)
                    Console.WriteLine("Последовательность должна состоять из 100 букв, введено = " + str.Length);
            } while (str.Length != 100);


            byte[,] grid = new byte[n, n];
            bool check = true;
            do
            {
                int i = 0;
                check = true;
                do
                {
                    Console.WriteLine("Введите строку матрицы-ключа с номером " + (i + 1));
                    string[] row = Console.ReadLine().Split(' ');
                    check = true;
                    int j = 0;
                    if (row.Length != n)
                        Console.WriteLine("Элементов в строке должно быть " + n + ", введено = " + row.Length);
                    else
                    {
                        while (check && j < n)
                        {
                            check = byte.TryParse(row[j], out grid[i, j]);
                            if (grid[i, j] != 0 && grid[i, j] != 1)
                                check = false;
                            j++;
                        }
                        if (check)
                            i++;
                    }
                    if (!check)
                        Console.WriteLine("У элемента с номером " + j + " неверный тип данных");
                } while (i < n);
                int a = 0;
                int b = 0; 
                while(a < n/2 && check)
                {
                    while (b < n / 2 && check)
                    {
                        if (grid[a, b] + grid[b, n - a - 1] + grid[n - a - 1, n - b - 1] + grid[n - b - 1, a] != 1)
                            check = false;
                        b++;
                    }
                    a++;
                    b = 0;
                }
                if (!check)
                {
                    Console.WriteLine();
                    Console.WriteLine("Неверная матрица-ключ");
                }
            } while (!check);
            return new Grid(grid, str);
        }

        [ExcludeFromCodeCoverage]
        public void Test()
        {
            int count = 0;
            for(int i = 0;i<n/2;i++)
                {
                    if (grid[i, i] + grid[n - i - 1, i] + grid[i, n - i - 1] + grid[n - i - 1, n - i - 1] != 1)
                        count++;
                }
            Console.WriteLine(count);
        }

        public void Generate()
        {
            byte[,] temp = new byte[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    temp[i, j] = 0;
            Random rnd = new Random();
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n / 2; j++)
                    switch (rnd.Next(1, 5))
                    {
                        case 1:
                            temp[i, j] = 1;
                            break;
                        case 2:
                            temp[j, n - i - 1] = 1;
                            break;
                        case 3:
                            temp[n - i - 1, n - j - 1] = 1;
                            break;
                        case 4:
                            temp[n - j - 1, i] = 1;
                            break;
                    }
            }
            this.grid = temp;
        }

        [ExcludeFromCodeCoverage]
        public void Write()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Console.Write(matrix[i, j]);
        }
    }
}
