using System.Collections;

namespace Interfaces.TableSpace {
    interface ITable {
        string this[ushort index] { get; }

        ushort exists(string _value);
        bool isInTable(string _value);
        void insert(string _value);
    }
}