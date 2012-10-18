using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public abstract class Round
    {
        public Persona Persona { get; protected set; }

        public PlayerInputClassification PlayerInputClassification { get; protected set; }

        public DateTime BeginTimestamp { get; protected set; }
        public DateTime EndTimestamp { get; protected set; }

        public Round(Persona persona)
        {
            Persona = persona;
        }
    }

    public static class RoundExtensions
    {
        public static int GetCount<T>(this List<T> rounds, PersonaClassification personaClassification)
            where T : Round
        {
            return rounds.Where(r => r.Persona.Classification == personaClassification).Count();
        }
    }
}
