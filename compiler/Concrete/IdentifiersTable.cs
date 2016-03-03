using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;
using System.IO;

namespace Concrete.IdentifierTableSpace {
    public class IdentifierTables : Table {
		Dictionary<ushort, Parser.Parser.States> IDType = new Dictionary<ushort, Parser.Parser.States>();
        public IdentifierTables() : base(1001) {
            //creates empty table
			this.ReadFromFile ("startid.dat");
        }
        public override void Insert(string _value) {
			this.Insert (_value, Parser.Parser.States.UNDEFINED);
        }

		public void Insert(string _value, Parser.Parser.States state) {
			base.Insert(_value);

			this.IDType.Add ((ushort)(this.CurrentIndex - 1), state);
		}

		public override void SaveToFile (string fileName, bool WithKeys)
    	{
			try {
				using (StreamWriter sw = new StreamWriter(fileName)) {
					foreach (var item in IDType.Keys) {
						if (WithKeys)
							sw.WriteLine(String.Format("{0}:{1} - {2}", item, this.data[item], this.IDType[item]));
						else
							sw.WriteLine(String.Format("{0} - {2}", this.data[item], this.IDType[item]));
					}
				}
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
    	}
    }
}
