using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Lab10
{
    public class Factory : Organization
    {
        protected string sector; //отрасль

        public string Sector
        {
            get { return sector; }
            set { sector = value; }
        }

        public Factory() : base()
        {
            Sector = "";
        }

        public Factory(string industry) : base()
        {
            Sector = industry;
        }

        public Factory(string name, string city, int employees, string industry) : base(name, city, employees)
        {
            Sector = industry;
        }

        [ExcludeFromCodeCoverage]
        public Factory(Organization org, string industry) : base(org.Name, org.City, org.Employees)
        {
            Sector = industry;
        }

        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Капитал - {Sector}.");
        }

        public override string ToString()
        {
            return ($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Капитал - {Sector}.");
        }

        public override object Clone()
        {
            return new Factory(Name, City, Employees, Sector);
        }

        public override object Copy()
        {
            return (Factory)MemberwiseClone();
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
            return name.GetHashCode() + city.GetHashCode() + employees.GetHashCode() + sector.GetHashCode();
        }

        public override void Input()
        {
            base.Input();


            Console.WriteLine("Введите отрасль завода");
            this.Sector = Console.ReadLine();
        }
    }
}
