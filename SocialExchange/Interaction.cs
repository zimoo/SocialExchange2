using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    public abstract class Interaction<TPlayerInput, TOutcome>
        where TPlayerInput : PlayerInput
        where TOutcome : Outcome
    {
        public Func<TPlayerInput, TOutcome> InputFunc { get; set; }
        public Func<TOutcome> OutputFunc { get; set; }

        public TOutcome Outcome { get; protected set; }
    }
}
