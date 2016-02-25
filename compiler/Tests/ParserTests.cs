using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concrete.Parser;
using System;
using Concrete;
using System.Collections.Generic;

namespace Concrete.Parser.Tests {
    [TestClass()]
    public class ParserTests {
        TableSpace.Table[] tables = new TableSpace.Table[] {
                new MultySymbolSeparatorsTableSpace.MultySymbolSeparatorsTable(),
                new KeyWordTableSpace.KeyWordsTable(),
                new ConstantsTableSpace.ConstantsTable(),
                new IdentifierTableSpace.IdentifierTables()
            };

        [TestMethod()]
        public void ParseTest1() {
            CollectionAssert.AreEqual(Parser.Parse("         PROGRAM PROGRAM one              -120#30\ndima<=s", tables),
                new List<int>() { "PROGRAM".GetHashCode(), " ".GetHashCode(), "PROGRAM".GetHashCode(),
                    " ".GetHashCode(), "one".GetHashCode(), " ".GetHashCode(),
                    "-".GetHashCode(), "120".GetHashCode(), "#".GetHashCode(), "30".GetHashCode(), " ".GetHashCode(),
                    "dima".GetHashCode(), "<=".GetHashCode(), "s".GetHashCode()});
        }

        [TestMethod]
        public void ParseTest2() {
            try {
                Parser.Parse("", tables);
            } 
            catch (Exception) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ParseTest3() {
            try {
                Parser.Parse(null, tables);   
            }
            catch (Exception) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ParseTest4() {
            CollectionAssert.AreEqual(Parser.Parse("PROGRAM", tables), 
                new List<int>() { "PROGRAM".GetHashCode() });
        }

        [TestMethod]
        public void ParseTest5() {
            CollectionAssert.AreEqual(Parser.Parse("*", tables),
                new List<int>() { "*".GetHashCode() });
        }

        [TestMethod]
        public void ParseTest6() {
            CollectionAssert.AreEqual(Parser.Parse("*<", tables),
                new List<int>() { "<ERR>".GetHashCode() });
        }
        [TestMethod]
        public void ParseTest7() {
            CollectionAssert.AreEqual(Parser.Parse("*<*", tables),
                new List<int>() { "<ERR>".GetHashCode() });
        }

        [TestMethod]
        public void ParseTest8() {
            CollectionAssert.AreEqual(Parser.Parse("*<>*", tables),
                new List<int>());
        }

        [TestMethod]
        public void ParseTest9() {
            CollectionAssert.AreEqual(Parser.Parse("*<text here will be ******>>>>> ignored>*", tables),
                new List<int>());
        }

        [TestMethod]
        public void ParseTest10() {
            List<int> result = Parser.ParseFile("test.txt", tables);
        }
    }
}