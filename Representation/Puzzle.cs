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
        private List<string> rules;
        private List<string> categories;
        private List<string> items;
        private List<string> keywords;
        private string semanticTag;
        private string otherTag;

        public List<string> getClues() {
            return clues;
        }

        public List<string> getRules() {
            return this.rules;
        }

        public Puzzle() { }
        public Puzzle(XmlNode n) {
            parseXMLToPuzzle(n);
            width = keywords.Count;
            height = ProvidedSolution.Count;
        }

        public void parseXMLToPuzzle(XmlNode input) {
            id = Convert.ToInt32(input.Attributes[0].Value.Substring(1));
            string size = input.Attributes["size"].Value;
            semanticTag = input.Attributes["semantic"].Value;
            otherTag = input.Attributes["other"] == null ? "" : input.Attributes["other"].Value;
            name = input["title"].InnerText.Trim();
            preamble = input["text"]["preamble"].InnerText;
            XmlNodeList hints = input["text"]["hints"].GetElementsByTagName("clue");
            clues = new List<string>();
            foreach (XmlNode n in hints) {
                if (!string.IsNullOrEmpty(n.InnerText)) {
                    clues.Add(n.InnerText);
                }
            }
            XmlNodeList cats = input["box"].GetElementsByTagName("category");
            categories = new List<string>();
            items = new List<string>();
            keywords = new List<string>();
            foreach (XmlNode n in cats) {
                categories.Add(n.Attributes["name"].Value);
                var keyNode = n.Attributes["keyword"];
                keywords.Add(keyNode == null ? "" : keyNode.Value);
                foreach (XmlNode i in n.ChildNodes) {
                    items.Add(i.InnerText.Trim());
                }
            }
            this.ProvidedSolution = transformRawSolution(input["box"]["solution"]);
        }

        private List<List<string>> transformRawSolution(XmlNode p) {
            List<List<string>> result = new List<List<string>>();
            foreach (XmlNode r in p.ChildNodes) {
                List<string> row = new List<string>(r.InnerText.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                result.Add(row);
            }
            return result;
        }

        public int getId() {
            return id;
        }

        public List<string> getCategories() {
            return categories;
        }

        public List<string> getItems() {
            return items;
        }
        
        public void setRules(List<string> rules) {
            this.rules = rules;
        }

        public string getNameAt(int cat, int index) {
            //items[0] => y(0), p(1), items[1] => y(0), p(1)
            if (index == 0) {
                //answer not found.
                return "Unknown";
            }
            return items[(cat * height) + (index - 1)];
        }

        public string[] getKeywords() {
            return keywords.ToArray();
        }

        public string getPreamble() {
            return preamble;
        }
    }
}
