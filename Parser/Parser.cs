using System.Collections.Generic;
using Representation;

namespace Parser {
    public class CZParser {

        private Puzzle puzzle;
        internal Tagger tagger;
        public CZParser(Puzzle p) {
            puzzle = p;
        }

        public List<string> Read() {
            tagger = new Tagger(puzzle.getCategories(), puzzle.getItems(), puzzle.getKeywords());
            List<string> taggedClues = tagger.tagClues(puzzle.getClues());
            List<string> relations = transformTagsToRelations(taggedClues);
            return relations;
        }

        internal List<string> transformTagsToRelations(List<string> tags) {
            List<string> results = new List<string>() { };
            foreach (string tag in tags) {
                results.Add(Translator.makeRelation(tag));
            }                
            return results;
        }

    }
}
