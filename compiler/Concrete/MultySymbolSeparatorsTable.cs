using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace Concrete.MultySymbolSeparatorsTableSpace {
    public class MultySymbolSeparatorsTable : Table {
        public MultySymbolSeparatorsTable(ushort _index = 301) : base(_index) {
            this.Insert("<=");
            this.Insert("<>");
            this.Insert(">=");
        }
        public override void Insert(string _value) {
            base.Insert(_value);
        }
    }
}
