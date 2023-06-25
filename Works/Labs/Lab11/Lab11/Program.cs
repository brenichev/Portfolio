using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;


namespace Lab11
{
    class Program
    {
        static void PrintCollection(SortedList collection, string message = "")
        {
            Console.Write(message);
            foreach (object obj in collection)
            {
                Console.WriteLine();
                (obj as Organization).Show();
            }
        }

        static SortedList CreateSortedList(Organization[] collection)
        {
            SortedList sortedList = new SortedList(4);

            foreach (Organization organization in collection)
                sortedList.Add(organization.Name, organization);

            return sortedList;
        }

        static void ShowSortedList(SortedList sortedList, ICollection keyList, string message = "")
        {
            Console.Write(message);
            if (sortedList.Count == 0)
                Console.WriteLine("=== Коллекция пустая ===");
            else
                foreach (string key in keyList)
                {
                    Organization o = (Organization)sortedList[key];
                    if (o != null)
                    {
                        o.Show();
                        Console.WriteLine();
                    }
                }
        }

        public static int ReadInt(int left = 0, int right = 100, string message = "")
        {
            Console.WriteLine(message);
            bool ok = false;
            int number = 0;
            do
            {
                try
                {
                    number = int.Parse(Console.ReadLine());
                    if (number >= left && number <= right) ok = true;
                    else
                    {
                        Console.WriteLine($"=== Неверно введено число. Введите число большее {left} и меньшее {right} ===");
                        ok = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("=== Введено не целое число. Введите целое число. ===");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"=== Неверно введено число. Введите число большее {left} и меньшее {right} ===");
                    ok = false;
                }
            } while (!ok);
            return number;
        }

