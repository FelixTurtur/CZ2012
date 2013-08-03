using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix
{
    public class SolutionUpdateArgs : System.EventArgs {
        public string item;
        public int line;

        public SolutionUpdateArgs(string p1, int n) {
            item = p1;
            line = n;
        }
    }
}
