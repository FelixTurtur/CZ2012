using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix {
   public class MatchEventArgs : System.EventArgs {
      public string item1 = "";
      public string item2 = "";

      public MatchEventArgs(string p1, string p2) {
         item1 = p1;
         item2 = p2;
      }
   }
}
