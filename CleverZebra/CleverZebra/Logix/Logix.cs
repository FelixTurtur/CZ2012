using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleverZebra.Logix;

namespace CleverZebra.Logix {
    public static class Logix {

        public static Solution Solve(Puzzle p) {
            Deducer brains = new Deducer(p.height, p.width);
            brains.setRules(p.getRules());
            Solution result = null;
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
