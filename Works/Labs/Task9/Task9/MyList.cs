using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class MyList
    {
        [ExcludeFromCodeCoverage]
        public class Point
        {
            public int Data { get; set; }
            public Point Next { get; set; }

            public Point()
            {
                Data = 0;
                Next = null;
            }

            public Point(int item)
            {
                Data = item;
                Next = null;
            }
        }

        public int Count { get; private set; }
        private Point beg;
        private Point end;

        public void Add(int num)
        {
            Point p = new Point(num);

            if (beg == null) beg = p;
            else
            {
                p.Next = beg;
                end.Next = p;
            }

            end = p;

            Count++;
        }

        public MyList()
        {
            Count = 0;
            beg = null;
            end = null;
        }

        [ExcludeFromCodeCoverage]
        public void Show()
        {
            if (Count == 0)
            {
                Console.WriteLine(" === Список пуст === ");
                return;
            }

            Point p = beg;
            int i = 0;

            while (p.Next != beg)
            {
                Console.WriteLine($"{i} : {p.Data}");
                i++;
                p = p.Next;
            }
            Console.WriteLine($"{i} : {p.Data}");
        }

        public bool Remove(int i)
        {
                return RemoveKey(beg, i);            
        }

        [ExcludeFromCodeCoverage]
        public void RemoveAt2(int i)
        {
            if (i < 0) Console.WriteLine(" === Номер элемента не может быть отрицательным === ");
            else
            if (i > Count) Console.WriteLine(" === Номер элемента должен быть меньше размера списка === ");
            else
                Remove2(i);
        }

        [ExcludeFromCodeCoverage]
        public void Remove2(int num)
        {
            if (num == 0)
            {
                beg = beg.Next;
                end.Next = beg;
                Count--;
                return;
            }

            Point find = beg;
            int ind = 0;
            while (ind + 1 < num)
            {
                find = find.Next;
                ind++;
            }

            find.Next = find.Next.Next;
            if (num == Count - 1)
            {
                end = find;
            }
            Count--;
        }

        public bool RemoveKey(Point find, int num)
        {
            if (find.Next == beg)
                if (beg.Data == num)
                {
                    beg = find.Next.Next;
                    find.Next = find.Next.Next;
                    Count--;
                    return true;
                }
                else
                    return false;
            else
            if (find.Next.Data == num)
            {
                    find.Next = find.Next.Next;
                    Count--;
                    return true;
            }
            else
                return RemoveKey(find.Next, num);
        }
        public int Search(int num)
        {
            return SearchFromBeg(num, beg);
        }

        public int SearchFromBeg(int num, Point find, int i = 0)
        {
            if (find.Next == beg)
                    return -1;
            if (find.Data != num)
                return SearchFromBeg(num, find.Next, i+1);
            else
                return i;
        }

        public void Task(int num)
        {
            if (this.Count < num)
            {
                this.Add(Count + 1);
                Task(num);
                return;
            }
            else
                return;
        }
    }
}
