using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Interfaces.TableSpace;

namespace Concrete.TableSpace {
    /// <summary>
    /// class includes dictionary, that has unique keys and unique values
    /// </summary>
    public abstract class Table : ITable {
        protected Dictionary<int, string> data;

        public Table() {
            this.data = new Dictionary<int, string>();
        }
        public string this[int index] {
            get {
                return this.data[index];
            }
        }

        public virtual void Insert(string _value) {
            this.data.Add(_value.GetHashCode(), _value);
        }

        public bool ContainsValue(string _value) {
            return this.data.ContainsValue(_value);
        }

        public virtual void ReadFromFile(string fileName) {
            using (StreamReader sr = new StreamReader(fileName)) {
                this.data.Clear();
                string _value;
                while ((_value = sr.ReadLine()) != null) {
                    this.Insert(_value);
                }
            }
        }

        public virtual void SaveToFile(string fileName, bool WithKeys = false) {
            using (StreamWriter sw = new StreamWriter(fileName)) {
                foreach (var item in this.data) {
                    if (WithKeys)
                        sw.WriteLine(String.Format("{0}:{1}\n", item.Key, item.Value));
                    else
                        sw.WriteLine(String.Format("{0}\n", item.Value));
                }
            }
        }

        public void Clear() {
            this.data.Clear();
        }
    }
}
