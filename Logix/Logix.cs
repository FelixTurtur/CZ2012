using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;


namespace Logix {
    public class Logix {

        public SolveCompleteHandler solveHandler;
        private Representation.Results latestResults;

        public Logix() { }

        public List<List<string>> Solve(Puzzle p) {
            Deducer brains = new Deducer(p.height, p.width);
            brains.Concluded += brains_solveComplete;
            brains.setClues(p.getClues());
            int[,] solutionMatrix = new int[p.height, p.width];
            
            try {
                solutionMatrix = brains.Go();
            }
            catch (Exception e) {
                throw e;
            }
            //Translate matrix solution to verbal solution
            return translateSolution(p, solutionMatrix);
        }

        private static List<List<string>> translateSolution(Puzzle p, int[,] solutionMatrix) {
            List<List<string>> solutionStrings = new List<List<string>>();
            for (int x = 0; x < p.height; x++) {
                List<string> row = new List<string>();
                for (int y = 0; y < p.width; y++) {
                    string item = p.getNameAt(y, solutionMatrix[x, y]);
                    if (p.width - y != 1) {
                        item += ", ";
                    }
                    row.Add(item);
                }
                solutionStrings.Add(row);
            }

            return solutionStrings;
        }

        void brains_solveComplete(Deducer sender, SolveCompleteArgs e) {
            this.latestResults = new Results(e.isSuccessful, e.turns, e.timeTaken);
        }

        public Results getLastResults() {
            return latestResults;
        }
    }
}
