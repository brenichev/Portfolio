using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab12;
using Lab10;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            int origWidth = Console.WindowWidth * 2;
            int origHeight = Console.WindowHeight * 2;
            Console.SetWindowSize(origWidth, origHeight);

            MyNewCollection firstList = new MyNewCollection("Первая коллекция", 5);
            MyNewCollection secondList = new MyNewCollection("Вторая коллекция", 5);

            Journal firstJournal = new Journal();
            firstList.CollectionCountChanged += new CollectionHandler(firstJournal.CollectionCountChanged); //+=firstJournal.CollectionCountChanged
            firstList.CollectionReferenceChanged += new CollectionHandler(firstJournal.CollectionReferenceChanged); 

            Journal secondJournal = new Journal();
            firstList.CollectionReferenceChanged += new CollectionHandler(secondJournal.CollectionReferenceChanged);
            secondList.CollectionReferenceChanged += new CollectionHandler(secondJournal.CollectionReferenceChanged);

            firstList.Add();
            firstList.Add();
            secondList.Add();

            firstList.Remove(2);
            secondList.Remove(2);

            firstList[2] = new InsuranceCompany("Страховая компания1", "Город1", 150, 1000000, "Регион1");
            secondList[3] = Organization.RandomOrganization();
            firstList[3] = new InsuranceCompany("Страховая компания2", "Город2", 100, 5000000, "Регион2");

            Console.WriteLine("=== Первый журнал: ===");            
            Console.WriteLine(firstJournal);
            Console.WriteLine();
            Console.WriteLine("=== Второй журнал: ===");
            Console.WriteLine(secondJournal);

            Console.ReadLine();
        }
    }
}
