using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task12;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var n = 1000;
            var a = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                a[i] = rnd.Next(0, 10000);
            }
            var a2 = a;
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;

            Program.BlockSort(ref a, ref x1, ref x2);
            Program.SelectionSort(ref a2, ref y1, ref y2);

            CollectionAssert.AreEqual(a, a2);
        }
    }
}
