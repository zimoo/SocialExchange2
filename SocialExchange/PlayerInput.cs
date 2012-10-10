using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    public static class PlayerInputs
    {
        public static readonly PlayerInput INDETERMINATE = new PlayerInput("PLAYERINPUT__INDETERMINATE");
        public static readonly PlayerInput TRUST_EXCHANGE_SKIP = new PlayerInput("PLAYERINPUT__TRUST_EXCHANGE_SKIP");
        public static readonly PlayerInput TRUST_EXCHANGE_GIVE_POINT = new PlayerInput("PLAYERINPUT__TRUST_EXCHANGE_GIVE_POINT");
        public static readonly PlayerInput IMPLICIT_WOULD_PLAY_PERSONA_AGAIN = new PlayerInput("PLAYERINPUT__IMPLICIT_WOULD_PLAY_PERSONA_AGAIN");
        public static readonly PlayerInput IMPLICIT_WOULD_NOT_PLAY_PERSONA_AGAIN = new PlayerInput("PLAYERINPUT__IMPLICIT_WOULD_NOT_PLAY_PERSONA_AGAIN");
        public static readonly PlayerInput EXPLICIT_DO_NOT_RECOGNIZE = new PlayerInput("PLAYERINPUT__EXPLICIT_DO_NOT_RECOGNIZE");
        public static readonly PlayerInput EXPLICIT_RECOGNIZE_PERSONA_AS_COOPERATOR = new PlayerInput("PLAYERINPUT__EXPLICIT_RECOGNIZE_PERSONA_AS_COOPERATOR");
        public static readonly PlayerInput EXPLICIT_RECOGNIZE_PERSONA_AS_DEFECTOR = new PlayerInput("PLAYERINPUT__EXPLICIT_RECOGNIZE_PERSONA_AS_DEFECTOR");
    }

    public class PlayerInput : ValueEnum<string>, IComparable<PlayerInput>
    {
        public PlayerInput(string value) : base(value) { }

        int IComparable<PlayerInput>.CompareTo(PlayerInput other)
        {
            return base.CompareTo(other);
        }
    }
}
