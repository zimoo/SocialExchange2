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
        public int TrustExchangePointsMultiplier { get; protected set; }

        public int TrustExchangeTaskRoundcount = 5;

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
            TrustExchangePointsMultiplier = 2;

            List<TrustExchangeRound> rounds = new List<TrustExchangeRound>(TrustExchangeTaskRoundcount);
            TrustExchangeTask = new TrustExchangeTask(rounds);

            while (rounds.Count < TrustExchangeTaskRoundcount)
            {
                Persona roundPersonaCandidate = Personas[new Random().Next(0, Personas.Count - 1)];
                if (!rounds.Select(r => r.Persona).Cast<Persona>().ToList().Contains(roundPersonaCandidate))
                {
                    rounds.Add(
                        new TrustExchangeRound
                        (
                            roundPersonaCandidate,
                            (points) =>
                                {
                                    return points * TrustExchangePointsMultiplier;
                                },
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
            }
        }
    }
}
