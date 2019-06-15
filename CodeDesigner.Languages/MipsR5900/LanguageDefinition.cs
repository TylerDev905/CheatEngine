using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CodeDesigner.Languages.MipsR5900
{
    public static class LanguageDefinition
    {
        public static List<EERegister> EERegisters { get; }
            = JsonConvert.DeserializeObject<List<EERegister>>(File.ReadAllText("MipsR5900/Resources/EERegisters.json"));
        public static List<COP0Register> COP0Registers { get; }
            = JsonConvert.DeserializeObject<List<COP0Register>>(File.ReadAllText("MipsR5900/Resources/COP0Registers.json"));
        public static List<COP1Register> COP1Registers { get; }
            = JsonConvert.DeserializeObject<List<COP1Register>>(File.ReadAllText("MipsR5900/Resources/COP1Registers.json"));
        public static List<Instruction> Instructions { get; }
            = JsonConvert.DeserializeObject<List<Instruction>>(File.ReadAllText("MipsR5900/Resources/Instructions.json"));

        public static class PlaceHolders
        {
            public static List<string> EERegisters { get; } = new List<string> { "base", "rs", "rt", "rd" };
            public static string COP0Registers { get; } = "reg";
            public static List<string> COP1Registers { get; } = new List<string> { "fs", "ft", "fd" };
            public static string JType { get; } = "target";
            public static List<string> IType { get; } = new List<string> { "immediate", "offset" };
            public static string SA { get; } = "sa";
            public static string Code { get; } = "code";
        }
    }
    public abstract class Register
    {
        public string TextDisplay { get; set; }
        public string Description { get; set; }
        public string Binary { get; set; }
        public int Value { get; set; }
    }
    public class EERegister : Register { }
    public class COP0Register : Register { }
    public class COP1Register : Register { }
    public class Instruction
    {
        public string TextDisplay { get; set; }
        public List<string> Syntax { get; set; }
        public string Binary { get; set; }
        public string Mask { get; set; }
        public string Description { get; set; }
        public List<InstructionArg> Args { get; set; }
        public class InstructionArg
        {
            public string Value { get; set; }
            public int Size { get; set; }
        }
    }
}
