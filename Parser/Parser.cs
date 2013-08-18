using System.Collections.Generic;
using Representation;

namespace CZParser {
    public class Parser {

        private Puzzle puzzle;
        internal Tagger tagger;
        internal Translator translator;

        public Parser(Puzzle p) {
            puzzle = p;
            tagger = new Tagger(puzzle.getCategories(), puzzle.getItems(), puzzle.getKeywords());
            translator = new Translator(puzzle.getKeywords(), p.height);
        }

        public List<string> Read() {
            List<string> tagLines = tagger.tagClues(puzzle.getClues());
            List<string> results = new List<string>();
            foreach (string line in tagLines) {
                results.AddRange(translator.makeRelations(line));
            }                
            return results;
        }

    }
}
