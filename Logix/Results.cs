using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logix
{
    internal class Results {
        bool Successful;
        int TurnsForCompletion;
        TimeSpan TimeTaken;

        internal Results(bool success, int turns, TimeSpan ms) {
            Successful = success;
            TurnsForCompletion = turns;
            TimeTaken = ms;
        }

        internal List<object> listResults() {
            return new List<object> { Successful, TurnsForCompletion, TimeTaken };
        }
    }
}
