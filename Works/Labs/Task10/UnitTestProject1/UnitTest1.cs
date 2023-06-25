using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task10;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte[,] matr = new byte[,] { { 0, 1, 0, 1 }, { 0, 0, 1, 0 }, { 1, 0, 0, 1 }, { 0, 0, 0, 0 } };
            int[] val = new int[] { 1, 2, 2, 3 };
            Graph graph = new Graph(4, val, matr);

            var expected = new byte[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 0, 0, 0 } };
            graph.Constrict(2);

            CollectionAssert.AreEqual(expected, graph.Matrix);
        }

        [TestMethod]
        public void TestMethod2()
        {
            byte[,] matr = new byte[,] { { 0, 1, 0, 1 }, { 0, 0, 1, 0 }, { 1, 0, 0, 1 }, { 0, 0, 0, 0 } };
            int[] val = new int[] { 1, 2, 2, 3 };
            Graph graph = new Graph(4, val, matr);

            byte[,] expected = new byte[,] { { 0, 1, 0, 1 }, { 0, 0, 1, 0 }, { 1, 0, 0, 1 }, { 0, 0, 0, 0 } };
            graph.Constrict(4);

            CollectionAssert.AreEqual(expected, graph.Matrix);
        }
    }
}
