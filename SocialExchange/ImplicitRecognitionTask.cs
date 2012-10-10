using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange.Tasks
{
    public partial class ImplicitRecognitionTask
    {
        public class ImplicitRecognitionRound : Rounds.Round
        {
            public ImplicitRecognitionActivity ImplicitRecognitionActivity { get; protected set; }

            public ImplicitRecognitionRound(Persona persona, Func<PersonaClassification> personaResponseLogic)
                : base(persona)
            {
                //TrustExchange = new TrustExchange(personaResponseLogic);
            }

            public void ProcessPlayerInput()
            {
            }
        }

        protected List<ImplicitRecognitionTask.ImplicitRecognitionRound> _rounds = null;
        public IList<ImplicitRecognitionRound> Rounds { get { return _rounds.AsReadOnly(); } }

        public DateTime BeginTimestamp { get; protected set; }
        public DateTime EndTimestamp { get; protected set; }

        public int CurrentRoundIndex { get; protected set; }
        public ImplicitRecognitionRound CurrentRound
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

        public ImplicitRecognitionTask(List<ImplicitRecognitionRound> rounds)
        {
            _rounds = rounds;
            CurrentRoundIndex = 0;
        }

        public ImplicitRecognitionRound Advance()
        {
            CurrentRoundIndex =
                (CurrentRoundIndex >= 0) && (CurrentRoundIndex + 1 < Rounds.Count) ?
                CurrentRoundIndex + 1 :
                CurrentRoundIndex;

            return CurrentRound;
        }

        //public PersonaClassification PlayerSubmits(bool givesPoint)
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
