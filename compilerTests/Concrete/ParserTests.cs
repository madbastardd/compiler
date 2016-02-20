using Microsoft.VisualStudio.TestTools.UnitTesting;
using lexical_analyzer_c_sharp.Concrete;
using System;
using System.Collections.Generic;

namespace Test.Concrete.Tests {
    [TestClass()]
    public class ParserTests {
        [TestMethod()]
        public void ParseTest() {
            try {
                Parser.Parse("         program one              1\ndima");
                CollectionAssert.AreEqual(Parser.getParsedString(), new List<string>() { "program", " ", "one", " ", "1", "\n", "dima" });
                Parser.Parse("");
                CollectionAssert.AreEqual(Parser.getParsedString(), new List<string>() { "" });
                try {
                    Parser.Parse(null);
                }
                catch (Exception) {

                }
                CollectionAssert.AreEqual(Parser.getParsedString(), null);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}