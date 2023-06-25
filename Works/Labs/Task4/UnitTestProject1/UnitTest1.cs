using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var expected = -1.1931471805599454;
                           
            var res = Program.func(0);

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var expected = 0.4375;

            var res = Program.res(0, 2, 0.1);

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var expected = 0.5;

            var res = Program.MyRound(0.49, 0.1);

            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var expected = 0.49;

            var res = Program.MyRound(0.49, 0.00000000000000001);

            Assert.AreEqual(expected, res);
        }
    }
}
