using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public abstract class PlayerInteraction
    {
        public InteractionOutcome InteractionOutcome { get; protected set; }
        public Func<InteractionOutcome> InteractionOutcomeLogic { get; protected set; }

        public PlayerInteraction(Func<InteractionOutcome> interactionOutcomeLogic)
        {
            InteractionOutcome = InteractionOutcomes.INDETERMINATE;
            InteractionOutcomeLogic = interactionOutcomeLogic;
        }
    }
}
