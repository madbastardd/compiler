using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concrete.MultySymbolSeparatorsTableSpace;

namespace Test.Concrete.Tests {
    [TestClass()]
    public class MultySymbolSeparatorsTableTests {
        [TestMethod()]
        public void MultySymbolSeparatorsTableTest() {
            MultySymbolSeparatorsTable table = new MultySymbolSeparatorsTable();
            Assert.AreEqual(301, table.exists(">="));
            Assert.AreEqual(302, table.exists("<="));
            Assert.AreEqual(303, table.exists(":="));
            Assert.AreEqual(304, table.exists("=="));
        }
    }
}