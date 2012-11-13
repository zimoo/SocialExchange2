using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class TrustExchangeTask
    {
        protected List<TrustExchangeRound> _rounds = null;
        public List<TrustExchangeRound> Rounds { get { return _rounds.AsReadOnly().ToList(); } }

        public DateTime BeginTimestamp { get; protected set; }
        public DateTime EndTimestamp { get; protected set; }

        public int CurrentRoundIndex { get; protected set; }
        public TrustExchangeRound CurrentRound
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

        public int StartingPoints { get; protected set; }
        public int PlayerScore 
        {
            get
            {
                int sumRawPlayerPointsIn = Rounds.Sum((round) => round.RawPlayerPointsIn);
                int sumMultipliedPersonaPointsOut = Rounds.Sum((round) => round.MultipliedPersonaPointsOut);;

                return
                    StartingPoints
                    - sumRawPlayerPointsIn
                    + sumMultipliedPersonaPointsOut;
            }
        }

        public TrustExchangeTask(List<TrustExchangeRound> rounds, int startingPoints)
        {
            _rounds = rounds;
            StartingPoints = startingPoints;

            CurrentRoundIndex = 0;
        }

        public void ProcessPlayerInput(int points)
        {
            CurrentRound.ProcessPlayerInput(points);

            EndTimestamp =
                CurrentRoundIndex == Rounds.Count - 1 ?
                DateTime.Now :
                default(DateTime);
        }


        public Round AdvanceToNextRound()
        {
            if (CurrentRoundIndex < Rounds.Count - 1)
            {
                CurrentRoundIndex =
                    (CurrentRoundIndex >= 0) && (CurrentRoundIndex + 1 < Rounds.Count) ?
                    CurrentRoundIndex + 1 :
                    CurrentRoundIndex;
            }

            return Rounds[CurrentRoundIndex];
        }
    }
}
