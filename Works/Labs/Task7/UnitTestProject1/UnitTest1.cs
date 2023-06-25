using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task7;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        static List<bool[]> Line = new List<bool[]>();
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

        [TestMethod]
        public void TestMethod1()
        {
            string[] str = new string[8] { "1", "0", "0", "1", "1", "0", "0", "1" };
            
            Polinom(Program.Convert(str));

            bool[] expected = new bool[] { true, false, false, true, true, false, false, true };
            bool[] actual = new bool[8];

            foreach (var item in Line)
                actual = item;

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethod2()
        {
            string[] str = new string[8] { "0", "0", "0", "0", "0", "0", "0", "1" };
            Line = new List<bool[]>();

            Polinom(Program.Convert(str));

            bool[] expected = new bool[8];
            bool[] actual = new bool[8];

            foreach (var item in Line)
                actual = item;

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