        static void FirstMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Добавить запись");
            Console.WriteLine("2 - Удалить запись");
            Console.WriteLine("3 - Вывести весь список");
            Console.WriteLine("4 - Количество организаций в городе");
            Console.WriteLine("5 - Количество заводов в коллекции");
            Console.WriteLine("6 - Вывод на экран всех библиотек из коллекции");
            Console.WriteLine("7 - Поиск записи");
            Console.WriteLine("8 - Создание копии записей");
            Console.WriteLine("9 - Вывести последнюю копию");
            Console.WriteLine("0 - Вернуться к выбору задания");
            Console.WriteLine();
        }
        static void SecondMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Добавить запись");
            Console.WriteLine("2 - Удалить запись");
            Console.WriteLine("3 - Вывести весь список");
            Console.WriteLine("4 - Количество организаций в городе");
            Console.WriteLine("5 - Количество заводов в коллекции");
            Console.WriteLine("6 - Вывод на экран всех библиотек из коллекции");
            Console.WriteLine("7 - Поиск записи");
            Console.WriteLine("8 - Сортировка коллекции");
            Console.WriteLine("9 - Создание копии записей");
            Console.WriteLine("10 - Вывести последнюю копию");
            Console.WriteLine("0 - Вернуться к выбору задания");
            Console.WriteLine();
        }

        static void ThirdMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Вывести коллекцию");
            Console.WriteLine("2 - Добавить запись");
            Console.WriteLine("3 - Удалить запись");
            Console.WriteLine("4 - Cравнение для первого элемента");
            Console.WriteLine("5 - Cравнение для центрального элемента");
            Console.WriteLine("6 - Cравнение для последнего элемента");
            Console.WriteLine("7 - Cравнение для несуществующего элемента");
            Console.WriteLine("0 - Вернуться к выбору задания");
            Console.WriteLine();
        }


        static void PrintMenuForElements()
        {
            Console.WriteLine("=== Выберите элемент, который необходимо добавить ===");
            Console.WriteLine("1 - Организация");
            Console.WriteLine("2 - Завод");
            Console.WriteLine("3 - Страховая компания");
            Console.WriteLine("4 - Библиотека");
            Console.WriteLine("5 - Судостроительная компания");
            Console.WriteLine();
        }
        static void PrintMenuForElementsSearch()
        {
            Console.WriteLine("=== Выберите элемент, который необходимо найти в коллекции ===");
            Console.WriteLine("1 - Организация");
            Console.WriteLine("2 - Завод");
            Console.WriteLine("3 - Страховая компания");
            Console.WriteLine("4 - Библиотека");
            Console.WriteLine("5 - Судостроительная компания");
            Console.WriteLine();
        }

        static void PartMenu()
        {
            Console.WriteLine("=== Выберите задание ===");
            Console.WriteLine("1 - Первое задание");
            Console.WriteLine("2 - Второе задание");
            Console.WriteLine("3 - Третье задание");
            Console.WriteLine("0 - Завершить работу");
            Console.WriteLine();
        }

        static void CountOrganizationsList(List<Organization> listOrganizations) // Количество организаций в городе
        {
            Console.WriteLine("=== Введите город... ===");
            string cityOrg = Console.ReadLine();

            bool exist = false;
            int count = 0;
            Organization current = new Organization();

            if (listOrganizations.Count > 0)
            {
                for (int i = 0; i < listOrganizations.Count; i++)
                {
                    if (listOrganizations[i].City == cityOrg)
                    {
                        exist = true;
                        count++;
                    }
                }

                if (exist) Console.WriteLine($"=== Количество организаций в городе {cityOrg}: {count} ===");
                else Console.WriteLine($"=== Организации в городе {cityOrg} не найдены ===");
            }
            else Console.WriteLine("=== Коллекция пуста ===");
        }

        static void CountOrganizations(SortedList collectionSorted, ICollection keyList) // Количество организаций в городе
        {
            Console.WriteLine("Введите город...");
            string cityOrg = Console.ReadLine();

            bool exist = false;
            int count = 0;
            Organization current = new Organization();

            if (collectionSorted.Count > 0)
            {
                foreach (string element in keyList)
                {
                    current = (Organization)collectionSorted[element];
                    if (current.City == cityOrg)
                    {
                        exist = true;
                        count++;
                    }
                }
                if (exist) Console.WriteLine($"=== Количество организаций в городе {cityOrg}: {count} ===");
                else Console.WriteLine($"=== Организации в городе {cityOrg} не найдены ===");
            }
            else Console.WriteLine("=== Коллекция пуста ===");
        }

        static void AddElement(ref SortedList sortedList)
        {
            PrintMenuForElements();

            int userChoice = ReadInt(1, 5, "=== Выберите тип элемента ===");

            switch (userChoice)
            {
                case 1:  // Type Organization
                    Organization o = new Organization();
                    o.Input();
                    if (sortedList.Contains(o.Name))
                        Console.WriteLine("=== Такой элемент уже есть ===");
                    else
                        sortedList.Add(o.Name, o);
                    break;
                case 2:  // Type Factory
                    Factory f = new Factory();
                    f.Input();
                    if (sortedList.Contains(f.Name))
                        Console.WriteLine("=== Такой элемент уже есть ===");
                    else
                        sortedList.Add(f.Name, f);
                    break;
                case 3:  // Type InsuranceCompany
                    InsuranceCompany i = new InsuranceCompany();
                    i.Input();
                    if (sortedList.Contains(i.Name))
                        Console.WriteLine("=== Такой элемент уже есть ===");
                    else
                        sortedList.Add(i.Name, i);
                    break;
                case 4:  // Type Library
                    Library l = new Library();
                    l.Input();
                    if (sortedList.Contains(l.Name))
                        Console.WriteLine("=== Такой элемент уже есть ===");
                    else
                        sortedList.Add(l.Name, l);
                    break;
                case 5:  // Type ShipbuildingCompany
                    ShipbuildingCompany s = new ShipbuildingCompany();
                    s.Input();
                    if (sortedList.Contains(s.Name))
                        Console.WriteLine("=== Такой элемент уже есть ===");
                    else
                        sortedList.Add(s.Name, s);
                    break;
            }
        }
        static void AddElementList(ref List<Organization> listOrganizations)
        {
            PrintMenuForElements();

            int userChoice = ReadInt(1, 5, "=== Выберите тип элемента ===");

            switch (userChoice)
            {
                case 1:  // Type Organization
                    Organization o = new Organization();
                    o.Input();
                    listOrganizations.Add(o);
                    break;
                case 2:  // Type Factory
                    Factory f = new Factory();
                    f.Input();
                    listOrganizations.Add(f);
                    break;
                case 3:  // Type InsuranceCompany
                    InsuranceCompany i = new InsuranceCompany();
                    i.Input();
                    listOrganizations.Add(i);
                    break;
                case 4:  // Type Library
                    Library l = new Library();
                    l.Input();
                    listOrganizations.Add(l);
                    break;
                case 5:  // Type ShipbuildingCompany
                    ShipbuildingCompany s = new ShipbuildingCompany();
                    s.Input();
                    listOrganizations.Add(s);
                    break;
            }
        }

        static Organization ReadElement()
        {
            PrintMenuForElementsSearch();
            Organization elem = new Organization();
            int userChoice = ReadInt(1, 5, "=== Выберите тип элемента ===");

            switch (userChoice)
            {
                case 1:  // Type Organization
                    Organization o = new Organization();
                    o.Input();
                    elem = o;
                    break;
                case 2:  // Type Factory
                    Factory f = new Factory();
                    f.Input();
                    elem = f;
                    break;
                case 3:  // Type InsuranceCompany
                    InsuranceCompany i = new InsuranceCompany();
                    i.Input();
                    elem = i;
                    break;
                case 4:  // Type Library
                    Library l = new Library();
                    l.Input();
                    elem = l;
                    break;
                case 5:  // Type ShipbuildingCompany
                    ShipbuildingCompany s = new ShipbuildingCompany();
                    s.Input();
                    elem = s;
                    break;
            }
            return elem;
        }

        static Organization[] CreateOrganizationArray(SortedList sortedList, ICollection keyList)
        {
            int length = sortedList.Count;
            Organization[] OrganizationArray = new Organization[length];
            int count = 0;

            foreach (string key in keyList)
            {
                OrganizationArray[count] = (Organization)sortedList[key];
                count++;
            }
            return OrganizationArray;
        }

        static void ShowOrganizationArray(Organization[] OrganizationArray)
        {
            foreach (Organization org in OrganizationArray)
            {
                org.Show();
                Console.WriteLine();
            }
        }

        static SortedList Copy(SortedList sortedList, ICollection keyList)
        {
            SortedList copiedList = new SortedList();

            foreach (string key in keyList)
                copiedList.Add(key, sortedList[key]);

            return copiedList;
        }

        public static List<Organization> Copy(List<Organization> list)
        {
            List<Organization> copiedList = new List<Organization>();
            foreach (Organization o in list)
            {
                copiedList.Add((Organization)o.Clone());
            }

            return copiedList;
        }

        public static void ShowList(List<Organization> list, string message)
        {
            Console.WriteLine(message);
            int i = 0;
            if (list.Count == 0)
                Console.WriteLine("=== Коллекция пустая ===");
            else
                foreach (Organization v in list)
                {
                    Console.Write(i + " ");
                    v.Show();
                    i++;
                }
            Console.WriteLine();
        }

        static void Task1()
        {
            Console.WriteLine("Создание коллекции (SortedList)");
            // Создание массива, который будет передан в конструктор коллекции
            Organization[] ListOrganizations =
            {
                new InsuranceCompany("Страховая компания", "Город", 50, 100000000, "Регион"),
                new ShipbuildingCompany("Судостроительная компания", "Город", 100, 15000000),
                new Factory("Завод", "Город", 150, "Отрасль"),
                new Library("Библиотека", "Город", 20, 15000)

            };
            SortedList collectionSorted = CreateSortedList(ListOrganizations);
            ICollection keyList = collectionSorted.Keys;
            Console.WriteLine("=== Коллекция создана ===");
            SortedList copy = new SortedList();
            ShowSortedList(collectionSorted, keyList, "Коллекция:\n");
            string key;

            int userChoice = 1;
            do
            {
                FirstMenu();
                userChoice = ReadInt(0, 9, "=== Выберите действие ===");
                switch (userChoice)
                {
                    case 1:
                        int countElem = collectionSorted.Count;
                        AddElement(ref collectionSorted);
                        if (collectionSorted.Count != countElem)
                            Console.WriteLine("=== Запись добавлена ===");
                        break;
                    case 2:
                        Console.WriteLine("Введите название организации");
                        key = Console.ReadLine();
                        if (!collectionSorted.ContainsKey(key)) Console.WriteLine("=== Элемент не найден ===");
                        else
                        {
                            collectionSorted.Remove(key);
                            Console.WriteLine("=== Элемент удален ===");
                        }
                        break;
                    case 3:
                        ShowSortedList(collectionSorted, keyList, "Коллекция: ");
                        break;
                    case 4:
                        CountOrganizations(collectionSorted, keyList);
                        break;
                    case 5:
                        Console.WriteLine("=== Количество заводов в коллекции ===");
                        int count = 0;
                        foreach (string element in keyList)
                        {
                            if (collectionSorted[element] is Factory)
                                count++;
                        }
                        //foreach (object obj in collectionSorted) if (obj is Factory) count++;
                        Console.WriteLine(count);
                        break;
                    case 6:
                        Console.WriteLine("=== Вывод на экран всех библиотек из коллекции ===");
                        bool exist = false;
                        foreach (string element in keyList)
                        {
                            if (collectionSorted[element] is Library)
                            {
                                //Library library = collectionSorted[element] as Library;
                                (collectionSorted[element] as Library).Show();
                                exist = true;
                            }
                        }
                        if (!exist) Console.WriteLine("=== В коллекции нет библиотек ===");
                        break;
                    case 7:
                        Console.WriteLine("=== Введите название организации ===");
                        key = Console.ReadLine();
                        if (!collectionSorted.ContainsKey(key)) Console.WriteLine("=== Запись не найденa ===");
                        else
                        {
                            Organization organization = collectionSorted[key] as Organization;
                            if (organization != null)
                            {
                                organization.Show();
                                Console.WriteLine();
                            }
                        }
                        break;
                    case 8:  // Клонирование коллекции
                        {
                            copy = Copy(collectionSorted, keyList);
                            Console.WriteLine("=== Коллекция скопирована ===");
                            break;
                        }
                    case 9:  // Вывод последней копии
                        {
                            ICollection copiedListKeys = copy.Keys;
                            ShowSortedList(copy, copiedListKeys);
                            break;
                        }
                }
            } while (userChoice != 0);
        }

        static void Task2()
        {
            List<Organization> ListOrganizations = new List<Organization>();
            ShowList(ListOrganizations, "=== Коллекция List<T> ===");
            List<Organization> copy = new List<Organization>();
            int userChoice = 1;
            string text;
            do
            {
                SecondMenu();
                userChoice = ReadInt(0, 10, "=== Выберите действие ===");
                switch (userChoice)
                {
                    case 1:
                        AddElementList(ref ListOrganizations);
                        Console.WriteLine("=== Запись добавлена ===");
                        break;
                    case 2:
                        ShowList(ListOrganizations, "Коллекция List<T>");
                        ListOrganizations.RemoveAt(ReadInt(0, ListOrganizations.Count() - 1, "Введите номер элемента, который необходимо удалить"));
                        Console.WriteLine("=== Запись удалена ===");
                        break;
                    case 3:
                        ShowList(ListOrganizations, "Коллекция List<T>: ");
                        break;
                    case 4:
                        CountOrganizationsList(ListOrganizations);
                        break;
                    case 5:
                        Console.WriteLine("=== Количество заводов в коллекции ===");
                        int count = 0;
                        foreach (object obj in ListOrganizations)
                            if (obj is Factory)
                                count++;
                        Console.WriteLine(count);
                        break;
                    case 6:
                        Console.WriteLine("=== Вывод на экран всех библиотек из коллекции ===");
                        bool exist = false;
                        foreach (object obj in ListOrganizations)
                            if (obj is Library)
                            {
                                (obj as Library).Show();
                                exist = true;
                            }
                        if (!exist) Console.WriteLine("=== В коллекции нет библиотек ===");
                        break;
                    case 7: //???
                        if (ListOrganizations.Count == 0) Console.WriteLine("Коллекция пуста");
                        else
                        {
                            Organization orgElem = new Organization();
                            orgElem = ReadElement();
                            if (ListOrganizations.Contains(orgElem)) Console.WriteLine("=== Данный элемент входит в коллекцию ===");
                            else Console.WriteLine("=== Данный элемент не входит в коллекцию ===");
                        }
                        break;
                    case 8:
                        ShowList(ListOrganizations, "=== До сортировки: ===\r\n");
                        ListOrganizations.Sort();
                        ShowList(ListOrganizations, "=== После сортировки: ===\r\n");
                        break;
                    case 9:  // Клонирование коллекции
                        {
                            copy = Copy(ListOrganizations);
                            Console.WriteLine("=== Коллекция скопирована ===");
                            break;
                        }
                    case 10:  // Вывод последней копии
                        {
                            ShowList(copy, "=== Последняя копия ====");
                            break;
                        }
                }
            } while (userChoice != 0);
        }

        static void Task3()
        {
            int length = ReadInt(3, 1000, "=== Введите размер коллекции ===");
            TestCollections testCollection = new TestCollections(length);
            testCollection = testCollection.CreateRand();
            int userChoice = 1;
            Stopwatch watch = new Stopwatch();
            do
            {
                ThirdMenu();

                userChoice = ReadInt(0, 7, "=== Выберите действие ===");

                switch (userChoice)
                {
                    case 1:  // Вывод

                        testCollection.Show();
                        break;

                    case 2:  // Добавление объекта

                        InsuranceCompany i = new InsuranceCompany();
                        i.Input();

                        testCollection.OrganizationQueue.Enqueue(i.BaseOrganization);
                        testCollection.StringQueue.Enqueue(i.BaseOrganization.ToString());
                        testCollection.OrganizationSDictionary.Add(i.BaseOrganization, i);
                        testCollection.StringSDictionary.Add(i.BaseOrganization.ToString(), i);
                        Console.WriteLine("=== Запись добавлена ===");

                        break;

                    case 3:  // Удаление объекта

                        if (testCollection.Length == 0)
                            Console.WriteLine("=== Коллекция пустая ===");
                        else
                        {
                            Organization p = new Organization();
                            p = testCollection.OrganizationQueue.Peek();
                            testCollection.OrganizationQueue.Dequeue();
                            testCollection.StringQueue.Dequeue();
                            testCollection.OrganizationSDictionary.Remove(p);
                            testCollection.StringSDictionary.Remove(p.Name);
                            Console.WriteLine("=== Первый элемент в очереди удален ===");
                        }
                        break;

                    case 4:  // Сравнение скорости поиска первого элемента   
                        string key = testCollection.CollectionStringFirst;
                        InsuranceCompany insComp = (InsuranceCompany)testCollection.CollectionFirst.Value.Clone();
                        Console.WriteLine("=== Сравнение скорости поиска первого элемента ===");

                        watch.Start();
                        bool result = testCollection.OrganizationQueue.Contains(insComp);
                        watch.Stop();
                        TimeSpan time = watch.Elapsed;
                        ShowTime("Queue<Organization>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.StringQueue.Contains(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<string>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsKey(insComp.BaseOrganization);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.StringSDictionary.ContainsKey(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<string, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsValue(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsValue", result, time);

                        break;

                    case 5:  // Сравнение скорости поиска последнего элемента

                        key = testCollection.StringQueue.Peek();
                        insComp = (InsuranceCompany)testCollection.StringSDictionary[key].Clone();

                        Console.WriteLine("=== Сравнение скорости поиска последнего элемента ===");

                        watch.Start();
                        result = testCollection.OrganizationQueue.Contains(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<Organization>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.StringQueue.Contains(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<string>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsKey(insComp.BaseOrganization);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.StringSDictionary.ContainsKey(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<string, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsValue(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsValue", result, time);

                        break;

                    case 6:  // Сравнение скорости поиска центрального элемента

                        key = testCollection.CollectionStringMiddle;
                        insComp = (InsuranceCompany)testCollection.CollectionMiddle.Value.Clone();

                        Console.WriteLine("=== Сравнение скорости поиска центрального элемента ===");

                        watch.Start();
                        result = testCollection.OrganizationQueue.Contains(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<Organization>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.StringQueue.Contains(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<string>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsKey(insComp.BaseOrganization);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.StringSDictionary.ContainsKey(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<string, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsValue(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsValue", result, time);

                        break;

                    case 7:  // Сравнение скорости поиска несуществующего элемента

                        insComp = new InsuranceCompany("Не существует", "Нет города", 0, 0, "Нет региона");
                        key = "Не существует";
                        Console.WriteLine("=== Сравнение скорости поиска несуществующего элемента ===");

                        watch.Start();
                        result = testCollection.OrganizationQueue.Contains(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<Organization>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.StringQueue.Contains(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("Queue<string>, метод Contains", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsKey(insComp.BaseOrganization);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.StringSDictionary.ContainsKey(key);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<string, InsuranceCompany>, метод ContainsKey", result, time);

                        watch.Restart();
                        result = testCollection.OrganizationSDictionary.ContainsValue(insComp);
                        watch.Stop();
                        time = watch.Elapsed;
                        ShowTime("SortedDictionary<Organization, InsuranceCompany>, метод ContainsValue", result, time);

                        break;


                }
            } while (userChoice != 0);
        }

        static void ShowTime(string collection, bool found, TimeSpan time)
        {
            if (found) Console.WriteLine($"{collection}: элемент найден. Затрачено времени: {time.Ticks}");
            else Console.WriteLine($"{collection}: элемент не найден. Затрачено времени: {time.Ticks}");
        }

        static void ChoosePart()
        {
            int userChoice = 1;
            do
            {
                PartMenu();
                userChoice = ReadInt(0, 3, "=== Введите число ===");
                switch (userChoice)
                {
                    case 1:

                        Task1();
                        break;

                    case 2:

                        Task2();
                        break;

                    case 3:

                        Task3();
                        break;
                }
            } while (userChoice != 0);
        }


        static void Main(string[] args)
        {
            int origWidth = Console.WindowWidth * 2;
            int origHeight = Console.WindowHeight * 2;
            Console.SetWindowSize(origWidth, origHeight);
            Console.BufferHeight = 1050;
            ChoosePart();
        }
    }
}
