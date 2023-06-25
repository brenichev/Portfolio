using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_2
{
    static class Data
    {
        public static int[] a { get; set; }
        public static int size { get; set; }
        public static int userChoice3  { get; set; }
        public static int[,] table { get; set; }
        public static int row { get; set; }
        public static int col { get; set; }
        public static int EmptyArr { get; set; }
        public static int[][] jag_arr { get; set; }
        public static int row2 { get; set; }
        public static int col2 { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
    }
}
