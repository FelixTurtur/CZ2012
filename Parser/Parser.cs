using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Representation;

namespace Parser {
    public class CZParser {

        private Puzzle puzzle;

        public CZParser(Puzzle p) {
            puzzle = p;
        }

        public List<string> Read() {
            CategoryDictionary catWords = new CategoryDictionary(puzzle.getCategories(), puzzle.getItems());
        }
    }
}
