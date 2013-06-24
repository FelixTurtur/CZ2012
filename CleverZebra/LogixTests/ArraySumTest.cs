using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleverZebra.Logix;

namespace LogixTests
{   
    [TestClass]
    public class LineTest
    {
        [TestMethod]
        public void Create_And_Test_Array()
        {
            var line1 = new Line('B', 5);
            Assert.AreEqual('B', line1.identifier);
            object[] list = { 5, 10, 15, 20, 25 };
            line1.enterValues(list);
            object result = line1.retrieveValue("B3");
            Assert.AreEqual(15, result);
        }
    }
}
