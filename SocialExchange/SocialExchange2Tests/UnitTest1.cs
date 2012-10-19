using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialExchange2;

namespace SocialExchangeTests
{
    [TestClass]
    public class LogicEngineTests
    {
        public int AssemblyImageCount { get; protected set; }
        public int TrustExchangeRoundCount { get; protected set; }
        public LogicEngine LogicEngine { get; protected set; }

        [TestInitialize]
        public void TestInitialize()
        {
            // 18 females, 20 males = 38
            AssemblyImageCount = 38;
            TrustExchangeRoundCount = 24;

            LogicEngine = new LogicEngine();
        }

        [TestMethod]
        public void LogicEngine_Ctor_PersonaCountEqualsAssemblyImageCount()
        {
            Assert.IsTrue(LogicEngine.Personas.Count == AssemblyImageCount);
        }

        [TestMethod]
        public void LogicEngine_Ctor_TrustExchangeRoundCountIsCorrect()
        {
            Assert.IsTrue(LogicEngine.TrustExchangeTask.Rounds.Count == TrustExchangeRoundCount);
        }

        [TestMethod]
        public void LogicEngine_GetNextPersonaClassification_Tests()
        {
            int totalCooperators = 0;
            int totalDefectors = 0;
            List<List<Persona>> cooperatorPersonas = new List<List<Persona>>();

            for(int i = 0; i < 10; i++)
            {
                LogicEngine = new SocialExchange2.LogicEngine();

                while (LogicEngine.TrustExchangeTask.CurrentRoundIndex < LogicEngine.TrustExchangeTask.Rounds.Count)
                {
                    LogicEngine.TrustExchangeTask.ProcessPlayerInput(2);
                    if (LogicEngine.TrustExchangeTask.CurrentRoundIndex == LogicEngine.TrustExchangeTask.Rounds.Count - 1)
                    {
                        break;
                    }
                    else
                    {
                        LogicEngine.TrustExchangeTask.AdvanceToNextRound();
                    }
                }

                List<PersonaClassification> classifications = LogicEngine.Personas.Select<Persona,PersonaClassification>(p => p.Classification).ToList();

                totalCooperators += classifications.Where(c => c == PersonaClassifications.Cooperator).ToList().Count();
                totalDefectors += classifications.Where(c => c == PersonaClassifications.Defector).ToList().Count();

                cooperatorPersonas.Add(LogicEngine.Personas.Where(p => p.Classification == PersonaClassifications.Cooperator).ToList());
            }

            Assert.AreEqual(totalCooperators, totalDefectors);
        }
    }
}
