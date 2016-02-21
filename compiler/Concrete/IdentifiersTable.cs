using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace Concrete.IdentifierTableSpace {
    public class IdentifierTables : Table {
        public IdentifierTables(ushort _index = 1001) : base(_index) {
            //creates empty table
        }
        public override void insert(string _value) {
            base.insert(_value);
        }
    }
}
