using System;
using System.Collections.Generic;
using System.Linq;

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
                char punctuation = '0';
                words[i].Replace("\"", "");
                if (words[i].EndsWith(",") || words[i].EndsWith(";") || words[i].EndsWith(".")) {
                    punctuation = words[i][words[i].Length - 1];
                    words[i] = words[i].Remove(words[i].Length - 1);
                }
                result.Add(words[i]);
                if (char.IsPunctuation(punctuation)) 
                    result.Add(punctuation.ToString());
            }
            return result;
        }

        private string condenseToString(List<string> tagLine) {
            string result = "";
            this.buffer = new ParsingBuffer(3);
            bool whitespaceGap = false;
            string heldTag = "";    //held items require recordable items afterwards to themselves be recorded.
            string lastNonBlank = "";
            for (int i = 0; i < tagLine.Count; i++) {
                if (string.IsNullOrEmpty(tagLine[i])) {
                    whitespaceGap = true;
                    continue;
                }
                if (i > 0 && tagLine[i] == lastNonBlank) {
                    continue;
                }
               if (buffer.isEmpty()) {
                   string newItem = evaluateTag(lastNonBlank, tagLine[i], ref heldTag);
                    result += addTag(ref heldTag, newItem);
                }
                else {
                    if (isCombinedCatTag(buffer.ToString())) {
                        string aux = evaluateCatTags(lastNonBlank, tagLine[i], ref heldTag);
                        result += addTag(ref heldTag, aux);
                    }
                    else if (isMixedTag(buffer.ToString())) {
                        if (isCatTag(tagLine[i])) {
                            if (buffer.Contains(tagLine[i])) {
                                result += addTag(ref heldTag, tagLine[i]);
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
                                string aux =  evaluateTermTags(lastNonBlank, tagLine[i], ref heldTag);
                                result += addTag(ref heldTag, aux);
                            }
                            else {
                                throw new ParserException("Wasn't expecting this combination: " + buffer.ToString() + " followed by " + tagLine[i]);
                            }

                        }
                    }
                    else {
                        string aux = evaluateTermTags(lastNonBlank, tagLine[i], ref heldTag);
                        result += addTag(ref heldTag, aux);
                    }
                }
                whitespaceGap = false;
                lastNonBlank = tagLine[i];
            }
            return result.Trim();
        }

        private string addTag(ref string heldTag, string p) {
            if (string.IsNullOrEmpty(heldTag)) {
                if (string.IsNullOrEmpty(p)) {
                    return null;
                }
                return p + " ";
            }
            if (string.IsNullOrEmpty(p)) {
                return null;
            }
            string addition = heldTag.Trim() + " " + p + " ";
            heldTag = "";
            return addition;
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

        internal static bool isCatTag(string tag) {
            if (tag.Length == 1 && !char.IsPunctuation(tag[0])) {
                return true;
            }
            int index;
            if (tag.Contains(",") == false && Int32.TryParse(tag.Substring(1), out index)) {
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

        private string evaluateTag(string previous, string tag, ref string heldTag) {
            if (tagMustBeHeld(tag)) {
                heldTag += tag + " ";
                return null;
            }
            if (isCatTag(tag)) {
                return tag;
            }
            else if (isCombinedCatTag(tag)) {
                //check previous item
                if (tag.Contains(previous)) {
                    //already covered by previous recorded tag
                    return null;
                }
                buffer.Add(tag);
            }
            else if (isTermTag(tag)) {
                if (tag == "To") {
                    if (pairsWithOf(previous)) {
                        return previous + " " + tag;
                    }
                    else return null;
                }
                if (tag == "Tw") {
                    if (pairsWithWith(previous)) {
                        return previous + " " + tag;
                    }
                    else return null;
                }
                buffer.Add(tag);
            }
            else if (isConsiderable(tag)) {
            }
            return null;
        }

        private bool tagMustBeHeld(string tag) {
            if (tag.Length == 1 && char.IsPunctuation(tag[0])) {
                return true;
            }
            if (isDisassociative(tag)) {
                return true;
            }
            return false;
        }

        private string evaluateCatTags(string previous, string tag, ref string heldTag) {
            //buffer contains cat items (from a multi-option tag)
            if (buffer.Contains(tag)) {
                //could be a two-option tag after a three-option tag. Unlikely, but safeguarding.
                if (isCatTag(tag)) {
                    return tag;
                }
                else buffer.Add(tag);
                return null;
            }
            else {
                //we have a term/combo cat-term tag
                if (isTermTag(tag)) {
                    //drop inconclusive buffer, start considering term
                    buffer.Clear();
                    return evaluateTermTags(previous, tag, ref heldTag);
                }
                else {
                    try {
                        if (buffer.Contains(getCatTagFromCombo(tag))) {
                            return getCatTagFromCombo(tag);
                        }
                    }
                    catch (ParserException p) {
                        throw p;
                    }
                    throw new ParserException("Wasn't expecting this buffer: " + buffer.ToString() + " to be followed by " + tag);
                }
            }
        }

        private string evaluateTermTags(string previous, string tag, ref string heldTag) {
            if (PatternBank.completesTagPattern(buffer.ToString(),tag)) {
                buffer.Add(tag);
                string aux = buffer.ToString();
                buffer.Clear();
                return aux;
            }
            else if (PatternBank.continuesTagPattern(buffer.ToString(), tag)) {
                buffer.Add(tag);
                return null;
            }
            else return evaluateTag(previous, tag, ref heldTag);
        }

        private string getCatTagFromCombo(string tag) {
            foreach (string bit in tag.Split(new char[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (isCatTag(bit)) {
                    return bit;
                }
            }
            throw new ParserException("Wasn't expecting to not find a cat tag in here: " + tag);
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
