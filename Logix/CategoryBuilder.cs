using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix
{
    internal class CategoryBuilder
    {
        private Category cat;

        internal CategoryBuilder() {}

        internal void newCat() {
            cat = new Category();
        }

        internal void setSize(int s) {
            cat.size = s;
        }

        internal void quickSet(char c, int s) {
            newCat();
            cat.identifier = c;
            cat.size = s;
        }

        internal void setIdentifier(char c) {
            cat.identifier = c;
        }

        internal void setKeyword(string key) {
            cat.setKeyword(key);
        }

        internal void setValues(object[] list) {
            cat.createInnerArray();
            cat.enterValues(list);
        }

        internal Category build() {
            if (!cat.isInnerArraySet()) {
                cat.createInnerArray();
            }
            return cat;
        }

    }
}
