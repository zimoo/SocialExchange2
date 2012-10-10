using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    public class SetOnceBackingField<TValue>
        where TValue : IComparable<TValue>
    {
        public readonly TValue DefaultValue;

        protected TValue _Value;

        public TValue Value 
        { 
            get
            {
                return _Value;
            }
            set 
            {
                _Value = (_Value.CompareTo(DefaultValue) == 0 ? value : _Value);
            } 
        }

        public SetOnceBackingField(TValue value = default(TValue), bool overrideDefaultValue = true)
        {
            DefaultValue = overrideDefaultValue ? value : default(TValue);
            _Value = value;
        }
    }

    public class Test
    {
        public SetOnceBackingField<DateTime> _Begin = new SetOnceBackingField<DateTime>();
        public DateTime Begin
        {
            get
            {
                return _Begin.Value;
            }
            set
            {
                _Begin.Value = value;
            }
        }
    }
}
