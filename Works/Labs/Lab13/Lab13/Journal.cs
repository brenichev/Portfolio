using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    class Journal
    {
        private List<JournalEntry> journalEntries = new List<JournalEntry>();

        public void CollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (args.Object != null)
                journalEntries.Add(new JournalEntry(args.CollectionName, args.ChangeType, args.Object.ToString()));
            else
                journalEntries.Add(new JournalEntry(args.CollectionName, args.ChangeType));
        }

        public void CollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (args.Object != null)
                journalEntries.Add(new JournalEntry(args.CollectionName, args.ChangeType, args.Object.ToString()));
            else
                journalEntries.Add(new JournalEntry(args.CollectionName, args.ChangeType));
        }

        public override string ToString()
        {
            string str = "";
            foreach (JournalEntry t in journalEntries)
            {
                str += "====================================\n" + t.ToString() + "\n";
            }
            str += "====================================";
            return str;
        }
    }

    class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ObjectInfo { get; set; }

        public JournalEntry(string name, string type, string obj)
        {
            CollectionName = name;
            ChangeType = type;
            ObjectInfo = obj;
        }

        public JournalEntry(string name, string type)
        {
            CollectionName = name;
            ChangeType = type;
            ObjectInfo = default;
        }

        public override string ToString()
        {
            return $"Название коллекции: {CollectionName}\nТип изменения: {ChangeType}\nОбъект: {ObjectInfo}";
        }
    }
}
