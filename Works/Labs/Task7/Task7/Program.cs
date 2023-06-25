using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    public class Program
    {
        static List<string[]> Booleans = new List<string[]>();
        static List<bool[]> Line = new List<bool[]>();
        public static bool[] Convert(string[] vectorP)
        {
            bool[] temp = new bool[vectorP.Length];
            for (int i = 0; i < vectorP.Length; i++)
                temp[i] = vectorP[i] == "1";
            return temp;
        }

        [ExcludeFromCodeCoverage]
        public static void Polinom(bool[] vector)
        {
            bool[] line = new bool[vector.Length];
            line[0] = vector[0];// метод неопределенных коэффициентов
            line[1] = line[0] ^ vector[1];// c1
            line[2] = line[0] ^ vector[2];//c2
            line[3] = line[0] ^ vector[3] ^ line[1] ^ line[2];//c3
            line[4] = line[0] ^ vector[4];//c4
            line[5] = line[0] ^ vector[5] ^ line[1] ^ line[4];//c5
            line[6] = line[0] ^ vector[6] ^ line[4] ^ line[2];//c6
            line[7] = line[0] ^ vector[7] ^ line[1] ^ line[2] ^ line[4] ^ line[3] ^ line[5] ^ line[6];//c7
            if (line[3] || line[5] || line[6] || line[7])//Проверка полинома
                return;
            else
                Line.Add(vector);
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            for (int x0 = 0; x0 < 2; x0++)
                for (int x1 = 0; x1 < 2; x1++)
                    for (int x2 = 0; x2 < 2; x2++)
                        for (int x3 = 0; x3 < 2; x3++)
                            for (int x4 = 0; x4 < 2; x4++)
                                for (int x5 = 0; x5 < 2; x5++)
                                    for (int x6 = 0; x6 < 2; x6++)
                                        for (int x7 = 0; x7 < 2; x7++)
                                            Booleans.Add(new string[] { x0.ToString(), x1.ToString(), x2.ToString(), x3.ToString(), x4.ToString(), x5.ToString(), x6.ToString(), x7.ToString() });

            Console.WriteLine("Булевые функции от 3 агументов, которые линейны, в лексикографичеком порядке");
            foreach (var item in Booleans)
            {
                Polinom(Convert(item));
            }

            foreach (var item in Line)
            {
                foreach (var x in item)
                {
                    if (x == true)
                        Console.Write("1");
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }
            //Проверка введенной функции на линейность
            Console.ReadLine();
        }
    }
}

