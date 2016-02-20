using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace Concrete.IdentifierTableSpace {
    public class IdentifierTable : Table {
        public IdentifierTable(ushort _index = 0) : base(_index) {
            //creates empty table
        }
    }
}
