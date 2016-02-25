using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.KeyWordTableSpace {
    public class KeyWordsTable : TableSpace.Table {
        public KeyWordsTable() : base() {
            //some keywords
            this.ReadFromFile("kwords.dat");
        }
        public override void Insert(string _value) {
            base.Insert(_value);
        }
    }
}
