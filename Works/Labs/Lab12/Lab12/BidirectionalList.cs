using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;

namespace Lab12
{
    public class BidirectionalList : IEnumerable
    {
        private class Point
        {
            public Organization Data { get; set; }
            public Point Next { get; set; }
            public Point Prev { get; set; }

            public Point(Organization item)
            {
                Data = item;
            }
        }

        public int Count { get; private set; }
        private Point beg;
        private Point end;

        public void AddToStart(Organization org)
        {
            Point p = new Point(org);

            if (beg != null)
            {
                p.Prev = null;
                p.Next = beg;
                beg.Prev = p;
            }
            else end = p;

            beg = p;
            Count++;
        }

        public void AddToEnd(Organization org)
        {
            Point p = new Point(org);

            if (beg == null) beg = p;
            else
            {
                end.Next = p;
                p.Prev = end;
                p.Next = null;
            }

            end = p;
            Count++;
        }

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

        public void DeleteList()
        {
            Point p = beg;
            while (p != null)
            {
                Point cur = p;
                p = p.Next;
                cur.Next = null;
                cur.Prev = null;
            }

            beg = null;
            end = null;
            Count = 0;
        }

        public void ShowForward()
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

        public void ShowBackward()
        {
            if (Count == 0)
            {
                Console.WriteLine(" === Список пуст === ");
                return;
            }

            Point p = end;
            int i = Count - 1;

            while (p != null)
            {
                Console.WriteLine($"{i} : {p.Data}");
                i--;
                p = p.Prev;
            }
        }

        public void TaskAddAt(int i, Organization org)
        {
            Console.WriteLine(" === Задание: === \n === Добавить элемент с заданным номером === \n");
            if (i < 0) Console.WriteLine(" === Номер элемента не может быть отрицательным === ");
            else
                if (i >= Count) Console.WriteLine(" === Номер элемента должен быть меньше размера листа === ");
            else
            {
                if (i == 0) AddToStart(org);
                else if (i == Count) AddToEnd(org);
                else
                {
                    Point find = beg;
                    int ind = 0;

                    while (ind < i)
                    {
                        find = find.Next;
                        ind++;
                    }

                    Point p = new Point(org);

                    p.Next = find;
                    p.Prev = find.Prev;
                    find.Prev.Next = p;
                    find.Prev = p;
                    Count++;
                }
            }
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
                    beg.Prev = null;
                    Count--;
                    return;
                }

                Point find = beg;
                int ind = 0;
                while (ind < i)
                {
                    find = find.Next;
                    ind++;
                }

                Point prev = find.Prev;
                Point next = find.Next;

                if (find == end)
                {
                    prev.Next = null;
                    end = prev;
                }
                else
                {
                    prev.Next = next;
                    next.Prev = prev;
                }
                Count--;
            }
        }
    }
}
