using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    class Program
    {
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
        static void Main(string[] args)
        {
            MyList list = new MyList();
            list.Task(ReadInt(1,1000000, "Введите количество элементов"));
            list.Show();

            Console.WriteLine();
            bool check = list.Remove(ReadInt(0, 1000000, "Введите элемент для удаления"));
            Console.WriteLine();
            if (check)
                Console.WriteLine("Элемент удален");
            else
                Console.WriteLine("Такого элемента нет");
            list.Show();


            Console.WriteLine();
            int find = list.Search(ReadInt(0, 1000000, "Введите элемент для поиска"));
            if (find == -1)
                Console.WriteLine("Такого элемента нет");
            else
            Console.WriteLine("Заданный элемент находится на " + find + " месте");
        }
    }
}
