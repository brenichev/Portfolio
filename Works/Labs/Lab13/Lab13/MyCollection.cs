using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10;
using Lab12;

namespace Lab13
{
    public class MyCollection : UnidirectionalList<Organization>
    {

        public int Length
        {
            get
            {
                return this.Count;
            }
        }

        public MyCollection()
        {
        }

        public MyCollection(int size)
        {
            for (int i = 0; i < size; i++)
                this.Add();
        }

        public virtual void Add()
        {
            base.AddToEnd(Organization.RandomOrganization());
        }

        public virtual void Add(Organization org)
        {
            base.AddToEnd(org);
        }

        public virtual void AddDefaults()
        {
            base.AddToEnd(default);
        }

        public virtual void Add(Organization[] orgs)
        {
            foreach (Organization add in orgs)
            {
                base.AddToEnd(add);
            }
        }

        public virtual bool Remove(int i)
        {
            if (i >= 0 && i < Length)
            {
                base.RemoveAt(i);
                return true;
            }
            else
                return false;
        }

        public void Sort()
        {
            for (int i = 1; i < this.Count; i++)
            {
                for (var j = 0; j < this.Count - i; j++)
                {
                    if (this[j].Employees > this[j + 1].Employees)
                    {
                        var temp = this[j];
                        this[j] = this[j + 1];
                        this[j + 1] = temp;
                    }
                }
            }
        }

        public Organization this[int i]
        {
            get
            {
                if (i > -1 && i < Count)
                {
                    var curr = this.Beg;
                    for (int index = 0; index < i; index++) curr = curr.Next;
                    return (curr.Data);
                }
                else
                {
                    Console.WriteLine("=== В коллекции нет элемента с таким индексом, либо коллекция пустая ===");
                    return new Organization();
                    //throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (i > -1 && i < Count)
                {
                    var curr = this.Beg;
                    for (int index = 0; index < i; index++) curr = curr.Next;
                    curr.Data = value;
                }
                else
                {
                    Console.WriteLine("=== В коллекции нет элемента с таким индексом, либо коллекция пустая ===");
                    //throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class MyNewCollection : MyCollection
    {        
        public string Name { get; private set; }

        public MyNewCollection(string collectionName)
        {
            Name = collectionName;
        }

        public MyNewCollection(string collectionName, int size)
        {
            Name = collectionName;

            for (int i = 0; i < size; i++)
                this.Add();
        }

        public Organization this[int i]
        {
            get
            {
                if (i > -1 && i < Count)
                {
                    var curr = this.Beg;
                    for (int index = 0; index < i; index++) curr = curr.Next;
                    return (curr.Data);
                }
                else
                {
                    Console.WriteLine("=== В коллекции нет элемента с таким индексом, либо коллекция пустая ===");
                    return new Organization();
                    //throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (i > -1 && i < Count)
                {
                    var curr = this.Beg;
                    for (int index = 0; index < i; index++) curr = curr.Next;
                    CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(Name, "Присвоение нового значения", value));
                    curr.Data = value;
                }
                else
                {
                    Console.WriteLine("=== В коллекции нет элемента с таким индексом, либо коллекция пустая ===");
                    //throw new ArgumentOutOfRangeException();
                }
            }
        }

        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }

        public override bool Remove(int index)
        {
            if (index > -1 && index < Count)
            {
                Organization org = this[index];
                base.RemoveAt(index);
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Remove", org));
                return true;
            }
            else return false;
        }

        public override void Add(Organization org)
        {
            base.Add(org);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Add", org));
        }
        public override void Add()
        {
            base.Add();
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "AddRandom", this[Count - 1]));
        }

        public override void AddDefaults()
        {
            base.AddDefaults();
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "AddDefaults"));
        }
        public override void Add(Organization[] orgs)
        {
            base.Add(orgs);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Add Organization[]", orgs));
        }
    }
}

