using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;

namespace Lab12
{
    public class UnidirectionalList : IEnumerable
    {
        private class Point
        {
            public Organization Data { get; set; }
            public Point Next { get; set; }

            public Point()
            {
                Data = null;
                Next = null;
            }

            public Point(Organization item)
            {
                Data = item;
                Next = null;
            }
        }

        public int Count { get; private set; }
        private Point beg;
        private Point end;

        public IEnumerator GetEnumerator()
        {
            var current = beg;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public UnidirectionalList()
        {
            Count = 0;
            beg = null;
            end = null;
        }

        public void AddToStart(Organization org)
        {
            Point p = new Point(org);

            if (beg != null) p.Next = beg;
            else end = p;
            beg = p;
            Count++;
        }

        public void AddToEnd(Organization org)
        {
            Point p = new Point(org);

            if (beg == null) beg = p;
            else
                end.Next = p;
            end = p;
            Count++;
        }

        public void Show()
        {
            if (Count == 0)
            {
                Console.WriteLine(" === Список пуст === ");
                return;
            }

            Point p = beg;
            int i = 0;

            while (p != null)
            {
                Console.WriteLine($"{i} : {p.Data}");
                i++;
                p = p.Next;
            }
        }

        public void DeleteList()
        {
            Point p = beg;
            while (p != null)
            {
                Point cur = p;
                p = p.Next;
                cur.Next = null;
            }

            Count = 0;
            beg = null;
            end = null;
        }

        public void RunTask()
        {
            Console.WriteLine(" === Задание: === \n === Удалить из списка последний элемент с четным информационным полем. === \n");

            Point p = beg;
            Point find = null;
            while (p != null && p.Next != null)
            {
                if (p.Next.Data.Employees % 2 == 0)
                {
                    find = p;
                    p = p.Next;
                }
                else p = p.Next;
            }
            if (find == null && beg.Data.Employees % 2 == 0)
            {
                beg = beg.Next;
                Count--;
            }
            else
            if (find != null)
            {
                find.Next = find.Next.Next;
                Count--;
            }
            else
                Console.WriteLine(" === Такого элемента нет === ");
        }

        public void RemoveAt(int i)
        {
            if (i < 0) Console.WriteLine(" === Номер элемента не может быть отрицательным === ");
            else
            if (i > Count) Console.WriteLine(" === Номер элемента должен быть меньше размера листа === ");
            else
            {
                if (i == 0)
                {
                    beg = beg.Next;
                    Count--;
                    return;
                }

                Point find = beg;
                int ind = 0;
                while (ind + 1 < i)
                {
                    find = find.Next;
                    ind++;
                }

                find.Next = find.Next.Next;
                if (i == Count - 1)
                {
                    end = find;
                }
                Count--;
            }
        }

    }
}
