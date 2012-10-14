using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class PlayerInputClassification : ValueEnum<string>
    {
        public PlayerInputClassification(string value) : base(value) { }
    }
    public class PlayerInputClassifications
    {
        public static readonly PlayerInputClassification GaveNoPoints = new PlayerInputClassification("GaveNoPoints");
        public static readonly PlayerInputClassification GavePoints = new PlayerInputClassification("GavePoints");
        public static readonly PlayerInputClassification ImplicitlyChosePersona = new PlayerInputClassification("ImplicitlyChosePersona");
        public static readonly PlayerInputClassification ImplicitlyDiscardedPersona = new PlayerInputClassification("ImplicitlyDiscardedPersona");
        public static readonly PlayerInputClassification ExplicitlyChoseCooperatorPersona = new PlayerInputClassification("ExplicitlyChoseCooperatorPersona");
        public static readonly PlayerInputClassification ExplicitlyChoseDefectorPersona = new PlayerInputClassification("ExplicitlyChoseDefectorPersona");
        public static readonly PlayerInputClassification ExplicitlyDiscardedPersona = new PlayerInputClassification("ExplicitlyDiscardedPersona");
    }
}
