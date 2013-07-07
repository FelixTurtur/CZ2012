using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Parser;
using Logix;
using Representation;

namespace CleverZebra {
    internal class Controller {

        #region Singletonia
        private static Controller instance = null;
        private Controller() {}
        public static Controller getInstance() {
            if (instance == null) {
                instance = new Controller();
            }
            return instance;
        }
        #endregion

        private List<Puzzle> puzzles;
        private Puzzle activePuzzle;

        public List<Puzzle> loadPuzzles(XmlDocument sourceDoc = null) {
            if (sourceDoc == null) {
                sourceDoc = new XmlDocument();
                sourceDoc.LoadXml(CleverZebra.Properties.Resources.puzzles);
            }
            else checkValidFile(sourceDoc);
            sourceDoc.Normalize();
            puzzles = new List<Puzzle>();
            XmlNodeList xmlPuzzles = sourceDoc.GetElementsByTagName("puzzle");
            foreach (XmlNode n in xmlPuzzles) {
                if (string.IsNullOrEmpty(n["title"].InnerText)) {
                    continue;
                }
                puzzles.Add(new Puzzle(n));
            }
            return puzzles;
        }

        private void checkValidFile(XmlDocument sourceDoc) {
            throw new NotImplementedException();
        }

        public void Solve(Puzzle puzzle) {
            this.activePuzzle = puzzle;
            try {
                List<string> rules = Parser.Parser.Read(puzzle);
                puzzle.setRules(rules);
            }
            catch (Exception e) {
                throw new Parser.ParserException("Unable to parse clues for puzzle id: " + puzzle.getId(), e);
            }
            List<string> result = null;
            try {
                result = Logix.Logix.Solve(puzzle);
            }
            catch (Exception e) {
                throw new Logix.LogicException("Unable to solve puzzle id: " + puzzle.getId(), e);
            }
            reportSuccess(result);
        }

        private void reportSuccess(List<string> result) {
            List<string> originalSolution = activePuzzle.ProvidedSolution;

        }
    }
}
