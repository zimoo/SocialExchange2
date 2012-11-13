using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class LikabilityRatingTask
    {
        public class LikabilityRatingRound
        {
            public Persona Persona { get; protected set; }
            public int LikabilityRatingIndex { get; set; }

            public LikabilityRatingRound(Persona persona)
            {
                Persona = persona;
            }

            public override string ToString()
            {
                return string.Format("{0}, LIKABILITY ==> {1}", Persona.ToString(), LikabilityRatingIndex.ToString());
            }
        }

        public List<LikabilityRatingRound> Rounds { get; protected set; }
        public int CurrentRoundIndex { get; protected set; }

        public LikabilityRatingTask(List<Persona> personas)
        {
            Rounds = new List<LikabilityRatingRound>();
            personas.ForEach(p => Rounds.Add(new LikabilityRatingRound(p)));

            CurrentRoundIndex = 0;
        }

        public LikabilityRatingRound GetCurrentRound()
        {
            return Rounds[CurrentRoundIndex];
        }

        public bool CanAdvanceToNextRound()
        {
            return CurrentRoundIndex < Rounds.Count - 1;
        }

        public void AdvanceToNextRound()
        {
            if(CanAdvanceToNextRound())
            {
                CurrentRoundIndex++;
            }
        }
    }
}
