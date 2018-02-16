using System;
using System.Collections.Generic;
using System.Text;

namespace HackAssembler
{
    class Parser
    {
        private string szCurrentInstruction;
        private int nInstrNumber;
        public int GetInstrNum() { return nInstrNumber; }

        private bool bIsAInstr;
        private bool bIsCInstr;
        private bool bIsLblDef;
        public bool IsAInstruction() { return bIsAInstr; }
        public bool IsCInstruction() { return bIsCInstr; }
        public bool IsLabelDefinition() { return bIsLblDef; }

        private string szAddrAInstr;
        private string szLblName;
        private string[] szEltsCInstr;
        public string GetAInstrElts() { return (bIsAInstr? szAddrAInstr : ""); }
        public string GetLblName() { return (bIsLblDef? szLblName : ""); }
        public string[] GetCInstrElts() { return (bIsCInstr? szEltsCInstr : new String[] { }); }

        public Parser()
        {
            Init();
        }

        public void Init()
        {
            nInstrNumber = 0;
            szCurrentInstruction = "";
            bIsAInstr = false;
            bIsCInstr = false;
            bIsLblDef = false;
        }

        private int StripCommentsAndSpaces(string szInstr)
        {
            string szCleanInstr = "";
            for (int i=0; i< szInstr.Length; i++)
            {
                if (!char.IsWhiteSpace(szInstr[i]))
                    szCleanInstr += szInstr[i];
                if ((i< szInstr.Length-1) && (szInstr[i] == '/') && (szInstr[i + 1] == '/'))
                    break;
            }
            szCurrentInstruction = string.Format(szCleanInstr);
            return 0;
        }

        public int NewInstr(string szInstr, bool bFirstPass)
        {
            StripCommentsAndSpaces(szInstr);
            if(szCurrentInstruction == "")
            {
                bIsAInstr = bIsCInstr = bIsLblDef = false;
            }
            else if(szCurrentInstruction[0]=='@')
            {
                bIsAInstr = true;
                bIsCInstr = bIsLblDef = false;
                // increment instruction number
                nInstrNumber++;
                // extract its elements
                szAddrAInstr = szCurrentInstruction.Substring(1);
            }
            else if(szCurrentInstruction[0] == '(')
            {
                bIsLblDef = true;
                bIsCInstr = bIsAInstr = false;
                // extract label name
                if (bFirstPass)
                {
                    szLblName = szCurrentInstruction.Remove(szCurrentInstruction.Length-1).Substring(1);
                }
            }
            else // Case of C instruction
            {
                bIsCInstr = true;
                bIsAInstr = bIsLblDef = false;
                // increment instruction number
                nInstrNumber++;
                // extract its elements
                if (!bFirstPass)
                {
                    szEltsCInstr = szCurrentInstruction.Split("=;".ToCharArray());
                }
            }

            return 0;
        }
    }
}
