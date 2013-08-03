using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Representation;

namespace CZParser
{
    class Translator
    {
        private static ParsingBuffer buffer = new ParsingBuffer(5);
        private List<char> keyCategories;

        public Translator(string[] keys) {
            keyCategories = new List<char>();
            char cat = 'A';
            for (int i = 0; i < keys.Length; i++, cat++) {
                if (!string.IsNullOrEmpty(keys[i])) {
                    keyCategories.Add(cat);
                }
            }
        }

        internal List<string> makeRelations(string line) {
            List<string> relations = new List<string>();
            if (containsMultipleStatements(line)) {
                relations.AddRange(getRelationsFromMultiLine(line));
            }
            else {
                relations.AddRange(getRelationsFromLine(line));
            }
            return relations;
        }

        private static bool containsMultipleStatements(string line) {
            if (line.Contains(".") || line.Contains(";"))
                return true;
            return false;
        }

        private List<string> getRelationsFromMultiLine(string line) {
            List<string> relations = new List<string>();
            string[] lines = line.Split(new string[] { ". ", "; " }, StringSplitOptions.RemoveEmptyEntries);
            if (line.Contains("; Tt Tx") || line.Contains(". Tt Tx") || line.Contains("; Tt Tp") || line.Contains(". Tt Tp")) {
                //two separate relations and also a comparative between one item from each to be created. Left then right.
                string termBlock = getTermBlock(lines[1]);
                string postBlock = getPostTermBlockPattern(lines[1]);

                List<string> leftRelation = getRelationsFromLine(lines[0]);
                relations.AddRange(leftRelation);
                string leftItem = getFirstItem(leftRelation[0]);

                List<string> rightRelation = getRelationsFromLine(postBlock);
                relations.AddRange(rightRelation);
                string rightItem = getFirstItem(postBlock);

                relations.Add(formRelation(leftItem, rightItem, termBlock));
                if (lines.Count() > 2) {
                    relations.AddRange(getRelationsFromMultiLine((line.Replace(lines[0], "")).Replace(lines[1], "")));
                }
            }
            else if (line.Contains("; Tx") || line.Contains(". Tx") || line.Contains("; Tp") || line.Contains(". Tp")) {
                //two separate relations and also a comparative between one item from each to be created. Right then left.
                string termBlock = getTermBlock(lines[1]);
                string postBlock = getPostTermBlockPattern(lines[1]);

                List<string> leftRelation = getRelationsFromLine(lines[0]);
                relations.AddRange(leftRelation);
                string leftItem = getFirstItem(leftRelation[0]);

                List<string> rightRelation = getRelationsFromLine(postBlock);
                relations.AddRange(rightRelation);
                string rightItem = getFirstItem(postBlock);

                relations.Add(formRelation(rightItem, leftItem, termBlock));
                if (lines.Count() > 2) {
                    relations.AddRange(getRelationsFromMultiLine((line.Replace(lines[0], "")).Replace(lines[1].Trim(), "")));
                }
            }
            else if (line.Contains("; Td") || line.Contains(". Td")) {
                //an additional relation must be added to the first-mentioned item.
                List<string> leftRelation = getRelationsFromLine(lines[0]);
                relations.AddRange(leftRelation);
                string leftItem = getFirstItem(leftRelation[0]);
                relations.Add(leftItem + Representation.Relations.Negative + getFirstCat(lines[1]));
                if (line.Length > lines[0].Length + "; Td A1".Length)
                    relations.AddRange(getRelationsFromMultiLine(line.Substring(lines[0].Length + "; Td".Length)));
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
                    relations.AddRange(getRelationsFromLine(l));
                }
            }
            return relations;
        }

        private static string getTermBlock(string p) {
            string terms = "";
            while (p[0] == 'T') {
                terms += p.Substring(0, p.IndexOf(' ') + 1);
                p = p.Remove(0, p.IndexOf(' ') + 1);
            }
            return terms;
        }

        private static string getPostTermBlockPattern(string p) {
            while (p[0] == 'T') {
                p = p.Remove(0, p.IndexOf(' ') + 1);
            }
            return p.Trim();
        }

        private static string getFirstItem(string p) {
            string result = p[0].ToString();
            int i = 1;
            int aux = 0;
            while (Int32.TryParse(p[i].ToString(), out aux)) {
                result += p[i];
                i++;
            }
            return result;
        }

        private static string getFirstCat(string p) {
            while (p[0] == 'T') {
                p = p.Substring(p.IndexOf(' ') + 1);
            }
            return p.Substring(0, p.IndexOf(' '));
        }

