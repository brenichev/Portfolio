using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var num = 1.5;

            var res = Program.Function(num);

            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var num = 4;

            var res = Program.Function(num);

            Assert.AreEqual(-3, res);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var num = 1;

            var res = Program.Function(num);

            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var num = 0;

            var res = Program.Function(num);

            Assert.AreEqual(0, res);
        }
    }
}
