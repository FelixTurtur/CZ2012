using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleverZebra.Logix;
using System.Collections.Generic;

namespace LogixTests
{
    [TestClass]
    public class DeducerTest
    {
        private RelationFactory relationBuilder;

        [TestInitialize]
        public void Initialise() {
            relationBuilder = RelationFactory.getInstance();
        }

        [TestMethod]
        public void Create_Deducer() {
            Deducer brains = new Deducer(4,5);
            List<Line> lines = brains.getLineCollection();
            Assert.AreEqual(4, lines.Count);
        }

        [TestMethod]
        public void Retrieve_Line_From_Identifier() {
            Deducer brains = new Deducer(4, 5);
            Line l = brains.getLineFromIdentifier('C');
            Assert.IsNotNull(l);
            brains.considerRelationToLine(relationBuilder.createRelation("C1=B3"), l);
            string consideredItem = l.checkForMatch("B3");
            Assert.AreEqual("C1", consideredItem);
        }
            
    }
}