        private List<string> getRelationsFromLine(string line) {
            line = line.Trim();
            if (isCatPair(line)) {
                return new List<string> { line.Substring(0, line.IndexOf(" ")) + Representation.Relations.Positive + line.Substring(line.IndexOf(" ") + 1) };
            }
            else if (isNegCatPair(line)) {
                return getNegativeFromPair(line);
            }
            else if (line.StartsWith("Td")) {
                List<string> result = new List<string>();
                try {
                    string firstTag = line.Substring(3, line.IndexOf(' ', 3) - 3);
                    string secondTag = line.Substring(line.IndexOf(firstTag) + firstTag.Length + 1);
                    secondTag = secondTag.Substring(0, secondTag.IndexOf(' '));
                    if (Tagger.isCatTag(secondTag)) {
                        if (line == "Td " + firstTag + " " + secondTag) {
                            result.Add(firstTag + Relations.Negative + secondTag);
                        }
                        else {
                            //still more to come
                            result.Add(firstTag + Relations.Negative + secondTag);
                            result.AddRange(getRelationsFromLine(line.Substring(line.IndexOf(secondTag) + secondTag.Length).Trim()));
                        }
                    }
                    else {
                        result = getRelationsFromLine(line.Substring(line.IndexOf(' ') + 1));
                    }
                    return result;
                }
                catch (IndexOutOfRangeException ex) {
                    return result;
                }
            }
            else if (!line.StartsWith("T") && !line.Contains("Tp")) {
                return formRelationsUsingBuffer(line);
            }
            else if (line.Contains("Tp")) {
                //first remove any category initials that are not comparative
                line = removeNonCompCats(line);
                return new List<string> { makeRelation(line) };
            }
            else {
                throw new ParserException("Unable to handle tag pattern: " + line);
            }
        }

        private string removeNonCompCats(string line) {
            foreach (string bit in line.Split(new char[] { ' ' })) {
                if (bit.Length == 1) {
                    if (!keyCategories.Contains(bit[0])) {
                        line = line.Remove(line.IndexOf(bit + " "), 2);
                    }
                }
            }
            return line;
        }

        private static List<string> formRelationsUsingBuffer(string line) {
            try {
                List<string> result = new List<string>();
                foreach (string bit in line.Split(new char[] { ' ' })) {
                    buffer.Add(bit);
                    if (PatternBank.holdsTagPattern(buffer)) {
                        result.Add(makeRelation(buffer.ToString()));
                        buffer.Clear();
                        buffer.Add(bit);
                    }
                }
                buffer.Clear();
                return result;
            }
            catch (Exception e) {
                throw e;
            }
        }

        private static string makeRelation(string p) {
            try {
                string[] bits = p.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch (PatternBank.getPatternNumber(p)) {
                    case 0: return bits[0] + Relations.Positive + bits[1];
                    case 1: return bits[0] + Relations.Negative + bits[2];
                    case 2:
                    case 3: return formRelation(bits[0], bits[4], bits[1] + " " + bits[2] + " " + bits[3]);
                    case 4: return twoTermRelative(bits[0], bits[1], bits[2], bits[3], bits[4]);
                    case 5: return twoTermRelative(bits[0], bits[3], bits[1], bits[2], bits[4]);
                    case 6: return oneTermRelative(bits[0], bits[1], bits[2], bits[3]);
                    case 7: return oneTermRelative(bits[0], bits[1], bits[2], bits[3]);
                    case 8: return oneTermRelative(bits[0], bits[2], bits[1], bits[3]);
                    default:
                        throw new ParserException("No logic to handle pattern number " + PatternBank.getPatternNumber(p));
                }
            }
            catch (Exception e) {
                throw e;
            }
        }

        private static string oneTermRelative(string left, string owned, string term, string right) {
            string direction = term.Contains("+") ? Relations.GreaterThan : Relations.LessThan;
            string relatedCat = Relations.makeRelatedCat(owned);
            if (Tagger.isCatTag(right)) {
                return left + relatedCat + direction + right + relatedCat;
            }
            return left + relatedCat + direction + right.Substring(3, right.Length - 4);
        }

        private static string twoTermRelative(string left, string owned, string amount, string comparator, string right) {
            string relatedCat = Relations.makeRelatedCat(owned);
            string difference = amount.Substring(3, amount.Length - 4);
            if (comparator.Contains("+")) {
                return left + relatedCat + Relations.Subtract + right + relatedCat + Relations.Positive + difference; 
            }
            else {
                return right + relatedCat + Relations.Subtract + left + relatedCat + Relations.Positive + difference;
            }
        }

        private static string formRelation(string leftItem, string rightItem, string termBlock) {
            string[] bits = termBlock.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string direction = "";
            string amount = "";
            string category = "";
            foreach (string bit in bits) {
                if (bit[1] == 'x') {
                    amount = bit.Substring(3, bit.Length - 4);
                }
                if (bit[1] == 'q') {
                    category = bit.Substring(3, 1);
                }
                if (bit[1] == 'p') {
                    direction = bit.Substring(3, 1);
                }
            }
            if (string.IsNullOrEmpty(amount)) {
                //unquantified relation
                direction = direction == "-" ? Relations.LessThan : Relations.GreaterThan;
                return leftItem + "(" + category + ")" + direction + rightItem + "(" + category + ")";
            }
            else {
                if (direction == "-") {
                    return rightItem + "(" + category + ")-" + leftItem + "(" + category + ")=" + amount;
                }
                else {
                    return leftItem + "(" + category + ")-" + rightItem + "(" + category + ")=" + amount;
                }
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
