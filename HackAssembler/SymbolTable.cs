using System;
using System.Collections.Generic;
using System.Text;

namespace HackAssembler
{
    class SymbolTable
    {
        private Dictionary<string, int> cMainSymbolTable;
        private int nVarDefValue;
        
        // Pre-defined symbols
        /**
         * R0       --- 0
         * R1       --- 1
         * R2       --- 2
         *      ...
         * R15      --- 15
         * SCREEN   --- 16384
         * KBD      --- 24576
         * SP       --- 0
         * LCL      --- 1
         * ARG      --- 2
         * THIS     --- 3
         * THAT     --- 4
        */
        // Constructor
        public SymbolTable()
        {
            // initialize variable defintion value
            nVarDefValue = 16;

            // initialize an empty symbol table
            cMainSymbolTable = new Dictionary<string, int>();

            // Add the pre-defined symbols
            for (int i=0; i<16; i++) // add the Rx symbols
                cMainSymbolTable.Add(string.Format("R{0}", i), i);

            cMainSymbolTable.Add("SCREEN", 16384); // add SCREEN
            cMainSymbolTable.Add("KBD", 24576); // add KBD

            cMainSymbolTable.Add("SP", 0); // add SP
            cMainSymbolTable.Add("LCL", 1); // add LCL
            cMainSymbolTable.Add("ARG", 2); // add ARG
            cMainSymbolTable.Add("THIS", 3); // add THIS
            cMainSymbolTable.Add("THAT", 4); // add THAT
        }

        // Adding Label symbols
        // (XXX)
        public int AddLabel(string szLblName, int nLblValue)
        {
            cMainSymbolTable.Add(szLblName, nLblValue);
            return 0;
        }

        // Adding Variable symbols
        public int AddVar(string szVarName)
        {
            int nAddr;
            if (!int.TryParse(szVarName, out nAddr))
            {
                cMainSymbolTable.Add(szVarName, nVarDefValue);
                nVarDefValue++;
            }          
            return 0;
        }

        // Check symbol table has this symbol
        public bool HasSymbol(string szSymbol)
        {
            return cMainSymbolTable.ContainsKey(szSymbol);
        }

        // Fetch the value of a given symbol
        public int FetchVal(string szSymbol)
        {
            try
            {
                return cMainSymbolTable[szSymbol];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Symbol = {0} is not found.", szSymbol);
                return -1;
            }
        }
    }
}
