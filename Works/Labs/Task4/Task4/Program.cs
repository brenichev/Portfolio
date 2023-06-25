using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class Program
    {
        public static double func(double x)
        {
            return (x + Math.Log(x + 0.5) - 0.5);
        }

        public static double res(double xn, double xk, double eps)
        {
            double xi = 0;
            do
            {
                double dx = (xk - xn) / 2;
                xi = xn + dx;
                if (func(xn) * func(xk) < 0)
                    xk = xi;
                else
                    xn = xi;
            } while (Math.Abs(xk - xn) > eps);
            return xi;
        }

        public static double MyRound(double x, double eps)
        {
            int count = 0;
            while (eps % 10 < 1)
            {
                eps *= 10;
                count++;
            }
            if (count > 15)
                count = 15;
            return Math.Round(x, count, MidpointRounding.AwayFromZero);
        }

        [ExcludeFromCodeCoverage]
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Задание №4. Метод деления отрезка пополам.");
            Console.WriteLine("x + ln(x + 0.5) - 0.5 = 0, [0,2]");
            Console.WriteLine("Для выхода введите -1");

            double eps = 0;
            do
            {
                bool check = true;
                do
                {
                    Console.WriteLine("Введите точность вычисления: ");
                    check = double.TryParse(Console.ReadLine(), out eps);
                    if (!check)
                        Console.WriteLine("Неверный формат числа");
                    if (eps < 0 && eps != -1)
                    {
                        Console.WriteLine("Введите положительное число");
                        check = false;
                    }
                    if (eps == 0)
                    {
                        Console.WriteLine("Введите точность отличную от 0");
                        check = false;
                    }

                } while (!check);

                if (eps != -1)
                {
                    double xn, xk, xi;
                    xn = 0;
                    xk = 2;
                    xi = 0;
                    xi = res(xn, xk, eps / 10);
                    Console.WriteLine("Корень уравнения = " + MyRound(xi, eps) + " с точностью по y = " + eps + ", точное значение = " + xi);
                    Console.WriteLine("Корень уравнения = {0:C4}", xi);
                }
            } while (eps != -1);
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            try
            {
                Menu();
            }
            catch
            {
                Console.WriteLine("Было введено слишком большое число");
            }

        }
    }
}
