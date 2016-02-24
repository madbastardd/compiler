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
        protected ushort current_index;
        protected SortedDictionary<ushort, string> data;

        public Table(ushort _current_index = 0) {
            this.current_index = _current_index;
            this.data = new SortedDictionary<ushort, string>();
        }
        public string this[ushort index] {
            get {
                var result = this.data[index];
                return result;
            }
            private set {
                this.data.Add(index, value);
            }
        }

        void Insert(KeyValuePair<ushort, string> pair) {
            if (this.IsInTable(pair.Value))
                throw new ArgumentException(pair.Value);
            this[pair.Key] = pair.Value;
        }

        public virtual void Insert(string _value) {
            try {
                this.Insert(new KeyValuePair<ushort, string>(this.current_index++, _value));
            } catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException) {
                this.current_index--;
                throw ex;
            }    
        }

        public ushort GetKey(string _value) {
            var result = from res in this.data
                         where res.Value == _value
                         select res;

            var key = result.First().Key;
            return key;
        }

        public bool IsInTable(string _value) {
            var result = from res in this.data
                         where res.Value == _value
                         select res;
            return result.Count() != 0;
        }

        public void ReadFromFile(string fileName) {
            using (StreamReader sr = new StreamReader(fileName)) {
                this.data.Clear();
                string _value;
                while ((_value = sr.ReadLine()) != null) {
                    this.Insert(_value);
                }
            }
        }

        public void SaveToFile(string fileName, bool WithKeys = false) {
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
