using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Program
    {
        public static double Function(double a)
        {
            if (a < 1)
                return Math.Abs(a);
            else
                if (a >= 1 && a <= 2)
                return 1;
            else
                return -2 * a + 5;
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            double a = 0;
            bool check = true;
            do
            {
                Console.WriteLine("Введите действительное число");
                check = double.TryParse(Console.ReadLine(), out a);
                if (!check) Console.WriteLine("Введено неправильное число");
            } while (!check);

            Console.WriteLine(Function(a));
            Console.ReadLine();
        }
    }
}
