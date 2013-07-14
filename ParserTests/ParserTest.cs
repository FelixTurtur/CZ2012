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
            Puzzle p = puzzles[0];
            CZParser parser = new CZParser(p);
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void First_Puzzle_Test() {
            Puzzle p = puzzles[0];
            CZParser parser = new CZParser(p);
            List<string> result = parser.Read();
            Assert.IsNotNull(result);
        }

    }
}
