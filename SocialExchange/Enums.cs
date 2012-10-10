using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange.Enums
{
    public abstract class CustomEnum<ValueType>
    {
        public abstract class Entry
        {
            public ValueType Value { get; protected set; }

            public Entry(ValueType value)
            {
                Value = value;
            }
        }

        public abstract class EntryManager<EntryType> where EntryType : Entry
        {
            protected List<EntryType> _Entries = new List<EntryType>();
            public List<EntryType> EntryCollectionClone { get { return _Entries.AsReadOnly().ToList(); } }

            public EntryType Add(EntryType entry)
            {
                _Entries.Add(entry);
                return entry;
            }
        }
    }

    public class Examples : CustomEnum<string>
    {
        public class Example : CustomEnum<string>.Entry
        {
            public Example(string value) : base(value) { }
        }

        public static ExampleEntryManager EntryManager = new ExampleEntryManager();
        public class ExampleEntryManager : EntryManager<Example> { }

        public static readonly Examples.Example One = Examples.EntryManager.Add(new Example("One"));
        public static readonly Examples.Example Two = Examples.EntryManager.Add(new Example("Two"));
        public static readonly Examples.Example Three = Examples.EntryManager.Add(new Example("Three"));
    }
}
