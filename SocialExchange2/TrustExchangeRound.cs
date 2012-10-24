using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class TrustExchangeRound : Round
    {
        public int RawPlayerPointsIn { get; protected set; }
        public int MultipliedPersonaPointsOut { get; protected set; }
        public Func<int, int> CalculateMultipliedPersonaPointsOut { get; protected set; }
        public Func<PersonaClassification> GetPersonaClassification { get; protected set; }

        public TrustExchangeRound
        (
            Persona persona, 
            Func<int, int> calculateMultipliedPersonaPointsOut, 
            Func<PersonaClassification> getPersonaClassification
        )
            : base(persona)
        {
            RawPlayerPointsIn = 0;
            MultipliedPersonaPointsOut = 0;

            Persona.Classification =
                Persona.Classification.Value == PersonaClassifications.Unused.Value ?
                Persona.Classification = PersonaClassifications.Indeterminate :
                Persona.Classification;

            CalculateMultipliedPersonaPointsOut = calculateMultipliedPersonaPointsOut;
            GetPersonaClassification = getPersonaClassification;
        }

        internal void ProcessPlayerInput(int points)
        {
            RawPlayerPointsIn += points;
            PlayerInputClassification = PlayerInputClassifications.GavePoints;

            Persona.Classification = GetPersonaClassification();
            if(Persona.Classification.Value == PersonaClassifications.Cooperator.Value)
            {
                MultipliedPersonaPointsOut += CalculateMultipliedPersonaPointsOut(points);
            }
        }

        public override string ToString()
        {
            return string.Format(base.ToString(), "TrustExchangeRound", RawPlayerPointsIn, MultipliedPersonaPointsOut);
        }
    }
}
