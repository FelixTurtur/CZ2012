using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Tagger {
        internal CategoryDictionary catWords;
        internal TermsDictionary puzzleWords;

        public Tagger(List<string> categories, List<string> items, string[] keywords) {
            catWords = new CategoryDictionary(categories, items);
            puzzleWords = new TermsDictionary(keywords);
        }

        internal List<string> tagClues(List<string> clues) {
            List<string> results = new List<string>() { };
            foreach (string clue in clues) {
                results.Add(formTagString(clue));
            }
            return results;
        }

        private string formTagString(string clue) {
            string[] words = clue.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            Stack<string> firstTagPattern = new Stack<string>();
            string auxCat = "";
            foreach (string word in words) {
                if (!string.IsNullOrEmpty(auxCat = catWords.findItemMatches(word))) {
                    firstTagPattern.Push(auxCat);
                }
                else if (!string.IsNullOrEmpty(auxCat = puzzleWords.findItem(word))) {
                    //can't do this as if..else.. as the word could be in two/more word groups
                }
                else if () {
                }
                else 
                    firstTagPattern.Push(" ");
            }
            return condenseToString(firstTagPattern);
        }

        private string condenseToString(Stack<string> firstTagPattern) {
            throw new NotImplementedException();
        }
    }

}
