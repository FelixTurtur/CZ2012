using Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverZebra
{
    internal class SolutionReachedArgs {
        internal List<List<string>> solution;
        internal bool success;
        internal int numTurns;
        internal TimeSpan solutionTime;

        internal SolutionReachedArgs(List<List<string>> strings, List<object> stats) {
            solution = strings;
            this.success = (bool)stats[0];
            this.numTurns = (int)stats[1];
            this.solutionTime = (TimeSpan)stats[2];
        }


    }
}
