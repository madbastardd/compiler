using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace lexical_analyzer_c_sharp.Concrete {
    class IdentifierTable : Table {
        IdentifierTable(ushort _index = 0) : base(_index) {
            //creates empty table
        }
    }
}
