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
            CollectionAssert.AreEqual(Parser.Parse("         PROGRAM one              120#30\ndima", tables),
                new List<UInt16>() { (tables[1] as KeyWordTableSpace.KeyWordsTable).GetKey("PROGRAM"),
                    32, 1001, 32, 501, '#', 502, 32, 1002 });
        }

        [TestMethod]
        public void ParseTest2() {
            CollectionAssert.AreEqual(Parser.Parse("", tables), new List<UInt16>());
        }

        [TestMethod]
        public void ParseTest3() {
            CollectionAssert.AreEqual(Parser.Parse(null, tables), new List<UInt16>());
        }

        [TestMethod]
        public void ParseTest4() {
            CollectionAssert.AreEqual(Parser.Parse("PROGRAM", tables), 
                new List<UInt16>() { (tables[1] as KeyWordTableSpace.KeyWordsTable).GetKey("PROGRAM") });
        }
    }
}