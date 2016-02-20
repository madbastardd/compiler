using System;
using System.Collections.Generic;

namespace Concrete.AttributeClassSpace {
    public class AttributeClass {
        public static readonly UInt16
            ERROR = 0,
            WHITE_SPACE = 1,
            ONE_SYMBOL_SEPARATORS = 2,
            MULTY_SYMBOL_SEPARATORS = 4,
            KEYWORDS = 8,
            CONST = 16,
            IDENTIFIERS = 32;

        static List<UInt16> attributes = new List<UInt16>();

        static AttributeClass() {
            for (byte ind = 0; ind < 128; ind++) {
                if ((char)ind >= 'A' && (char)ind <= 'Z' || (char)ind >= 'a' && (char)ind <= 'z') {
                    //keyword or identifier
                    attributes.Add((UInt16)(IDENTIFIERS | KEYWORDS));
                }
                else if ((char)ind >= '0' && (char)ind <= '9') {
                    //constant
                    attributes.Add((UInt16)(CONST));
                }
                else if ((char)ind == ':' || (char)ind == ';') {
                    //separators
                    attributes.Add((UInt16)(ONE_SYMBOL_SEPARATORS | MULTY_SYMBOL_SEPARATORS));
                }
                else if ((char)ind == ' ' || (char)ind == '\n') {
                    //white spaces
                    attributes.Add((UInt16)(WHITE_SPACE));
                }
                else {
                    attributes.Add((UInt16)(ERROR));
                }
            }
        }

        public static UInt16 get(ushort index) {
            return AttributeClass.attributes[index];
        }

        public static char[] getWhiteSpacesChar() {
            List<char> trimChars = new List<char>();
            for (ushort ind = 0; ind < attributes.Count; ind++) {
                if ((attributes[ind] & WHITE_SPACE) != 0) {
                    trimChars.Add((char)ind);
                }
            }
            return trimChars.ToArray();
        }
    }
}
