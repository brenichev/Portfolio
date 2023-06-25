using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Lab10
{
    public class ShipbuildingCompany : Organization
    {
        protected int capital;

        public int Capital
        {
            get { return capital; }
            set { capital = value; }
        }

        public ShipbuildingCompany() : base()
        {
            Capital = 0;
        }

        public ShipbuildingCompany(int money) : base()
        {
            Capital = money;
        }

        public ShipbuildingCompany(string name, string city, int employees, int money) : base(name, city, employees)
        {
            Capital = money;
        }

        [ExcludeFromCodeCoverage]
        public ShipbuildingCompany(Organization org, int money) : base(org.Name, org.City, org.Employees)
        {
            Capital = money;
        }

        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Капитал - {Capital}.");
        }

        public override string ToString()
        {
            return ($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Капитал - {Capital}.");
        }

        public override object Clone()
        {
            return new ShipbuildingCompany(Name, City, Employees, Capital);
        }

        public override object Copy()
        {
            return (ShipbuildingCompany)MemberwiseClone();
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
            return name.GetHashCode() + city.GetHashCode() + employees.GetHashCode() + capital.GetHashCode();
        }

        public override void Input()
        {
            base.Input();
            bool check = false;

            do
            {
                int value;
                Console.WriteLine("Введите капитал компании");
                check = int.TryParse(Console.ReadLine(), out value);
                if (!check) Console.WriteLine("Введены неверные данные");

                else if (check)
                {
                    this.Capital = value;
                    if (this.capital == 0)
                    {
                        check = false;
                        Console.WriteLine("Введены неверные данные");
                    }
                    else check = true;
                }
            } while (!check);  // ввод капитала         
        }
    }
}
