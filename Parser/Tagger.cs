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

        internal string formTagString(string clue) {
            List<string> words = separateWordsAndPunctuation(clue);
            List<string> firstTagPattern = new List<string>();
            string auxCat1 = "";
            string auxCat2 = "";
            foreach (string word in words) {
                if(word.Length==1 && char.IsPunctuation(word[0])) {
                    firstTagPattern.Add(word);
                    continue;
                }
                auxCat1 = catWords.findItemMatches(word);
                auxCat2 = puzzleWords.defineItem(word);
                firstTagPattern.Add(string.IsNullOrEmpty(auxCat1) ? auxCat2 : string.IsNullOrEmpty(auxCat2) ? auxCat1 : auxCat1 + "," + auxCat2);
            }
            return condenseToString(firstTagPattern);
        }

        internal static List<string> separateWordsAndPunctuation(string clue) {
            if (string.IsNullOrEmpty(clue)) return null;
            string[] words = clue.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            for (int i = 0; i < words.Count(); i++) {
                words[i].Replace("\"", "");
                if (words[i].EndsWith(",") || words[i].EndsWith(";") || words[i].EndsWith(".")) {
                    char punctuation = words[i][words[i].Length - 1];
                    result.Add(punctuation.ToString());
                    words[i] = words[i].Remove(words[i].Length - 1);
                }
                result.Add(words[i]);
            }
            return result;
        }

        private string condenseToString(List<string> firstTagPattern) {
            string result = "";
            string buffer1 = "";
            string buffer2 = "";
            string buffer3 = "";
            string previous = "";
            for (int i = 0; i < firstTagPattern.Count; i++) {

            }
            return null;
        }

        private bool pairsWithOf(string tag) {
            if (tag == "Tf" || tag == "Tl") {
                return true;
            }
            return false;
        }

        private bool pairsWithWith(string tag) {
            if (tag == "Tp") {
                return true;
            }
            return false;
        }
    }

}
