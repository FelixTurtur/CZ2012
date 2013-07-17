using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Representation;
using System.Collections.Generic;
using System.Xml;
using Parser;

namespace ParserTests
{
    [TestClass]
    public class ParserTest
    {
        private List<Puzzle> puzzles;

        [TestInitialize]
        public void loadPuzzles() {
            puzzles = new List<Puzzle>();
            XmlDocument sourceDoc = new XmlDocument();
            sourceDoc.LoadXml(ParserTests.Properties.Resources.puzzles_sample);
            sourceDoc.Normalize();
            XmlNodeList xmlPuzzles = sourceDoc.GetElementsByTagName("puzzle");
            foreach (XmlNode n in xmlPuzzles) {
                if (string.IsNullOrEmpty(n["title"].InnerText)) {
                    continue;
                }
                puzzles.Add(new Puzzle(n));
            }
        }

        [TestMethod]
        public void Initialise_Parser() {
            Puzzle p = puzzles[1];
            CZParser parser = new CZParser(p);
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void First_Parser_Test() {
            Puzzle p = puzzles[1];
            CZParser parser = new CZParser(p);
            List<string> result = parser.Read();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_Dictionary_Creation() {
            Puzzle p = puzzles[0];
            CZParser parser = new CZParser(p);
            parser.Read();
            List<string> puzzle1Items = new List<string> { "Brendan", "Briese", "Gareth", "Gale", "Zachary", "Zeffer", "Baseball", "Cap", "Bowler", "Hat", "Flat", "Cap", "Monday", "Wednesday", "Friday", "Dual", "Carriageway", "River", "Treetops" };
            for (int i = 0; i < puzzle1Items.Count; i++) {
                Assert.AreEqual(parser.tagger.catWords.getItems()[i], puzzle1Items[i]);
            }
            List<string> puz1Quants = new List<string> { "of", "day", "days", "night", "nights" };
            for (int i = 0; i < puz1Quants.Count; i++) {
                Assert.AreEqual(parser.tagger.puzzleWords.getQuantifiers()[i], puz1Quants[i]);
            }
        }

        [TestMethod]
        public void Check_Tagging() {
            Puzzle p = puzzles[1];
            CZParser parser = new CZParser(p);
            parser.tagger.catWords = new CategoryDictionary(p.getCategories(), p.getItems());
            parser.tagger.puzzleWords = new TermsDictionary(p.getKeywords());
            List<string> taggedClues = parser.tagger.tagClues(p.getClues());
            Assert.IsNotNull(taggedClues);
            List<string> correctTags = new List<string>() {"C2 D1 D1", "A1 B1 ; A3 C3", "B3 D3", "B2 D2" };
            Assert.AreEqual(taggedClues.Count, correctTags.Count);
            foreach (string clue in taggedClues) {
                Assert.IsTrue(correctTags.Contains(clue));
            }
        }
    }
}
