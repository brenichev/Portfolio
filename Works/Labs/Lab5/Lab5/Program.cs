using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static int ReadNum(string message, int MinNum, int MaxNum) //Ввод числа и проверка на правильность ввода
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
        static int[] CreateArray(int size) //Выбор варианта формирования массива
        {
            Console.WriteLine("Выберите тип ввода массива ");
            Console.WriteLine("1 - Формирование массива ДСЧ ");
            Console.WriteLine("2 - Ввод массива с клавиатуры ");
            int userChoice = 1;
            int[] a = new int[size];
            userChoice = ReadNum("Введите число ", 1, 2);
            switch (userChoice)
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

        static int[,] CreateArray(int row, int col) //Выбор варианта формирования массива
        {
            Console.WriteLine("Выберите тип ввода массива ");
            Console.WriteLine("1 - Формирование массива ДСЧ ");
            Console.WriteLine("2 - Ввод массива с клавиатуры ");
            int userChoice = 1;
            int[,] table = new int[row, col];
            userChoice = ReadNum("Введите число ", 1, 2);
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
        }

        static int[][] CreateJagArray(int row2) //Формирование рваного массива
        {
            Console.WriteLine("Выберите тип ввода массива ");
            Console.WriteLine("1 - Формирование массива ДСЧ ");
            Console.WriteLine("2 - Ввод массива с клавиатуры ");
            int userChoice2 = 1;
            int[][] jag_arr = new int[row2][];
            Random r = new Random();
            int col2 = 0;
            userChoice2 = ReadNum("Введите число ", 1, 2);
            switch (userChoice2)
            {
                case 1:
                    for (int i = 0; i <= row2 - 1; i++)
                    {
                        col2 = ReadNum("Введите количество столбцов ", 0, 100);
                        jag_arr[i] = new int[col2];
                        for (int j = 0; j <= col2 - 1; j++)
                            jag_arr[i][j] = r.Next(-100, 100);
                    }
                    break;

                case 2:
                    for (int i = 0; i <= row2 - 1; i++)
                    {
                        col2 = ReadNum("Введите количество столбцов ", 0, 100);
                        jag_arr[i] = new int[col2];
                        Console.WriteLine("Введите строку рваного массива ");
                        for (int j = 0; j <= col2 - 1; j++)
                            jag_arr[i][j] = ReadNum("", -100, 100);
                    }
                    break;
            }
            WriteJagArray(jag_arr, row2);
            return jag_arr;
        }

        static int[] CreateRandomArray(int size) //Формирование массива ДСЧ
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

        static int[,] CreateRandomArray(int row, int col) //Формирование массива ДСЧ
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

        static int[] ReadArray(int size) //Ввод массива с клавиатуры
        {
            int[] a = new int[size];
            if (size > 0)
                Console.WriteLine("Введите массив(по элементу в строке) ");
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = ReadNum("", -100, 100);
            }
            WriteArray(a, size);
            return a;
        }

        static void WriteArray(int[] a, int size) //Вывод массива на экран
        {
            if (a.Length == 0)
                Console.WriteLine("Массив пустой");
            else
                for (int i = 0; i <= size - 1; i++)
                    Console.Write(a[i] + " ");
            Console.WriteLine();
        }

        static int[,] ReadArray(int row, int col) //Ввод массива с клавиатуры
        {
            int[,] table = new int[row, col];
            if (row > 0 && col > 0)
                Console.WriteLine("Введите массив(по элементу в строке) ");
            for (int i = 0; i <= row - 1; i++)
            {
                for (int j = 0; j <= col - 1; j++)
                    table[i, j] = ReadNum("", -100, 100);
                Console.WriteLine("Следующая строка ");
            }
            WriteArray(table, row, col);
            return table;
        }

        static void WriteArray(int[,] table, int row, int col) //Вывод массива на экран
        {
            if (table.Length == 0)
                Console.WriteLine("Массив пустой ");
            else
                for (int i = 0; i <= row - 1; i++)
                {
                    for (int j = 0; j <= col - 1; j++)
                        Console.Write("{0,3} ", table[i, j]);
                    Console.WriteLine();
                }
            Console.WriteLine();
        }

        static void WriteJagArray(int[][] jag_arr, int row2)
        {
            if (jag_arr.Length == 0)
                Console.WriteLine("Массив пустой ");
            else
                for (int i = 0; i < row2; i++)
                {
                    for (int j = 0; j < jag_arr[i].Length; j++)
                        Console.Write("{0,3} ", jag_arr[i][j]);
                    Console.WriteLine();
                }
        }

        static void SearchArifm(ref int[] a, ref int size) //удалить элемент равный среднему арифметическому элементов массива
        {
            if (a.Length == 0)
            {
                Console.WriteLine("Одномерный массив пустой");
                return;
            }

            int sum = 0;
            for (int j = 0; j <= size - 1; j++)
            {
                sum = sum + a[j];
            }

            int srArifm = sum / size;
            int number = 0;
            int found = -1;
            while (number <= size - 1 && a[number] != srArifm)
            {
                number++;
                if (a[number] == srArifm)
                    found = number;
                
            }
            
            if (a[0] == srArifm)
                found = 0;
            int k = found;
            if (found != -1)
            {
                int[] temp = new int[size - 1];
                Console.WriteLine("Элемент с номером {0} равен среднему арифметическому элементов массива = {1}", found + 1, a[found]);
                for (int i = 0; i <= found - 1; i++)
                {
                    temp[i] = a[i];
                }
                for (int i = found + 1; i <= size - 1; i++)
                {
                    temp[k] = a[i];
                    k++;
                }
                a = temp;
                size = size - 1;
                WriteArray(a, size);
            }
            else
            {
                Console.WriteLine("Такого элемента нет");
            }
        }

        static void AddElemToArr(ref int[,] table, ref int row, ref int col) //Добавить столбец в конец матрицы
        {
            int t = 1;
            int newcol = col + 1;
            int[,] temp = new int[row, newcol];
            for (int i = 0; i <= row - 1; i++)
            {
                for (int j = 0; j <= col - 1; j++)
                    temp[i, j] = table[i, j];
            }
            int p = 0;
            if (table.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                Console.WriteLine("Добавить 1 столбец в массив? 1 - да, 2 - нет");
                t = ReadNum("Введите число ", 1, 2);
                p = 1;
            }
            if (t == 1)
            {
                Console.WriteLine("Выберите тип ввода столбца массива ");
                Console.WriteLine("1 - Формирование столбца ДСЧ ");
                Console.WriteLine("2 - Ввод столбца с клавиатуры ");
                int userChoice = 1;
                userChoice = ReadNum("Введите число ", 1, 2);
                if(p==1)
                {
                    row = ReadNum("Введите количество строк ", 0, 100);
                    temp = new int[row, newcol];
                }                
                switch (userChoice)
                {
                    case 1:
                        Random r = new Random();
                        for (int i = 0; i <= row - 1; i++)
                        {
                            temp[i, col] = r.Next(-100, 100);
                        }
                        break;

                    case 2:
                        for (int i = 0; i <= row - 1; i++)
                        {
                            temp[i, col] = ReadNum("Введите элемент для вставки ", -100, 100);
                        }
                        break;
                }
                table = temp;
                col = newcol;                
            }
            WriteArray(table, row, col);
            return;
        }

        static void DelFromArray(ref int[][] jag_arr, ref int row2) //Удалить строки, где есть число K
        {
            if (jag_arr.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return;
            }
            int k = ReadNum("Введите число K, строки с которым нужно удалить ", -100, 100);
            int foundk = -1;
            int m = 0;
            int[][] temp = new int[row2][];
            for (int i = 0; i <= row2 - 1; i++)
            {
                for (int j = 0; j <= jag_arr[i].Length - 1; j++)
                {
                    if (jag_arr[i][j] == k)
                    {
                        foundk = i;
                    }

                }
                if (foundk == -1)
                {
                    temp[m] = new int[jag_arr[i].Length];
                    for (int j = 0; j < jag_arr[i].Length; j++)
                        temp[m][j] = jag_arr[i][j];
                    m++;
                }

                foundk = -1;
            }
            jag_arr = temp;
            row2 = m;
            if (jag_arr.Length == 0)
                Console.WriteLine("Пустой массив");
            else
                WriteJagArray(jag_arr, row2);
            return;
        }


        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.WindowWidth + 20, Console.WindowHeight + 10);
            int userChoice = 1;
            int size = 0;
            int row = 0, col = 0;
            int[] a = new int[size];
            int[,] table = new int[row, col];
            int row2 = 0;
            int[][] jag_arr = new int[row2][];

            do
            {
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Выберите операцию");
                        Console.WriteLine("1 - создание одномерного массива");
                        Console.WriteLine("2 - создание двумерного массива");
                        Console.WriteLine("3 - создание рваного массива");
                        userChoice = ReadNum("Введите число ", 1, 3);
                        switch (userChoice)
                        {
                            case 1:
                                size = ReadNum("Введите количество элементов массива ", 0, 100);
                                a = new int[size];
                                a = CreateArray(size);
                                break;
                            case 2:
                                row = ReadNum("Введите количество строк ", 0, 100);
                                col = ReadNum("Введите количество столбцов ", 0, 100);
                                table = new int[row, col];
                                table = CreateArray(row, col);
                                break;
                            case 3:
                                row2 = ReadNum("Введите количество строк ", 0, 100);
                                jag_arr = new int[row2][];
                                jag_arr = CreateJagArray(row2);
                                break;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Выберите операцию");
                        Console.WriteLine("1 - вывод одномерного массива");
                        Console.WriteLine("2 - вывод двумерного массива");
                        Console.WriteLine("3 - вывод рваного массива");
                        userChoice = ReadNum("Введите число ", 1, 3);
                        switch (userChoice)
                        {
                            case 1:
                                WriteArray(a, size);
                                break;
                            case 2:
                                WriteArray(table, row, col);
                                break;
                            case 3:
                                WriteJagArray(jag_arr, row2);
                                break;

                        }
                        break;

                    case 3:
                        SearchArifm(ref a, ref size);
                        break;

                    case 4:
                        AddElemToArr(ref table, ref row, ref col);
                        break;
                    case 5:
                        DelFromArray(ref jag_arr, ref row2);
                        break;

                }

                Console.WriteLine("Выберите операцию");
                Console.WriteLine("1 - создание массива");
                Console.WriteLine("2 - вывод массива на экран");
                Console.WriteLine("3 - удаление элемента равного среднему арифметическому элементов в одномерном массиве");
                Console.WriteLine("4 - добавить столбец в конец матрицы");
                Console.WriteLine("5 - удалить строки, в которых встречается K");
                Console.WriteLine("0 - выход");
                userChoice = ReadNum("Введите число ", 0, 5);

            } while (userChoice != 0);

        }
    }
}
