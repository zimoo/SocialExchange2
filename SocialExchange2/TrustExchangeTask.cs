﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public partial class TrustExchangeTask
    {
        protected List<TrustExchangeRound> _rounds = null;
        public IList<TrustExchangeRound> Rounds { get { return _rounds.AsReadOnly(); } }

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

        public TrustExchangeTask(List<TrustExchangeRound> rounds)
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

        //public InteractionOutcome PlayerSubmits(bool givesPoint)
        //{
        //    if (givesPoint)
        //    {
        //        CurrentRound.PlayerGivesPointToPersona();
        //    }
        //    else
        //    {
        //        CurrentRound.PlayerSkipsPersona();
        //    }

        //    EndTimestamp =
        //        CurrentRoundIndex == Rounds.Count - 1 ?
        //        DateTime.Now :
        //        default(DateTime);

        //    return CurrentRound.TrustExchange.PersonaClassification;
        //}

        //public int GetCount(PersonaClassification classification)
        //{
        //    return Rounds.Where(r => r.TrustExchange.PersonaClassification == classification).Count();
        //}
    }
}
