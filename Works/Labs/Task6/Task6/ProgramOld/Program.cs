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
        public static void Sequence(ref double[] a, int N, int last)
        {
            if (last < N)
                a[last] = 13 * a[last - 1] - 10 * a[last - 2] + a[last - 3];
            else
                return;
            last++;
            Sequence(ref a, N, last);
        }

        public static double SequenceT(int k)
        {
            if (k == 1) return a1;
            if (k == 2) return a2;
            if (k == 3) return a3;
            return(13 * SequenceT(k - 1) - 10 * SequenceT(k - 2) + SequenceT(k - 3));
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

            a[0] = a1;
            a[1] = a2;
            a[2] = a3;
            int last = 3;
            Array.Resize(ref a, N);
            Sequence(ref a, N, last);
            Console.WriteLine();

            int i = 1;
            bool grow = true;
            while (grow != false && i < N)
            {
                if (i + 2 < N)
                {
                    if (a[i] > a[i + 2])
                        grow = false;
                    i += 2;
                }
                else i = N;
            }

            int j = 0;
            while(j < N && !double.IsNaN(a[j]))
            {
                Console.Write(a[j] + " ");
                j++;
            }
            Console.WriteLine("Слишком большие значения в последовательности");
            if (double.IsNaN(a[j - 1]))
                Console.WriteLine("Слишком большие значения в последовательности");
            for (int w = 1; w <= N; w++)
                Console.WriteLine(SequenceT(w));
            Console.WriteLine();
            if (grow)
                Console.WriteLine("Элементы, стоящие на четных местах образуют возрастающую последовательность");
            else
                Console.WriteLine("Элементы, стоящие на четных местах НЕ образуют возрастающую последовательность");

            Console.ReadLine();
        }
    }
}
