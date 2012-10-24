using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class Persona
    {
        public PersonaClassification Classification { get; set; }

        public Bitmap Image { get; protected set; }
        public string FileName { get; protected set; }

        public Persona(Bitmap image, string fileName)
        {
            Classification = PersonaClassifications.Unused;

            Image = image;
            FileName = fileName;
        }

        public override string ToString()
        {
            return 
                String.Join(", ",
                    "[PERSONA]",
                    string.Format("{0}",FileName),
                    string.Format("{0}",Classification)
                    );
        }
    }
}
