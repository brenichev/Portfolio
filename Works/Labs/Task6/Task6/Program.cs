using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class Program
    {
        static double a1, a2, a3;

        public static double SequenceT(int k, double a1, double a2, double a3)
        {
            if (k == 1) return a1;
            if (k == 2) return a2;
            if (k == 3) return a3;
            return (13 * SequenceT(k - 1, a1, a2, a3) - 10 * SequenceT(k - 2, a1, a2, a3) + SequenceT(k - 3, a1, a2, a3));
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            int origWidth = Console.WindowWidth * 2;
            int origHeight = Console.WindowHeight * 2;
            Console.SetWindowSize(origWidth, origHeight);

            Console.WriteLine("Задание №6. Ввести а1, а2, а3, N. Построить последовательность чисел ак = 13*а(к – 1) – 10*а(к-2) + а(к–3).");
            Console.WriteLine("Построить N элементов последовательности проверить, образуют ли элементы, стоящие на четных местах, возрастающую подпоследовательность.");
            Console.WriteLine("Для выхода введите -1");
            Console.WriteLine();
            bool check = true;
            int N = 0;
            double[] a = new double[3];
            do
            {
                check = true;
                Console.WriteLine("Введите a1, a2, a3, N (в одной строке, через пробел)");
                string s = Console.ReadLine();
                string[] input = s.Split(' ');
                if (input.Length != 4)
                {
                    Console.WriteLine("Неверный ввод");
                    check = false;
                }
                else
                {
                    check = double.TryParse(input[0], out a1);
                    if (check)
                    {
                        check = double.TryParse(input[1], out a2);
                        if (check)
                        {
                            check = double.TryParse(input[2], out a3);
                            if (check)
                            {
                                check = int.TryParse(input[3], out N);
                            }
                        }
                    }
                    if (N < 3)
                    {
                        Console.WriteLine("N не может быть меньше 4");
                        check = false;
                    }
                    if (!check)
                        Console.WriteLine("Неверный ввод");
                }
            } while (check != true);

            Console.WriteLine();

            int i = 1;
            bool grow = true;
            while (grow != false && i < N)
            {
                if (i + 2 < N)
                {
                    if (SequenceT(i, a1, a2, a3) > SequenceT(i + 2, a1, a2, a3))
                        grow = false;
                    i += 2;
                }
                else i = N;
            }

            for (int w = 1; w <= N; w++)
            {
                double temp = 0;
                temp = SequenceT(w, a1, a2, a3);
                if (!double.IsNaN(temp))
                    Console.Write(SequenceT(w, a1, a2, a3) + " ");
                else
                {
                    Console.WriteLine("Слишком большие значения в последовательности");
                    w = N + 1;
                }
            }

            Console.WriteLine();
            if (grow)
                Console.WriteLine("Элементы, стоящие на четных местах образуют возрастающую последовательность");
            else
                Console.WriteLine("Элементы, стоящие на четных местах НЕ образуют возрастающую последовательность");

            Console.ReadLine();
        }
    }
}
