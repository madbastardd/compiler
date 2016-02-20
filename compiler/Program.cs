using System;
using Concrete.IdentifierTableSpace;
using Concrete.TableSpace;
using Concrete.AttributeClassSpace;

namespace lexical_analyzer_c_sharp {
    class Program {
        
        
        static void Main(string[] args) {
            AttributeClass attributes = new AttributeClass();

            //Console.WriteLine(attributes[(ushort)'A']);

            Table table = new IdentifierTable();

            table.insert("dimasik");
        }
    }
}
