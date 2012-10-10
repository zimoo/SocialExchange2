using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SocialExchange2
{
    [TestClass]
    public class SetOnceBackingFieldTests
    {
        [TestMethod]
        public void SetOnceBackingField_StringFieldBacker_Default()
        {
            SetOnceBackingFieldTests_TestClass testClass = new SetOnceBackingFieldTests_TestClass();
            string one = "ONE";
            string two = "TWO";

            testClass.StringFieldBacker_Default = one;
            testClass.StringFieldBacker_Default = two;

            Assert.AreEqual<string>(testClass.StringFieldBacker_Default, one);
        }
    }

    public class SetOnceBackingFieldTests_TestClass
    {
        internal SetOnceBackingField<string> _StringFieldBacker_Default = new SetOnceBackingField<string>();
        public string StringFieldBacker_Default
        {
            get
            {
                return _StringFieldBacker_Default.Value;
            }
            set
            {
                _StringFieldBacker_Default.Value = value;
            }
        }

        internal SetOnceBackingField<string> _StringFieldBacker_NewDefault = new SetOnceBackingField<string>("NewDefault");
        public string StringFieldBacker_NewDefault
        {
            get
            {
                return _StringFieldBacker_NewDefault.Value;
            }
            set
            {
                _StringFieldBacker_NewDefault.Value = value;
            }
        }

    }
}
