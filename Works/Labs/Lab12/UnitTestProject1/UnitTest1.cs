using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab12;
using Lab10;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new UnidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToEnd(new Organization("Организация3", "Город3", 303));

            var expectedList = new UnidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 503));
            expectedList.AddToEnd(new Organization("Организация3", "Город3", 303));

            Organization actual = null;
            Organization expected = null;
            int i = 0;
            foreach (Organization org1 in list)
            {
                if (i == 2)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 2)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var list = new UnidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToEnd(new Organization("Организация3", "Город3", 303));

            var expectedList = new UnidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 503));
            expectedList.AddToEnd(new Organization("Организация3", "Город3", 303));

            Assert.AreEqual(list.Count, expectedList.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var list = new UnidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new UnidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 503));
            expectedList.AddToStart(new Organization("Организация3", "Город3", 303));

            Organization actual = null;
            Organization expected = null;
            int i = 0;
            foreach (Organization org1 in list)
                if (i == 0)
                    actual = org1;

            i = 0;
            foreach (Organization org2 in expectedList)
                if (i == 0)
                    expected = org2;

            expectedList.Show();
            list.Show();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var list = new UnidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new UnidirectionalList();

            list.DeleteList();

            Assert.AreEqual(expectedList.Count, list.Count);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var list = new UnidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new UnidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 503));
            expectedList.AddToStart(new Organization("Организация3", "Город3", 303));

            list.RemoveAt(1);
            list.RunTask();

            expectedList.RemoveAt(1);
            Organization actual = null;
            Organization expected = null;
            int i = 0;
            foreach (Organization org1 in list)
            {
                if (i == 1)
                    actual = org1;
                i++;
            }
                

            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 1)
                    expected = org2;
                i++;
            }
                

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var list = new UnidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 500));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new UnidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 500));
            expectedList.AddToStart(new Organization("Организация3", "Город3", 303));


            list.RunTask();

            Organization actual = null;
            Organization expected = null;
            int i = 0;
            foreach (Organization org1 in list)
            {
                if (i == 1)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 1)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodBidirect1()
        {
            var list = new BidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 500));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new BidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 500));
            expectedList.AddToStart(new Organization("Организация3", "Город3", 303));


            list.TaskAddAt(2, new Organization("Организация4", "Город4", 125));
            expectedList.TaskAddAt(2, new Organization("Организация4", "Город4", 125));

            Organization actual = null;
            Organization expected = null;
            int i = 0;
            foreach (Organization org1 in list)
            {
                if (i == 2)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 2)
                    expected = org2;
                i++;
            }


            list.ShowBackward();
            list.ShowForward();
            expectedList.ShowBackward();
            expectedList.ShowForward();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodBidirect2()
        {
            var list = new BidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 500));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new BidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 500));
            expectedList.AddToStart(new Organization("Организация3", "Город3", 303));


            list.RemoveAt(1);
            expectedList.RemoveAt(1);

            Organization actual = null;
            Organization expected = null;
            int i = 0;
            foreach (Organization org1 in list)
            {
                if (i == 1)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 1)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodBidirect3()
        {
            var list = new BidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new BidirectionalList();

            list.DeleteList();
            list.ShowBackward();
            list.ShowForward();

            Assert.AreEqual(expectedList.Count, list.Count);
        }

        [TestMethod]
        public void TestMethodBidirect4()
        {
            var list = new BidirectionalList();
            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 500));
            list.AddToStart(new Organization("Организация3", "Город3", 303));

            var expectedList = new BidirectionalList();
            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 500));
            expectedList.AddToStart(new Organization("Организация3", "Город3", 303));


            list.RemoveAt(0);
            list.RemoveAt(1);
            list.RemoveAt(-5);
            list.RemoveAt(100);
            expectedList.RemoveAt(0);
            expectedList.RemoveAt(1);


            Assert.AreEqual(expectedList.Count, list.Count);
        }

        [TestMethod]
        public void TestMethodTree1()
        {
            var list = new BinaryTree<Organization>(3);
            var expectedList = new BinaryTree<Organization>(3);

            int i = 0;
            Organization actual = null;
            Organization expected = null;
            foreach (Organization org1 in list)
            {
                if (i == 1)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 1)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodTree2()
        {
            var list = new BinaryTree<Organization>(3);
            var expectedList = new BinaryTree<Organization>();

            list.DeleteTree();

            Assert.AreEqual(expectedList.Count, list.Count);
        }

        [TestMethod]
        public void TestMethodTree3()
        {
            var list = new BinaryTree<Organization>(3);
            var expectedList = new BinaryTree<Organization>();

            list.CreateSearchTree();
            list.Show();
            list.Task();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void TestMethodBidirectRing1()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>();

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddToEnd(new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            var expectedList = new BidirectionalRingList<Organization>();

            organizations.Delete();
            organizations.ShowForward();

            Assert.AreEqual(expectedList.Count, organizations.Count);
        }

        [TestMethod]
        public void TestMethodBidirectRing2()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>();

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddToEnd(new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            var expectedList = new BidirectionalRingList<Organization>(organizations);

            bool expected = expectedList.Contains(new Organization("Организация1", "Город1", 103));
            bool actual = organizations.Contains(new Organization("Организация1", "Город1", 103));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodBidirectRing3()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>();

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddToEnd(new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            BidirectionalRingList<Organization> clone = new BidirectionalRingList<Organization>();

            clone = (BidirectionalRingList<Organization>)organizations.Clone();

            organizations.RemoveAt(2);
            clone.RemoveAt(2);

            int i = 0;
            Organization actual = null;
            Organization expected = null;
            foreach (Organization org1 in organizations)
            {
                if (i == 2)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in clone)
            {
                if (i == 2)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodBidirectRing4()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>();

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddToEnd(new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            BidirectionalRingList<Organization> copy = new BidirectionalRingList<Organization>();

            copy = organizations.Copy();

            organizations.Remove(new Organization("Организация1", "Город1", 103));            

            int i = 0;
            Organization actual = null;
            Organization expected = null;
            foreach (Organization org1 in organizations)
                if (i == 0)
                    actual = org1;

            i = 0;
            foreach (Organization org2 in copy)
                if (i == 0)
                    expected = org2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethodBidirectRing5()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>(4);

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddAt(2, new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            BidirectionalRingList<Organization> expectedList = new BidirectionalRingList<Organization>(4);

            expectedList.AddToEnd(new Organization("Организация1", "Город1", 103));
            expectedList.AddToEnd(new Organization("Организация2", "Город2", 503));
            expectedList.AddAt(2, new Organization("Организация3", "Город3", 303));
            expectedList.AddToStart(new Organization("Организация4", "Город4", 203));

            organizations.ShowForward();

            int i = 0;
            Organization actual = null;
            Organization expected = null;
            foreach (Organization org1 in organizations)
            {
                if (i == 2)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in expectedList)
            {
                if (i == 2)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethodBidirectRing6()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>();

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddToEnd(new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            BidirectionalRingList<Organization> clone = new BidirectionalRingList<Organization>();

            clone = (BidirectionalRingList<Organization>)organizations.Clone();

            organizations.RemoveAt(0);
            organizations.RemoveAt(3);
            organizations.RemoveAt(100);
            organizations.RemoveAt(-24);
            clone.RemoveAt(0);
            clone.RemoveAt(3);

            int i = 0;
            Organization actual = null;
            Organization expected = null;
            foreach (Organization org1 in organizations)
            {
                if (i == 1)
                    actual = org1;
                i++;
            }


            i = 0;
            foreach (Organization org2 in clone)
            {
                if (i == 1)
                    expected = org2;
                i++;
            }


            Assert.AreEqual(expected, actual);
        }
    }
}
