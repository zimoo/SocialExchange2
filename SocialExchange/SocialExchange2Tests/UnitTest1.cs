using System;
using System.Collections.Generic;
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
            List<PersonaClassification> classifications = new List<PersonaClassification>(24);

            classifications.ForEach(c => c = PersonaClassifications.Indeterminate);

            classifications.ForEach(c => c = LogicEngine.GetNextTrustExchangePersonaClassification(classifications));
        }
    }
}
