using Microsoft.VisualStudio.TestTools.UnitTesting;
using lexical_analyzer_c_sharp.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Concrete.Tests {
    [TestClass()]
    public class MultySymbolSeparatorsTableTests {
        [TestMethod()]
        public void MultySymbolSeparatorsTableTest() {
            MultySymbolSeparatorsTable table = new MultySymbolSeparatorsTable();
            Assert.AreEqual(200, table.exists(">="));
            Assert.AreEqual(201, table.exists("<="));
            Assert.AreEqual(202, table.exists(":="));
            Assert.AreEqual(203, table.exists("=="));
        }
    }
}