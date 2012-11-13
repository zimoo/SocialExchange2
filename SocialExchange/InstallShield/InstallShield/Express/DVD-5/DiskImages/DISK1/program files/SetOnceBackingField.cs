using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class SetOnceBackingField<TValue>
    {
        protected bool hasBeenSet = false;

        protected TValue _Value;

        public TValue Value 
        { 
            get
            {
                return _Value;
            }
            set 
            {
                _Value =
                    !hasBeenSet ?
                    value :
                    _Value;

                hasBeenSet = true;
            } 
        }

        public SetOnceBackingField() { }

        public SetOnceBackingField(TValue value)
        {
            _Value = value;

            hasBeenSet = true;
        }
    }

    public class Test
    {
        //public SetOnceBackingField<DateTime> _Begin = new SetOnceBackingField<DateTime>();
        //public DateTime Begin
        //{
        //    get
        //    {
        //        return _Begin.Value;
        //    }
        //    set
        //    {
        //        _Begin.Value = value;
        //    }
        //}
    }
}
