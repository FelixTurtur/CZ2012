using System;
using System.Collections.Generic;
using System.Xml;

namespace Representation {
    public class Puzzle {
        public int width { get; private set; }
        public int height { get; private set; }
        public string name { get; private set; }
        private int id;
        private string preamble;
        private List<string> clues;
        private List<string> categories;
        private List<string> items;
        private Solution providedSolution;
        private string ordering;
        private string semanticTag;
        private string otherTag;

        private List<string> rules;
        public Solution solution { get; private set; }

        public List<string> getClues() {
            return clues;
        }

        public List<string> getRules() {
            return rules;
        }

        public Puzzle() { }
        public Puzzle(XmlNode n) {
            initialise(n);
        }

        public void initialise(XmlNode input) {
            id = Convert.ToInt32(input.Attributes[0].Value);
            string size = input.Attributes["size"].Value;
            ordering = input.Attributes["ordering"].Value;
            semanticTag = input.Attributes["semantic"].Value;
            otherTag = input.Attributes["other"] == null ? "" : input.Attributes["other"].Value;
            name = input["title"].Value;
            preamble = input["text"]["preamble"].Value;
            XmlNodeList hints = input["text"]["hints"].GetElementsByTagName("clue");
            clues = new List<string>();
            foreach (XmlNode n in hints) {
                clues.Add(n.Value);
            }
            XmlNodeList cats = input["box"].GetElementsByTagName("category");
            categories = new List<string>();
            items = new List<string>();
            foreach (XmlNode n in cats) {
                categories.Add(n.Attributes["name"].Value);
                foreach (XmlNode i in n.ChildNodes) {
                    items.Add(i.Value);
                }
            }
            providedSolution = new Solution(input["box"]["solution"].Value);
        }

        public int getId() {
            return id;
        }

        public void setRules(List<string> rules) {
            throw new NotImplementedException();
        }
    }
}
