using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task6;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double a1 = 1, a2 = 2, a3 = 3;
            int N = 9;

            var expected = 5100538;

            Assert.AreEqual(expected, Program.SequenceT(N, a1, a2, a3));
        }
    }
}
