﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CZParser
{
    public class TermsDictionary
    {
        internal List<string> disassociatives; //e.g. "not"; signifies negative relationship.
        internal List<string> numbers;
        internal List<List<string>> quantifiers; //e.g. "days"; signifies the unit in a comparative relationship.
        internal List<string> prepositions; //e.g. "before"; signifies direction of comparative relationship. Stored in opposite pairs, with - before +.
        internal List<string> superlatives;
        internal char? currency;
        private static string OF = "To";
        private static string WITH = "Tw";
        private static string AND = "Ta";
        private static string EITHER = "Te";
        private static string BUT = "Tb";
        private static string NEGATIVE = "Td";
        private static string SUPER = "Ts";
        private static string FORMER = "Tf";
        private static string LATTER = "Tl";
        private static string THIS = "Tt";
        private static string THAN = "Th";

        public TermsDictionary(string[] keywords, char? currency = null) {
            this.currency = currency;
            disassociatives = setupStandardDisassociatives();
            quantifiers = new List<List<string>>();
            numbers = new List<string>();
            prepositions = new List<string>();
            for (int i = 0; i < keywords.Count(); i++) {
                addQuantifiers(keywords[i]);
                addSuperlatives(keywords[i]);
                if (!string.IsNullOrEmpty(keywords[i])) {
                    numbers.AddRange(getNumericTermsForKey(keywords[i]));
                    setStandardPrepositions();
                    prepositions.AddRange(getPrepositionsForKey(keywords[i]) ?? new List<string>());
                }
            }
        }


        private void setStandardPrepositions() {
            if (prepositions.Count == 0) {
                prepositions.AddRange(new List<string> { "before", "after", "less", "greater", "fewer", "more", "lower", "higher", "behind", "ahead", "rear", "front", "begins", "ends", "beginning", "ending" });
            }
        }

        private void addQuantifiers(string key) {
            quantifiers.Add(getQuantifiersForKey(key));
        }

        private List<string> setupStandardSuperlatives() {
            return new List<string>();
        }

        private List<string> setupStandardDisassociatives() {
            return new List<string> {"neither", "nor", "not", "isn't", "doesn't", "wasn't", "didn't", "never" };
        }

        private List<string> getQuantifiersForKey(string key)
        {
            if (string.IsNullOrEmpty(key)) {
                return new List<string>() { };
            }
            switch (key) {
                case "left-right": return new List<string> { "left", "right" };
                case "days": return new List<string> { "day", "days", "night", "nights" };
                case "months": return new List<string> { "month", "months" };
                case "years": return new List<string> { "year", "years" };
                case "numeric": return new List<string> { "times", "twice", "half", "double", "quarter" };
                case "currency": return new List<string> { "pounds", "dollars", "euros", "money", "cash", "wealth", "half", "double", "quarter", "twice" };
                case "dates": return new List<string> { "day", "days", "week", "weeks" };
                case "time": return new List<string> { "hour", "hours" };
                case "ordinals": return new List<string>() { };
                case "alphabet":
                case "length" :
                case "words": return new List<string> { "letter", "word" };
                default:
                    throw new ArgumentException("Keyword not recognised: " + key);
            }
        }

        private void addSuperlatives(string key) {
            if (string.IsNullOrEmpty(key)) {
                return;
            }
            if (superlatives == null) {
                superlatives = setupStandardSuperlatives();
            }
            switch (key) {
                case "time": 
                case "days":
                case "months":
                case "years":
                case "ordinals": superlatives.AddRange(new List<string> { "earliest", "latest", "first", "last" }); return;
                case "numeric": superlatives.AddRange(new List<string> { "lowest", "highest" }); return;
            }
        }

        private List<string> getPrepositionsForKey(string key) {
            switch (key) {
                case "dates":
                case "time":
                case "days":
                case "months":
                case "ordinals":
                case "alphabet":
                case "years": return new List<string> { "earlier", "later" };
                case "currency": return new List<string> { "cheap", "expensive", "cheaper", "dearer", "economical", "costly" };
                case "numeric":
                case "left-right": return null;
                case "length":
                case "words": return new List<string> { "shorter", "longer" };
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
                case "currency": return new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "15", "20", "50", "100", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "hundred", "thousand", "million", "billion" };
                case "words":
                case "length":
                case "numeric":
                case "dates": 
                case "ordinals":
                case "alphabet":
                case "time": return new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };

                default:
                    throw new ArgumentException("Keyword not recognised: " + key);
            }
        }

        public List<string> getQuantifiers() {
            List<string> quants = new List<string>();
            foreach (List<string> list in quantifiers) {
                quants.AddRange(list);
            }
            return quants;
        }

        internal string defineItem(string word) {
            try {
                double result;
                if (double.TryParse(word, out result)) {
                    return word; //All numbers should be kept.
                }
                if (word == "of") {
                    return OF;
                }
                if (word == "with") {
                    return WITH;
                }
                if (word == "and") {
                    return AND;
                }
                if (word == "either") {
                    return EITHER;
                }
                if (word == "or") {
                    return EITHER;
                }
                if (word == "but") {
                    return BUT;
                }
                if (word == "this") {
                    return THIS;
                }
                if (word == "than") {
                    return THAN;
                }
                for (int i = 0; i < disassociatives.Count; i++) {
                    if (word.ToLower() == disassociatives[i]) {
                        return NEGATIVE;
                    }
                }
                if (superlatives != null) {
                    for (int i = 0; i < superlatives.Count; i++) {
                        if (word.ToLower() == superlatives[i]) {
                            return i % 2 == 0 ? SUPER + "(-)" : SUPER + "(+)";
                        }
                    }
                }

                if (isFormerReferencer(word.ToLower())) {
                    return FORMER;
                }
                else if (isLatterReferencer(word.ToLower())) {
                    return LATTER;
                }
                for (int i = 0; i < numbers.Count; i++) {
                    if (word.ToLower() == numbers[i]) {
                        return "Tx(" + makeNumber(word) + ")";
                    }
                }
                for (int i = 0; i < quantifiers.Count; i++) {
                    if (quantifiers[i].Contains(word.ToLower())) {
                        return "Tq(" + Convert.ToChar('A' + i) + ")";
                    }
                }
                if (currency.HasValue) {
                    if (word[0] == currency.Value) {
                        return "Tx(" + word + ")";
                    }
                }
                for (int i = 0; i < prepositions.Count; i++) {
                    if (word.ToLower() == prepositions[i]) {
                        return "Tp(" + getDirection(i) + ")";
                    }
                }
                return string.Empty;
            }
            catch (Exception e) {
                throw e;
            }
        }

        private string makeNumber(string word) {
            word = word.ToLower();
            int result = 0;
            string digits = "";
            if (Int32.TryParse(word, out result)) {
                return result.ToString();
            }
            foreach (char c in word) {
                if (Int32.TryParse(c.ToString(), out result)) {
                    digits += c;
                }
                else {
                    break;
                }
            }
            if (!string.IsNullOrEmpty(digits)) {
                return digits;
            }
            List<string> units = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
            for (int i = 0; i <= 20; i++) {
                if (word == units[i]) {
                    return i.ToString();
                }
            }
            List<string> bigNums = new List<string>() { "hundred", "100", "thousand", "1000", "million", "1000000", "billion", "1000000000" };
            for (int i = 0; i < bigNums.Count; i++) {
                //only numbers as words can get here
                if (word == bigNums[i]) {
                    return bigNums[i+1];
                }
            }
            throw new ParserException("Can't numberify this yet: " + word);
        }

        private string getDirection(int index) {
            if (index % 2 == 0) {
                return "-";
            }
            return "+";
        }

        internal static bool isOf(string tag) {
            return tag == OF;
        }

        internal static bool isWith(string tag) {
            return tag == WITH;
        }

        internal static bool isAnd(string tag) {
            return tag == AND;
        }

        internal static bool isBut(string tag) {
            return tag == BUT;
        }

        internal static bool isSingleTermItem(string tag) {
            return tag == THIS || tag == BUT || tag.Contains(SUPER) || tag == EITHER;
        }

        internal static bool isNegative(string p) {
            return p == NEGATIVE;
        }

        private bool isFormerReferencer(string word) {
            return word == "former" || word == "first";
        }
        private bool isLatterReferencer(string word) {
            return word == "latter" || word == "second";
        }

    }
}
