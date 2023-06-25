using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;

namespace Lab14
{
    class Program
    {
        public static List<Organization> collection1;
        public static List<Organization> collection2;


        static List<Organization> CreateList(int size)
        {
            List<Organization> list = new List<Organization>(size);
            Random rnd = new Random();

            for (int j = 0; j < size; j++)
            {
                list.Add(Organization.RandomOrganization(1, 5));
                list[j].Name = list[j].Name + rnd.Next(0, size / 2).ToString();
                list[j].City = list[j].City + rnd.Next(0, size / 2).ToString();
            }

            return list;
        }

        static List<Organization> CreateList2(int size)
        {
            List<Organization> list = new List<Organization>(size);
            Random rnd = new Random();

            for (int j = 0; j < size; j++)
            {
                list.Add(Organization.RandomOrganization(5, 5));
                list[j].Name = list[j].Name + rnd.Next(0, size / 2).ToString();
                list[j].City = list[j].City + rnd.Next(0, size / 2).ToString();
                ((InsuranceCompany)list[j]).Region = ((InsuranceCompany)list[j]).Region + rnd.Next(0, size / 2).ToString();
            }

            return list;
        }

        static void Request1(ref List<List<Organization>> organization)
        {
            var request1linq = (from request in organization from org in request where org.Name == "Организация1" select org.ToString());
            var request1exp = organization.SelectMany(items => items).Where(item => item.Name == "Организация1").Select(item => item.ToString());

            Console.WriteLine("\nЗапрос №1 (выборка): Организации с названием \"Организация1\"");
            Show(request1linq, "LINQ");
            Show(request1exp, "Метод расширения");
        }

        static void Request2(ref List<List<Organization>> organization)
        {
            var request2linq = (from request in organization from org in request where org.Employees > 3000 select org);
            var request2exp = organization.SelectMany(items => items).Where(item => item.Employees > 3000);

            Console.WriteLine("\nЗапрос №2 (счетчик): Количество организаций, с сотрудниками > 3000");
            Console.WriteLine($"{request2linq.Count()} : {request2exp.Count()}");
        }

        static void Request3(ref List<List<Organization>> organization)
        {
            var request3linq = (from request in organization from org in request where org is InsuranceCompany select (((InsuranceCompany)org).InsuranceFund));
            //var request3_2linq = (from request in organization from org in request where org is InsuranceCompany && ((InsuranceCompany)org).Region == "Регион2" select (((InsuranceCompany)org).InsuranceFund));
            var request3exp = organization.SelectMany(items => items).Where(org => org is InsuranceCompany).Select(org => ((InsuranceCompany)org).InsuranceFund);
            //var request3_2exp = organization.SelectMany(items => items).Where(org => org is InsuranceCompany && ((InsuranceCompany)org).Region == "Регион2").Select(org => ((InsuranceCompany)org).InsuranceFund);

            Console.WriteLine("\nЗапрос №3 (аггрегация): Суммарный страховой фонд всех страховых организаций");
            Console.WriteLine($"{request3linq.Sum()} : {request3exp.Sum()}");
            //Console.WriteLine("\nЗапрос №3 (аггрегация): Суммарный страховой фонд страховых организаций в регионе \"Регион2\"");
            //Console.WriteLine($"{request3_2linq.Sum()} : {request3_2exp.Sum()}");
        }

        static void Request4(ref List<List<Organization>> organization)
        {
            var request4linq = (from request in organization from org in request where org.Employees < 3000 select org).
                Except(from request in organization from org in request where org.City == "Город0" select org);
            var request4exp = organization.SelectMany(items => items).Where(item => item.Employees < 3000).
                Except(organization.SelectMany(items => items).Where(item => item.City == "Город0"));

            Console.WriteLine("\nЗапрос №4 (разность множеств): A - B: A = Количество работников < 3000; B = Организации в \"Город0\"");
            Show(request4linq, "LINQ");
            Show(request4exp, "Метод расширения");
        }

        static void Request5(ref List<List<Organization>> organization)
        {
            var request5linq = (from request in organization from org in request select org);
            request5linq = (from org in request5linq where org is Factory select org).
                Intersect(from org in request5linq where org.Employees < 1000 select org);

            var request5exp = organization.SelectMany(items => items);
            request5exp = request5exp.Where(item => item is Factory).Intersect(request5exp.Where(item => item.Employees < 1000));

            Console.WriteLine("\nЗапрос №5 (пересечение множеств): A пересекает B: A = Заводы; B = Компании с количеством сотрудников < 1000");
            Show(request5linq, "LINQ");
            Show(request5exp, "Метод расширения");
        }

        static void Request6(ref List<List<Organization>> organization)
        {
            var request6linq = (from request in organization from org in request select org);
            request6linq = (from org in request6linq where org is ShipbuildingCompany select org).
                Union(from org in request6linq where org is Library && (org as Library).NumberOfBooks >= 80000 select org);

            var request6exp = organization.SelectMany(items => items);
            request6exp = request6exp.Where(item => item is ShipbuildingCompany).
                Union(request6exp.Where(item => item is Library && (item as Library).NumberOfBooks >= 80000));

            Console.WriteLine("\nЗапрос №6 (объединение множеств): A + B: A = Судостроительные компании; B = Библиотеки с количеством книг > 80000");
            Show(request6linq, "LINQ");
            Show(request6exp, "Метод расширения");
        }

        static void Request7(ref List<List<Organization>> organization)
        {
            var organizationGroups = from request in organization from org in request group org by org.GetType().ToString();

            Console.WriteLine("\nЗапрос №7 (группировка): Группировка по типу организации");
            foreach (IGrouping<string, Organization> g in organizationGroups)
            {
                Console.WriteLine("=== " + g.Key.Replace("Lab10.", "") + " ===");
                foreach (var t in g)
                    Console.WriteLine(t);
                Console.WriteLine();
            }
        }

        public static void Show(IEnumerable<Organization> collection, string message)
        {
            Console.WriteLine("\n===== " + message + " =====");

            int length = 0;
            foreach (var item in collection)
            {
                Console.WriteLine("{0, -4}{1}", length + ".", item);
                length++;
            }

            if (length == 0) Console.WriteLine("Элементов нет");
        }

        public static void Show(IEnumerable<string> collection, string message)
        {
            Console.WriteLine("\n===== " + message + " =====");

            int length = 0;
            foreach (var item in collection)
            {
                Console.WriteLine("{0, -4}{1}", length + ".", item);
                length++;
            }

            if (length == 0) Console.WriteLine("Элементов нет");
        }

        public static void ShowAll(List<List<Organization>> collection)
        {
            Console.WriteLine("===== Вся коллекция =====\n");
            int index = 0;
            foreach (var list in collection)
                foreach (var item in list)
                    Console.WriteLine("{0, -4}{1}", index++ + ".", item);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int origWidth = Console.WindowWidth * 2;
            int origHeight = Console.WindowHeight * 2;
            Console.SetWindowSize(origWidth, origHeight);

            List<List<Organization>> collection = new List<List<Organization>>();
            int size = 20;
            int size2 = 10;

            collection1 = CreateList(size);
            collection2 = CreateList2(size2);
            collection.Add(collection1);
            collection.Add(collection2);

            ShowAll(collection);
            Console.WriteLine();

            Request1(ref collection);
            Request2(ref collection);
            Request3(ref collection);
            Request4(ref collection);
            Request5(ref collection);
            Request6(ref collection);
            Request7(ref collection);
        }
    }
}
