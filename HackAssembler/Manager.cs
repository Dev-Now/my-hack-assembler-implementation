using System;
using System.IO;

namespace HackAssembler
{
    class Manager
    {
        static int Main(string[] args)
        {
            // The program should receive a text file named XXX.asm 
            // (it should act as a command-line and receive XXX.asm as an argument)
            string szFile = args[0];
            // -- check the file extention is asm
            if(szFile.Substring(szFile.Length-4)!=".asm")
            {
                Console.WriteLine("Manager.Main: Error! the received file doesn't have the right extension.");
                return 1;
            }
            // -- create the symbol table
            SymbolTable cSymbTab = new SymbolTable();
            // -- create generated code string
            string szGenCode = "";
            // -- check the file exists
            if (!File.Exists(@szFile)) {
                Console.WriteLine("Manager.Main: Error! the received file doesn't exist.");
                return 1;
            }
            FileInfo fAsm = new FileInfo(@szFile);
            // -- open file and read it line by line
            using (StreamReader fsAsm = fAsm.OpenText())
            {
                // First pass
                string szCurrLine = "";
                Parser cParser = new Parser();
                while ((szCurrLine = fsAsm.ReadLine()) != null)
                {
                    cParser.NewInstr(szCurrLine, true);
                    if (cParser.IsLabelDefinition())
                    {
                        cSymbTab.AddLabel(cParser.GetLblName(), cParser.GetInstrNum());
                    }
                }

                // Second pass
                cParser.Init();
                fsAsm.DiscardBufferedData();
                fsAsm.BaseStream.Seek(0, SeekOrigin.Begin);
                Code cCode = new Code();
                while ((szCurrLine = fsAsm.ReadLine()) != null)
                {
                    cParser.NewInstr(szCurrLine, false);
                    if (cParser.IsAInstruction())
                    {
                        string szAddr = cParser.GetAInstrElts();
                        cSymbTab.AddVar(szAddr);
                        if (cSymbTab.HasSymbol(szAddr)) { szAddr = cSymbTab.FetchVal(szAddr).ToString(); }
                        szGenCode += string.Format("{0}\n", cCode.translateAInstr(szAddr));
                    }
                    else if (cParser.IsCInstruction())
                    {
                        string[] szElts = cParser.GetCInstrElts();
                        szGenCode += string.Format("{0}\n", cCode.translateCInstr(szElts));
                    }
                }
            }

            // The generated code is written into a text file named XXX.hack
            // ASSUMPTION: XXX.asm is error free
            string szOutFile = szFile.Substring(0, szFile.Length - 3); // truncate asm extension
            szOutFile+="hack"; // update with hack extension
            File.WriteAllText(@szOutFile, szGenCode.Substring(0, szGenCode.Length - 1));

            return 0;
        }
    }
}