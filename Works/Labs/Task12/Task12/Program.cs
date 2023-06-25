using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class Program
    {
        public static void SortArr(ref int[,] arr, int i, int first, int last, ref int colComparBlock, ref int colTransBlock)
        {
            int L, R, c, x;
            colComparBlock++;
            if (first < last)
            {                
                x = arr[i, (first + last) / 2];
                L = first; R = last;
                colComparBlock++;
                while (L <= R)
                {
                    colComparBlock++;
                    while (arr[i, L] < x)
                    {
                        L++;
                    }
                    colComparBlock++;
                    while (arr[i, R] > x)
                    {
                        R--;
                    }
                    colComparBlock++;
                    if (L <= R)
                    {                        
                        colTransBlock += 3;
                        c = arr[i, L];
                        arr[i, L] = arr[i, R];
                        arr[i, R] = c;
                        L++;
                        R--;
                    }
                }
                SortArr(ref arr, i, first, R, ref colComparBlock, ref colTransBlock);
                SortArr(ref arr, i, L, last,  ref colComparBlock, ref colTransBlock);
            }
        }

        public static void BlockSort(ref int[] a, ref int colComparBlock, ref int colTransBlock)
        {
            int max = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] > max)
                    max = a[i];
            int[,] b = new int[max + a.Length, a.Length];
            int[] exist = new int[a.Length];
            int[] Sizes = new int[max + exist.Length];
            for (int i = 0; i < a.Length; i++)
            {
                colTransBlock += 2;
                b[a[i] / 10, Sizes[(a[i] / 10)]] = a[i];                
                Sizes[(a[i] / 10)] = Sizes[(a[i] / 10)] + 1;
                exist[i] = a[i] / 10;
            }

            for (int i = 0; i < exist.Length; i++)
            {
                    SortArr(ref b, i, 0, Sizes[exist[i]] - 1, ref colComparBlock, ref colTransBlock);
            }              

            int k = 0;
            for (int i = 0; i < max; i++)
                for (int j = 0; j < Sizes[i]; j++)
                {
                    colTransBlock++;
                    a[k] = b[i, j];
                    k++;
                }
        }

        public static void SelectionSort(ref int[] a, ref int colComparSelect, ref int colTransSelect)
        {
            int i, j, min, temp;
            for (i = 0; i < a.Length - 1; i++)
            {
                min = i;
                
                for (j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[min])
                    {                        
                        min = j;
                    }
                    colComparSelect++;
                }
                colTransSelect += 3;
                temp = a[i];
                a[i] = a[min];
                a[min] = temp;
            }
        }

        [ExcludeFromCodeCoverage]
        public static void ReadArray(ref int[] array)
        {
            int i = 0;
            bool check = true;
            do
            {
                string[] row = Console.ReadLine().Split(' ');
                check = true;
                if (row.Length != array.Length)
                {
                    Console.WriteLine("Элементов в строке должно быть " + array.Length + ", введено = " + row.Length);
                    check = false;
                }
                else
                {
                    while (check && i < array.Length)
                    {
                        check = int.TryParse(row[i], out array[i]);
                        i++;
                    }
                    if (!check)
                        Console.WriteLine("У элемента с номером " + i + " неверный тип данных");
                }
            } while (!check);
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            int origWidth = Console.WindowWidth * 2;
            int origHeight = Console.WindowHeight * 2;
            //Console.BufferWidth += 10;
            Console.SetWindowSize(origWidth, origHeight);
            int n = 1000;
            int colComparBlock = 0;
            int colTransBlock = 0;
            int colComparSelect = 0;
            int colTransSelect = 0;

            int[] a = new int[n];

            /*bool check = false;
            do
            {
                Console.WriteLine("Введите количество элементов");
                check = int.TryParse(Console.ReadLine(), out n);
            } while (!check);*/
            Console.WriteLine("Сортировка массива упорядоченного по возрастанию");
            Random rnd = new Random();
            for(int i = 0; i < n; i++)
            {
                if (i == 0)
                    a[i] = rnd.Next(1, 25);
                else
                    a[i] = a[i - 1] + rnd.Next(1, 25);
            }

            //ReadArray(ref a);

            int[] a2 = new int[n];
            a2 = a;

            BlockSort(ref a, ref colComparBlock, ref colTransBlock);
            SelectionSort(ref a2, ref colComparSelect, ref colTransSelect);

            Console.WriteLine("Количество сравнений блочной сортировки = " + colComparBlock);
            Console.WriteLine("Количество пересылок блочной сортировки = " + colTransBlock);
            Console.WriteLine("Количество сравнений сортировки выбором = " + colComparSelect);
            Console.WriteLine("Количество пересылок  сортировки выбором = " + colTransSelect);

            for (int i = 0; i < n; i++)
            {
                a[i] = a[n - i - 1];
            }

            a2 = a;
            Console.WriteLine();
            Console.WriteLine("Сортировка массива упорядоченного по убыванию");
            BlockSort(ref a, ref colComparBlock, ref colTransBlock);
            SelectionSort(ref a2, ref colComparSelect, ref colTransSelect);

            Console.WriteLine("Количество сравнений блочной сортировки = " + colComparBlock);
            Console.WriteLine("Количество пересылок блочной сортировки = " + colTransBlock);
            Console.WriteLine("Количество сравнений сортировки выбором = " + colComparSelect);
            Console.WriteLine("Количество пересылок  сортировки выбором = " + colTransSelect);

            Console.WriteLine();
            Console.WriteLine("Сортировка неупорядоченного массива");
            for (int i = 0; i < n; i++)
            {
                    a[i] = rnd.Next(0, 10000);
            }

            a2 = a;
            Console.WriteLine();
            Console.WriteLine("Сортировка массива упорядоченного по убыванию");
            BlockSort(ref a, ref colComparBlock, ref colTransBlock);
            SelectionSort(ref a2, ref colComparSelect, ref colTransSelect);

            Console.WriteLine("Количество сравнений блочной сортировки = " + colComparBlock);
            Console.WriteLine("Количество пересылок блочной сортировки = " + colTransBlock);
            Console.WriteLine("Количество сравнений сортировки выбором = " + colComparSelect);
            Console.WriteLine("Количество пересылок  сортировки выбором = " + colTransSelect);

            /*for (int i = 0; i < a.Length; i++)//вывод массива a
            {
                if (i % 10 == 0)
                    Console.WriteLine();
                Console.Write(a[i] + " ");
            }

            Console.WriteLine();
            for (int i = 0; i < a2.Length; i++)//вывод массива a2
            {
                if (i % 10 == 0)
                    Console.WriteLine();
                Console.Write(a2[i] + " ");
            }*/

        }
    }
}
