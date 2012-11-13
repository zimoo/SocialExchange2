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

        public LikabilityRatingTask LikabilityRatingTask { get; protected set; }

        public int TrustExchangePointsMultiplier = 2;
        public int TrustExchangeStartingPoints = 24;
        public int RoundCountPerTask = 24;

        public LogicEngine()
        {
            InitializePersonasAndLikabilityRatingTask();
            InitializeTrustExchangeTask();
        }

        private void InitializePersonasAndLikabilityRatingTask()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Personas =
                assembly
                .GetManifestResourceNames()
                .Where(s => !s.Contains("questionHead"))
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

            LikabilityRatingTask = new LikabilityRatingTask(Personas.Shuffle());
        }

        private void InitializeTrustExchangeTask()
        {
            InitializeTask<TrustExchangeTask, TrustExchangeRound>
            (
                ref _TrustExchangeTask,

                (rounds) => new TrustExchangeTask(rounds, TrustExchangeStartingPoints),

                GetPersonas<Round>(PersonaClassifications.Unused),

                (persona) =>
                        new TrustExchangeRound
                        (
                            persona,
                            //(points) => points * TrustExchangePointsMultiplier + 1,
                            (points) => points * TrustExchangePointsMultiplier,
                            GetNextTrustExchangePersonaClassification
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
            Random r = new Random();

            List<Persona> allMatchingPersonas =
                Personas
                .OrderBy(t => r.Next())
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


        public PersonaClassification GetNextTrustExchangePersonaClassification()
        {
            List<PersonaClassification> classifications = Personas.Select<Persona, PersonaClassification>(p => p.Classification).ToList();

            int cooperatorCount = classifications.Where(c => c.Value == PersonaClassifications.Cooperator.Value).Count();
            int defectorCount = classifications.Where(c => c.Value == PersonaClassifications.Defector.Value).Count();
            int indeterminateRemainingCount = RoundCountPerTask - cooperatorCount - defectorCount;
            
            Func <PersonaClassification> getRandomPersonaClassification =             
                () =>
                {
                    PersonaClassification[] options =
                        new PersonaClassification[] { PersonaClassifications.Cooperator, PersonaClassifications.Defector };

                    return
                        options[new Random().Next(0, options.Count())];
                };

            if (indeterminateRemainingCount >= RoundCountPerTask / 2)
            {
                return getRandomPersonaClassification();
            }
            else
            {
                if (cooperatorCount > defectorCount)
                {
                    if(indeterminateRemainingCount >= (cooperatorCount - defectorCount))
                    {
                        return PersonaClassifications.Defector;
                    }
                    else
                    {
                        return PersonaClassifications.Cooperator;
                    }
                }
                else
                {
                    return PersonaClassifications.Cooperator;
                }
            }
        }

        public void SaveResults(string appendText)
        {
            LogicEngineExtensions.SaveText
            (
                string.Join
                (
                    Environment.NewLine,
                    new object[] 
                    {
                        RoundExtensions.GetCommaDelimitedColumnNames(),
                        string.Join(Environment.NewLine, TrustExchangeTask.Rounds.Select<TrustExchangeRound, object>(r => r.ToString()).ToArray()),
                        string.Join(Environment.NewLine, ImplicitRecognitionTask.Rounds.Select<RecognitionRound, object>(r => r.ToString()).ToArray()),
                        string.Join(Environment.NewLine, ExplicitRecognitionTask.Rounds.Select<RecognitionRound, object>(r => r.ToString()).ToArray()),
                        string.Join(Environment.NewLine, LikabilityRatingTask.Rounds.Select<LikabilityRatingTask.LikabilityRatingRound, object>(r => r.ToString()).ToArray()),
                        "TOTAL POINTS EARNED: " + TrustExchangeTask.PlayerScore.ToString(), 
                        appendText
                    }
                )
            );
        }
    }
    
    public static class QuestionHeadBitmapMetaContainers
    {
        public class BitmapMetaContainer
        {
            public Bitmap Bitmap { get; protected set; }
            public string FileName { get; protected set; }

            public BitmapMetaContainer(Bitmap bitmap, string fileName)
            {
                Bitmap = bitmap;
                FileName = fileName;
            }
        }

        public static BitmapMetaContainer Black { get; private set; }
        public static BitmapMetaContainer Blue { get; private set; }
        public static BitmapMetaContainer Red { get; private set; }
        public static BitmapMetaContainer Green { get; private set; }

        static QuestionHeadBitmapMetaContainers()
        {
            string[] strings =
                Assembly
                .GetExecutingAssembly()
                .GetManifestResourceNames();

            List<BitmapMetaContainer> bitmapMetaContainers =
                Assembly
                .GetExecutingAssembly()
                .GetManifestResourceNames()
                .Where(s => s.Contains("questionHead"))
                .ToList()
                .Select
                (
                    s =>
                        new BitmapMetaContainer
                        (
                            new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(s)),
                            s
                        )
                )
                .Cast<BitmapMetaContainer>()
                .ToList();

            Black = bitmapMetaContainers.Where(bmc => bmc.FileName.Contains("BLACK")).FirstOrDefault();
            Blue = bitmapMetaContainers.Where(bmc => bmc.FileName.Contains("BLUE")).FirstOrDefault();
            Red = bitmapMetaContainers.Where(bmc => bmc.FileName.Contains("RED")).FirstOrDefault();
            Green = bitmapMetaContainers.Where(bmc => bmc.FileName.Contains("GREEN")).FirstOrDefault();
        }
    }

    public static class LogicEngineExtensions
    {
        public static void SaveText(string text, string fileFullPath = null)
        {
            DateTime now = DateTime.Now;
            fileFullPath =
                fileFullPath ??
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                        "TrustGame_" +
                        now.Year.ToString("D4") +
                        now.Month.ToString("D2") +
                        now.Day.ToString("D2") + "~" +
                        now.ToString("HH") +
                        now.ToString("mm") +
                        now.Second.ToString("D2") +
                        ".txt"
                );

            FileInfo fileInfo = new FileInfo(fileFullPath);
            if (fileInfo.Exists)
            {
                throw new Exception(string.Format("File with path {0} already exists. Please choose another filename or path.", fileFullPath));
            }
            else
            {
                StreamWriter writer = null;
                try
                {
                    writer = fileInfo.CreateText();
                    writer.Write(text);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                        writer = null;
                    }
                }

            }
        }

        public static string ToChronologicallySortableString(this DateTime dateTime)
        {
            return
                dateTime.Year.ToString("D4") +
                dateTime.Month.ToString("D2") +
                dateTime.Day.ToString("D2") + "~" +
                dateTime.ToString("HH") +
                dateTime.ToString("mm") +
                dateTime.Second.ToString("D2");
        }

        public static List<T> Shuffle<T>(this List<T> list, int iterations = 3)
        {
            List<T> shuffledList = new List<T>(list);

            Enumerable.Range(0, iterations)
                .ToList()
                .ForEach
                (
                    x =>
                    {
                        Random r = new Random();
                        int n = 0;
                        while (n < shuffledList.Count)
                        {
                            int i = r.Next(0, n + 1);
                            T value = shuffledList[i];
                            shuffledList[i] = shuffledList[n];
                            shuffledList[n] = value;
                            n++;
                        }
                    }
                );

            return shuffledList;
        }
    }
}
