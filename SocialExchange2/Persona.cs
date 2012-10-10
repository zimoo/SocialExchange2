using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange2
{
    public class Persona
    {
        public Bitmap Image { get; protected set; }
        public string Filename { get; protected set; }

        public Persona(Bitmap image, string filename)
        {
            Image = image;
            Filename = filename;
        }
    }
}
