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
            while (LogicEngine.TrustExchangeTask.CurrentRoundIndex < LogicEngine.TrustExchangeTask.Rounds.Count)
            {
                Console.WriteLine(string.Format("ROUND {0} / {1}:", LogicEngine.TrustExchangeTask.CurrentRoundIndex + 1, LogicEngine.TrustExchangeTask.Rounds.Count));
                Console.WriteLine(string.Format("PERSONA: {0}", LogicEngine.TrustExchangeTask.CurrentRound.Persona.Filename));

                Console.Write("Give how many points? (1/2) ");

                int points = -1;
                Int32.TryParse(Console.In.ReadLine(), out points);//.StartsWith("1", true, CultureInfo.InvariantCulture);

                // TODO : wrap this in order to redirect invalid input
                LogicEngine.TrustExchangeTask.ProcessPlayerInput(points);

                Console.WriteLine(string.Format("RESPONSE: {0}", LogicEngine.TrustExchangeTask.CurrentRound.PersonaClassification.Value));
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

            Console.WriteLine(string.Format("TOTAL COOPERATORS: {0}", LogicEngine.TrustExchangeTask.GetCount(PersonaClassifications.Cooperator)));
            Console.WriteLine(string.Format("TOTAL DEFECTORS:   {0}", LogicEngine.TrustExchangeTask.GetCount(PersonaClassifications.Defector)));
            Console.WriteLine(string.Format("TOTAL INDETERMINATE:   {0}", LogicEngine.TrustExchangeTask.GetCount(PersonaClassifications.Indeterminate)));
            Console.WriteLine(string.Format("TOTAL NOVEL:   {0}", LogicEngine.TrustExchangeTask.GetCount(PersonaClassifications.Novel)));
        }
    }
}
