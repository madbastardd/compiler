using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.KeyWordTableSpace {
    public class KeyWordsTable : TableSpace.Table {
        public KeyWordsTable(ushort _index = 401) : base(_index) {
            //some keywords
            this.Insert("PROGRAM");
            this.Insert("BEGIN");
            this.Insert("END");
            this.Insert("OR");
            this.Insert("AND");
            this.Insert("NOT");
            this.Insert("MOD");
            this.Insert("EXP");
            this.Insert("SIGNAL");
            this.Insert("COMPLEX");
            this.Insert("INTEGER");
            this.Insert("FLOAT");
            this.Insert("BLOCKFLOAT");
            this.Insert("EXT");
            this.Insert("VAR");
            this.Insert("CONST");
        }
        public override void Insert(string _value) {
            base.Insert(_value);
        }
    }
}
