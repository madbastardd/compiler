using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.AttributeClassSpace;

namespace lexical_analyzer_c_sharp.Concrete {
    public class Parser {
        static List<string> parsed_string = null;
        static public string source_string { get; private set; }

        Parser() {
            
        }
        public static void Parse(string _source) {
            if (_source == null) {
                parsed_string = null;
                throw new ArgumentNullException("source_string");
            }

            parsed_string = new List<string>();
            source_string = removeDublicates(_source);

            UInt16 symbolAttribute;
            try {
                symbolAttribute = AttributeClass.get(source_string[0]);
            } catch (IndexOutOfRangeException) {
                //empty string
                Parser.parsed_string.Add("");
                return;
            }
            Int32 start = 0,
                end = 0;

            foreach (var symbol in source_string) {
                ushort symbol_attr;
                if (((symbol_attr = AttributeClass.get(symbol)) & symbolAttribute) == 0) {
                    //another symbol type
                    symbolAttribute = symbol_attr;
                    parsed_string.Add(source_string.Substring(start, end - start));
                    start = end;
                }
                end++;
            }
            parsed_string.Add(source_string.Substring(start, end - start));
        }

        private static string removeDublicates(string _source) {
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

        public static List<string> getParsedString() {
            return Parser.parsed_string;
        }
    }
}
