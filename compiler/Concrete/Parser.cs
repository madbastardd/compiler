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
		public enum States {
            UNDEFINED, VAR, CONST, PROCEDURE
        }
		static States state = States.UNDEFINED;
        public static List<int> Parse(string sentence, Table[] tables) {
            /*
                tables:
                    0 - Multy Symbol Table
                    1 - Keywords
                    2 - Constants
                    3 - Identifiers
            */
            List<int> result = new List<int>();

            sentence = sentence.Trim(new char[] { ' ', '\n', '\t', '\v', (char)12 });
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

            string lexem;
            int currentIndex = 0;
            ushort currentAttribute = AttributeClass.Get(sentence[0]);

            while (currentIndex < sentence.Length) {
                lexem = sentence[currentIndex].ToString();
                if ((currentAttribute & (AttributeClass.WORD)) != 0) {
                    //idintifier ow keyword handler
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.WORD | AttributeClass.DIGIT)) != 0) {
                        lexem += sentence[currentIndex];
                    }

                    if (!KWTable.ContainsValue(lexem) && !IDTable.ContainsValue(lexem)) 
                        IDTable.Insert(lexem, state);
                    
                    result.Add(lexem.GetHashCode());

					if (lexem == "VAR") {
						state = States.VAR;
					} else if (lexem == "CONST") {
						state = States.CONST;
					} else if (lexem == "PROCADURE" || lexem == "PROGRAM") {
						state = States.PROCEDURE;
					}
                }
                else if ((currentAttribute & (AttributeClass.DIGIT)) != 0) {
                    //number handler
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.DIGIT)) != 0) {
                        lexem += sentence[currentIndex];
                    }

                    if (!CTable.ContainsValue(lexem))
                        CTable.Insert(lexem);

                    result.Add(lexem.GetHashCode());
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
                            //unclosed comment
                            if (currentIndex >= sentence.Length) {
                                result.Add("<ERR>".GetHashCode());
								Console.WriteLine ("Unclosed comment");
                                break;
                            }
                        } while (true);
                    } else {
                        //it is separator
                        result.Add("*".GetHashCode());
                    }
                }
                else if ((currentAttribute & (AttributeClass.SEPARATOR)) != 0) {
                    //separator
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.SEPARATOR)) != 0) {
                        lexem += sentence[currentIndex];
                    }

                    if (!MSTable.ContainsValue(lexem)) {
                        foreach (var item in lexem)
                            result.Add(item.ToString().GetHashCode());
                    }
                    else {
                        result.Add(lexem.GetHashCode());
                    }
                }
                else if ((currentAttribute & AttributeClass.WHITE_SPACE) != 0) {
                    //spaces
                    while (++currentIndex < sentence.Length &&
                        ((currentAttribute = AttributeClass.Get(sentence[currentIndex])) & (AttributeClass.WHITE_SPACE)) != 0) ;

                    result.Add(" ".GetHashCode());
                }
                else if (currentAttribute == AttributeClass.ERROR) {
                    //error
                    result.Add("<ERR>".GetHashCode());
                    currentAttribute = (++currentIndex < sentence.Length) ?
                        AttributeClass.Get(sentence[currentIndex]) :
                        AttributeClass.ERROR;
					Console.WriteLine ("Undefined symbol");
                }
            }
            return result;
        }

        public static List<int> ParseFile(string filename, Table[] tables) {
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
