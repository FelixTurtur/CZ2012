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
            bool reflectional = line.Contains("Tf") || line.Contains("Tl");
            string[] lines = line.Split(new char[] { '.', ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> relations = new List<string>();
            if (!reflectional) {
                foreach (string l in lines) {
                    relations.AddRange(getRelationsFromLine(l.Trim()));
                }
            }
            else {
                //will need to refer to one statement from another
            }
            return relations;
        }

        private static List<string> getRelationsFromLine(string line) {
            if (isCatPair(line)) {
                return new List<string> { line.Substring(0, line.IndexOf(" ")) + Representation.Relations.Positive + line.Substring(line.IndexOf(" ") + 1) };
            }
            else if (isNegCatPair(line)) {
                return new List<string> { line.Substring(0, line.IndexOf(" ")) + Representation.Relations.Negative + line.Substring(line.LastIndexOf(" ") + 1) };
            }
            else {
                throw new ParserException("Unable to match tag pattern: " + line);
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
            if (words.Count() == 3 && TermsDictionary.isNegative(words[1]) && Tagger.isCatTag(words[2])) {
                return true;
            }
            return false;
        }

    }
}
