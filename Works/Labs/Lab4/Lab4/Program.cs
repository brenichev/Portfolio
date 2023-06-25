using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static int ReadNum(string message, int MinNum, int MaxNum) //Ввод числа и проверка на правильность ввода
        {
            bool ok = false;
            int number = 0;
            do
            {
                Console.Write(message + " ");
                string input = Console.ReadLine();
                ok = int.TryParse(input, out number);
                if (!ok)
                    Console.WriteLine("Введено не целое число");
                else
                    if (number < MinNum)
                {
                    Console.WriteLine("Число должно быть больше {0}", MinNum - 1);
                    ok = false;
                }
                else
                        if (number > MaxNum)
                {
                    Console.WriteLine("Число должно быть меньше {0}", MaxNum + 1);
                    ok = false;
                }
            } while (!ok);

            return number;
        }
        static int[] CreateArray(int size) //Выбор варианта формирования массива
        {
            Console.WriteLine("Выберите тип ввода массива");
            Console.WriteLine("1 - Формирование массива ДСЧ");
            Console.WriteLine("2 - Ввод массива с клавиатуры");
            int userChoice = 1;
            int[] a = new int[size];
            userChoice = ReadNum("Введите число", 1, 2);
            switch (userChoice)
            {
                case 1:
                    a = CreateRandomArray(size);
                    break;

                case 2:
                    a = ReadArray(size);
                    break;
            }
            return a;
        }

        static int[] CreateRandomArray(int size) //Формирование массива ДСЧ
        {
            int[] a = new int[size];
            Random r = new Random();
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = r.Next(-100, 100);
            }
            WriteArray(a, size);
            return a;
        }

        static int[] ReadArray(int size) //Ввод массива с клавиатуры
        {
            int[] a = new int[size];
            Console.WriteLine("Введите массив(по элементу в строке)");
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = ReadNum("", -100, 100);
            }
            WriteArray(a, size);
            return a;
        }

        static void WriteArray(int[] a, int size) //Вывод массива на экран
        {
            if (a.Length == 0)
                Console.WriteLine("Массив пустой");
            else
                for (int i = 0; i <= size - 1; i++)
                    Console.Write(a[i] + " ");
            Console.WriteLine();
        }

        static void DelFromArray(ref int[] a, ref int size) //Удалить N элементов, начиная с номера K
        {
            if (a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return;
            }
            int k = ReadNum("Введите позицию начала удаления элементов из массива ", 1, size);
            int n = ReadNum("Введите количество элементов, которое нужно удалить из массива", 0, 100);
            int userChoice = 1;
            int newSize = size - n;
            if (n > size - k + 1)
            {
                Console.WriteLine("Количество удаляемых элементов больше, чем в массиве");
                Console.WriteLine("1 - удалить до конца массива");
                Console.WriteLine("2 - не удалять");
                userChoice = ReadNum("Введите число", 1, 2);
                newSize = k - 1;
            }
            if (userChoice == 1)
            {
                int j = 0;
                int[] temp = new int[newSize];
                for (int i = 0; i <= k - 2; i++) //заполнение нового массива числами до k
                    temp[i] = a[i];
                j = k - 1;
                for (int i = k - 1 + n; i <= size - 1; i++) //заполнение оставшихся элементов
                {
                    temp[j] = a[i];
                    j++;
                }
                a = temp;
                size = newSize;
            }
            if (size == 0)
                Console.WriteLine("Пустой массив");
            else
                WriteArray(a, size);
            return;
        }

        static void AddElemToArr(ref int[] a, ref int size) //Добавить элемент с номером K
        {
            int newSize = size + 1;
            int j = 0;
            int[] temp = new int[newSize];
            if (a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                Console.WriteLine("Добавить 1 элемент в массив? 1 - да, 2 - нет");
                if (ReadNum("Введите ответ", 1, 2) == 1)
                {
                    temp[0] = ReadNum("Введите элемент для вставки", -100, 100);
                    a = temp;
                    size = newSize;
                }
            }
            else
            {
                int k = ReadNum("Введите позицию вставки элемента в массив ", 1, size + 1); //Заполнение нового массива до позиции вставки
                for (int i = 0; i <= k - 2; i++)
                    temp[i] = a[i];
                j = k - 1;
                temp[j] = ReadNum("Введите элемент для вставки", -100, 100); //Ввод числа для вставки
                j++;
                for (int i = k - 1; i <= size - 1; i++) //Заполнение оставшихся чисел
                {
                    temp[j] = a[i];
                    j++;
                }
                a = temp;
                size = newSize;
            }
            WriteArray(a, size);
            return;
        }

        static void ChangeArray(ref int[] a, ref int size) //Поменять местами элементы с четными и нечетными номерами
        {
            if (a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return;
            }
            int k = size - 1;
            int[] temp = new int[size];
            int nechet = 0;
            if (size % 2 == 1)
            {
                k = k - 1;
                nechet = 1;
            }

            for (int i = 0; i <= k; i = i + 2)
            {
                temp[i] = a[i + 1];
                temp[i + 1] = a[i];
            }
            if (nechet == 1)
                temp[size - 1] = a[size - 1];
            a = temp;
            WriteArray(a, size);
            return;
        }

        static void SearchArifm(int[] a, int size, int Sorted) //Найти элемент равный среднему арифметическому элементов массива
        {
            if (a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return;
            }
            if (Sorted == -1)
            {
                Console.WriteLine("Массив не отсортирован");
                Console.WriteLine("Выполнить поиск? 1 - да, 2 - нет");
                if (ReadNum("Введите число ", 1, 2) == 2)
                    return;
            }
            int sum = 0;
            for (int j = 0; j <= size - 1; j++)
            {
                sum = sum + a[j];
            }

            int srArifm = sum / size;
            int number = 0;
            int found = 0;
            while (number <= size - 1 && a[number] != srArifm)
            {
                if (a[number] == srArifm)
                    found = number;
                number++;
            }

            if (a[found] != srArifm)
            {
                Console.WriteLine("Такого элемента нет");
                Console.WriteLine("Количество сравнений = {0}", number);
            }               
            else
            {
                Console.WriteLine("Элемент с номером {0} равен среднему арифметическому элементов массива = {1}", number + 1, a[number]);                
                if (number == 0)
                    number = 1;
                Console.WriteLine("Количество сравнений = {0}", number);
            }
        }

        static int SortArray(ref int[] a, int size) //Сортировка простым обменом
        {
            int Sorted = 1;
            if (a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return Sorted = -1;
            }
            int j;
            int i;
            for (i = 1; i < size; i++)
                for (j = size - 1; j >= i; j--)
                    if (a[j] < a[j - 1])
                    {
                        int temp = a[j];
                        a[j] = a[j - 1];
                        a[j - 1] = temp;
                    }
            WriteArray(a, size);
            return Sorted;
        }


        static void Main(string[] args)
        {
            int size = ReadNum("Введите количество элементов массива ", 0, 100);
            int[] a = new int[size];
            a = CreateArray(size);
            int Sorted = -1;
            int userChoiceMax = 7;
            int userChoice = -1;

            while (userChoice != 0)
            {
                do
                {
                    Console.WriteLine("Выберите операцию");
                    Console.WriteLine("1 - создание массива");
                    Console.WriteLine("2 - вывод массива на экран");
                    Console.WriteLine("3 - удаление N элементов, начиная с номера K");
                    Console.WriteLine("4 - вставка элемента с номером К");
                    Console.WriteLine("5 - поменять местами элементы с четными и нечетными номерами");
                    Console.WriteLine("6 - поиск элемента равного среднему арифметическому элементов массива");
                    Console.WriteLine("7 - сортировка простым обменом");
                    Console.WriteLine("0 - выход");
                    userChoice = ReadNum("Введите число", 0, userChoiceMax);
                    if (userChoice < 0 || userChoice > userChoiceMax) //Проверка правильности ввода команды
                        Console.WriteLine("Введите число от 1 до 7 обозначающее выбранную операцию, либо 0 для выхода");
                }
                while (userChoice < 0 || userChoice > userChoiceMax); //До тех пор пока не будет введена правильная команда
                if (userChoice == 0)
                    break;

                switch (userChoice)
                {
                    case 1:
                        size = ReadNum("Введите количество элементов массива ", 0, 100);
                        a = new int[size];
                        a = CreateArray(size);
                        Sorted = -1;
                        break;

                    case 2:
                        WriteArray(a, size);
                        break;

                    case 3:
                        DelFromArray(ref a, ref size);
                        break;

                    case 4:
                        AddElemToArr(ref a, ref size);
                        break;

                    case 5:
                        ChangeArray(ref a, ref size);
                        break;

                    case 6:
                        SearchArifm(a, size, Sorted);
                        break;

                    case 7:
                        Sorted = SortArray(ref a, size);
                        break;


                }
            }

        }
    }
}
