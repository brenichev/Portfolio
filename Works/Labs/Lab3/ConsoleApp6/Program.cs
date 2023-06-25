using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            double y;
            double x, x2;
            int n = 10;            
            double sn = 0, se = 0;
            double e = 0.0001;
            double a = 0.1, b = 0.8;
            int k = 10;
            double step = (b - a) / k;
            b = b + 0.01; //При 0.8 цикл не выполняется, зависит от типа(в типе float цикл выполняется)
            double xn;

            for (x = a; x <= b; x = x + step)
            {
                y = x * Math.Atan(x) - Math.Log(Math.Sqrt(1 + x * x));
                x2 = x * x;
                xn = x2;
                for(int i = 1; i <= n; i++)
                {
                    
                    sn = sn + xn / (2 * i) / (2 * i - 1);
                    xn = xn * -x2;
                }

                xn = -1;
                int i1 = 1;
                do
                {
                    xn = xn * -x2;
                    se = se + xn / (2 * i1) / (2 * i1 - 1);                                        
                    i1++;
                } while (Math.Abs((xn / (2 * i1) / (2 * i1 - 1))) >= e);

                Console.WriteLine("X = {0:N2}    SN = {1:N7}    SE = {2:N7}    Y = {3:N7}", x, sn, se, y);
                Console.WriteLine();
                sn = 0;
                se = 0;
            }
            Console.ReadLine();
        }
    }
}
