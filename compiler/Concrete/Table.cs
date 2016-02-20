using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces.TableSpace;

namespace Concrete.TableSpace {
    /// <summary>
    /// class includes dictionary, that has unique keys and unique values
    /// </summary>
    public abstract class Table : IEnumerable, ITable {
        protected ushort current_index;
        protected SortedDictionary<ushort, string> data;

        public Table(ushort _current_index = 0) {
            this.current_index = _current_index;
            this.data = new SortedDictionary<ushort, string>();
        }

        ~Table() {
            this.data.Clear();
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

        void insert(KeyValuePair<ushort, string> pair) {
            if (this.isInTable(pair.Value))
                throw new ArgumentException(pair.Value);
            this[pair.Key] = pair.Value;
        }

        public void insert(string _value) {
            try {
                this.insert(new KeyValuePair<ushort, string>(this.current_index++, _value));
            } catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException) {
                this.current_index--;
                throw ex;
            }    
        }

        public ushort exists(string _value) {
            var result = from res in this.data
                         where res.Value == _value
                         select res;

            var key = result.First().Key;
            return key;
        }

        public IEnumerator GetEnumerator() {
            return this.data.GetEnumerator();
        }

        public bool isInTable(string _value) {
            var result = from res in this.data
                         where res.Value == _value
                         select res;
            return result.Count() != 0;
        }
    }
}
