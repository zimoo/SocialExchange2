using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public abstract class Outcome : ValueEnum<string>
    {
        public Outcome(string value) : base(value) { }
    }
}
