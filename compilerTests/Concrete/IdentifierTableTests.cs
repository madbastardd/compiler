using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concrete.IdentifierTableSpace;
using Concrete.TableSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.IdentifierTableSpace.Tests {
    [TestClass()]
    public class IdentifierTableTests {
        [TestMethod()]
        public void IdentifierTableInsert() {
            Table table = new IdentifierTable();
            table.insert("dima");
            try {
                table.insert("dima");
                Assert.Fail("table");
            }
            catch (Exception) {
                //All OK
            }
            table.insert("dima2");
            Assert.AreEqual(true, table.isInTable("dima"));
            Assert.AreEqual(0, table.exists("dima"));
        }
    }
}