using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logix;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace LogixTests
{   
    [TestClass]
    public class CategoryTest
    {
        private CategoryBuilder catBuilder;
        private RelationFactory relationBuilder;

        [TestMethod]
        public void Create_And_Test_Line() {
            catBuilder.newCat();
            catBuilder.setIdentifier('B');
            catBuilder.setSize(5);
            catBuilder.setKeyword("Numeric");
            var cat1 = catBuilder.build();
            Assert.AreEqual('B', cat1.identifier);
            object[] list = { 5, 10, 15, 20, 25 };
            cat1.enterValues(list);
            object result = cat1.retrieveValue("B3");
            Assert.AreEqual("15", result.ToString());
        }

        [TestInitialize]
        public void Initialize() {
            this.relationBuilder = RelationFactory.getInstance();
            this.catBuilder = new CategoryBuilder();
        }

        [TestMethod]
        public void Test_AllButOneFound() {
            catBuilder.quickSet('A', 5);
            Category cat = catBuilder.build();
            cat.addRelation("A1", "B1");
            cat.addRelation("A2", "B2");
            cat.addRelation("A3", "B3");
            cat.addRelation("A4", "B4");
            string AforB5 = cat.checkForMatch("B5");
            Assert.AreEqual("A5", AforB5);
        }

        [TestMethod]
        public void Test_InconclusiveResult() {
            catBuilder.quickSet('A', 5);
            Category cat = catBuilder.build();
            cat.addRelation("A1", "B1");
            string AforB = cat.checkForMatch("B5");
            Assert.IsNull(AforB);
        }

        [TestMethod]
        public void Consider_Positive_Rule() {
            catBuilder.quickSet('A', 5);
            Category cat = catBuilder.build();
            Relation r = relationBuilder.createRelation("A1=B3");
            List<Relation> rules = cat.considerRelationToCategory(r, false);
            Assert.AreEqual("A1", cat.checkForMatch("B3", Category.Rows.Positives));
        }
        
        [TestMethod]
        public void Consider_Relative_Rule() {
            catBuilder.quickSet('B', 5);
            Category cat = catBuilder.build();
            cat.setKeyword("Numeric");
            cat.enterValues(new object[] { 5, 10, 15, 20, 25 });
            cat.addRelation("B1", "A3");
            Relation r = relationBuilder.createRelation("A1(B)-A3(B)=5");
            List<Relation> rules = cat.considerRelationToCategory(r, false);
            Assert.AreEqual("B2", cat.checkForMatch("A1", Category.Rows.Positives));
        }
    }
}
