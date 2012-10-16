using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class RecognitionTask
    {
        protected List<RecognitionRound> _rounds = null;
        public IList<RecognitionRound> Rounds { get { return _rounds.AsReadOnly(); } }

        public DateTime BeginTimestamp { get; protected set; }
        public DateTime EndTimestamp { get; protected set; }
        public RecognitionTask(List<RecognitionRound> rounds)
        {
            _rounds = rounds;
        }

        public void ProcessPlayerInput(RecognitionRound round, PlayerInputClassification playerInputClassification)
        {
            round.ProcessPlayerInput(playerInputClassification);

            EndTimestamp = DateTime.Now;
        }

        public int GetCount(PersonaClassification personaClassification)
        {
            return Rounds.Where(r => r.PersonaClassification == personaClassification).Count();
        }
    }
}
