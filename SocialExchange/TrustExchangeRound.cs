using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange.Tasks
{
    public partial class TrustExchangeTask
    {
        public class Round : Rounds.Round
        {
            public TrustExchange TrustExchange { get; protected set; }

            public Round(Persona persona, Func<PersonaClassification> personaResponseLogic)
                : base(persona)
            {
                TrustExchange = new TrustExchange(personaResponseLogic);                
            }

            public void PlayerGivesPointToPersona()
            {
                TrustExchange.PlayerGivesPointToPersona();
            }

            public void PlayerSkipsPersona()
            {
                TrustExchange.PlayerSkipsPersona();
            }
        }
    }
}
