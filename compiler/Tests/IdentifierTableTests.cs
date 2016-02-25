using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concrete.IdentifierTableSpace;
using Concrete.TableSpace;
using System;

namespace Test.IdentifierTableSpace.Tests {
    [TestClass()]
    public class IdentifierTableTests {
        [TestMethod()]
        public void IdentifierTableInsert() {
            Table table = new IdentifierTables();
            table.Insert("dima");
            table.Insert("dima2");
            Assert.AreEqual(true, table.ContainsValue("dima"));
            Assert.AreEqual(false, table.ContainsValue("dima3"));
        }
    }
}