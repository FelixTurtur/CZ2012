using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Representation
{
    public class Results {
        bool Successful;
        int TurnsForCompletion;
        TimeSpan TimeTaken;

        public Results(bool success, int turns, TimeSpan ms) {
            Successful = success;
            TurnsForCompletion = turns;
            TimeTaken = ms;
        }

        public List<object> listResults() {
            return new List<object> { Successful, TurnsForCompletion, TimeTaken };
        }
    }
}
