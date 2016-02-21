using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.KeyWordTableSpace {
    public class KeyWordsTable : TableSpace.Table {
        public KeyWordsTable(ushort _index = 401) : base(_index) {
            //some keywords
            this.insert("program");
        }
        public override void insert(string _value) {
            base.insert(_value);
        }
    }
}
