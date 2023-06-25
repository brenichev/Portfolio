using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Program
    {
        static char ReadSym(string message) //Ввод символа и проверка на правильность ввода
        {
            bool ok = false;
            char number;
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                ok = char.TryParse(input, out number);
                if (!ok)
                    Console.WriteLine("Введена не буква ");
            } while (!ok);
            Console.WriteLine();
            return number;
        }

        static int ReadSym(string message, int MinNum, int MaxNum) //Ввод числа и проверка на правильность ввода
        {
            bool ok = false;
            int number = 0;
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                ok = int.TryParse(input, out number);
                if (!ok)
                    Console.WriteLine("Введено не целое число ");
                else
                    if (number < MinNum)
                {
                    Console.WriteLine("Число должно быть больше {0} ", MinNum - 1);
                    ok = false;
                }
                else
                        if (number > MaxNum)
                {
                    Console.WriteLine("Число должно быть меньше {0} ", MaxNum + 1);
                    ok = false;
                }
            } while (!ok);
            Console.WriteLine();
            return number;
        }

        static char[] CreateArray(int size) //Выбор варианта формирования массива
        {
            Console.WriteLine("Выберите тип ввода массива ");
            Console.WriteLine("1 - Формирование массива ДСЧ ");
            Console.WriteLine("2 - Ввод массива с клавиатуры ");
            int userChoice = 1;
            char[] a = new char[size];
            userChoice = ReadSym("Введите число ", 1, 2);
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
        static char[] CreateRandomArray(int size) //Формирование массива ДСЧ
        {
            char[] a = new char[size];
            Random r = new Random();
            string alph = "abcdefghigklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789";
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = alph[r.Next(0, 68)];
            }
            WriteArray(a, size);
            Console.WriteLine();
            return a;
        }

        static void ReadString(ref string b) //Ввод строки с помощью ДСЧ, либо с клавиатуры
        {
            Console.WriteLine("Выберите тип ввода строки ");
            Console.WriteLine("1 - Формирование строки ДСЧ ");
            Console.WriteLine("2 - Ввод строки с клавиатуры ");
            int userChoice = 1;
            b = "";
            Random r = new Random();
            int strsize = 0;           
            userChoice = ReadSym("Введите число ", 1, 2);            
            switch (userChoice)
            {
                case 1:
                    strsize = ReadSym("Введите количество предложений(0 - случайное количество) ", 0, 10);
                    if (strsize == 0)
                        strsize = r.Next(1, 5);
                    string[] ArrWord = new string[7] { "aaaa", "bbbbb", "ccccc", "ddd", "ааа", "бббб", "вввв" };
                    string[] ArrZnak = new string[9] { "", "", "", "", "", "", ",", ";", ":" };
                    string[] ArrEnd = new string[6] { "!", ".", "?", ".", ".", "." };                                      
                    int words;
                    while (strsize > 0)
                    {
                        int minWords = 1;
                        int maxWords = 3;
                        words = r.Next(minWords, maxWords);
                        for (int i = 1; i <= words; i++)
                        {
                            b = b + ArrWord[r.Next(0, 6)];
                            if (i < words)
                                b = b + ArrZnak[r.Next(0, 8)] + " ";
                            if (words > i && words < i)
                                b = b + " ";
                        }
                        b = b + ArrEnd[r.Next(0, 5)] + " ";
                        strsize--;
                    }
                    Console.WriteLine(b);
                    Console.WriteLine();
                    break;

                case 2:
                    Console.WriteLine("Введите строку "); 
                    b = Console.ReadLine();
                    if (b != "")
                    {
                        string zn = ".!?,;:";
                        int k = -2;
                        for (int i = 0; i <= 5; i++)// добавление пробелов после знаков препинания
                        {
                            while (k != -1 && k + 2 <= b.Length - 1)
                            {
                                k = b.IndexOf(zn[i], k + 2);
                                if (k + 2 <= b.Length - 1)
                                    if (k != -1 && b[k + 1] != ' ')
                                        b = b.Insert(k + 1, " ");
                            }
                            k = -2;
                        }
                        if (b.Length - 1 != b.LastIndexOf(".") + 1 && b.Length != b.LastIndexOf(".") + 1)
                            b = b.Insert(b.Length, ".");
                        if (b[b.Length - 1] != ' ')
                            b = b + " ";
                        Console.WriteLine();
                        Console.WriteLine(b);
                        Console.WriteLine();
                    }
                    else
                        Console.WriteLine("Строка пустая");                    
                    break;
            }
        }

        static char[] ReadArray(int size) //Ввод массива с клавиатуры
        {
            char[] a = new char[size];
            if (size > 0)
                Console.WriteLine("Введите массив(по элементу в строке) ");
            for (int i = 0; i <= size - 1; i++)
            {
                a[i] = ReadSym("");
            }
            WriteArray(a, size);            
            return a;
        }

        static void WriteArray(char[] a, int size) //Вывод массива на экран
        {
            if (a.Length == 0)
                Console.WriteLine("Массив пустой");
            else
                for (int i = 0; i <= size - 1; i++)
                    Console.Write(a[i] + " ");
            Console.WriteLine();            
        }

        static void DelLastGl(ref char[] a, ref int size) // удаляет последюю гласную букву в массиве
        {
            string Gl = "aeiouyаоуэыяёюеи";
            int max = -1;
            if(a == null)
            {
                Console.WriteLine("Массив пустой");
            }
            else
            {
                if (a.Length == 0)
                    Console.WriteLine("Массив пустой");
                else
                {
                    for (int i = 0; i <= Gl.Length - 1; i++)
                    {
                        if (max <= Array.LastIndexOf(a, Gl[i]))
                        {
                            max = Array.LastIndexOf(a, Gl[i]);
                        }
                    }
                    char[] temp = new char[size - 1];
                    if (max > -1)
                    {
                        for (int i = 0; i < max; i++)
                        {
                            temp[i] = a[i];
                        }
                        int j = max;
                        for (int i = max + 1; i <= size - 1; i++)
                        {
                            temp[j] = a[i];
                            j++;
                        }
                        a = temp;
                        size--;
                        WriteArray(a, size);
                    }
                    else
                        Console.WriteLine("В массиве нет гласных букв ");
                    Console.WriteLine();
                }                
            }            
        }

        static void Change(ref string b) //меняет местами первое и последнее предложение
        {
            string temp = "";
            if (b.Length == 0)
                Console.WriteLine("Строка пустая ");
            else
            {
                int maxst = b.Length;
                int start = b.IndexOf(".");
                if (start < maxst && start != -1)
                    maxst = start;
                start = b.IndexOf("!");
                if (start < maxst && start != -1)
                    maxst = start;
                start = b.IndexOf("?");
                if (start < maxst && start != -1)
                    maxst = start;
                int maxfin = -1;
                int fin = b.LastIndexOf(".", b.Length - 3);
                if (fin > maxfin)
                    maxfin = fin;
                fin = b.LastIndexOf("!", b.Length - 3);
                if (fin > maxfin)
                    maxfin = fin;
                fin = b.LastIndexOf("?", b.Length - 3);
                if (fin > maxfin)
                    maxfin = fin;
                if (maxfin == -1 || maxst == -1)
                    Console.WriteLine("В строке 1 предложение, либо неверное расставлены знаки препинания ");
                else
                {
                    temp = temp + b.Substring(maxfin + 2, b.Length - 1 - (maxfin + 1));
                    temp = temp + b.Substring(maxst + 2, maxfin + 1 - (maxst + 1));
                    temp = temp + b.Substring(0, maxst + 1);
                    b = temp;
                    Console.WriteLine(b);
                }
            }
            Console.WriteLine();

        }

        static void Main(string[] args)
        {          
            string b = "";                      
            int userChoice = -1;
            int size = 0;
            char[] a = new char[size];
            a = null;
            do
            {
                switch (userChoice)
                {
                    case 1:
                        size = ReadSym("Введите размер масива ", 0, 20);
                        a = new char[size];
                        a = CreateArray(size);
                        break;

                    case 2:
                        ReadString(ref b);
                        break;

                    case 3:
                        DelLastGl(ref a, ref size);
                        break;

                    case 4:
                        Change(ref b);
                        break;
                }

                Console.WriteLine("Выберите операцию");
                Console.WriteLine("1 - создание одномерного массива");
                Console.WriteLine("2 - создание строки");
                Console.WriteLine("3 - удаление из массива последней гласной буквы");
                Console.WriteLine("4 - поменять местами первое и последнее предложение");
                Console.WriteLine("0 - выход");
                userChoice = ReadSym("Введите число ", 0, 4);

            } while (userChoice != 0);            
        }
    }
}
