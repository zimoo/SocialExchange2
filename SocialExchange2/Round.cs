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

        public DateTime BeginTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }

        public Round(Persona persona)
        {
            Persona = persona;
        }
        
        public override string ToString()
        {
            return
                String.Join( ", ",
                    "{0}",
                    PlayerInputClassification.Value,
                    Persona.FileName,
                    Persona.Classification.Value,
                    BeginTimestamp.ToLongDateString(),
                    EndTimestamp.ToLongDateString(),
                    "{1}",
                    "{2}"
                );
        }
    }

    public static class RoundExtensions
    {
        public static int GetCount<T>(this List<T> rounds, PersonaClassification personaClassification)
            where T : Round
        {
            return rounds.Where(r => r.Persona.Classification == personaClassification).Count();
        }

        public static string GetCommaDelimitedColumnNames()
        {
            return 
                String.Join(", ",
                    "Type",
                    "PlayerInputClassification.Value",
                    "Persona.FileName",
                    "Persona.Classification.Value",
                    "BeginTimestamp",
                    "EndTimestamp",
                    "RawPlayerPointsIn",
                    "MultipliedPersonaPointsOut"
                );
        }
    }
}
