using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Representation
{
    public class SolutionBoxEventArgs : EventArgs {
        public string item;
        public int line;
        public int catIndex;

        public SolutionBoxEventArgs(string name, int lineIndex, int cat) {
            item = name;
            line = lineIndex;
            catIndex = cat;
        }
    }
}
