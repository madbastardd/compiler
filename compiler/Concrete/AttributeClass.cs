using System;
using System.Collections.Generic;

namespace Concrete.AttributeClassSpace {
    /// <summary>
    /// use like table for OneSymbolSeparate symbols
    /// </summary>
    public class AttributeClass {
        public static readonly UInt16
            ERROR = 0,
            WHITE_SPACE = 1,
            SEPARATOR = 2,
            WORD = 4,
            DIGIT = 8,
            FRACTIONAL_PART_SIGN = 16,
            DOLLAR_SIGN = 32,
            NUMBER_SIGN = 64;

        static List<UInt16> attributes = new List<UInt16>();

        static AttributeClass() {
            for (byte ind = 0; ind < 255; ind++) {
                if (ind >= 'A' && ind <= 'Z' || ind >= 'a' && ind <= 'z') {
                    //keyword or identifier
                    attributes.Add((UInt16)(WORD));
                }
                else if (ind >= '0' && ind <= '9') {
                    //constant
                    attributes.Add((UInt16)(DIGIT));
                }
                else if (ind == '+' || ind == '-') {
                    //number sign
                    attributes.Add((UInt16)(NUMBER_SIGN));
                }
                else if (ind == ':' || ind == '>' || ind == '<' || ind == ';' || ind == '*' || ind == '=' || ind == '!' || ind == '/' || ind == '&' || ind == '^') {
                    //separators
                    attributes.Add((UInt16)(SEPARATOR));
                }
                else if (ind == ' ' || ind == '\n' || ind == '\t' || ind == '\v' || ind == 12) {
                    //white spaces
                    attributes.Add((UInt16)(WHITE_SPACE));
                }
                else if (ind == '#') {
                    //fractional part
                    attributes.Add((UInt16)(FRACTIONAL_PART_SIGN));
                }
                else if (ind == '$') {
                    //dolla sign of EXP
                    attributes.Add((UInt16)(DOLLAR_SIGN));
                }
                else {
                    attributes.Add((UInt16)(ERROR));
                }
            }
        }

        public static UInt16 Get(ushort index) {
            if (index >= 255)
                throw new IndexOutOfRangeException("index");
            return AttributeClass.attributes[index];
        }
    }
}
