using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public abstract class Round
    {
        public Persona Persona { get; protected set; }

        public DateTime BeginTimestamp { get; protected set; }
        public DateTime EndTimestamp { get; protected set; }

        public Round(Persona persona)
        {
            Persona = persona;
        }
    }
}
