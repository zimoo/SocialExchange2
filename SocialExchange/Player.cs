using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    public class Player
    {
        public string UniqueIdentifier { get; protected set; }
        public string Lastname { get; protected set; }
        public string Firstname { get; protected set; }

        public Player(string uniqueIdentifier, string firstname = "", string lastname = "")
        {
            UniqueIdentifier = uniqueIdentifier;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
