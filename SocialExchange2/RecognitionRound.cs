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
            Persona persona,
            Func<PlayerInputClassification, PersonaClassification> getPersonaClassification
        )
            : base(persona)
        {
            PersonaClassification = PersonaClassifications.Indeterminate;
            GetPersonaClassification = getPersonaClassification;
        }

        public void ProcessPlayerInput(PlayerInputClassification playerInputClassification)
        {
            PlayerInputClassification = playerInputClassification;

            PersonaClassification = GetPersonaClassification(PlayerInputClassification);

            EndTimestamp = DateTime.Now;
        }
    }
}
