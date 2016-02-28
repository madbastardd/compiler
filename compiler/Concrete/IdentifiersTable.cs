using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.TableSpace;
using System.IO;

namespace Concrete.IdentifierTableSpace {
    public class IdentifierTables : Table {
		Dictionary<int, Parser.Parser.States> IDType = new Dictionary<int, Parser.Parser.States>();
        public IdentifierTables() : base() {
            //creates empty table
        }
        public override void Insert(string _value) {
			this.Insert (_value, Parser.Parser.States.UNDEFINED);
        }

		public void Insert(string _value, Parser.Parser.States state) {
			base.Insert(_value);

			this.IDType.Add (_value.GetHashCode (), state);
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
