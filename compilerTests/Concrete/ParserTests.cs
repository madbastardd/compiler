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
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void ParseTest2() {
            Parser.Parse("");
            CollectionAssert.AreEqual(Parser.getParsedString(), new List<string>() { "" });
        }

        [TestMethod]
        public void ParseTest3() {
            try {
                Parser.Parse(null);
            }
            catch (Exception) {

            }
            CollectionAssert.AreEqual(Parser.getParsedString(), null);
        }

        [TestMethod]
        public void ParseTest4() {
            try {
                List<string> parsedText = new List<string>();
                using (System.IO.StreamReader sr = new System.IO.StreamReader("test.txt")) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        Parser.Parse(line);
                        parsedText.AddRange(Parser.getParsedString());
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