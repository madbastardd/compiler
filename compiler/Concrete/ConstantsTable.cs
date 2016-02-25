using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.ConstantsTableSpace {
    public class ConstantsTable : TableSpace.Table {
        public ConstantsTable() : base() {
            //constant create
        }
        public override void Insert(string _value) {
            base.Insert(_value);
        }
    }
}
