using System.Collections.Generic;
using Representation;

namespace Parser {
    public class CZParser {

        private Puzzle puzzle;
        internal Tagger tagger;
        public CZParser(Puzzle p) {
            puzzle = p;
            tagger = new Tagger(puzzle.getCategories(), puzzle.getItems(), puzzle.getKeywords());
        }

        public List<string> Read() {
            List<string> tagLines = tagger.tagClues(puzzle.getClues());
            List<string> results = new List<string>();
            foreach (string line in tagLines) {
                results.AddRange(Translator.makeRelations(line));
            }                
            return results;
        }

    }
}
