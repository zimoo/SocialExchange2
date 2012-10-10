using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class TrustExchangeInteraction : PlayerInteraction
    {
        public int PlayerToPersonaRawPoints { get; protected set; }
        public int PersonaToPlayerRawPoints { get; protected set; }

        public TrustExchangeInteraction(Func<InteractionOutcome> interactionOutcomeLogic)
            : base(interactionOutcomeLogic)
        {
            PlayerToPersonaRawPoints = 0;
            PersonaToPlayerRawPoints = 0;
            InteractionOutcome = InteractionOutcomes.INDETERMINATE;
            InteractionOutcomeLogic = interactionOutcomeLogic;
        }

        public void PlayerGivesPointsToPersona(int rawPoints)
        {
            PlayerToPersonaRawPoints += rawPoints;
            //InteractionOutcome = InteractionOutcomeLogic();
            PersonaToPlayerRawPoints +=
                (InteractionOutcome == InteractionOutcomes.COOPERATOR ? rawPoints : 0);
        }
    }
}
