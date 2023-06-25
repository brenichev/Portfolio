using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design.Serialization;
using Lab10;
using Lab12;

namespace Lab12
{
    public class BinaryTree<T> : IEnumerable<Organization>
        where T : IComparable
    {
        public class Point<T> where T:IComparable
        {
            public Organization Data;
            public Point<T> Left;
            public Point<T> Right;

            public Point(Organization org)
            {
                Data = org;
                Left = null;
                Right = null;
            }

            int CompareTo(T other)
            {
                return Data.CompareTo(other);
            }

            public override string ToString()
            {
                return Data.ToString() + " ";
            }
        }

        public int Count { get; set; }
        public Point<T> root = null;
        private Random rand = new Random();
        private int j;

        public Organization CreateRand()
        {
            string[] namesList = new string[3];
            string[] citiesList = new string[3];
            string[] regionList = new string[3];

            namesList[0] = "Организация";
            namesList[1] = "Другая Организация";
            namesList[2] = "Новая Организация";

            citiesList[0] = "Город";
            citiesList[1] = "Другой Город";
            citiesList[2] = "Снова Город";
            // Инициализация

            int employyesNum = rand.Next(25, 10000);
            int nrnd = rand.Next(0, 2);
            int crnd = rand.Next(0, 2);
            j++;
            return new Organization(namesList[nrnd] + j, citiesList[crnd], employyesNum);
        }

        public BinaryTree(int size)
        {
            j = 0;
            root = null;
            Count = size;

            root = CreatePointTree(root, size);
        }

        public BinaryTree()
        {
            j = 0;
            root = null;
            Count = 0;
        }

        private Point<T> CreatePointTree(Point<T> root, int size)
        {
            Point<T> p = null;
            if (size == 0) return null;

            int left = size / 2;
            int right = size - left - 1;
            Organization org = CreateRand();

            p = new Point<T>(org);
            p.Left = CreatePointTree(p.Left, left);
            p.Right = CreatePointTree(p.Right, right);
            return p;
        }

        public void Show()
        {
            ShowTree(root, 0);
        }

        private void ShowTree(Point<T> p, int l)
        {
            if (p != null)
            {
                ShowTree(p.Left, l + 4);
                for (int i = 0; i < l; i++) Console.Write(" ");
                Console.WriteLine(p.Data);
                ShowTree(p.Right, l + 4);
            }
        }

        public void Task()
        {
            Console.WriteLine(" === Задание: найти минимальный элемент в дереве === ");

            Point<T> min = root;
            Run(root, ref min);

            Console.WriteLine($" === Минимальный элемент = {min.Data} === ");
        }

        private void Run(Point<T> p, ref Point<T> min)
        {
            if (p != null)
            {
                //if (p.Data.CompareTo(min.Data) == -1) min = p; //Сравнение по имени
                if (p.Data.Employees < min.Data.Employees) //Сравнение по количеству работников
                    min = p;
                Run(p.Left, ref min);
                Run(p.Right, ref min);
            }
        }
        public IEnumerator<Organization> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Organization> InOrderTraversal()
        {
            // Это нерекурсивный алгоритм.
            // Он использует стек для того, чтобы избежать рекурсии.
            if (root != null)
            {
                // Стек для сохранения пропущенных узлов.
                Stack<Point<T>> stack = new Stack<Point<T>>();

                Point<T> current = root;

                // Когда мы избавляемся от рекурсии, нам необходимо
                // запоминать, в какую стороны мы должны двигаться.
                bool goLeftNext = true;

                // Кладем в стек корень.
                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        // Кладем все, кроме самого левого узла на стек.
                        // Крайний левый узел мы вернем с помощю yield.
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    Organization cur = current.Data as Organization;
                    yield return cur;

                    if (current.Right != null)
                    {
                        current = current.Right;

                        // После того, как мы пошли направо один раз,
                        // мы должным снова пойти налево.
                        goLeftNext = true;
                    }
                    else
                    {
                        // Если мы не можем пойти направо, мы должны достать родительский узел
                        // со стека, обработать его и идти в его правого ребенка.
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }


        private void ClearRun(Point<T> p)
        {
            if (p == null) return;

            ClearRun(p.Left);
            ClearRun(p.Right);
            p = null;
        }
        
        public void DeleteTree()
        {
            ClearRun(root);
            root = null;
            Count = 0;
        }

        public void CreateSearchTree()
        {
            Organization[] arr = new Organization[Count];
            ToArray(root, 0, Count, ref arr);

            ClearRun(root);
            root = null;
            foreach (Organization org in arr) root = Insert(root, org);
        }

        private Point<T> Insert(Point<T> root, Organization org)
        {
            if (root == null) return new Point<T>(org);

            if (org.Employees < root.Data.Employees)
                root.Left = Insert(root.Left, org);
            else root.Right = Insert(root.Right, org);

            return root;
        }

        private void ToArray(Point<T> node, int i, int size, ref Organization[] arr)
        {
            if (node == null) return;

            arr[i] = (Organization)node.Data.Clone();
            ToArray(node.Left, i + 1, size / 2, ref arr);
            ToArray(node.Right, i + size / 2 + 1, size - size / 2 - 1, ref arr);
        }
    }
}

