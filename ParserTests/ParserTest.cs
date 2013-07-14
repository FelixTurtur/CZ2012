using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Representation;
using System.Collections.Generic;
using System.Xml;

namespace ParserTests
{
    [TestClass]
    public class ParserTest
    {
        private List<Puzzle> puzzles;

        [TestMethod]
        public void Initialise_Parser() {
            Puzzle p = puzzles[0];
            Parser.Parser parser = new Parser.Parser(p);
            Assert.IsNotNull(parser);
        }

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

    }
}
