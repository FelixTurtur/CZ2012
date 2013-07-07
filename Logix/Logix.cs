using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;


namespace Logix {
    public class Logix {

        public static List<string> Solve(Puzzle p) {
            Deducer brains = new Deducer(p.height, p.width);
            brains.setClues(p.getClues());
            List<string> result = null;
            try {
                result = brains.go();
            }
            catch (Exception e) {
                throw e;
            }
            if (result == null) { throw new InconclusiveException(p); }
            return result;
        }
    }
}
