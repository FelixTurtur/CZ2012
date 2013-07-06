using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logix;
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
            List<Category> cats = brains.getCategoryCollection();
            Assert.AreEqual(4, cats.Count);
        }

        [TestMethod]
        public void Retrieve_Line_From_Identifier() {
            Deducer brains = new Deducer(4, 5);
            Category l = brains.getCategoryFromIdentifier('C');
            Assert.IsNotNull(l);
            brains.considerRelationToCategory(relationBuilder.createRelation("C1=B3"), l);
            string consideredItem = l.checkForMatch("B3");
            Assert.AreEqual("C1", consideredItem);
        }
            
    }
}
