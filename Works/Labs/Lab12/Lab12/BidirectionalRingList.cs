using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;

namespace Lab12
{
    public class BidirectionalRingList<T> : ICloneable, IEnumerable<T>
    {
        private class Point
        {
            public T Data { get; set; }
            public Point Next { get; set; }
            public Point Prev { get; set; }

            public Point(T item)
            {
                Data = item;
                Next = null;
                Prev = null;
            }
        }

        public int Count { get; private set; }

        private Point beg;
        private Point end;

        public BidirectionalRingList()
        {
            Count = 0;
            beg = null;
        }

        public BidirectionalRingList(int size)
        {
            if (size < 0) Console.WriteLine(" === Размер коллекции не должен быть меньше 0 === ");
            else
            {
                Count = size;
                beg = null;
            }
        }

        public BidirectionalRingList(BidirectionalRingList<T> list)
        {
            Count = 0;
            Point p = list.beg;
            Point end2 = list.end;
            bool start = true;

            while (p.Prev != end2 || start == true)
            {
                AddToEnd(p.Data);
                p = p.Next;
                start = false;
            }
            /*p.Next = beg;
            beg.Prev = end;*/
        }

        public void AddToStart(T item)
        {
            Point p = new Point(item);

            if (beg != null)
            {
                p.Prev = end;
                p.Next = beg;
                beg.Prev = p;
                end.Next = p;
            }
            else end = p;

            beg = p;
            Count++;
        }

        public void AddToEnd(T item)
        {
            Point p = new Point(item);

            if (beg == null) beg = p;
            else
            {
                end.Next = p;
                beg.Prev = p;
                p.Prev = end;
                p.Next = beg;
            }

            end = p;
            Count++;
        }

        public void AddAt(int i, T org)
        {
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

        public void ShowForward()
        {
            if (Count == 0)
            {
                Console.WriteLine(" === Список пуст === ");
                return;
            }

            Point p = beg;
            int i = 0;
            bool start = true;

            //while (i < Count)
            while(p != beg || start == true)
            {
                Console.WriteLine($"{i} : {p.Data}");
                i++;
                p = p.Next;
                start = false;
            }            
        }

        public object Clone()
        {
            return new BidirectionalRingList<T>(this);
        }

        public BidirectionalRingList<T> Copy()
        {
            return this;
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
                    beg.Prev = end;
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
                    prev.Next = beg;
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

        public void Remove(Organization item)
        {
            Point find = beg;
            Point prev = null;

            if (beg.Data.Equals(item))
            {
                beg = beg.Next;
                beg.Prev = null;
                Count--;
                return;
            }

            while (find != null)
            {
                if (find.Data.Equals(item)) break;
                prev = find;
                find = find.Next;
            }

            if (find == null) Console.WriteLine(" === Такой элемент не найден === ");
            else
            {
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

        public bool Contains(T item)
        {
            if (item == null) throw new NullReferenceException();

            bool ok = false;
            Point p = beg;

            while (p.Next != beg && !ok)
            {
                ok = p.Data.Equals(item);
                p = p.Next;
            }

            return ok;
        }

        public void Delete()
        {
            Point p = beg;

            while (p != null)
            {
                Point prev = p;
                p = p.Next;
                prev.Next = null;
            }

            beg = null;
            end = null;
            Count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        class MyEnumerator : IEnumerator<T>
        {
            Point beg;//начало коллекции
            Point current;//текущий элемент коллекции
            public MyEnumerator(BidirectionalRingList<T> c)//конструктор
            {

                beg = c.beg;
                current = null;

            }
            /*свойство, которое возвращает информационное поле текущего элемента, реализует свойство интерфейса IEnumerator<T>*/
            public T Current
            {
                get { return current.Data; }
            }
            /*свойство, которое реализует интерфейс IEnumerator и преобразует Т в object*/

            object IEnumerator.Current
            {
                get { return Current; }
            }
            /*метод для перехода к следующему элементу списка, реализует интерфейс IEnumerator */
            public bool MoveNext()
            {
                if (current == null)
                {
                    current = beg;
                    return true;
                }
                if (current.Next == beg)//конец списка
                {
                    Reset();//переход на начало коллекции
                    return false;
                }
                else
                {
                    //переход к следующему элементу коллекции
                    current = current.Next;
                    return true;
                }
            }
            /*метод, который ставит текущий элемент на начало коллекции, реализует интерфейс IEnumerator */
            public void Reset()
            {
                current = this.beg;

            }
            /*метод для удаления ресурсов нумератора, реализует интерфейс IEnumerator<T> */
            public void Dispose() { }
        }

    }
}
