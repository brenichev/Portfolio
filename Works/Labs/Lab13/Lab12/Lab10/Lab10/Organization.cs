using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IInterface : ICloneable, IComparable
    {
        //object Clone();
        int Compare(object obj1, object obj2);

        //int CompareTo(object obj1);

        void Show();

    }
    [ExcludeFromCodeCoverage]
    public class Organization : IInterface
    {
        protected string name;
        protected string city;
        protected int employees;

        [ExcludeFromCodeCoverage]
        public int Compare(object obj1, object obj2)//реализация интерфейса
        {
            Organization temp1 = (Organization)obj1;
            Organization temp2 = (Organization)obj2;
            if (String.Compare(temp1.Name, temp2.Name) > 0) return 1;
            if (String.Compare(temp1.Name, temp2.Name) < 0) return -1;
            return 0;
        }

        [ExcludeFromCodeCoverage]
        public int CompareTo(object obj1)//реализация интерфейса
        {
            Organization temp = (Organization)obj1;
            if (String.Compare(this.Name, temp.Name) > 0) return 1;
            if (String.Compare(this.Name, temp.Name) < 0) return -1;
            return 0;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(Object obj)
        {
            Organization p = obj as Organization;

            if (p == null) return false;
            else return (Name == p.Name && City == p.City && Employees == p.Employees);
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public int Employees
        {
            get { return employees; }
            set
            {
                if (value < 0) employees = 0;
                else employees = value;
            }
        }

        [ExcludeFromCodeCoverage]
        public Organization()
        {
            Name = "";
            City = "";
            Employees = 0;
        }

        [ExcludeFromCodeCoverage]
        public Organization(string name, string city, int employees)
        {
            Name = name;
            City = city;
            Employees = employees;
        }

        [ExcludeFromCodeCoverage]
        public virtual void Show()
        {
            Console.WriteLine($"Название - {Name}. Город - {City}. Количество сотрудников - {Employees}");
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return ($"Название - {Name}. Город - {City}. Количество сотрудников - {Employees}");
        }

        public virtual object Clone()
        {
            return new Organization(Name, City, Employees);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + city.GetHashCode() + employees.GetHashCode();
        }

        [ExcludeFromCodeCoverage]
        public virtual object Copy()
        {
            return (Organization)MemberwiseClone();
        }

        public virtual void Input()
        {
            Console.WriteLine("Введите название организации");
            this.Name = Console.ReadLine();

            Console.WriteLine("Введите город, в котором находится организация");
            this.City = Console.ReadLine();

            bool check = false;
            do
            {
                int value;
                Console.WriteLine("Введите количество работников");
                check = int.TryParse(Console.ReadLine(), out value);
                if (!check) Console.WriteLine("Введены неверные данные");

                else if (check)
                {
                    this.Employees = value;
                    if (this.employees == 0)
                    {
                        check = false;
                        Console.WriteLine("Введены неверные данные");
                    }
                    else check = true;
                }
            } while (!check);  // ввод количества работников
        }

        public static Random rnd = new Random();

        public static Organization RandomOrganization()
        {
            Organization org = null;
            int type = rnd.Next(1, 5);

            switch (type)
            {
                case 1:

                    org = new Organization("Организация", "Город", rnd.Next(20, 10000));
                    break;

                case 2:

                    org = new Factory("Завод", "Город", rnd.Next(20, 10000), "Отрасль");
                    break;

                case 3:

                    org = new InsuranceCompany("Страховая компания", "Город", rnd.Next(20, 10000), rnd.Next(1000000, 100000000), "Регион");
                    break;
                case 4:

                    org = new Library("Библиотека", "Город", rnd.Next(20, 10000), rnd.Next(500, 100000));
                    break;
                case 5:

                    org = new ShipbuildingCompany("Судостроительная компания", "Город", rnd.Next(20, 10000), rnd.Next(1000000, 100000000));
                    break;
            }

            return org;
        }

    }
}
