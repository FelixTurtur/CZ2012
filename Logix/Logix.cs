using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;


namespace Logix {
    public delegate void LogixUpdateHandler(Logix sender, SolutionBoxEventArgs e);

    public class Logix {

        public SolveCompleteHandler solveHandler;
        public LogixUpdateHandler updater;
        private Representation.Results latestResults;
        private Puzzle p;
        internal bool goSlow;

        public Logix(bool slow) { goSlow = slow; }

        public List<List<string>> Solve(Puzzle p) {
            this.p = p;
            Deducer brains = new Deducer(p, goSlow );
            brains.Concluded += brains_solveComplete;
            brains.Update += brains_Update;
            brains.setClues(p.getRules());
            int[,] solutionMatrix = new int[p.height, p.width];

            try {
                solutionMatrix = brains.Go();
            }
            catch (LogicException l) {
                if (l.problemItem != null) {
                    string friendlyName = p.getNameAt(l.problemItem[0] - 'A', Convert.ToInt32(l.problemItem.Substring(1)));
                    throw new LogicException(l.Message + friendlyName + ".\nReasoning failed.", l.problemItem);
                }
                else throw l;
            }
            catch (Exception e) {
                throw e;
            }
            //Translate matrix solution to verbal solution
            return translateSolution(p, solutionMatrix);
        }

        void brains_Update(Deducer sender, SolutionUpdateArgs e) {
            if (updater != null) {
                int category = e.item[0] - 'A';
                int catItem =  Convert.ToInt32(e.item.Substring(1));
                updater(this, new SolutionBoxEventArgs(p.getNameAt(category, catItem), e.line, category));
            }
        }

        private static List<List<string>> translateSolution(Puzzle p, int[,] solutionMatrix) {
            List<List<string>> solutionStrings = new List<List<string>>();
            for (int x = 0; x < p.height; x++) {
                List<string> row = new List<string>();
                for (int y = 0; y < p.width; y++) {
                    string item = p.getNameAt(y, solutionMatrix[x, y]);
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
