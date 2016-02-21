using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;

namespace Concrete.MultySymbolSeparatorsTableSpace {
    public class MultySymbolSeparatorsTable : Table {
        public MultySymbolSeparatorsTable(ushort _index = 301) : base(_index) {
            this.insert(">=");
            this.insert("<=");
            this.insert(":=");
            this.insert("==");
        }
        public override void insert(string _value) {
            base.insert(_value);
        }
    }
}
