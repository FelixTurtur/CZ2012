using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleverZebra.Logix;

namespace LogixTests
{   
    [TestClass]
    public class LineTest
    {
        private Category cat;
        private RelationFactory relationBuilder;

        [TestMethod]
        public void Create_And_Test_Line() {
            var cat1 = new Category('B', 5);
            Assert.AreEqual('B', cat1.identifier);
            object[] list = { 5, 10, 15, 20, 25 };
            cat1.enterValues(list);
            object result = cat1.retrieveValue("B3");
            Assert.AreEqual(15, result);
        }

        [TestInitialize]
        public void Initialize() {
            this.relationBuilder = RelationFactory.getInstance();
        }

        [TestMethod]
        public void Test_AllButOneFound() {
            cat = new Category('A', 5);
            cat.addRelation("A1", "B1");
            cat.addRelation("A2", "B2");
            cat.addRelation("A3", "B3");
            cat.addRelation("A4", "B4");
            string AforB5 = cat.checkForMatch("B5");
            Assert.AreEqual("A5", AforB5);
        }

        [TestMethod]
        public void Test_InconclusiveResult() {
            cat = new Category('A', 5);
            cat.addRelation("A1", "B1");
            string AforB = cat.checkForMatch("B5");
            Assert.IsNull(AforB);
        }

        [TestMethod]
        public void Consider_Positive_Rule() {
            cat = new Category('A', 5);
            Relation r = relationBuilder.createRelation("A1=B3");
            //Deducer brain = new Deducer(size);

        }
        
        [TestMethod]
        public void Consider_Relative_Rule() {
            cat = new Category('A', 5);
            cat.enterValues(new object[] { 5, 10, 15, 20, 25 });
            cat.addRelation("A3", "B1");
            Relation r = relationBuilder.createRelation("A1(B)-A3(B)=5");
            //bool used = cat.considerRelation(r);
            //Assert.IsFalse(used);
        }
    }
}
