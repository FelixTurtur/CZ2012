using CleverZebra.Logix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverZebra.Representation
{
    public static class Relations
    {
        public enum Directions
        {
            Lower,
            Higher
        }

        public enum Sides
        {
            Left,
            Related,
            Right
        }
        public static string Positive = "=";
        public static string Negative = "!=";
        //Comparators are listed in pairs of opposites
        private static List<string> Comparators = new List<string> { ">", "<", "+", "-", "*", "/" };
        private static List<string> GreaterThanTerms = new List<string> { ">" };
        private static List<string> LessThanTerms = new List<string> { "<" };
        private static List<string> EqualityChars = new List<string> { "!=", "=" };
        private static List<char> PossessiveChars = new List<char> { '(', ')' };

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

        internal static string comparativeAmount(string input, bool inverse) {
            string comparator = "";
            foreach(string c in Comparators) {
                if (input.Contains(c)) {
                    comparator = c;
                }
            }
            comparator = inverse ? Relations.getInverse(comparator) : comparator;
            string difference = input.Substring(input.IndexOf(Relations.Positive) + 1).Trim();
            return comparator + " " + difference;
        }

        internal static string getInverse(string comparator) {
            for (int i = 0; i < Comparators.Count; i++) {
                if (Comparators[i] == comparator) {
                    if (i % 2 == 0) {
                        return Comparators[i + 1];
                    }
                    else {
                        return Comparators[i - 1];
                    }
                }
            }
            throw new ArgumentException("Comparator not found");
        }


        internal static string getComparator(string rule) {
            foreach (string c in Comparators) {
                if (rule.Contains(c)) {
                    return c;
                }
            }
            return null;
        }

        internal static Directions checkDirection(string comparator) {
            if (Comparators.Contains(comparator) && GreaterThanTerms.Contains(comparator)) {
                return Directions.Higher;
            }
            else if (Comparators.Contains(comparator) && LessThanTerms.Contains(comparator)) {
                return Directions.Lower;
            }
            throw new ArgumentException("Term provided is not known as a term for either Greater or Less than: " + comparator);
        }
    }
    
    
}
