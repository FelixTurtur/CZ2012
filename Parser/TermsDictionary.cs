using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    public class TermsDictionary
    {
        internal List<string> disassociatives; //e.g. "not"; signifies negative relationship.
        internal List<string> numbers;
        internal List<string> quantifiers; //e.g. "days"; signifies the unit in a comparative relationship.
        internal List<string> prepositions; //e.g. "before"; signifies direction of comparative relationship.

        public TermsDictionary(string[] keywords) {
            disassociatives = setupStandardDisassociatives();
            quantifiers = new List<string>();
            numbers = new List<string>();
            prepositions = new List<string>();
            if (keywords.Count() > 0) {
                prepositions.AddRange(new List<string> { "before", "after", "less", "greater", "more", "lower", "higher", "ahead", "behind", "front", "rear", "begins", "ends", "beginning", "ending" });
                foreach (string key in keywords) {
                    numbers.AddRange(getNumericTermsForKey(key));
                    prepositions.AddRange(getPrepositionsForKey(key));
                    quantifiers.AddRange(getQuantifiersForKey(key));
                }
            }
        }

        private List<string> setupStandardDisassociatives() {
            return new List<string> {"neither", "nor", "not", "isn't", "doesn't", "wasn't", "didn't" };
        }

        private bool isFormerReferencer(string word) {
            return word == "former" || word == "first";
        }
        private bool isLatterReferencer(string word) {
            return word == "latter" || word == "second";
        }

        private List<string> getQuantifiersForKey(string key)
        {
            switch (key) {
                case "left-right": return new List<string> { "left", "right" };
                case "days": return new List<string> { "day", "days", "night", "nights" };
                case "months": return new List<string> { "month", "months" };
                case "years": return new List<string> { "month", "months" };
                case "numeric": return new List<string> { "times", "twice", "half", "double", "quarter" };
                case "currency": return new List<string> { "pounds", "dollars" };
                case "date": return new List<string> { "day", "days", "week", "weeks" };
                case "time": return new List<string> { "hour", "hours" };
                case "ordinals": return null;
                case "alphabet": return new List<string> { "letter", "word" };
                default:
                    throw new ArgumentException("Keyword not recognised: " + key);
            }
        }

        private List<string> getPrepositionsForKey(string key) {
            switch (key) {
                case "date":
                case "time":
                case "days":
                case "months":
                case "years": return new List<string> { "earlier", "later" };
                case "currency": return new List<string> { "expensive", "dearer", "cheap", "cheaper", "economical" };
                case "numeric":
                case "left-right":
                case "ordinals": return null;
                default:
                    throw new ArgumentException("Keyword not recognised: " + key);
            }
        }

        private List<string> getNumericTermsForKey(string key) {
            switch (key) {
                case "left-right":
                case "days":
                case "months":
                case "years":
                case "numeric": return new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen" };
                case "currency": return new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "15", "20", "50", "100", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "hundred", "thousand", "million", "billion" };
                case "date": return new List<string> { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th", "21st", "22nd", "23rd", "24th", "25th", "26th", "27th", "28th", "29th", "30th", "31st" };
                case "time": return new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
                case "ordinals": return new List<string> { "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "ninth", "tenth" };
                default:
                    throw new ArgumentException("Keyword not recognised: " + key);
            }
        }

        public List<string> getQuantifiers() {
            return quantifiers;
        }

        internal string defineItem(string word) {
            double result;
            if (double.TryParse(word, out result)) {
                return word; //All numbers should be kept.
            }
            if (word == "of") {
                return "To";
            }
            if (word == "with") {
                return "Tw";
            }
            if (word == "either") {
                return "Te";
            }
            for (int i = 0; i < disassociatives.Count; i++) {
                if (word.ToLower() == disassociatives[i]) {
                    return "Td";
                }
            }
            if (isFormerReferencer(word.ToLower())) {
                return "Tf";
            }
            else if (isLatterReferencer(word.ToLower())) {
                return "Tl";
            }
            for (int i = 0; i < numbers.Count; i++) {
                if (word.ToLower() == numbers[i]) {
                    return "Tn(" + word + ")";
                }
            }
            for (int i = 0; i < quantifiers.Count; i++) {
                if (word.ToLower() == quantifiers[i]) {
                    return "Tq(" + word + ")";
                }
            }
            for (int i = 0; i < prepositions.Count; i++) {
                if (word.ToLower() == prepositions[i]) {
                    return "Tp(" + word + ")";
                }
            }
            return string.Empty;
        }

    }
}
