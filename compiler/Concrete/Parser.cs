using System;
using System.Collections.Generic;
using System.Linq;
using Concrete.TableSpace;
using Concrete.KeyWordTableSpace;
using Concrete.IdentifierTableSpace;
using Concrete.ConstantsTableSpace;
using Concrete.AttributeClassSpace;
using Concrete.MultySymbolSeparatorsTableSpace;

namespace Concrete.Parser {
    public class Parser {
        public static List<string> Parse(string _source) {
            if (_source == null)
                throw new ArgumentNullException("source_string");

            List<string> parsed_string = new List<string>();
            string source_string = removeDublicatesSpaces(_source);

            UInt16 previousAttribute;
            try {
                previousAttribute = AttributeClass.get(source_string[0]);
            } catch (IndexOutOfRangeException) {
                //empty string
                throw new ArgumentNullException("source_string");
            }
            Int32 start = 0,
                end = 0;

            foreach (var symbol in source_string) {
                ushort symbol_attr;
                if (((symbol_attr = AttributeClass.get(symbol)) & previousAttribute) == 0 ||
                    previousAttribute == AttributeClass.ONE_SYMBOL_SEPARATORS) {
                    //another symbol type
                    previousAttribute = symbol_attr;
                    parsed_string.Add(source_string.Substring(start, end - start));
                    start = end;
                }
                end++;
            }
            parsed_string.Add(source_string.Substring(start, end - start));

            return parsed_string;
        }

        private static string removeDublicatesSpaces(string _source) {
            char[] whiteSpaces = AttributeClass.getWhiteSpacesChar();
            _source = _source.Trim(whiteSpaces);

            for (int i = 0; i < _source.Length - 1; i++) {
                var check = from res in whiteSpaces
                            where res == _source[i]
                            select res; //return not empty enumerator if source[i] is white space
                if (check.Count() > 0 && _source[i] == _source[i + 1]) {
                    _source = _source.Remove(i + 1, 1);
                    i--;
                }
            }
            return _source;
        }

        public static List<UInt16> Recognize(List<string> parsedString, Table[] tables) {
            /*
                0 - Multy Symbol Table
                1 - Keywords
                2 - Constants
                3 - Identifiers
            */
            List<UInt16> analyzedString = new List<ushort>();

            foreach (string item in parsedString) {
                ushort charAttribute = AttributeClass.get(item[0]);
                if ((charAttribute & AttributeClass.WHITE_SPACE) != 0) {
                    //white space
                    continue;
                }
                else if ((charAttribute & AttributeClass.ONE_SYMBOL_SEPARATORS) != 0 &&
                    item.Length == 1) {
                    //one symbol separator or white space
                    analyzedString.Add(charAttribute);
                }
                else if ((charAttribute & AttributeClass.MULTY_SYMBOL_SEPARATORS) != 0 &&
                    item.Length != 1) {
                    //multy symbol separator
                    MultySymbolSeparatorsTable table = tables[0] as MultySymbolSeparatorsTable;
                    UInt16 key;
                    try {
                        key = table.exists(item);
                        analyzedString.Add(key);
                    } catch (Exception) {
                        //it is several one symbol separators
                        foreach (var chars in item) {
                            analyzedString.Add(AttributeClass.get(chars));
                        }
                    }
                } else if ((charAttribute & AttributeClass.KEYWORDS) != 0) {
                    //keyword or identifier
                    UInt16 key;
                    KeyWordsTable table = tables[1] as KeyWordsTable;
                    try {
                        key = table.exists(item);
                        analyzedString.Add(key);
                    } catch (Exception) {
                        //it is identifier
                        IdentifierTables tableID = tables[3] as IdentifierTables;
                        try {
                            key = tableID.exists(item);
                            analyzedString.Add(key);
                        } catch (Exception) {
                            //new ID
                            tableID.insert(item);
                            analyzedString.Add(tableID.exists(item));
                        }
                    }
                } else if ((charAttribute & AttributeClass.CONST) != 0) {
                    //constant
                    ConstantsTable table = tables[2] as ConstantsTable;
                    UInt16 key;
                    try {
                        key = table.exists(item);
                        analyzedString.Add(key);
                    } catch (Exception) {
                        table.insert(item);
                        analyzedString.Add(table.exists(item));
                    }
                } else {
                    throw new ArgumentException("item");
                }
            }
            return analyzedString;
        }
    }
}
