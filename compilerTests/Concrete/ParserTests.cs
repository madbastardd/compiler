using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concrete.Parser;
using System;
using Concrete;
using System.Collections.Generic;

namespace Concrete.Parser.Tests {
    [TestClass()]
    public class ParserTests {
        [TestMethod()]
        public void RecognizeTest() {
            TableSpace.Table[] tables = new TableSpace.Table[] {
                new MultySymbolSeparatorsTableSpace.MultySymbolSeparatorsTable(),
                new KeyWordTableSpace.KeyWordsTable(),
                new ConstantsTableSpace.ConstantsTable(),
                new IdentifierTableSpace.IdentifierTables()
            };
            CollectionAssert.AreEqual(Parser.Recognize(Parser.Parse("         program one              1\ndima"), tables),
                new List<UInt16>() { 401, 1001, 501, 1002 });
        }
    }
}

namespace Test.Concrete.Tests {
    [TestClass()]
    public class ParserTests {
        [TestMethod()]
        public void ParseTest() {
            CollectionAssert.AreEqual(Parser.Parse("         program one              1\ndima"),
                new List<string>() { "program", " ", "one", " ", "1", "\n", "dima" });
        }
        [TestMethod]
        public void ParseTest2() {
            try {
                Parser.Parse("");
                Assert.Fail();
            } catch (Exception) {
                //ALL ok
            }
        }

        [TestMethod]
        public void ParseTest3() {
            try {
                Parser.Parse(null);
            }
            catch (Exception) {

            }
            
        }

        [TestMethod]
        public void ParseTest4() {
            try {
                List<string> parsedText = new List<string>();
                using (System.IO.StreamReader sr = new System.IO.StreamReader("test.txt")) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        parsedText.AddRange(Parser.Parse(line));
                    }
                }

                CollectionAssert.AreEqual(new List<string>() { "test", "test", "2", "inc", "best", " ", "dima" }, parsedText);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}