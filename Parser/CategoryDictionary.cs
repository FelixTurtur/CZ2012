using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
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
                                words.Add(itemWord);
                            }
                        }
                    }
                    else {
                        codes.Add(catIdentifier.ToString() + (j + 1));
                        words.Add(items[(i * catHeight) + j]);
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
                if (words[i] == word || words[i] + "'s" == word) {
                    if (result.Length > 0) {
                        result += ",";
                    }
                    result += codes[i];
                }
            }
            for (int i = 0; i < categoryTitles.Count; i++) {
                if (categoryTitles[i] == word) {
                    if (result.Length > 0) {
                        result += ",";
                    }
                    result += ('A' + i).ToString();
                }
            }
            return result;
        }

    }
}
