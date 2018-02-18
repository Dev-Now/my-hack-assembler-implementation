using System;
using System.Collections.Generic;
using System.Text;

namespace HackAssembler
{
    class Code
    {
        private Dictionary<string, string> cCodeDict;
        private Dictionary<string, string> cDestDict;
        private Dictionary<string, string> cJumpDict;

        // Constructor
        public Code()
        {
            cCodeDict = new Dictionary<string, string>();
            cDestDict = new Dictionary<string, string>();
            cJumpDict = new Dictionary<string, string>();

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
            cDestDict.Add("null",   "000");
            cDestDict.Add("M",      "001");
            cDestDict.Add("D",      "010");
            cDestDict.Add("MD",     "011");
            cDestDict.Add("A",      "100");
            cDestDict.Add("AM",     "101");
            cDestDict.Add("AD",     "110");
            cDestDict.Add("AMD",    "111");

            // Add jump code dictionnary values
            cJumpDict.Add("null",   "000");
            cJumpDict.Add("JGT",    "001");
            cJumpDict.Add("JEQ",    "010");
            cJumpDict.Add("JGE",    "011");
            cJumpDict.Add("JLT",    "100");
            cJumpDict.Add("JNE",    "101");
            cJumpDict.Add("JLE",    "110");
            cJumpDict.Add("JMP",    "111");
        }

        public string translateCInstr(string[] szEltsCInstr)
        {
            // 111 comp dest jump
            return ("111"+  cCodeDict[szEltsCInstr[Convert.ToInt32(C_INSTR_ELTS.eCOMP)]] +
                            cDestDict[szEltsCInstr[Convert.ToInt32(C_INSTR_ELTS.eDEST)]] +
                            cJumpDict[szEltsCInstr[Convert.ToInt32(C_INSTR_ELTS.eJUMP)]] );
        }

        public string translateAInstr(string szAddrAInstr)
        {
            int nAddr = Convert.ToInt32(szAddrAInstr);
            char[] bits = new char[15];
            bits = "000000000000000".ToCharArray();
            int i = bits.Length-1;
            while (nAddr != 0)
            {
                bits[i--] = (nAddr & 1) == 1 ? '1' : '0';
                nAddr >>= 1;
            }
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
