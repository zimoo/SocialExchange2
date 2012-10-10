using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange.Tasks
{
    public partial class TrustExchangeTask
    {
        protected List<Round> _rounds = null;
        public IList<Round> Rounds { get { return _rounds.AsReadOnly(); } }

        public DateTime BeginTimestamp { get; protected set; }
        public DateTime EndTimestamp { get; protected set; }

        public int CurrentRoundIndex { get; protected set; }
        public Round CurrentRound 
        { 
            get 
            { 
                BeginTimestamp = 
                    BeginTimestamp == default(DateTime) ? 
                    DateTime.Now : 
                    BeginTimestamp; 

                return 
                    Rounds[CurrentRoundIndex];
            } 
        }

        public TrustExchangeTask(List<Round> rounds)
        {
            _rounds = rounds;
            CurrentRoundIndex = 0;
        }

        public Round Advance()
        {
            CurrentRoundIndex = 
                (CurrentRoundIndex >= 0) && (CurrentRoundIndex + 1 < Rounds.Count) ?
                CurrentRoundIndex + 1 :
                CurrentRoundIndex;

            return CurrentRound;
        }

        public PersonaClassification PlayerSubmits(bool givesPoint)
        {
            if(givesPoint)
            {
                CurrentRound.PlayerGivesPointToPersona();
            }
            else
            {
                CurrentRound.PlayerSkipsPersona();
            }

            EndTimestamp =
                CurrentRoundIndex == Rounds.Count - 1 ?
                DateTime.Now :
                default(DateTime);

            return CurrentRound.TrustExchange.PersonaClassification;
        }

        public int GetCount(PersonaClassification classification)
        {
            return Rounds.Where(r => r.TrustExchange.PersonaClassification == classification).Count();
        }
    }
}
