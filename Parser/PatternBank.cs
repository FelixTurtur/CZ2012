using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZParser {
    internal class PatternBank {

        private static List<string> termPatterns = new List<string> { "Tx,Tq,Tp", "Tx,Tp,Tq", "Tx,Tp,Th", "Tf,To", "Tp,Tw", "Tp, Tx", "Tp,Th", "Tp,Tq,Th", "Tx,Tq,A", "Ta,Td", "Tp,Th,Tx", "Tq,Tp"};
        private static List<string> relationPatterns =  new List<string> { "C,C", "C,Td,C", "C,Tx,Tq,Tp,C", "C,Tx,Tp,Tq,C", 
            "C,A,Tx,Tp,C", "C,Tx,Tp,A,C", "C,A,Tp,Tx", "C,A,Tp,C", "C,Tp,A,C", "C,Tp,Tq,C", "C,Tp,C", "C,Tx,Tp,C", "C,Ts", "C,Td,Ts", "C,Tx,Tq,A", "Tx,Tq,Tp,C,C", "C,Tq,Tp,C" };

        internal static bool completesTagPattern(string buffer, string tag) {
            string[] matchedPatterns = matchCurrentBuffer(buffer);
            foreach (string p in matchedPatterns) {
                if (completesPattern(p, buffer, tag)) {
                    return true;
                }
            }
            return false;
        }

        internal static bool continuesTagPattern(string buffer, string tag) {
            string[] matchedPatterns = matchCurrentBuffer(buffer);
            foreach (string p in matchedPatterns) {
                if (continuesPattern(p, buffer, tag)) {
                    return true;
                }
            }
            return false;
        }

        private static string[] matchCurrentBuffer(string buffer) {
            List<string> matches = new List<string>();
            foreach (string p in termPatterns) {
                if (expressesPattern(buffer, p))
                    matches.Add(p);
            }
            return matches.ToArray();
        }

        private static bool expressesPattern(string buffer, string p) {
            return p.Contains(removeParentheticals(buffer));
        }

        private static bool completesPattern(string pattern, string buffer, string tag) {
            if (pattern == removeParentheticals(buffer) + "," + removeParentheticals(tag)) {
                return true;
            }
            return false;
        }

        private static bool continuesPattern(string pattern, string buffer, string tag) {
            if (pattern.Contains(removeParentheticals(buffer) + "," + removeParentheticals(tag))) {
                return true;
            }
            return false;
        }

        private static string removeParentheticals(string buffer) {
            while (buffer.Contains("(")) {
                buffer = buffer.Remove(buffer.IndexOf('('), (buffer.IndexOf(')') - buffer.IndexOf('('))+1);
            }
            foreach (string tag in buffer.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (tag[0] == 'T')
                    continue;
                else
                    buffer.Replace(tag, "A");
            }
            return buffer;
        }

        internal static bool holdsTagPattern(ParsingBuffer buffer) {
            if (buffer.isEmpty()) {
                return false;
            }
            string bufferPattern = getShortTagVersion(buffer.ToString());
            return relationPatterns.Contains(bufferPattern);
        }

        internal static int getPatternNumber(string p) {
            string s = getShortTagVersion(p);
            for (int i = 0; i < relationPatterns.Count; i++) {
                if (s == relationPatterns[i])
                    return i;
            }
            throw new ParserException("Pattern not found in bank: " + p);
        }

         private static string getShortTagVersion(string tagline) {
            string bufferPattern = "";
            foreach (string tag in tagline.ToString().Split(new char[] { ',',' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (Tagger.isCatTag(tag)) {
                    if (tag.Length == 1) 
                        bufferPattern += string.IsNullOrEmpty(bufferPattern) ? "A" : ",A";
                    else 
                        bufferPattern += string.IsNullOrEmpty(bufferPattern) ? "C" : ",C";
                }
                else {
                    string shortTag = removeParentheticals(tag);
                    bufferPattern += string.IsNullOrEmpty(bufferPattern) ? shortTag : "," + shortTag;
                }
            }
            return bufferPattern;
        }

   }
}
