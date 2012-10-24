using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using SocialExchange2;

namespace SocialExchangeConsole
{
    class Program
    {
        public static bool logicEngineIsRunning = true;
        public static LogicEngine LogicEngine = new LogicEngine();

        static void Main(string[] args)
        {
            RunTrustExchangeTask();
        }

        private static void RunTrustExchangeTask()
        {
            Console.WriteLine("TRUST EXCHANGE TASK:");
            Console.WriteLine();

            while (LogicEngine.TrustExchangeTask.CurrentRoundIndex < LogicEngine.TrustExchangeTask.Rounds.Count)
            {
                Console.WriteLine(string.Format("ROUND {0} / {1}:", LogicEngine.TrustExchangeTask.CurrentRoundIndex + 1, LogicEngine.TrustExchangeTask.Rounds.Count));
                Console.WriteLine(string.Format("PERSONA: {0}", LogicEngine.TrustExchangeTask.CurrentRound.Persona.FileName));

                Console.Write("Give how many points? (1/2) ");

                int points = -1;
                Int32.TryParse(Console.In.ReadLine(), out points);

                // TODO : wrap this in order to redirect invalid input
                LogicEngine.TrustExchangeTask.ProcessPlayerInput(points);

                Console.WriteLine(string.Format("PLAYER  GAVE: {0} POINTS.", LogicEngine.TrustExchangeTask.CurrentRound.RawPlayerPointsIn));
                Console.WriteLine(string.Format("PERSONA GAVE: {0} POINTS", LogicEngine.TrustExchangeTask.CurrentRound.MultipliedPersonaPointsOut));
                Console.WriteLine(string.Format("PERSONA CLASSIFICATION: {0}", LogicEngine.TrustExchangeTask.CurrentRound.Persona.Classification.Value));
                Console.WriteLine();

                if(LogicEngine.TrustExchangeTask.CurrentRoundIndex == LogicEngine.TrustExchangeTask.Rounds.Count - 1)
                {
                    break;
                }
                else
                {
                    LogicEngine.TrustExchangeTask.AdvanceToNextRound();
                }
            }

            Console.WriteLine("PERSONA SUMMARY:");
            LogicEngine.Personas.ForEach(persona => Console.WriteLine(persona.ToString()));
            Console.WriteLine();

            Console.WriteLine(string.Format("TOTAL COOPERATORS: {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Cooperator).Count));
            Console.WriteLine(string.Format("TOTAL DEFECTORS:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Defector).Count));
            Console.WriteLine(string.Format("TOTAL INDETERMINATE:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Indeterminate).Count));
            Console.WriteLine(string.Format("TOTAL NOVEL:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Novel).Count));
            Console.WriteLine(string.Format("TOTAL UNUSED:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Unused).Count));
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("==========================================================================================================================");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("IMPLICIT RECOGNITION TASK:");
            Console.WriteLine();

            LogicEngine.InitializeImplicitRecognitionTask();
            int fauxImplicitRoundIndex = 0;
            while (fauxImplicitRoundIndex < LogicEngine.ImplicitRecognitionTask.Rounds.Count)
            {
                RecognitionRound currentImplicitRound = LogicEngine.ImplicitRecognitionTask.Rounds[fauxImplicitRoundIndex];

                Console.WriteLine(string.Format("ROUND {0} / {1}:", fauxImplicitRoundIndex + 1, LogicEngine.ImplicitRecognitionTask.Rounds.Count));
                Console.WriteLine(string.Format("{0}", currentImplicitRound.Persona));

                Console.Write("How do you classify this persona? (1=ImplicitChoose, 2=ImplicitDiscard) ");

                int implicitInput = -1;
                Int32.TryParse(Console.In.ReadLine(), out implicitInput);

                // TODO : wrap this in order to redirect invalid input
                LogicEngine
                    .ImplicitRecognitionTask
                    .ProcessPlayerInput
                    (
                        currentImplicitRound,
                        implicitInput == 1 ? 
                        PlayerInputClassifications.ImplicitlyChosePersona : 
                        PlayerInputClassifications.ImplicitlyDiscardedPersona
                    );

                Console.WriteLine(string.Format("PLAYER  INPUT : {0}", currentImplicitRound.PlayerInputClassification));
                Console.WriteLine(string.Format("PERSONA ACTUAL: {0}", currentImplicitRound.Persona.Classification));
                Console.WriteLine();

                if (fauxImplicitRoundIndex == LogicEngine.ImplicitRecognitionTask.Rounds.Count - 1)
                {
                    break;
                }
                else
                {
                    fauxImplicitRoundIndex++;
                }
            }

            Console.WriteLine("PERSONA SUMMARY:");
            LogicEngine.Personas.ForEach(persona => Console.WriteLine(persona.ToString()));
            Console.WriteLine();

            Console.WriteLine(string.Format("TOTAL COOPERATORS: {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Cooperator).Count));
            Console.WriteLine(string.Format("TOTAL DEFECTORS:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Defector).Count));
            Console.WriteLine(string.Format("TOTAL INDETERMINATE:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Indeterminate).Count));
            Console.WriteLine(string.Format("TOTAL NOVEL:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Novel).Count));
            Console.WriteLine(string.Format("TOTAL UNUSED:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Unused).Count));
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("==========================================================================================================================");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("EXPLICIT RECOGNITION TASK:");
            Console.WriteLine();

            LogicEngine.InitializeExplicitRecognitionTask();
            int fauxExplicitRoundIndex = 0;
            while (fauxExplicitRoundIndex < LogicEngine.ExplicitRecognitionTask.Rounds.Count)
            {
                RecognitionRound currentExplicitRound = LogicEngine.ExplicitRecognitionTask.Rounds[fauxExplicitRoundIndex];

                Console.WriteLine(string.Format("ROUND {0} / {1}:", fauxExplicitRoundIndex + 1, LogicEngine.ExplicitRecognitionTask.Rounds.Count));
                Console.WriteLine(string.Format("{0}", currentExplicitRound.Persona));

                Console.Write("How do you classify this persona? (1=ExplicitChooseCooperator, 2=ExplicitChooseDefector, 3=ExplicitDiscard) ");

                int explicitInput = -1;
                Int32.TryParse(Console.In.ReadLine(), out explicitInput);

                // TODO : wrap this in order to redirect invalid input
                LogicEngine
                    .ExplicitRecognitionTask
                    .ProcessPlayerInput
                    (
                        currentExplicitRound,
                        explicitInput == 1 ?
                        PlayerInputClassifications.ExplicitlyChoseCooperatorPersona :
                            explicitInput == 2 ?
                            PlayerInputClassifications.ExplicitlyChoseDefectorPersona :
                                PlayerInputClassifications.ExplicitlyDiscardedPersona
                    );

                Console.WriteLine(string.Format("PLAYER  INPUT : {0}", currentExplicitRound.PlayerInputClassification));
                Console.WriteLine(string.Format("PERSONA ACTUAL: {0}", currentExplicitRound.Persona.Classification));
                Console.WriteLine();

                if (fauxExplicitRoundIndex == LogicEngine.ExplicitRecognitionTask.Rounds.Count - 1)
                {
                    break;
                }
                else
                {
                    fauxExplicitRoundIndex++;
                }
            }

            Console.WriteLine("PERSONA SUMMARY:");
            LogicEngine.Personas.ForEach(persona => Console.WriteLine(persona.ToString()));
            Console.WriteLine();

            Console.WriteLine(string.Format("TOTAL COOPERATORS: {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Cooperator).Count));
            Console.WriteLine(string.Format("TOTAL DEFECTORS:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Defector).Count));
            Console.WriteLine(string.Format("TOTAL INDETERMINATE:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Indeterminate).Count));
            Console.WriteLine(string.Format("TOTAL NOVEL:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Novel).Count));
            Console.WriteLine(string.Format("TOTAL UNUSED:   {0}", LogicEngine.GetPersonas<Round>(PersonaClassifications.Unused).Count));
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
