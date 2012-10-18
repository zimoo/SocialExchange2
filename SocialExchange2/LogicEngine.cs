using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SocialExchange2
{
    public class LogicEngine
    {
        public List<Persona> Personas { get; protected set; }

        protected TrustExchangeTask _TrustExchangeTask = null;
        public TrustExchangeTask TrustExchangeTask
        {
            get
            {
                return _TrustExchangeTask;
            }
            protected set
            {
                _TrustExchangeTask = value;
            }
        }

        protected RecognitionTask _ImplicitRecognitionTask = null;
        public RecognitionTask ImplicitRecognitionTask
        {
            get
            {
                return _ImplicitRecognitionTask;
            }
            protected set
            {
                _ImplicitRecognitionTask = value;
            }
        }

        protected RecognitionTask _ExplicitRecognitionTask = null;
        public RecognitionTask ExplicitRecognitionTask
        {
            get
            {
                return _ExplicitRecognitionTask;
            }
            protected set
            {
                _ExplicitRecognitionTask = value;
            }
        }

        public int TrustExchangePointsMultiplier = 2;
        public int RoundCountPerTask = 24;

        public LogicEngine()
        {
            InitializePersonas();
            InitializeTrustExchangeTask();
        }

        private void InitializePersonas()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Personas =
                assembly
                .GetManifestResourceNames()
                .ToList()
                .Select(
                    s =>
                        new Persona(
                            new Bitmap(assembly.GetManifestResourceStream(s)),
                            s
                        )
                    )
                .Cast<Persona>()
                .ToList();
        }

        private void InitializeTrustExchangeTask()
        {
            InitializeTask<TrustExchangeTask, TrustExchangeRound>
            (
                ref _TrustExchangeTask,

                (rounds) => new TrustExchangeTask(rounds),

                GetPersonas<Round>(PersonaClassifications.Unused),

                (persona) =>
                        new TrustExchangeRound
                        (
                            persona,
                            (points) => points * TrustExchangePointsMultiplier,
                            () =>
                            {
                                PersonaClassification[] options =
                                    new PersonaClassification[] { PersonaClassifications.Cooperator, PersonaClassifications.Defector };

                                return
                                    options[new Random().Next(0, options.Count())];
                            }
                        )
            );
        }

        public void InitializeImplicitRecognitionTask()
        {
            InitializeRecognitionTask(ref _ImplicitRecognitionTask, (List<Round>) null);
        }

        public void InitializeExplicitRecognitionTask()
        {
            InitializeRecognitionTask(ref _ExplicitRecognitionTask, ImplicitRecognitionTask.Rounds.ToList<Round>());
        }

        private void InitializeRecognitionTask<T>(ref RecognitionTask task, List<T> notInRounds = null)
            where T : Round
        {
            InitializeTask<RecognitionTask, RecognitionRound>
            (
                ref task,

                (rounds) => new RecognitionTask(rounds),

                GetPersonas<T>(PersonaClassifications.Cooperator, RoundCountPerTask / 4, notInRounds)
                    .Concat(GetPersonas<T>(PersonaClassifications.Defector, RoundCountPerTask / 4, notInRounds))
                    .Concat(GetPersonas<Round>(PersonaClassifications.Unused, RoundCountPerTask / 2)).ToList(),

                (persona) => new RecognitionRound(persona)
            );
        }

        private void InitializeTask<TTask, TRound>
        (
            ref TTask task, 
            Func<List<TRound>, TTask> taskInitializer, 
            List<Persona> availablePersonas, 
            Func<Persona, TRound> roundInitializer
        )
            where TRound : Round
        {
            List<TRound> rounds = new List<TRound>(RoundCountPerTask);
            task = taskInitializer(rounds);

            while (rounds.Count < RoundCountPerTask)
            {
                Persona personaCandidate = availablePersonas[new Random().Next(0, availablePersonas.Count)];
                if (!rounds.Select(r => r.Persona).Cast<Persona>().ToList().Contains(personaCandidate))
                {
                    rounds.Add(roundInitializer(personaCandidate));
                }
            }
        }

        public List<Persona> GetPersonas<T>(PersonaClassification personaClassification, int take = -1, List<T> notInRounds = null)
            where T : Round
        {
            List<Persona> allMatchingPersonas =
                Personas
                .Where
                (
                    persona => 
                        persona.Classification == personaClassification && 
                        (
                            ReferenceEquals(notInRounds, null) || 
                            (!ReferenceEquals(notInRounds, null) && !notInRounds.Select<T,Persona>(round => round.Persona).Contains(persona))
                        )
                )
                .ToList();

            return
                allMatchingPersonas
                .Take(take == -1 ? allMatchingPersonas.Count : take)
                .ToList();
        }

        public static PersonaClassification GetNextTrustExchangePersonaClassification(List<PersonaClassification> currentClassifications)
        {
            int totalCount = currentClassifications.Count;
            int indeterminateCount = currentClassifications.Where(c => c.Value == PersonaClassifications.Indeterminate.Value).Count();
            int absDiffTotalIndeterminate = (int) Math.Abs(totalCount - indeterminateCount);

            int cooperatorCount = currentClassifications.Where(c => c.Value == PersonaClassifications.Cooperator.Value).Count();
            int defectorCount = currentClassifications.Where(c => c.Value == PersonaClassifications.Defector.Value).Count();
            int absDiffDefectorCooperator = (int)Math.Abs(cooperatorCount - defectorCount);

            if(absDiffDefectorCooperator >= absDiffTotalIndeterminate)
            {

            }
        }
    }
}
