using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;


namespace Logix {
    public class Logix {

        public static List<List<string>> Solve(Puzzle p) {
            Deducer brains = new Deducer(p.height, p.width);
            brains.setClues(p.getClues());
            List<List<string>> result = null;
            int[,] finalMatrix = new int[p.height, p.width];
            try {
                finalMatrix = brains.go();
            }
            catch (Exception e) {
                throw e;
            }
            if (finalMatrix == null) { throw new InconclusiveException(p); }
            for (int x = 0; x < p.height; x++) {
                List<string> row = new List<string>();
                for (int y = 0; y < p.width; y++) {
                    string item = p.getNameAt(y, finalMatrix[x,y]);
                    if (p.width - y != 1) {
                        item += ", ";
                    }
                    row.Add(item);
                }
                result.Add(row);
            }
            return result;
        }
    }
}
