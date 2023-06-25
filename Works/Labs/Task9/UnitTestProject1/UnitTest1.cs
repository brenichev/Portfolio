using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task9;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MyList list = new MyList();
            list.Task(5);

            MyList expected = new MyList();
            for (int i = 1; i < 4; i++)
                expected.Add(i);
            expected.Add(5);

            list.Remove(5);
            var actual = list.Search(4);
            var expectedRes = expected.Search(4);

            Assert.AreEqual(expectedRes, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            MyList list = new MyList();
            list.Task(5);

            MyList expected = new MyList();
            for (int i = 2; i < 5; i++)
                expected.Add(i);
            expected.Add(5);

            list.Remove(1);
            var actual = list.Search(1);
            var expectedRes = expected.Search(1);

            Assert.AreEqual(expectedRes, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            MyList list = new MyList();
            list.Task(5);

            MyList expected = new MyList();
            for (int i = 1; i < 5; i++)
                expected.Add(i);
            expected.Add(5);

            var actual = list.Search(6);
            var expectedRes = expected.Search(6);

            Assert.AreEqual(expectedRes, actual);
        }
    }
}
