using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Representation;

namespace Parser
{
    class Translator
    {
        internal static List<string> makeRelations(string line) {
            List<string> relations = new List<string>();
            if (containsMultipleStatements(line)) {
                //split
                relations.AddRange(getRelationsFromMultiLine(line));
            }
            else {
                relations.AddRange(getRelationsFromLine(line));
            }
            return relations;
        }

        private static bool containsMultipleStatements(string line) {
            string[] lines = line.Split(new char[] { '.', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Count() > 1)
                return true;
            return false;
        }

        private static List<string> getRelationsFromMultiLine(string line) {
            List<string> relations = new List<string>();
            string[] lines = line.Split(new char[] { '.', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (line.Contains("; Tt Tn") || line.Contains(". Tt Tn")) {
                //two separate relations and also a comparative between one item from each to be created. Left then right.
                
            }
            if (line.Contains("; Tn") || line.Contains(". Tn")) {
                //two separate relations and also a comparative between one item from each to be created. Right then left.

            }
            else if (line.Contains("Tf")) {
                //a negative/comparative relation and a second relation to the former item to be created
            }
            else if (line.Contains("Tl")) {
                //a negative/comparative relation and a second relation to the latter item to be created
            }
            else if (line.Contains("Tt")) {
                //a second relation must be formed with an item from the first relation
            }
            else {
                foreach (string l in lines) {
                    relations.AddRange(getRelationsFromLine(l.Trim()));
                }
            }
            return relations;
        }

        private static List<string> getRelationsFromLine(string line) {
            if (isCatPair(line)) {
                return new List<string> { line.Substring(0, line.IndexOf(" ")) + Representation.Relations.Positive + line.Substring(line.IndexOf(" ") + 1) };
            }
            else if (isNegCatPair(line)) {
                return getNegativeFromPair(line);
            }
            else if (line.Contains(",")) {
                if (line.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries).Count() == 3) {
                    return new List<string> { line.Substring(0, line.IndexOf(" ")) + Representation.Relations.Positive + line.Substring(line.LastIndexOf(" ") + 1) };
                }
                throw new ParserException("Unable to handle tag pattern: " + line);
            }
            else {
                throw new ParserException("Unable to handle tag pattern: " + line);
            }
        }

        private static bool isCatPair(string line) {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Count() == 2) {
                return true;
            }
            return false;
        }

        private static bool isNegCatPair(string line) {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Count() == 3 && TermsDictionary.isNegative(words[0]) && Tagger.isCatTag(words[1]) && Tagger.isCatTag(words[2])) {
                return true;
            }
            if (words.Count() == 3 && TermsDictionary.isNegative(words[1]) && Tagger.isCatTag(words[0]) && Tagger.isCatTag(words[2])) {
                return true;
            }
            if (words.Count() == 3 && TermsDictionary.isNegative(words[2]) && Tagger.isCatTag(words[0]) && Tagger.isCatTag(words[1])) {
                return true;
            }
            return false;
        }

        private static List<string> getNegativeFromPair(string line) {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string left = "";
            string right = "";
            foreach (string word in words) {
                if (TermsDictionary.isNegative(word)) {
                    continue;
                }
                if (string.IsNullOrEmpty(left)) {
                    left = word;
                }
                right = word;
            }
            return new List<string> { left + Relations.Negative + right };
        }

    }
}
