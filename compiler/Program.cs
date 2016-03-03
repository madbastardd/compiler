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

namespace compiler {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter text file name: ");
            string textFile = Console.ReadLine();

            Stopwatch sw = new Stopwatch();

            sw.Start();
			try {
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
				            
                using (StreamWriter sr = new StreamWriter(result_textFile)) {
                    foreach (var item in list) {
                        sr.Write(item + " ");
                    }
                }
				tables[0].SaveToFile("ms.dat", true);
				tables[1].SaveToFile("kw.dat", true);
				tables[2].SaveToFile("ct.dat", true);
				tables[3].SaveToFile("id.dat", true);

				Console.WriteLine("Parsed for " + sw.ElapsedMilliseconds + " milliseconds");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } 
        }
    }
}
