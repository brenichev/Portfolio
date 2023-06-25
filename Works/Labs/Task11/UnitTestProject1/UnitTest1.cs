using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task11;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Grid grid = new Grid();
            var str = "Это повторяется еще дважды. Таким образом, на бумагу наносится 100 букв текста. Решетку можно изобра";
            grid.Str = str;
            grid.Generate();
            grid.Encode();
            grid.Decode();

            Assert.AreEqual(str, grid.Str);
        }

        [TestMethod]
        public void TestMethod2()
        {
            byte[,] grid2 = new byte[,] { { 0, 0, 1, 0, 0, 0, 0, 1, 0, 0 },
                                          { 0, 0, 0, 0, 0, 1, 1, 0, 1, 0 },
                                          { 0, 1, 0, 0, 1, 0, 1, 0, 0, 0 },
                                          { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 1, 1, 0, 1, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
                                          { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 1, 1, 0, 0, 0, 1, 0, 0, 0 },
                                          { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                                          { 0, 1, 0, 1, 0, 1, 0, 0, 0, 1 } };
            var str = "Это повторяется еще дважды. Таким образом, на бумагу наносится 100 букв текста. Решетку можно изобра";
            Grid grid = new Grid(grid2, str);

            grid.Encode();
            var expected = "гыЭ.ус тнт аТаао кпн.оиов тсим Реошобетсят ряреакузо 1м00тмся ,ож  наноещб уе кб ви тдзоубмвеаажркад";

            Assert.AreEqual(expected, grid.Str);
        }
    }
}
