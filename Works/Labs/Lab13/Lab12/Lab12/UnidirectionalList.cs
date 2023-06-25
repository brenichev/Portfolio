using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;

namespace Lab12
{
    public class UnidirectionalList<T> /*IEnumerable*/ where T:IComparable
    {
        public class Point<T> where T: IComparable
        {
            public T Data { get; set; }
            public Point<T> Next { get; set; }

            public Point()
            {
                Data = default(T);
                Next = null;
            }

            public Point(T item)
            {
                Data = item;
                Next = null;
            }
        }

        public int Count { get; private set; }
        public Point<T> beg;
        public Point<T> end;

        public IEnumerator GetEnumerator()
        {
            var current = beg;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public Point<T> Beg
        {
            get { return beg; }
            set { beg = value; }
        }

        public UnidirectionalList()
        {
            Count = 0;
            beg = null;
            end = null;
        }

        public void AddToStart(T org)
        {
            Point<T> p = new Point<T>(org);

            if (beg != null) p.Next = beg;
            else end = p;
            beg = p;
            Count++;
        }

        public void AddToEnd(T org)
        {
            Point<T> p = new Point<T>(org);

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

            Point<T> p = beg;
            int i = 0;

            while (p != null)
            {
                Console.WriteLine($"{i} : {p.Data}");
                i++;
                p = p.Next;
            }
        }

        public void Delete()
        {
            Point<T> p = beg;
            while (p != null)
            {
                Point<T> cur = p;
                p = p.Next;
                cur.Next = null;
            }

            Count = 0;
            beg = null;
            end = null;
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

                Point<T> find = beg;
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
