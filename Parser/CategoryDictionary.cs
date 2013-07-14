using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    internal class CategoryDictionary {
        internal List<string> codes;
        internal List<string> words;

        public CategoryDictionary(List<string> categories, List<string> items) {
            int catCount = categories.Count;
            int catHeight = items.Count / categories.Count;
            char catIdentifier = 'A';
            for (int i = 0; i < catCount; i++, catIdentifier++) {
                for (int j = 0; j < catHeight; j++) {
                    if (items[(i * catHeight) + j].Trim().Contains(" ")) {
                        //multiple words
                    }
                    else {
                        codes.Add(catIdentifier.ToString() + (j + 1));
                        words.Add(items[(i * catHeight) + j]);
                    }
                }
            }
        }
    }
}
