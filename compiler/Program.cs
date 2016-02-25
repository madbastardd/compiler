using System;
using Concrete.IdentifierTableSpace;
using Concrete.TableSpace;
using Concrete.AttributeClassSpace;
using System.Collections.Generic;
using Concrete.Parser;
using Concrete.MultySymbolSeparatorsTableSpace;
using Concrete.KeyWordTableSpace;
using Concrete.ConstantsTableSpace;
using System.IO;
using System.Diagnostics;

namespace lexical_analyzer_c_sharp {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter text file name: ");
            string textFile = Console.ReadLine();

            Stopwatch sw = new Stopwatch();

            sw.Start();
            Table[] tables = new Table[] {
                new MultySymbolSeparatorsTable(),
                new KeyWordsTable(),
                new ConstantsTable(),
                new IdentifierTables()
            };
            List<int> list = Parser.ParseFile(textFile, tables);
            sw.Stop();

            Console.WriteLine("Enter result text file name: ");
            string result_textFile = Console.ReadLine();

            try {
                using (StreamWriter sr = new StreamWriter(result_textFile)) {
                    foreach (var item in list) {
                        sr.WriteLine(item);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Parsed for " + sw.ElapsedMilliseconds + " milliseconds");
        }
    }
}
