using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CleverZebra.Parser;

namespace CleverZebra {
    internal class Controller {

        #region Singletonia
        private static Controller instance = null;
        private Controller() { }
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

        public void Solve(Puzzle p) {
            this.activePuzzle = p;
            try {
                List<string> rules = Parser.Parser.Read(p);
                p.setRules(rules);
            }
            catch (Exception e) {
                throw new Parser.ParserException("Unable to parse clues for puzzle id: " + p.getId(), e);
            }
            Solution result = null;
            try {
                result = Logix.Logix.Solve(p);
            }
            catch (Exception e) {
                throw new Logix.LogicException("Unable to solve puzzle id: " + p.getId(), e);
            }
            reportSuccess(result);
        }

        private void reportSuccess(Solution result) {
            throw new NotImplementedException();
        }
    }
}
