using System;
using System.Collections.Generic;
using System.Xml;

namespace Representation {
    public class Puzzle {
        public int width { get; private set; }
        public int height { get; private set; }
        public string name { get; private set; }
        public List<List<string>> ProvidedSolution { get; private set; }
        public List<List<string>> Solution { get; private set; }

        private int id;
        private string preamble;
        private List<string> clues;
        private List<string> categories;
        private List<string> items;
        private string ordering;
        private string semanticTag;
        private string otherTag;

        public List<string> getClues() {
            return clues;
        }

        public Puzzle() { }
        public Puzzle(XmlNode n) {
            parseXMLToPuzzle(n);
        }

        public void parseXMLToPuzzle(XmlNode input) {
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
            this.ProvidedSolution = transformRawSolution(input["box"]["solution"].InnerText);
        }

        private List<List<string>> transformRawSolution(string p) {
            string[] r = p.Split(new string[] { "{", "},{", "}" }, StringSplitOptions.RemoveEmptyEntries);
            List<List<string>> result = new List<List<string>>();
            foreach (string line in r) {
                List<string> row = new List<string>(line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                result.Add(row);
            }
            return result;
        }

        public int getId() {
            return id;
        }

        public void setRules(List<string> rules) {
            throw new NotImplementedException();
        }

        public string getNameAt(int cat, int index) {
            //items[0] => y(0), p(1), items[1] => y(0), p(1)
            if (index == 0) {
                //answer not found.
                return "Unknown";
            }
            return items[(cat * width) + (index - 1)];
        }
    }
}
