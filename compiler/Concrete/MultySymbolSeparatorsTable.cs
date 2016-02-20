using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace lexical_analyzer_c_sharp.Concrete {
    public class MultySymbolSeparatorsTable : Table {
        public MultySymbolSeparatorsTable(ushort _index = 301) : base(_index) {
            this.insert(">=");
            this.insert("<=");
            this.insert(":=");
            this.insert("==");
        }
    }
}
