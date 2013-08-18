using System;
using System.Collections.Generic;

namespace CZParser
{
    public class CategoryDictionary
    {
        internal List<string> codes;
        internal List<string> words;
        private List<string> categoryTitles;
        private static List<string> neutrals = new List<string> {"of", "the" };

        public CategoryDictionary(List<string> categories, List<string> items) {
            categoryTitles = new List<string>(categories);
            codes = new List<string>();
            words = new List<string>();
            int catCount = categories.Count;
            int catHeight = items.Count / categories.Count;
            char catIdentifier = 'A';
            for (int i = 0; i < catCount; i++, catIdentifier++) {
                for (int j = 0; j < catHeight; j++) {
                    if (items[(i * catHeight) + j].Trim().Contains(" ")) {
                        //multiple words
                        var splitItem = items[(i * catHeight) + j].Split(' ');
                        foreach (string itemWord in splitItem) {
                            if (shouldBeConsidered(itemWord, categories)) {
                                codes.Add(catIdentifier.ToString() + (j + 1));
                                words.Add(itemWord.ToLower().Trim());
                            }
                        }
                    }
                    else {
                        codes.Add(catIdentifier.ToString() + (j + 1));
                        words.Add(items[(i * catHeight) + j].ToLower().Trim());
                    }
                }
            }
        }

        private bool shouldBeConsidered(string s, List<string> categories) {
            if (neutrals.Contains(s)) {
                return false;
            }
            return true;
        }

        public List<string> getItems() {
            return words;
        }

        internal string findItemMatches(string word) {
            string result = "";
            for (int i = 0; i < codes.Count; i++) {
                if (words[i] == word.ToLower() || words[i] + "'s" == word.ToLower() || words[i] == word + "s") {
                    if (result.Length > 0) {
                        result += ",";
                    }
                    result += codes[i];
                }
            }
            for (int i = 0; i < categoryTitles.Count; i++) {
                if (categoryTitles[i].ToLower() == word.ToLower()) {
                    if (result.Length > 0) {
                        result += ",";
                    }
                    result += (Convert.ToChar('A' + i)).ToString();
                }
            }
            return result;
        }

    }
}
