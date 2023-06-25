using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public object Object { get; set; }

        public CollectionHandlerEventArgs(string name, string type, object obj) : base()
        {
            CollectionName = name;
            ChangeType = type;
            Object = obj;
        }

        public CollectionHandlerEventArgs(string name, string type)
        {
            CollectionName = name;
            ChangeType = type;
            Object = null;
        }

        public override string ToString()
        {
            return $"Название коллекции: {CollectionName}\nТип изменения: {ChangeType}\nОбъект: {Object}";
        }
    }
}
