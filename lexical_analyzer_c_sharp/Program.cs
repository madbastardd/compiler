namespace lexical_analyzer_c_sharp {
    class Program {
        enum LEXEM_TYPE {
            SPACE = 1,
            ONE_SYMBOL = 2,
            MULTY_SYMBOL = 4,
            KEYWORDS = 8,
            CONST = 16,
            IDENTIFIERS = 32
        };
        static void Main(string[] args) {
            System.Collections.Generic.List<LEXEM_TYPE> attributes = new System.Collections.Generic.List<LEXEM_TYPE>();
            for (byte ind = 0; ind < 128; ind++) {
                if ((char)ind >= 'A' && (char)ind <= 'Z')
                    attributes.Add(LEXEM_TYPE.IDENTIFIERS | LEXEM_TYPE.KEYWORDS);
                else
                    attributes.Add(0);
            }

            Table table = new Table();

            table.insert("dimasik");

            foreach(var elem in table) {
                
            }
                
        }
    }
}
