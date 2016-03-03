using System.Collections;

namespace Interfaces.TableSpace {
    interface ITable {
		string this[ushort index] { get; }
        bool ContainsValue(string _value);
        void Insert(string _value);
        void Clear();
        void ReadFromFile(string fileName);
        void SaveToFile(string fileName, bool WithKeys = false);
		ushort GetKey (string _value);
    }
}