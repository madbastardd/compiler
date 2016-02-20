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
            table.insert("dima");
            table.insert("dima2");
            Assert.AreEqual(true, table.isInTable("dima"));
            Assert.AreEqual(1002, table.exists("dima2"));
        }
        [TestMethod]
        public void IdentifierTableInsert2() {
            try {
                Table table = new IdentifierTables();
                table.insert("dima");
                Assert.Fail("table");
            }
            catch (Exception) {
                //All OK
            }
        }
    }
}