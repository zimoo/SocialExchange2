using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public abstract class Task
    {
        protected List<Round> _rounds = null;
        public virtual IList<Round> Rounds { get { return _rounds.AsReadOnly(); } }

        public int CurrentRoundIndex { get; protected set; }

        public Task(List<Round> rounds)
        {
            _rounds = rounds;
            CurrentRoundIndex = 0;
        }
    }
}
