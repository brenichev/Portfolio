using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{
    class Program
    {

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            Graph graph = Graph.ReadMatrix();

            Console.WriteLine("Введите значение вершины для стягивания");
            int userChoice;
            bool check = true;
            do
            {
                check = int.TryParse(Console.ReadLine(), out userChoice);
            }while (!check);

            graph.Constrict(userChoice);
            graph.WriteMatrix();
            Console.ReadLine();
        }
    }
}
