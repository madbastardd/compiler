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
            CollectionAssert.AreEqual(Parser.Parse("         PROGRAM one              -120#30\ndima<=s", tables),
                new List<UInt16>() {(tables[1] as KeyWordTableSpace.KeyWordsTable).GetKey("PROGRAM"),
                    32, 1001, 32, '-', 501, '#', 502, 32, 1002, 301, 1003 });
        }

        [TestMethod]
        public void ParseTest2() {
            try {
                Parser.Parse("", tables);
                Assert.Fail();
            } 
            catch (Exception) {
                //all ok
            }
        }

        [TestMethod]
        public void ParseTest3() {
            try {
                Parser.Parse(null, tables);
                Assert.Fail();
            }
            catch (Exception) {
                //all ok
            }
        }

        [TestMethod]
        public void ParseTest4() {
            CollectionAssert.AreEqual(Parser.Parse("PROGRAM", tables), 
                new List<UInt16>() { (tables[1] as KeyWordTableSpace.KeyWordsTable).GetKey("PROGRAM") });
        }

        [TestMethod]
        public void ParseTest5() {
            CollectionAssert.AreEqual(Parser.Parse("*", tables),
                new List<UInt16>() { '*' });
        }

        [TestMethod]
        public void ParseTest6() {
            CollectionAssert.AreEqual(Parser.Parse("*<", tables),
                new List<UInt16>() { 0 });
        }
        [TestMethod]
        public void ParseTest7() {
            CollectionAssert.AreEqual(Parser.Parse("*<*", tables),
                new List<UInt16>() { 0 });
        }

        [TestMethod]
        public void ParseTest8() {
            CollectionAssert.AreEqual(Parser.Parse("*<>*", tables),
                new List<UInt16>());
        }

        [TestMethod]
        public void ParseTest9() {
            CollectionAssert.AreEqual(Parser.Parse("*<text here will be ******>>>>> ignored>*", tables),
                new List<UInt16>());
        }

        [TestMethod]
        public void ParseTest10() {
            List<ushort> result = Parser.ParseFile("test.txt", tables);
        }
    }
}