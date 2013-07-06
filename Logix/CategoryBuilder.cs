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

        internal void newKitty() {
            cat = new Category();
        }

        internal void setSize(int s) {
            cat.size = s;
        }

        internal void quickSet(char c, int s) {
            newKitty();
            cat.identifier = c;
            cat.size = s;
        }

        internal void setIdentifier(char c) {
            cat.identifier = c;
        }

        internal void setKeyword(string key) {
            cat.setKeyword(key);
        }

        internal void enterCategoryValues(object[] list) {
            cat.enterValues(list);
        }

        internal Category build() {
            cat.createInnerArray();
            return cat;
        }
    }
}
