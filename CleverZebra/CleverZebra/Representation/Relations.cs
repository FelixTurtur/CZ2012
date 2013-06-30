using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Representation
{
    public static class Relations
    {
        public static string Positive = "=";
        public static string Negative = "!=";
        private static List<string> Comparators = new List<string> { ">", "<", "+", "-", "*", "/" };
        private static List<string> EqualityChars = new List<string> { "!=", "=" };
        private static List<char> PossessiveChars = new List<char> { '(', ')' };

        public enum Sides
        {
            Left,
            Related,
            Right
        }

        public static bool isEqualityChar(char c) {
            foreach (string e in EqualityChars) {
                if (e.Contains(c)) { return true; }
            }
            return false;
        }

        public static bool isComparator(char c) {
            foreach (string e in Comparators) {
                if (e.Contains(c)) { return true; }
            }
            return false;
        }

        public static bool isPossessive(char c) {
            foreach (char e in PossessiveChars) {
                if (e==c) { return true; }
            }
            return false;
        }

        internal static bool isRelative(string input) {
            foreach (string comparator in Representation.Relations.Comparators) {
                if (input.Contains(comparator)) {
                    return true;
                }
            }
            return false;
        }

        internal static bool isQuantified(string input) {
            return input.Contains(Relations.Positive);
        }
    }
    
    
}
