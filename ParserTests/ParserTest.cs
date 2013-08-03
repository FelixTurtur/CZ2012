using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Representation;
using System.Collections.Generic;
using System.Xml;
using CZParser;

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
            Parser parser = new Parser(p);
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void Check_Dictionary_Creation() {
            Puzzle p = puzzles[0];
            Parser parser = new Parser(p);
            List<string> puzzle1Items = new List<string> { "brendan", "briese", "gareth", "gale", "zachary", "zeffer", "baseball", "cap", "bowler", "hat", "flat", "cap", "monday", "wednesday", "friday", "dual", "carriageway", "river", "tree" };
            for (int i = 0; i < puzzle1Items.Count; i++) {
                Assert.AreEqual(puzzle1Items[i], parser.tagger.catWords.getItems()[i]);
            }
            List<string> puz1Quants = new List<string> { "day", "days", "night", "nights" };
            for (int i = 0; i < puz1Quants.Count; i++) {
                Assert.AreEqual(puz1Quants[i], parser.tagger.terms.getQuantifiers()[i]);
            }
        }

        [TestMethod]
        public void Check_Dictionary_Tagging() {
            Puzzle p = puzzles[14];
            Parser parser = new Parser(p);
            List<string> firstTagPattern = new List<string>();
            string auxCat1 = "";
            string auxCat2 = "";
            var words = Tagger.separateWordsAndPunctuation("Oliver Newton does not play the part of either a corner shop owner or a graphic designer working from a studio in their attic.");
            foreach (string word in words) {
                    if (word.Length == 1 && char.IsPunctuation(word[0])) {
                        firstTagPattern.Add(word);
                        continue;
                    }
                    auxCat1 = parser.tagger.catWords.findItemMatches(word);
                    auxCat2 = parser.tagger.terms.defineItem(word);
                    firstTagPattern.Add(string.IsNullOrEmpty(auxCat1) ? auxCat2 : string.IsNullOrEmpty(auxCat2) ? auxCat1 : auxCat1 + "," + auxCat2);
            }
            List<string> testTags = new List<string> {"A5", "B4", "Td", "To", "Te", "D4", "D4", "D2", "." };
            List<string> producedTags = justTags(firstTagPattern);
            for (int i = 0; i < testTags.Count; i++) {
                Assert.AreEqual(testTags[i], producedTags[i]);
            }
        }

        private List<string> justTags(List<string> firstTagPattern) {
            List<string> result = new List<string>();
            foreach (string item in firstTagPattern) {
                if (!string.IsNullOrEmpty(item)) {
                    result.Add(item);
                }
            }
            return result;
        }

        [TestMethod]
        public void Check_First_Tagging() {
            Puzzle p = puzzles[1];
            Parser parser = new Parser(p);
            List<string> taggedClues = parser.tagger.tagClues(p.getClues());
            Assert.IsNotNull(taggedClues);
            List<string> correctTags = new List<string>() {"C2 D1", "A1 B1 ; A3 C3", "B3 Td D3", "B2 D2" };
            Assert.AreEqual(correctTags.Count, taggedClues.Count);
            foreach (string clue in taggedClues) {
                Assert.IsTrue(correctTags.Contains(clue));
            }
        }

        [TestMethod]
        public void Check_First_Translating() {
            Puzzle p = puzzles[1];
            Parser parser = new Parser(p);
            List<string> relations = parser.Read();
            List<string> manualRelations = new List<string> {"C2=D1", "A1=B1", "A3=C3", "B3!=D3", "B2=D2" };
            foreach (string rule in manualRelations) {
                Assert.IsTrue(relations.Contains(rule));
            }
            foreach (string rule in relations) {
                Assert.IsTrue(manualRelations.Contains(rule));
            }
        }

        [TestMethod]
        public void Check_Second_Tagging() {
            Puzzle p = puzzles[0];
            Parser parser = new Parser(p);
            List<string> taggedClues = parser.tagger.tagClues(p.getClues());
            Assert.IsNotNull(taggedClues);
            List<string> correctTags = new List<string>() { "A3 D1 ; Tt Tx(2) Tq(C) Tp(-) A2 B3", "B2 D3", "Td C2 D2" };
            Assert.AreEqual(correctTags.Count, taggedClues.Count);
            foreach (string clue in taggedClues) {
                Assert.IsTrue(correctTags.Contains(clue));
            }
        }

        [TestMethod]
        public void Check_Second_Translating() {
            Puzzle p = puzzles[0];
            Parser parser = new Parser(p);
            List<string> relations = parser.Read();
            List<string> manualRelations = new List<string> { "A3=D1", "A2(C)-A3(C)=2", "A2=B3", "B2=D3", "C2!=D2" };
            foreach (string rule in manualRelations) {
                Assert.IsTrue(relations.Contains(rule));
            }
            foreach (string rule in relations) {
                Assert.IsTrue(manualRelations.Contains(rule));
            }
        }

        [TestMethod]
        public void Check_Third_Tagging() {
            Puzzle p = puzzles[3];
            Parser parser = new Parser(p);
            List<string> taggedClues = parser.tagger.tagClues(p.getClues());
            Assert.IsNotNull(taggedClues);
            List<string> correctTags = new List<string>() { "C1 D3 ; Td B2 Td C2 Td A4", "B1 D1", "B4 A3 Td D4", "C4 A2 Td D2", "B3 A Tx(1) Tp(+) C C3" };
            Assert.AreEqual(correctTags.Count, taggedClues.Count);
            foreach (string clue in taggedClues) {
                Assert.IsTrue(correctTags.Contains(clue));
            }
        }

        [TestMethod]
        public void Check_Third_Translating() {
            Puzzle p = puzzles[3];
            Parser parser = new Parser(p);
            List<string> relations = parser.Read();
            List<string> manualRelations = new List<string> {"C1=D3", "C1!=B2", "B2!=C2", "C2!=A4", "B1=D1", "B4=A3", "A3!=D4", "C4=A2", "A2!=D2", "B3(A)-C3(A)=1" };
            foreach (string rule in manualRelations) {
                Assert.IsTrue(relations.Contains(rule));
            }
            foreach (string rule in relations) {
                Assert.IsTrue(manualRelations.Contains(rule));
            }
        }
    }
}
