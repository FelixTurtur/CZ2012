using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logix;
using System.Runtime.CompilerServices;

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
            var cat1 = catBuilder.build();
            Assert.AreEqual('B', cat1.identifier);
            object[] list = { 5, 10, 15, 20, 25 };
            cat1.enterValues(list);
            object result = cat1.retrieveValue("B3");
            Assert.AreEqual(15, result);
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
            //Deducer brain = new Deducer(size);

        }
        
        [TestMethod]
        public void Consider_Relative_Rule() {
            catBuilder.quickSet('A', 5);
            Category cat = catBuilder.build();
            cat.enterValues(new object[] { 5, 10, 15, 20, 25 });
            cat.addRelation("A3", "B1");
            Relation r = relationBuilder.createRelation("A1(B)-A3(B)=5");
            //bool used = cat.considerRelation(r);
            //Assert.IsFalse(used);
        }
    }
}
