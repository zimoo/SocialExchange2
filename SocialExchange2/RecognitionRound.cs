using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class RecognitionRound : Round
    {
        public Func<PlayerInputClassification, PersonaClassification> GetPersonaClassification { get; protected set; }

        public RecognitionRound
        (
            Persona persona
        )
            : base(persona)
        {
            Persona.Classification =
                Persona.Classification.Value == PersonaClassifications.Unused.Value ? 
                Persona.Classification = PersonaClassifications.Novel :
                Persona.Classification;
        }

        public void ProcessPlayerInput(PlayerInputClassification playerInputClassification)
        {
            PlayerInputClassification = playerInputClassification;

            EndTimestamp = DateTime.Now;
        }
    }
}
