using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Lab10
{
    public class Library : Organization
    {
        protected int numberOfBooks; //Количество книг в бибилиотеке

        public int NumberOfBooks
        {
            get { return numberOfBooks; }
            set 
            {
                if (numberOfBooks < 0) numberOfBooks = 0;
                else numberOfBooks = value; 
            }
        }

        public Library() : base()
        {
            NumberOfBooks = 0;
        }

        public Library(int books) : base()
        {
            NumberOfBooks = books;
        }

        public Library(string name, string city, int employees, int books) : base(name, city, employees)
        {
            NumberOfBooks = books;
        }

        [ExcludeFromCodeCoverage]
        public Library(Organization org, int books) : base(org.Name, org.City, org.Employees)
        {
            NumberOfBooks = books;
        }

        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Количество книг - {NumberOfBooks}.");
        }

        public override string ToString()
        {
            return ($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Количество книг - {NumberOfBooks}.");
        }

        public override object Clone()
        {
            return new Library(Name, City, Employees, NumberOfBooks);
        }

        public override object Copy()
        {
            return (Library)MemberwiseClone();
        }

        public new int Compare(object obj1, object obj2)//реализация интерфейса
        {
            Organization temp1 = (Organization)obj1;
            Organization temp2 = (Organization)obj2;
            if (String.Compare(temp1.Name, temp2.Name) > 0) return 1;
            if (String.Compare(temp1.Name, temp2.Name) < 0) return -1;
            return 0;
        }

        public new int CompareTo(object obj1)//реализация интерфейса
        {
            Organization temp = (Organization)obj1;
            if (String.Compare(this.Name, temp.Name) > 0) return 1;
            if (String.Compare(this.Name, temp.Name) < 0) return -1;
            return 0;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + city.GetHashCode() + employees.GetHashCode() + numberOfBooks.GetHashCode();
        }

        public override void Input()
        {
            base.Input();
            bool check = false;

            do
            {
                int value;
                Console.WriteLine("Введите количество книг в библиотеке");
                check = int.TryParse(Console.ReadLine(), out value);
                if (!check) Console.WriteLine("Введены неверные данные");

                else if (check)
                {
                    this.NumberOfBooks = value;
                    if (this.numberOfBooks == 0)
                    {
                        check = false;
                        Console.WriteLine("Введены неверные данные");
                    }
                    else check = true;
                }
            } while (!check);  // ввод количества книг         
        }
    }
}
