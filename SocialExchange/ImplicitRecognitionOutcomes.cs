using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    public static class ImplicitRecognitionOutcomes
    {
        public static readonly ImplicitRecognitionOutcome INDETERMINATE = new ImplicitRecognitionOutcome("ImplicitRecognitionOutcomes__INDETERMINATE");
        public static readonly ImplicitRecognitionOutcome PLAYER_CHOSE_YES_TO_PLAY_PERSONA_AGAIN = new ImplicitRecognitionOutcome("ImplicitRecognitionOutcomes__PLAYER_CHOSE_YES_TO_PLAY_PERSONA_AGAIN");
        public static readonly ImplicitRecognitionOutcome PLAYER_CHOSE_NO_TO_PLAY_PERSONA_AGAIN = new ImplicitRecognitionOutcome("ImplicitRecognitionOutcomes__PLAYER_CHOSE_NO_TO_PLAY_PERSONA_AGAIN");
    }

    public class ImplicitRecognitionOutcome : Outcome
    {
        public ImplicitRecognitionOutcome(string value) : base(value) { }
    }
}
