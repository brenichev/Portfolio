using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Program
    {
        [ExcludeFromCodeCoverage]
        static int Menu()
        {
            Console.Clear();
            //Console.CursorVisible = false;

            int current = 0;
            int previous = 0;
            int positionX = 5, positionY = 2;
            bool Chosen = false;

            string[] items = { "Заполнение матрицы инциденций случайным образом", "Ручное заполнение матрицы инциденций", "Выход" };

            //Вывод пунктов меню
            for (int i = 0; i < items.Length; i++)
            {
                Console.CursorLeft = positionX;
                Console.CursorTop = positionY + i;
                Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(items[i]);
            }

            do
            {

                Console.CursorLeft = positionX;
                Console.CursorTop = positionY + previous;
                Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(items[previous]);

                Console.CursorLeft = positionX;
                Console.CursorTop = positionY + current;
                Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(items[current]);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                previous = current;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        current++;
                        if (current >= items.Length)
                            current = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        current--;
                        if (current < 0)
                            current = items.Length - 1;
                        break;
                    case ConsoleKey.Enter:
                        Chosen = true;
                        break;
                }
            }
            while (!Chosen);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
            return current;
        }

        public static int Start(int[,] arr)
        {
            int start = arr.GetLength(0) - 1;
            int[] check = new int[arr.GetLength(0)];
            for (int i = 0; i < arr.GetLength(1); i++)
                for (int j = 0; j < arr.GetLength(0); j++)
                    if (arr[j, i] == 1)
                        check[j]++;
            if (check[start] == 0)
                return -1;
            for (int i = 0; i < check.GetLength(0); i++)
                if (check[start] > check[i])
                    if (check[start] == 0)
                        return -1;
                    else
                        start = i;
            return start;

        }

        public static void dfs(int t, bool[] Checked, int k, int r, int[,] arr, int i, int j, ref int cycle) //обход в глубину
        {
            if (t != -1)
            {
                Checked[t] = true;
                int n = arr.GetLength(0);
                int m = arr.GetLength(1);
                int p;

                for (i = k; i < n; i++)
                {
                    j = r;
                    if ((arr[i, j] != 0) && (!Checked[i]))
                    {
                        Checked[i] = true;
                        p = i;

                        cycle++;

                        for (j = 0; j < m; j++)
                        {
                            if (arr[i, j] != 0)
                            {
                                r = j;

                                for (k = 0; k < n; k++)
                                {

                                    if ((arr[k, j] != 0) && (!Checked[k]))
                                    {
                                        dfs(i, Checked, k, r, arr, i, j, ref cycle);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static int[,] RandomGraph(int n)
        {
            int[,] b = new int[n, n];
            int[,] a = new int[n, n - 1];
            int[] bLength = new int[n];
            Random rnd = new Random();
            int num = 0;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == j)
                        b[i, j] = -1;
                    else
                        b[i, j] = j;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n - 1; j++)
                    a[i, j] = 0;

            for (int i = 0; i < n; i++)
                bLength[i] = n - 1;
            for (int i = 0; i < n; i++)
            {
                b[i, i] = b[i, bLength[i]];
                bLength[i]--;
            }
            /*for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                    Console.Write(b[i, j] + " ");
                Console.WriteLine();
            }*/
            for (int i = 0; i < n - 1; i++)
            {
                num = rnd.Next(0, bLength[i]);
                if (b[i, num] == -1)
                {
                    b[i, num] = b[i, bLength[i]];
                    bLength[i]--;
                }

                a[i, i] = 1;
                a[b[i, num], i] = 1;
            }
            return a;
        }

        [ExcludeFromCodeCoverage]
        static void Show(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write(a[i, j] + " ");
                Console.WriteLine();
            }
        }

        [ExcludeFromCodeCoverage]
        public static int ReadInt(int left = 0, int right = 100, string message = "")
        {
            Console.WriteLine(message);
            bool ok = false;
            int number = 0;
            do
            {
                try
                {
                    number = int.Parse(Console.ReadLine());
                    if (number > left && number <= right) ok = true;
                    else
                    {
                        Console.WriteLine($"=== Неверно введено число. Введите число больше {left} и меньше {right} ===");
                        ok = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("=== Введено не целое число. Введите целое число. ===");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"=== Неверно введено число. Введите число больше {left} и меньше {right} ===");
                    ok = false;
                }
            } while (!ok);
            return number;
        }

        [ExcludeFromCodeCoverage]
        public static void ReadMatrix(ref int size, ref int[,] a)
        {            
            bool check = true;
            do
            {
                Console.WriteLine("Введите количество вершин графа");
                check = int.TryParse(Console.ReadLine(), out size);
            } while (!check);

            int[] Nums = new int[size];
            int i = 0;
            a = new int[size, size - 1];
            Console.WriteLine("Введите матрицу инцидентности графа размером " + size + " строк и " + (size - 1) + " столбцов");
            do
            {
                Console.WriteLine("Введите строку матрицы графа с номером " + (i + 1));
                string[] row = Console.ReadLine().Split(' ');
                check = true;
                int j = 0;
                if (row.Length != size - 1)
                    Console.WriteLine("Элементов в строке должно быть " + (size - 1) + ", введено = " + row.Length);
                else
                {
                    while (check && j < size - 1)
                    {
                        check = int.TryParse(row[j], out a[i, j]);
                        if (a[i, j] != 0 && a[i, j] != 1)
                            check = false;
                        j++;
                    }
                    if (check)
                        i++;
                }
                if (!check)
                    Console.WriteLine("У элемента с номером " + j + " неверный тип данных");
                //проверка матрицы на лишние 1
                if(i==size)
                {
                    int g = 0;
                    while(g<size-1 && check)
                    {
                        int sum = 0;
                        for (int f = 0; f < size; f++)
                            if (a[f, g] == 1)
                                sum++;
                        if(sum!=2)
                        {
                            check = false;
                            i = 0;
                            Console.WriteLine("Неверная матрица инциденций графа");
                        }
                        g++;
                    }
                }
            } while (i < size);
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            int[,] a = new int[1, 1];
            bool f = false;
            bool check = false;
            int k = 0;
            int r = 0;
            int i = 0;
            int j = 0;
            int cycle = 0;
            bool[] Checked = new bool[a.GetLength(0)];
            int num = 1;

            do
            {
                k = 0;
                r = 0;
                i = 0;
                j = 0;
                cycle = 0;
                Checked = new bool[a.GetLength(0)];

                switch (Menu())
                {

                    case 0:
                        Console.CursorTop += 3;
                        num = ReadInt(1, 100, "Введите количество вершин");
                        a = RandomGraph(num);
                        break;
                    case 1:
                        Console.CursorTop += 3;
                        ReadMatrix(ref num, ref a);
                        break;
                    case 2:
                        check = true;
                        break;
                }

                Checked = new bool[a.GetLength(0)];
                Show(a);
                Console.WriteLine();
                dfs(Start(a), Checked, k, r, a, i, j, ref cycle);
                if (a.GetLength(1) == cycle)
                    Console.WriteLine("Дерево");
                else
                    Console.WriteLine("Не дерево");
                Console.ReadLine();

            } while (!check);
        }
    }
}
