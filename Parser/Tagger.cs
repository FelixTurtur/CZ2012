using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Tagger
    {
        internal CategoryDictionary catWords;
        internal TermsDictionary puzzleWords;

        public Tagger(List<string> categories, List<string> items, string[] keywords) {
            catWords = new CategoryDictionary(categories, items);
            puzzleWords = new TermsDictionary(keywords);
        }

        internal List<string> tagClues(List<string> clues) {
            return new List<string>() { };
        }

    }
}
