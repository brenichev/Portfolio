using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task8;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var arr = new int[5, 4] { { 1, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 1, 1, 0 }, { 0, 0, 0, 1 }, { 0, 0, 0, 0 } };
            bool[] Checked = new bool[arr.GetLength(0)];
            var cycle = 0;

            Program.dfs(Program.Start(arr), Checked, 0, 0, arr, 0, 0, ref cycle);
            var expected = false;
            var actual = true;
            if (arr.GetLength(1) != cycle)
                actual = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var arr = new int[5, 4] { { 1,0,0,0 }, { 0,1,1,0 }, { 0,1,1,1 }, { 0,0,0,1 }, { 1,0,0,0 } };
            bool[] Checked = new bool[arr.GetLength(0)];
            var cycle = 0;

            Program.dfs(Program.Start(arr), Checked, 0, 0, arr, 0, 0, ref cycle);
            var expected = false;
            var actual = true;
            if (arr.GetLength(1) != cycle)
                actual = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var arr = new int[5, 4] { { 1,1,1,1 }, { 0,1,0,0 }, {0,0,1,0 }, { 0,0,0,1 }, { 1, 0, 0, 0 } };
            bool[] Checked = new bool[arr.GetLength(0)];
            var cycle = 0;

            Program.dfs(Program.Start(arr), Checked, 0, 0, arr, 0, 0, ref cycle);
            var expected = true;
            var actual = arr.GetLength(1) == cycle;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var arr = new int[5, 4];
            arr = Program.RandomGraph(10);
            bool[] Checked = new bool[arr.GetLength(0)];

            var expected = true;
            var actual = Program.Start(arr) != 0;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var arr = new int[5, 4] { { 1, 1, 1, 1 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 }, { 0, 0, 0, 0 } };
            bool[] Checked = new bool[arr.GetLength(0)];

            var expected = false;
            var actual = Program.Start(arr) != -1;

            Assert.AreEqual(expected, actual);
        }
    }
}
