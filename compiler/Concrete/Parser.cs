using System;
using System.Collections.Generic;
using Concrete.TableSpace;
using Concrete.KeyWordTableSpace;
using Concrete.IdentifierTableSpace;
using Concrete.ConstantsTableSpace;
using Concrete.AttributeClassSpace;
using Concrete.MultySymbolSeparatorsTableSpace;
using System.Text;
using System.IO;

namespace Concrete.Parser {
	/// <summary>
	/// Parser class
	/// </summary>
    public static class Parser {

		public enum States {
			/// <summary>
			/// states of ID
			/// </summary>
            UNDEFINED, VAR, CONST, PROCEDURE
        }
		static States state = States.UNDEFINED;	//type of ID (var, const, etc.)

		private static void IDParse(string sentence, Table[] tables, string lexem, ref int currentIndex, List<int> result) {
			while (++currentIndex < sentence.Length &&
			       (AttributeClass.Get(sentence[currentIndex]) & (AttributeClass.WORD | AttributeClass.DIGIT)) != 0) {
				lexem += sentence[currentIndex];
			}

			ushort key;
			KeyWordsTable KWTable = tables[1] as KeyWordsTable;
			IdentifierTables IDTable = tables[3] as IdentifierTables;

			if (!KWTable.ContainsValue (lexem)) {
				if (!IDTable.ContainsValue (lexem))
					IDTable.Insert (lexem, state);
				key = IDTable.GetKey(lexem);
			} else 
				key = KWTable.GetKey(lexem);

			result.Add(key);

			if (lexem == "VAR") {
				state = States.VAR;
			} else if (lexem == "CONST") {
				state = States.CONST;
			} else if (lexem == "PROCEDURE" || lexem == "PROGRAM") {
				state = States.PROCEDURE;
			}
		}

		private static void DigitParse(string sentence, Table[] tables, string lexem, ref int currentIndex, List<int> result) {
			while (++currentIndex < sentence.Length &&
			       (AttributeClass.Get(sentence[currentIndex]) & (AttributeClass.DIGIT)) != 0) {
				lexem += sentence[currentIndex];
			}

			ConstantsTable CTable = tables[2] as ConstantsTable;
			if (!CTable.ContainsValue(lexem))
				CTable.Insert(lexem);

			result.Add(CTable.GetKey(lexem));
		}

		private static void CommentParse(string sentence, Table[] tables, string lexem, ref int currentIndex, List<int> result) {
			if (++currentIndex < sentence.Length 
			    && (AttributeClass.Get(sentence[currentIndex])
			    & AttributeClass.COMMENT_START_BRACKER) != 0) {
				// this is comment
				do {
					while (++currentIndex < sentence.Length &&
					       (AttributeClass.Get(sentence[currentIndex]) & (AttributeClass.COMMENT_END_BRACKET)) == 0) ;

					if (++currentIndex < sentence.Length
					    && (AttributeClass.Get(sentence[currentIndex]) & AttributeClass.COMMENT_STARTER_ENDER) != 0) {
						++currentIndex;
						break;
					}
					//unclosed comment
					else if (currentIndex >= sentence.Length) {
						result.Add(-1);
						Console.WriteLine ("Unclosed comment");
						break;
					}
				} while (true);
			} else {
				//it is separator
				result.Add('*');
			}
		}

		private static void SeparatorParse(string sentence, Table[] tables, string lexem, ref int currentIndex, List<int> result) {
			while (++currentIndex < sentence.Length &&
			       (AttributeClass.Get(sentence[currentIndex]) & (AttributeClass.SEPARATOR)) != 0) {
				lexem += sentence[currentIndex];
			}

			MultySymbolSeparatorsTable MSTable = tables[0] as MultySymbolSeparatorsTable;
			if (!MSTable.ContainsValue(lexem)) {
				foreach (var item in lexem)
					result.Add(item);
			}
			else {
				result.Add(MSTable.GetKey(lexem));
			}
		}

		private static void WhiteSpaceParse(string sentence, ref int currentIndex, List<int> result) {
			while (++currentIndex < sentence.Length &&
			       (AttributeClass.Get(sentence[currentIndex]) & (AttributeClass.WHITE_SPACE)) != 0) ;
		}

		private static void ErrorParse(ref int currentIndex, List<int> result) {
			result.Add(-1);
			++currentIndex;
			Console.WriteLine ("Undefined symbol");
		}

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

			int currentIndex = 0;
			ushort currentAttribute;

			while (currentIndex < sentence.Length) {
				currentAttribute = AttributeClass.Get (sentence [currentIndex]);
				if ((currentAttribute & (AttributeClass.WORD)) != 0) {
					//idintifier ow keyword handler
					IDParse (sentence, tables, sentence[currentIndex].ToString(), ref currentIndex, result);
                }
                else if ((currentAttribute & (AttributeClass.DIGIT)) != 0) {
                    //number handler
					DigitParse(sentence, tables, sentence[currentIndex].ToString(), ref currentIndex, result);
                }
                else if ((currentAttribute & AttributeClass.COMMENT_STARTER_ENDER) != 0) {
                    //comment parse
					CommentParse(sentence, tables, sentence[currentIndex].ToString(), ref currentIndex, result);
                }
                else if ((currentAttribute & (AttributeClass.SEPARATOR)) != 0) {
                    //separator
					SeparatorParse(sentence, tables, sentence[currentIndex].ToString(), ref currentIndex, result);
                }
                else if ((currentAttribute & AttributeClass.WHITE_SPACE) != 0) {
                    //spaces
					WhiteSpaceParse(sentence, ref currentIndex, result);
                }
                else {
                    //error
					ErrorParse (ref currentIndex, result);
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
