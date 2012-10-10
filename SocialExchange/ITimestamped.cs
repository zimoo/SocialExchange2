using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialExchange
{
    interface ITimestamped
    {
        DateTime BeginTimestamp { get; }
        DateTime EndTimestamp { get; }

        DateTime Begin();
        DateTime End();
    }
}
