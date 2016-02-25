using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace Concrete.IdentifierTableSpace {
    public class IdentifierTables : Table {
        public IdentifierTables() : base() {
            //creates empty table
        }
        public override void Insert(string _value) {
            base.Insert(_value);
        }
    }
}
