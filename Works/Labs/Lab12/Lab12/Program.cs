using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;

namespace Lab12
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void TaskOne()
        {
            Console.WriteLine(" === Однонаправленный список: === \n");
            UnidirectionalList list = new UnidirectionalList();

            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToEnd(new Organization("Организация3", "Город3", 303));
            list.AddToStart(new Organization("Организация4", "Город4", 203));

            list.Show();
            Console.WriteLine();

            list.RunTask();
            list.Show();

            Console.WriteLine();
            list.DeleteList();
            list.Show();
        }

        static void TaskTwo()
        {
            Console.WriteLine(" === Двунаправленный список: === \n");
            BidirectionalList list = new BidirectionalList();

            list.AddToEnd(new Organization("Организация1", "Город1", 103));
            list.AddToEnd(new Organization("Организация2", "Город2", 503));
            list.AddToEnd(new Organization("Организация3", "Город3", 303));
            list.AddToStart(new Organization("Организация4", "Город4", 203));

            list.ShowForward();
            Console.WriteLine();

            list.TaskAddAt(1, new Organization("Организация5", "Город5", 1004));
            list.ShowForward();

            Console.WriteLine();
            list.DeleteList();
            list.ShowForward();
        }

        static void TaskThree()
        {
            Console.WriteLine(" === Бинарное дерево: === \n");
            BinaryTree<Organization> tree = new BinaryTree<Organization>(10);

            tree.Show();
            Console.WriteLine();

            tree.Task();
            Console.WriteLine();

            tree.CreateSearchTree();
            tree.Show();
            Console.WriteLine();

            tree.DeleteTree();
            tree.Show();
        }
        
        static void PartTwo()
        {
            BidirectionalRingList<Organization> organizations = new BidirectionalRingList<Organization>();
            Console.WriteLine(" === Создан пустой двунаправленный колцевой список === ");

            organizations.AddToEnd(new Organization("Организация1", "Город1", 103));
            organizations.AddToEnd(new Organization("Организация2", "Город2", 503));
            organizations.AddToEnd(new Organization("Организация3", "Город3", 303));
            organizations.AddToStart(new Organization("Организация4", "Город4", 203));

            Console.WriteLine(" === Добавлены 4 элемента. Количество элементов в коллекции(Count) = " + organizations.Count + " === ");
            Console.WriteLine();
            Console.WriteLine(" === Вывод с помощью foreach:  === ");
            foreach (Organization org in organizations)
            {
                Console.WriteLine(org);
            }
            bool exist, notExist;
            exist = organizations.Contains(new Organization("Организация1", "Город1", 103));
            notExist  = organizations.Contains(new Organization("Организация15", "Город13", 103));

            Console.WriteLine("\n === Существующий элемент: {0}", exist ? "Найден === " : "Не найден === ");
            Console.WriteLine(" === Не существующий элемент: {0}\n", notExist ? "Найден === " : "Не найден === ");

            organizations.ShowForward();
            BidirectionalRingList<Organization> clone = new BidirectionalRingList<Organization>();
            BidirectionalRingList<Organization> copy = new BidirectionalRingList<Organization>();
            Console.WriteLine();
            Console.WriteLine(" === Клонирование и поверхностное копирование коллекции === ");
            clone = (BidirectionalRingList<Organization>)organizations.Clone();
            copy = organizations.Copy();
            clone.ShowForward();

            Console.WriteLine("\n === Удаление элемента с индексом 2 из коллекции === ");
            organizations.RemoveAt(2);
            Console.WriteLine();

            Console.WriteLine(" === Клонирование === ");
            clone.ShowForward();
            Console.WriteLine();

            Console.WriteLine(" === Поверхностное копирование === ");
            copy.ShowForward();
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

        static void PartMenu()
        {
            Console.WriteLine("=== Выберите задание ===");
            Console.WriteLine("1 - Задание с однонаправленным списком");
            Console.WriteLine("2 - Задание с двунаправленным списком");
            Console.WriteLine("3 - Задание с бинарным деревом");
            Console.WriteLine("4 - Задание с двунаправленным кольцевым списком");
            Console.WriteLine("0 - Завершить работу");
            Console.WriteLine();
        }

        static void ChoosePart()
        {
            int userChoice = 1;
            do
            {
                PartMenu();
                userChoice = ReadInt(0, 4, "=== Введите число ===");
                switch (userChoice)
                {
                    case 1:

                        TaskOne();
                        break;

                    case 2:

                        TaskTwo();
                        break;

                    case 3:

                        TaskThree();
                        break;
                    case 4:

                        PartTwo();
                        break;
                }
            } while (userChoice != 0);
        }


        static void Main(string[] args)
        {
            int origWidth = Console.WindowWidth * 2;
            int origHeight = Console.WindowHeight * 2;
            Console.SetWindowSize(origWidth, origHeight);
            ChoosePart();
        }
    }
}
