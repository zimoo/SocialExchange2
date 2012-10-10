using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    public class TrustExchange
    {
        public int PlayerToPersonaRawPoints { get; protected set; }
        public int PersonaToPlayerRawPoints { get; protected set; }
        public PersonaClassification PersonaClassification { get; protected set; }
        public Func<PersonaClassification> PersonaResponseLogic { get; protected set; }

        public TrustExchange(Func<PersonaClassification> personaResponseLogic)
        {
            PlayerToPersonaRawPoints = 0;
            PersonaToPlayerRawPoints = 0;
            PersonaClassification = PersonaClassifications.INDETERMINATE;
            PersonaResponseLogic = personaResponseLogic;
        }

        public void PlayerGivesPointToPersona()
        {
            PlayerToPersonaRawPoints++;
            PersonaClassification = PersonaResponseLogic();
            PersonaToPlayerRawPoints +=
                (PersonaClassification == PersonaClassifications.COOPERATOR ? 1 : 0);
        }

        public void PlayerSkipsPersona()
        {
            PersonaClassification = PersonaClassifications.SKIPPED;
        }

        public int PersonaGivesPoint()
        {
            return (PersonaToPlayerRawPoints++);
        }
    }
}
