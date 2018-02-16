using System;
using System.Collections.Generic;
using System.Text;

namespace HackAssembler
{
    class Code
    {
        private Dictionary<string, string> cCodeDict;

        // Constructor
        public Code()
        {
            cCodeDict = new Dictionary<string, string>();

            // Add comp code dictionnary values
            cCodeDict.Add("0",      "0101010");
            cCodeDict.Add("1",      "0111111");
            cCodeDict.Add("-1",     "0101010");
            cCodeDict.Add("D",      "0001100");
            cCodeDict.Add("A",      "0110000");
            cCodeDict.Add("M",      "1110000");
            cCodeDict.Add("!D",     "0001101");
            cCodeDict.Add("!A",     "0110001");
            cCodeDict.Add("!M",     "1110001");
            cCodeDict.Add("-D",     "0001111");
            cCodeDict.Add("-A",     "0110011");
            cCodeDict.Add("-M",     "1110011");
            cCodeDict.Add("D+1",    "0011111");
            cCodeDict.Add("A+1",    "0110111");
            cCodeDict.Add("M+1",    "1110111");
            cCodeDict.Add("D-1",    "0001110");
            cCodeDict.Add("A-1",    "0110010");
            cCodeDict.Add("M-1",    "1110010");
            cCodeDict.Add("D+A",    "0000010");
            cCodeDict.Add("D+M",    "1000010");
            cCodeDict.Add("D-A",    "0010011");
            cCodeDict.Add("D-M",    "1010011");
            cCodeDict.Add("A-D",    "0000111");
            cCodeDict.Add("M-D",    "1000111");
            cCodeDict.Add("D&A",    "0000000");
            cCodeDict.Add("D&M",    "1000000");
            cCodeDict.Add("D|A",    "0010101");
            cCodeDict.Add("D|M",    "1010101");

            // Add dest code dictionnary values
            cCodeDict.Add("null",   "000");
            cCodeDict.Add("M",      "001");
            cCodeDict.Add("D",      "010");
            cCodeDict.Add("MD",     "011");
            cCodeDict.Add("A",      "100");
            cCodeDict.Add("AM",     "101");
            cCodeDict.Add("AD",     "110");
            cCodeDict.Add("AMD",    "111");

            // Add jump code dictionnary values
            cCodeDict.Add("null",   "000");
            cCodeDict.Add("JGT",    "001");
            cCodeDict.Add("JEQ",    "010");
            cCodeDict.Add("JGE",    "011");
            cCodeDict.Add("JLT",    "100");
            cCodeDict.Add("JNE",    "101");
            cCodeDict.Add("JLE",    "110");
            cCodeDict.Add("JMP",    "111");
        }

        public string translateCInstr(string[] szEltsCInstr)
        {
            // 111 comp dest jump
            return ("111"+  cCodeDict[szEltsCInstr[Convert.ToInt32(C_INSTR_ELTS.eCOMP)]] +
                            cCodeDict[szEltsCInstr[Convert.ToInt32(C_INSTR_ELTS.eDEST)]] +
                            cCodeDict[szEltsCInstr[Convert.ToInt32(C_INSTR_ELTS.eJUMP)]] );
        }

        public string translateAInstr(string szAddrAInstr)
        {
            int nAddr = Convert.ToInt32(szAddrAInstr);
            char[] bits = new char[15];
            int i = 0;
            while (nAddr != 0)
            {
                bits[i++] = (nAddr & 1) == 1 ? '1' : '0';
                nAddr >>= 1;
            }
            Array.Reverse(bits, 0, i);
            return ("0" + new string(bits));
        }
    }

    enum C_INSTR_ELTS
    {
        eDEST,
        eCOMP,
        eJUMP
    }
}
