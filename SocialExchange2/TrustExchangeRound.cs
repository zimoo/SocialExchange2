using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class TrustExchangeRound : Round
    {
        public TrustExchangeInteraction TrustExchangeInteraction { get; protected set; }

        public TrustExchangeRound(Persona persona, Func<InteractionOutcome> interactionOutcomeLogic)
            : base(persona)
        {
            TrustExchangeInteraction = new TrustExchangeInteraction(interactionOutcomeLogic);                
        }

        //public void PlayerGivesPointToPersona()
        //{
        //    TrustExchangeInteraction.PlayerGivesPointToPersona();
        //}

        //public void PlayerSkipsPersona()
        //{
        //    TrustExchangeInteraction.PlayerSkipsPersona();
        //}
    }
}
