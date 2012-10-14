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
        public Func<int, int> GetPersonaReturnPoints { get; protected set; }
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
            GetPersonaReturnPoints = getPersonaReturnPoints;
            GetPersonaClassification = getPersonaClassification;
        }

        internal void PlayerSubmits(bool givesPoint)
        {
            throw new NotImplementedException();
        }


        //public int PlayerGivesPointsToPersona(int rawPoints)
        //{
        //    PlayerToPersonaRawPoints = rawPoints;

        //    InteractionOutcome =
        //        GetInteractionOutcome
        //        (
        //            rawPoints > 0 ?
        //            Interaction.PlayerInput.GavePoints :
        //            Interaction.PlayerInput.GaveZeroPoints
        //        );

        //    return PersonaToPlayerRawPoints = GetPersonaReturnPoints(rawPoints);
        //}
    }
}
