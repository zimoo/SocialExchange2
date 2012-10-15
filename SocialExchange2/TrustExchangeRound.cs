using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class TrustExchangeRound : Round
    {
        public int PlayerToPersonaRawPoints { get; protected set; }
        public int PersonaToPlayerRawPoints { get; protected set; }
        public Func<int, int> GetRawPersonaReturnPoints { get; protected set; }
        public Func<PlayerInputClassification, PersonaClassification> GetPersonaClassification { get; protected set; }

        public TrustExchangeRound
        (
            Persona persona, 
            Func<int, int> getPersonaReturnPoints, 
            Func<PlayerInputClassification, PersonaClassification> getPersonaClassification
        )
            : base(persona)
        {
            PlayerToPersonaRawPoints = 0;
            PersonaToPlayerRawPoints = 0;
            PersonaClassification = PersonaClassifications.Indeterminate;
            GetRawPersonaReturnPoints = getPersonaReturnPoints;
            GetPersonaClassification = getPersonaClassification;
        }

        internal void ProcessPlayerInput(int points)
        {
            PlayerToPersonaRawPoints += points;
            PlayerInputClassification = PlayerInputClassifications.GavePoints;

            PersonaClassification = GetPersonaClassification(PlayerInputClassification);
            if(PersonaClassification.Value == PersonaClassifications.Cooperator.Value)
            {
                PersonaToPlayerRawPoints += GetRawPersonaReturnPoints(points);
            }
        }
    }
}
