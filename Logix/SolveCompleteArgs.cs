using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix
{
    public class SolveCompleteArgs : EventArgs {
        public bool isSuccessful;
        public int turns;
        public TimeSpan timeTaken;

        public SolveCompleteArgs(bool success, int turn, TimeSpan time) {
            isSuccessful = success;
            turns = turn;
            timeTaken = time;
        }
    }
}
