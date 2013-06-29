using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleverZebra.Logix;

namespace LogixTests
{   
    [TestClass]
    public class LineTest
    {
        public Line line;
        public RelationFactory relationBuilder;

        [TestMethod]
        public void Create_And_Test_Line() {
            var line1 = new Line("B", 5);
            Assert.AreEqual("B", line1.identifier);
            object[] list = { 5, 10, 15, 20, 25 };
            line1.enterValues(list);
            object result = line1.retrieveValue("B3");
            Assert.AreEqual(15, result);
        }

        [TestInitialize]
        public void Initialize() {
            this.relationBuilder = RelationFactory.getInstance();
        }

        [TestMethod]
        public void Test_AllButOneFound() {
            line = new Line("A", 5);
            line.addRelation("A1", "B1");
            line.addRelation("A2", "B2");
            line.addRelation("A3", "B3");
            line.addRelation("A4", "B4");
            string AforB5 = line.checkForMatch("B5");
            Assert.AreEqual("A5", AforB5);
        }

        [TestMethod]
        public void Test_InconclusiveResult() {
            line = new Line("A", 5);
            line.addRelation("A1", "B1");
            string AforB = line.checkForMatch("B5");
            Assert.IsNull(AforB);
        }

        [TestMethod]
        public void Consider_Positive_Rule() {
            line = new Line("A", 5);
            Relation r = relationBuilder.createRelation("A1=B3");
            Assert.AreEqual(r.GetType().Name, "DirectRelation");
            bool used = line.considerRelation(r);
            Assert.IsTrue(used);
        }
        
        [TestMethod]
        public void Consider_Relative_Rule() {
            line = new Line("A", 5);
            line.enterValues(new object[] { 5, 10, 15, 20, 25 });
            line.addRelation("A3", "B1");
            Relation r = relationBuilder.createRelation("A1(B)-A3(B)=5");
            Assert.AreEqual(r.GetType().Name, "RelativeRelation");
            bool used = line.considerRelation(r);
            Assert.IsTrue(used);
        }
    }
}
