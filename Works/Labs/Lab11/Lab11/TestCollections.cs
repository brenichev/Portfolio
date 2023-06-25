using Lab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    class TestCollections
    {
        Queue<Organization> organizationQueue;
        Queue<string> stringQueue;
        SortedDictionary<Organization, InsuranceCompany> organizationSDictionary;
        SortedDictionary<string, InsuranceCompany> stringSDictionary;
        int length;

        public Queue<Organization> OrganizationQueue
        {
            get { return organizationQueue; }
        }
        public Queue<string> StringQueue
        {
            get { return stringQueue; }
        }
        public SortedDictionary<Organization, InsuranceCompany> OrganizationSDictionary
        {
            get { return organizationSDictionary; }
        }
        public SortedDictionary<string, InsuranceCompany> StringSDictionary
        {
            get { return stringSDictionary; }
        }
        public int Length
        {
            get { return length; }
        }

        public KeyValuePair<string, InsuranceCompany> CollectionFirst
        {
            get
            {
                int k = 0;
                foreach (KeyValuePair<string, InsuranceCompany> org in stringSDictionary)
                {
                    if (k == 0) return org;
                    k++;
                }
                return new KeyValuePair<string, InsuranceCompany>(null, null);
            }
        }

        public string CollectionStringFirst
        {
            get
            {
                int k = 0;
                foreach (string org in stringQueue)
                {
                    if (k == 0) return org;
                    k++;
                }
                return null;
            }
        }

        public KeyValuePair<string, InsuranceCompany> CollectionMiddle
        {
            get
            {
                int k = 0;
                foreach (KeyValuePair<string, InsuranceCompany> org in stringSDictionary)
                {
                    if (k++ == Length / 2) return org;
                }
                return new KeyValuePair<string, InsuranceCompany>(null, null);
            }            
        }

        public string CollectionStringMiddle
        {
            get
            {
                int k = 0;
                foreach (string org in stringQueue)
                {
                    if (k++ == Length / 2) return org;
                }
                return null;
            }
        }

        public void Show()
        {
            foreach (KeyValuePair<string, InsuranceCompany> pair in stringSDictionary)
            {
                pair.Value.Show();
                Console.WriteLine();
            }
        }

        public TestCollections(int l)
        {
            organizationQueue = new Queue<Organization>(l);
            stringQueue = new Queue<string>(l);
            organizationSDictionary = new SortedDictionary<Organization, InsuranceCompany>();
            stringSDictionary = new SortedDictionary<string, InsuranceCompany>();
            length = l;
        }

        public TestCollections CreateRand()
        {
            TestCollections collection = new TestCollections(length);
            string[] namesList = new string[3];
            string[] citiesList = new string[3];
            string[] regionList = new string[3];

            namesList[0] = "Организация";
            namesList[1] = "Другая Организация";
            namesList[2] = "Новая Организация";         

            citiesList[0] = "Город";
            citiesList[1] = "Другой Город";
            citiesList[2] = "Снова Город";

            regionList[0] = "Регион";
            regionList[1] = "Этот Регион";
            regionList[2] = "Другой Регион";
            // Инициализация


            Random rand = new Random();

            for (int j = 0; j < length; j++)
            {
                    int employyesNum = rand.Next(25, 10000);
                int insuranceFundNum = rand.Next(1000000, 100000000);
                int nrnd = rand.Next(0,2);
                int crnd = rand.Next(0, 2);
                int rrnd = rand.Next(0, 2);
                InsuranceCompany i = new InsuranceCompany(namesList[nrnd], citiesList[crnd], employyesNum, insuranceFundNum, regionList[rrnd]);
                InsuranceCompany current = (InsuranceCompany)i.Clone();
                current.Name = i.Name + j.ToString();
                Organization o = current.BaseOrganization;
                string name = o.ToString();

                collection.organizationQueue.Enqueue(o);
                collection.stringQueue.Enqueue(name);
                collection.organizationSDictionary.Add(o, current);
                collection.stringSDictionary.Add(name, current);
            }

            return collection;
        }
    }
}
