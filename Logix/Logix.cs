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
            int[,] solutionMatrix = new int[p.height, p.width];
            
            try {
                solutionMatrix = brains.go();
            }
            catch (Exception e) {
                throw e;
            }
            if (solutionMatrix == null) { 
                throw new InconclusiveException(p); 
            }
            //Translate matrix solution to verbal solution
            List<List<string>> solutionStrings = new List<List<string>>();
            for (int x = 0; x < p.height; x++) {
                List<string> row = new List<string>();
                for (int y = 0; y < p.width; y++) {
                    string item = p.getNameAt(y, solutionMatrix[x,y]);
                    if (p.width - y != 1) {
                        item += ", ";
                    }
                    row.Add(item);
                }
                solutionStrings.Add(row);
            }

            return solutionStrings;
        }
    }
}
