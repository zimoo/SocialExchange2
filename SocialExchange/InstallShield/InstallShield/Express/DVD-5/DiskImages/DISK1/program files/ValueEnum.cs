using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public abstract class ValueEnum<TValue> 
        : IComparable<ValueEnum<TValue>>
        where TValue : IComparable
    {
        public TValue Value { get; protected set; }

        public ValueEnum(TValue value)
        {
            Value = value;
        }

        public int CompareTo(ValueEnum<TValue> other)
        {
            return 
                (other != null) ? this.Value.CompareTo(other.Value) : 1;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
