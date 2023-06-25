using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Lab10
{
    public class InsuranceCompany : Organization
    {
        protected int insuranceFund;
        protected string region;

        public int InsuranceFund
        {
            get { return insuranceFund; }
            set { insuranceFund = value; }
        }

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        public InsuranceCompany() : base()
        {
            InsuranceFund = 0;
            Region = "";
        }

        public InsuranceCompany(int fund, string reg) : base()
        {
            InsuranceFund = fund;
            Region = reg;
        }

        public InsuranceCompany(string name, string city, int employees, int fund, string reg) : base(name, city, employees)
        {
            InsuranceFund = fund;
            Region = reg;
        }

        [ExcludeFromCodeCoverage] //Organization - абстрактный класс
        public InsuranceCompany(Organization org, int fund, string reg) : base(org.Name, org.City, org.Employees)
        {
            InsuranceFund = fund;
            Region = reg;
        }

        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Страховой фонд - {InsuranceFund}. Регион компании - {Region}");
        }

        public override string ToString()
        {
            return ($"Название организации - {Name}. Город - {City}. Количество сотрудников - {Employees}. Страховой фонд - {InsuranceFund}. Регион компании - {Region}");
        }

        public override object Clone()
        {
            return new InsuranceCompany(Name, City, Employees, InsuranceFund, Region);
        }

        public override object Copy()
        {
            return (InsuranceCompany)MemberwiseClone();
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
            return name.GetHashCode() + city.GetHashCode() + employees.GetHashCode() + insuranceFund.GetHashCode()+ region.GetHashCode();
        }

        public Organization BaseOrganization
        {
            get { return new Organization(name, city, employees); }
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
                    this.InsuranceFund = value;
                    if (this.insuranceFund == 0)
                    {
                        check = false;
                        Console.WriteLine("Введены неверные данные");
                    }
                    else check = true;
                }
            } while (!check);  // ввод страхового фонда

            Console.WriteLine("Введите регион работы страховой компании");
            this.Region = Console.ReadLine();
        }
    }
}
