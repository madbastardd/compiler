using System;
using System.Collections.Generic;
using System.Linq;
using Concrete.TableSpace;
using Concrete.KeyWordTableSpace;
using Concrete.IdentifierTableSpace;
using Concrete.ConstantsTableSpace;
using Concrete.AttributeClassSpace;
using Concrete.MultySymbolSeparatorsTableSpace;
using System.Text;
using System.IO;

namespace Concrete.Parser {
    public class Parser {
        enum States {
            VAR_READ, CONST_READ, PROCEDURE_READ
        }
        static bool ParsedWithError = false;
        public static List<UInt16> Parse(string sentence, Table[] tables) {
            /*
                tables:
                    0 - Multy Symbol Table
                    1 - Keywords
                    2 - Constants
                    3 - Identifiers
            */
            List<UInt16> result = new List<ushort>();

            //checks
            if (sentence == null)
                throw new ArgumentNullException("sentence");
            else if (sentence == "")
                throw new ArgumentException("sentence");
            if (tables.Length != 4)
                throw new ArgumentException("tables");

            MultySymbolSeparatorsTable MSTable = tables[0] as MultySymbolSeparatorsTable;
            KeyWordsTable KWTable = tables[1] as KeyWordsTable;
            ConstantsTable CTable = tables[2] as ConstantsTable;
            IdentifierTables IDTable = tables[3] as IdentifierTables;

            sentence = sentence.Trim(new char[] { ' ', '\n', '\t', '\v', (char)12 });
            String lexem;
            int currentIndex = 0;
            ushort currentAttribute = AttributeClass.Get(sentence[0]);

            Parser.ParsedWithError = false;
            while (currentIndex < sentence.Length) {
                lexem = sentence[currentIndex].ToString();
                if ((currentAttribute & (AttributeClass.WORD)) != 0) {
                    //idintifier ow keyword handler
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.WORD | AttributeClass.DIGIT)) != 0) {
                        lexem += sentence[currentIndex];
                    }

                    ushort key;
                    if (!KWTable.IsInTable(lexem)) {
                        if (!IDTable.IsInTable(lexem))
                            IDTable.Insert(lexem);

                        key = IDTable.GetKey(lexem);
                    }
                    else {
                        key = KWTable.GetKey(lexem);
                    }
                    result.Add(key);
                }
                else if ((currentAttribute & (AttributeClass.DIGIT)) != 0) {
                    //number handler
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.DIGIT)) != 0) {
                        lexem += sentence[currentIndex];
                    }

                    if (!CTable.IsInTable(lexem))
                        CTable.Insert(lexem);

                    result.Add(CTable.GetKey(lexem));
                }
                else if ((currentAttribute & AttributeClass.COMMENT_STARTER_ENDER) != 0) {
                    if (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex]))
                        & AttributeClass.COMMENT_START_BRACKER) != 0) {
                        // this is comment
                        do {
                            while (++currentIndex < sentence.Length &&
                            ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.COMMENT_END_BRACKET)) == 0) ;

                            if (currentIndex < sentence.Length) {
                                currentAttribute = ++currentIndex < sentence.Length ?
                                    AttributeClass.Get(sentence[currentIndex]) :
                                    AttributeClass.ERROR;

                                if ((currentAttribute & AttributeClass.COMMENT_STARTER_ENDER) != 0) {
                                    currentAttribute = ++currentIndex < sentence.Length ?
                                        AttributeClass.Get(sentence[currentIndex]) :
                                        AttributeClass.ERROR;
                                    break;
                                }
                            }
                            //got errors
                            if (currentIndex >= sentence.Length) {
                                result.Add(0);
                                Parser.ParsedWithError = true;
                                break;
                            }
                        } while (true);
                    } else {
                        //it is separator
                        result.Add(sentence[currentIndex - 1]);
                    }
                }
                else if ((currentAttribute & (AttributeClass.SEPARATOR)) != 0) {
                    //separator
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.SEPARATOR)) != 0) {
                        lexem += sentence[currentIndex];
                    }

                    if (!MSTable.IsInTable(lexem)) {
                        foreach (var item in lexem)
                            result.Add(item);
                    }
                    else {
                        result.Add(MSTable.GetKey(lexem));
                    }
                }
                else if ((currentAttribute & AttributeClass.WHITE_SPACE) != 0) {
                    //spaces
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.WHITE_SPACE)) != 0) ;

                    result.Add(' ');
                }
                else if (currentAttribute == AttributeClass.ERROR) {
                    //error
                    result.Add(0);
                    currentAttribute = (++currentIndex < sentence.Length) ?
                        AttributeClass.Get(sentence[currentIndex]) :
                        AttributeClass.ERROR;
                    Parser.ParsedWithError = true;
                }
            }
            return result;
        }

        public static List<UInt16> ParseFile(string filename, Table[] tables) {
            using (StreamReader sr = new StreamReader(filename)) {
                string line = "", tmp_line;
                while (!sr.EndOfStream) {
                    tmp_line = sr.ReadLine() + '\n';
                    line += tmp_line;
                }

                return Parse(line, tables);
            }
        }
    }
}
