using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Representation
{
    public class Relations
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
        private static char Conditional = '?';
        private static char ConditionalDivider = ':';
        //Comparators are listed in pairs of opposites
        private static List<string> Comparators = new List<string> { ">", "<", "+", "-", "*", "/" };
        private static List<string> GreaterThanTerms = new List<string> { ">", "-", "/" };
        private static List<string> LessThanTerms = new List<string> { "<", "+", "*" };
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

        public static string getComparator(string s) { 
            foreach (string c in Comparators) {
                if (s.IndexOf(c) != -1) {
                    return c;
                }
            }
            return null;
        }

        public static bool isPossessive(char c) {
            foreach (char e in PossessiveChars) {
                if (e == c) { return true; }
            }
            return false;
        }

        public static bool isRelative(string input) {
            foreach (string comparator in Representation.Relations.Comparators) {
                if (input.Contains(comparator)) {
                    return true;
                }
            }
            return false;
        }

        public static bool isQuantified(string input) {
            return input.Contains(Relations.Positive);
        }

        public static bool isConditional(string input) {
            return input.Contains(Conditional);
        }

        public static string comparativeAmount(string input, bool inverse) {
            if (!isQuantified(input)) {
                return null;
            }
            string comparator = "";
            foreach (string c in Comparators) {
                if (input.Contains(c)) {
                    comparator = c;
                }
            }
            comparator = inverse ? getInverse(comparator) : comparator;
            string difference = input.Substring(input.IndexOf(Relations.Positive) + 1).Trim();
            return comparator + " " + difference;
        }

        public static string getInverse(string comparator) {
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

        public static Directions checkDirection(string comparator) {
            if (Comparators.Contains(comparator) && GreaterThanTerms.Contains(comparator)) {
                return Directions.Higher;
            }
            else if (Comparators.Contains(comparator) && LessThanTerms.Contains(comparator)) {
                return Directions.Lower;
            }
            throw new ArgumentException("Term provided is not known as a term for either Greater or Less than: " + comparator);
        }


        public static string getConditionalStatement(string rule) {
            //Conditional rules are surrounded by '?'
            if (rule[0] != Conditional) {
                return null;
            }
            return rule.Substring(1, rule.IndexOf(Conditional, 1) - 1);
        }

        public static string getIfFalseStatement(string rule) {
            rule = rule.Substring(rule.IndexOf(Conditional, 1) + 1);
            return rule.Substring(rule.IndexOf(ConditionalDivider) + 1);
        }

        public static string getIfTrueStatement(string rule) {
            rule = rule.Substring(rule.IndexOf(Conditional, 1) + 1);
            return rule.Substring(0, rule.IndexOf(ConditionalDivider));
        }

    }
}
