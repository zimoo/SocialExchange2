using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class PersonaClassification : ValueEnum<string>
    {
        public PersonaClassification(string value) : base(value) { }
    }

    public static class PersonaClassifications
    {
        public static readonly PersonaClassification Unused = new PersonaClassification("Unused");
        public static readonly PersonaClassification Novel = new PersonaClassification("Novel");
        public static readonly PersonaClassification Cooperator = new PersonaClassification("Cooperator");
        public static readonly PersonaClassification Defector = new PersonaClassification("Defector");
        public static readonly PersonaClassification Indeterminate = new PersonaClassification("Indeterminate");
    }
}
