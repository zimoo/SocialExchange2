using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange.Tasks
{
    //public class Task<TInteraction, TPlayerInput, TOutcome>
    //    where TInteraction : Interaction<TPlayerInput, TOutcome>, new()
    //    where TOutcome : Outcome
    //{
    //    public abstract class TaskRound<TInteraction, TOutcome> : Rounds.Round
    //        where TInteraction : Interaction<TPlayerInput, TOutcome>, new()
    //        where TOutcome : Outcome 
    //    {
    //        public TInteraction Interaction { get; protected set; }

    //        public TaskRound(Persona persona, Func<TOutcome> outputFunc)
    //            : base(persona) 
    //        {
    //            Interaction = new TInteraction();
    //            Interaction.OutputFunc = outputFunc;
    //        }
    //    }

    //    protected List<TaskRound<TInteraction, TOutcome>> _rounds = null;
    //    public IList<TaskRound<TInteraction, TOutcome>> Rounds { get { return _rounds.AsReadOnly(); } }

    //    public DateTime BeginTimestamp { get; protected set; }
    //    public DateTime EndTimestamp { get; protected set; }

    //    public int CurrentRoundIndex { get; protected set; }
    //    public TaskRound<TInteraction, TOutcome> CurrentRound
    //    {
    //        get
    //        {
    //            BeginTimestamp =
    //                BeginTimestamp == default(DateTime) ?
    //                DateTime.Now :
    //                BeginTimestamp;

    //            return
    //                Rounds[CurrentRoundIndex];
    //        }
    //    }

    //    public Task(List<TaskRound<TInteraction, TOutcome>> rounds)
    //    {
    //        _rounds = rounds;
    //        CurrentRoundIndex = 0;
    //    }

    //    public TaskRound<TInteraction, TOutcome> Advance()
    //    {
    //        CurrentRoundIndex =
    //            (CurrentRoundIndex >= 0) && (CurrentRoundIndex + 1 < Rounds.Count) ?
    //            CurrentRoundIndex + 1 :
    //            CurrentRoundIndex;

    //        return CurrentRound;
    //    }

    //    public TOutcome PlayerSubmits(bool givesPoint)
    //    {
    //        if (givesPoint)
    //        {
    //            CurrentRound.PlayerGivesPointToPersona();
    //        }
    //        else
    //        {
    //            CurrentRound.PlayerSkipsPersona();
    //        }

    //        EndTimestamp =
    //            CurrentRoundIndex == Rounds.Count - 1 ?
    //            DateTime.Now :
    //            default(DateTime);

    //        return CurrentRound.Interaction.PersonaClassification;
    //    }

    //    public int GetCount(TOutcome classification)
    //    {
    //        return Rounds.Where(r => r.Interaction.PersonaClassification == classification).Count();
    //    }
    //}
}
