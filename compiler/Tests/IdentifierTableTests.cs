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
            Assert.AreEqual(true, table.IsInTable("dima"));
            Assert.AreEqual(1002, table.GetKey("dima2"));
        }
        [TestMethod]
        public void IdentifierTableInsert2() {
            try {
                Table table = new IdentifierTables();
                table.Insert("dima");
                Assert.Fail("table");
            }
            catch (Exception) {
                //All OK
            }
        }
    }
}