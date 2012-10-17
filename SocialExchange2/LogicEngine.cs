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

        public TrustExchangeTask TrustExchangeTask { get; protected set; }
        public RecognitionTask ImplicitRecognitionTask { get; protected set; }
        public RecognitionTask ExplicitRecognitionTask { get; protected set; }

        public int TrustExchangePointsMultiplier = 2;
        public int RoundCountPerTask = 5;

        public LogicEngine()
        {
            InitializePersonas();
            InitializeTrustExchangeTask();
            //InitializeImplicitRecognitionTask();
            //InitializeExplicitRecognitionTask();
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

        private List<Persona> GetUnutilizedPersonas()
        {
            return Personas.Where(persona => persona.Utilized == false).ToList();
        }

        private List<Persona> GetNovelPersonas(PersonaClassification personaClassification, List<Round> notInRounds, int count)
        {
            return
                TrustExchangeTask.Rounds.Where
                (
                    round =>
                        round.PersonaClassification.Value == personaClassification.Value &&
                        !notInRounds.Contains(round)
                )
                .Select<Round, Persona>
                (
                    round =>
                        round.Persona
                )
                .Take(count)
                .ToList();
        }

        private void InitializeTrustExchangeTask()
        {
            InitializeTask<TrustExchangeTask, TrustExchangeRound>
            (
                TrustExchangeTask,
                (rounds) => new TrustExchangeTask(rounds),
                GetUnutilizedPersonas(),
                (persona) =>
                        new TrustExchangeRound
                        (
                            persona,
                            (points) => points * TrustExchangePointsMultiplier,
                            (points) =>
                            {
                                PersonaClassification[] options =
                                    new PersonaClassification[] { PersonaClassifications.Cooperator, PersonaClassifications.Defector };

                                return
                                    options[new Random().Next(0, options.Count())];
                            }
                        )
            );
        }

        private void InitializeImplicitRecognitionTask()
        {
            InitializeRecognitionTask(ImplicitRecognitionTask, new List<Round>());
        }

        private void InitializeExplicitRecognitionTask()
        {
            InitializeRecognitionTask(ExplicitRecognitionTask, ImplicitRecognitionTask.Rounds.ToList<Round>());
        }

        private void InitializeRecognitionTask(RecognitionTask task, List<Round> notInRounds)
        {
            InitializeTask<RecognitionTask, RecognitionRound>
            (
                task,
                (rounds) => new RecognitionTask(rounds),
                GetNovelPersonas(PersonaClassifications.Cooperator, notInRounds, 6)
                    .Concat(GetNovelPersonas(PersonaClassifications.Defector, notInRounds, 6))
                        .Concat(GetUnutilizedPersonas())
                            .ToList(),
                (persona) =>
                        new RecognitionRound
                        (
                            persona,
                            (playerInputClassification) =>
                            {
                                if(playerInputClassification.Value == PlayerInputClassifications.ImplicitlyChosePersona.Value)
                                {
                                    return PersonaClassifications.Cooperator;
                                }
                                else
                                    if (playerInputClassification.Value == PlayerInputClassifications.ImplicitlyDiscardedPersona.Value)
                                    {
                                        return PersonaClassifications.Defector;
                                    }
                                    else
                                        if (playerInputClassification.Value == PlayerInputClassifications.ExplicitlyChoseCooperatorPersona.Value)
                                        {
                                            return PersonaClassifications.Cooperator;
                                        }
                                        else
                                            if (playerInputClassification.Value == PlayerInputClassifications.ExplicitlyChoseDefectorPersona.Value)
                                            {
                                                return PersonaClassifications.Defector;
                                            }
                                            else
                                                if (playerInputClassification.Value == PlayerInputClassifications.ExplicitlyDiscardedPersona.Value)
                                                {
                                                    return PersonaClassifications.Indeterminate;
                                                }
                                                else
                                                {
                                                    throw new NotImplementedException();
                                                }

                            }
                        )
            );
        }

        private void InitializeTask<TTask, TRound>(TTask task, Func<List<TRound>, TTask> taskInitializer, List<Persona> availablePersonas, Func<Persona, TRound> roundInitializer)
            where TRound : Round
        {
            List<TRound> rounds = new List<TRound>(RoundCountPerTask);
            task = taskInitializer(rounds);

            while (rounds.Count < RoundCountPerTask)
            {
                Persona personaCandidate = availablePersonas[new Random().Next(0, availablePersonas.Count - 1)];
                if (!rounds.Select(r => r.Persona).Cast<Persona>().ToList().Contains(personaCandidate))
                {
                    rounds.Add(roundInitializer(personaCandidate));
                    personaCandidate.Utilized = true;
                }
            }
        }

        
        //private void InitializeTrustExchangeTask_OLD()
        //{
        //    List<TrustExchangeRound> rounds = new List<TrustExchangeRound>(RoundCountPerTask);
        //    TrustExchangeTask = new TrustExchangeTask(rounds);

        //    while (rounds.Count < RoundCountPerTask)
        //    {
        //        Persona roundPersonaCandidate = Personas[new Random().Next(0, Personas.Count - 1)];
        //        if (!rounds.Select(r => r.Persona).Cast<Persona>().ToList().Contains(roundPersonaCandidate))
        //        {
        //            rounds.Add(
        //                new TrustExchangeRound
        //                (
        //                    roundPersonaCandidate,
        //                    (points) =>
        //                    {
        //                        return points * TrustExchangePointsMultiplier;
        //                    },
        //                    (points) =>
        //                    {
        //                        PersonaClassification[] options =
        //                            new PersonaClassification[] { PersonaClassifications.Cooperator, PersonaClassifications.Defector };

        //                        return
        //                            options[new Random().Next(0, options.Count())];
        //                    }
        //                )
        //            );
        //        }
        //    }
        //}
    }
}
