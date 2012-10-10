using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public static class InteractionOutcomes
    {
        public static readonly InteractionOutcome INDETERMINATE = new InteractionOutcome("INDETERMINATE");
        public static readonly InteractionOutcome DEFECTOR = new InteractionOutcome("DEFECTOR");
        public static readonly InteractionOutcome COOPERATOR = new InteractionOutcome("COOPERATOR");
        public static readonly InteractionOutcome NOVEL = new InteractionOutcome("NOVEL");
        public static readonly InteractionOutcome IMPLICITLY_CHOSEN = new InteractionOutcome("IMPLICITLY_CHOSEN");
        public static readonly InteractionOutcome IMPLICITLY_DISCARDED = new InteractionOutcome("IMPLICITLY_DISCARDED");
        public static readonly InteractionOutcome EXPLICITLY_RECOGNIZED_AS_COOPERATOR = new InteractionOutcome("EXPLICITLY_RECOGNIZED_AS_COOPERATOR");
        public static readonly InteractionOutcome EXPLICITLY_RECOGNIZED_AS_DEFECTOR = new InteractionOutcome("EXPLICITLY_RECOGNIZED_AS_DEFECTOR");
        public static readonly InteractionOutcome EXPLICITLY_NOT_RECOGNIZED = new InteractionOutcome("EXPLICITLY_NOT_RECOGNIZED");
    }

    public class InteractionOutcome : ValueEnum<string>
    {
        public const string Prefix = "";

        public InteractionOutcome(string value) : base(Prefix + value) { }
    }
}
