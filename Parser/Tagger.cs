using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Tagger {
        internal CategoryDictionary catWords;
        internal TermsDictionary terms;
        private ParsingBuffer buffer;

        public Tagger(List<string> categories, List<string> items, string[] keywords) {
            catWords = new CategoryDictionary(categories, items);
            terms = new TermsDictionary(keywords);
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
                auxCat2 = terms.defineItem(word);
                firstTagPattern.Add(string.IsNullOrEmpty(auxCat1) ? auxCat2 : string.IsNullOrEmpty(auxCat2) ? auxCat1 : auxCat1 + "," + auxCat2);
            }
            return condenseToString(firstTagPattern);
        }

        internal static List<string> separateWordsAndPunctuation(string clue) {
            if (string.IsNullOrEmpty(clue)) return null;
            string[] words = clue.Split(new char[] {' ', '-'}, StringSplitOptions.RemoveEmptyEntries);
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

        private string condenseToString(List<string> tagLine) {
            string result = "";
            this.buffer = new ParsingBuffer(3);
            bool Considering = false;
            bool whitespaceGap = false;
            for (int i = 0; i < tagLine.Count; i++) {
                if (!Considering) {
                    if (!string.IsNullOrEmpty(tagLine[i])) {
                        result += evaluateTag(tagLine[i], ref Considering);
                    }
                }
                else if (Considering) {
                    if (string.IsNullOrEmpty(tagLine[i])) {
                        whitespaceGap = true;
                        continue;
                    }
                    if (isCombinedCatTag(buffer.ToString())) {
                        result += evaluateCatTags(tagLine[i], ref Considering);
                    }
                    else if (isMixedTag(buffer.ToString())) {
                        if (isCatTag(tagLine[i])) {
                            if (buffer.Contains(tagLine[i])) {
                                result += tagLine[i];
                                buffer.Clear();
                                buffer.Add(tagLine[i]);
                            }
                            else {
                                throw new ParserException("Wasn't expecting this combination: " + buffer.ToString() + " followed by " + tagLine[i]);
                            }
                        }
                        else {
                            if (!whitespaceGap) {
                                buffer.dropNonTermTags();
                                result += evaluateTermTags(tagLine[i], ref Considering);
                            }
                            else {
                                throw new ParserException("Wasn't expecting this combination: " + buffer.ToString() + " followed by " + tagLine[i]);
                            }

                        }
                    }
                    else {
                        result += evaluateTermTags(tagLine[i], ref Considering);
                    }
                }
                whitespaceGap = false;
            }
            return result;
        }

        private bool isMixedTag(string tag) {
            bool hasTerm = false;
            bool hasCat = false;
            string[] bits = tag.Split(new char[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string t in bits) {
                if (!hasCat && isCatTag(t)) {
                    hasCat = true;
                }
                else if (!hasTerm && isTermTag(t)) {
                    hasTerm = true;
                }
            }
            return hasCat && hasTerm;
        }

        private bool isCatTag(string tag) {
            if (tag.Length == 1) {
                return true;
            }
            int index;
            if (Int32.TryParse(tag.Substring(1), out index)) {
                return Char.IsLetter(tag[0]);
            }
            return false;
        }

        private bool isDisassociative(string tag) {
            return tag == "Td";
        }

        private bool isTermTag(string tag) {
            return tag[0] == 'T';
        }

        private bool isConsiderable(string tag) {
            return false; //No extras currently being considered. May be where semantic information is dropped in?
        }

        private bool isCombinedCatTag(string tag) {
            string[] bits = tag.Split(new char[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string t in bits) {
                if (!isCatTag(t)) {
                    return false;
                }
            }
            return true;
        }

        private string evaluateTag(string tag, ref bool Considering) {
            string result = "";
            if (isCatTag(tag)) {
            }
            else if (isCombinedCatTag(tag)) {
            }
            else if (isDisassociative(tag)) {
            }
            else if (isTermTag(tag)) {
            }
            else if (isConsiderable(tag)) {
            }
            return result;
        }

        private string evaluateCatTags(string tag, ref bool Considering) {
            throw new NotImplementedException();
        }

        private string evaluateTermTags(string tag, ref bool Considering) {
            if (terms.completesPattern(tag)) {
                buffer.Add(tag);
                string result = buffer.ToString();
                buffer.Clear();
                return result;
            }
            else if (terms.continuesPattern(tag)) {
                buffer.Add(tag);
                return null;
            }
            else return evaluateTag(tag, ref Considering);
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
