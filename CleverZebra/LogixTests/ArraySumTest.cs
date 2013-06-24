using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleverZebra.Logix;

namespace LogixTests
{
    [TestClass]
    public class ArraySumTest
    {
        [TestMethod]
        public void Create_And_Test_Array()
        {
            var array1 = new Line('B', 5);
            Assert.AreEqual('B', array1.identifier);
        }
    }
}
