using CodeDesigner.Languages.Interfaces;
using CodeDesigner.Languages.MipsR5900.BaseTypes;
using System;
using System.Collections.Generic;

namespace CodeDesigner.Languages.MipsR5900
{
    public class Assembler : IAssemblable
    {
        public string Assemble(string hexString)
        {
            var binary = new HexString(hexString).ToBinaryString();
            var instruction = GetInstruction(binary);

            var bitIndex = 0;
            var syntax = instruction.Syntax[0];
             
            foreach(var instructionArg in instruction.Args)
            {
                var argBinary = new BinaryString(binary.Value
                    .Substring(bitIndex, instructionArg.Size));

                var syntaxArg = GetSyntaxArgByBinary(instructionArg.Value, argBinary);

                if(syntaxArg != null)
                {
                    syntax = syntax.Replace(instructionArg.Value, syntaxArg);
                }

                bitIndex += instructionArg.Size;
            }

            return syntax.ToLower();
        }
        public string GetSyntaxArgByBinary(string placeholder, BinaryString argBinary)
        {
            if (LanguageDefinition.PlaceHolders.EERegisters.Contains(placeholder))
            {
                return LanguageDefinition.EERegisters
                    .Find(r => r.Binary == argBinary.Value)?.TextDisplay;
            }
            else if (LanguageDefinition.PlaceHolders.COP0Registers.Equals(placeholder))
            {
                return LanguageDefinition.COP0Registers
                    .Find(r => r.Binary == argBinary.Value)?.TextDisplay;
            }
            else if (LanguageDefinition.PlaceHolders.COP1Registers.Contains(placeholder))
            {
                return LanguageDefinition.COP1Registers
                    .Find(r => r.Binary == argBinary.Value)?.TextDisplay;
            }
            else if (LanguageDefinition.PlaceHolders.JType.Equals(placeholder))
            {
                return "$" + Convert.ToString(argBinary.ToInt() * 4, 16).PadLeft(8, '0');
            }
            else if (LanguageDefinition.PlaceHolders.IType.Contains(placeholder))
            {
                return "$" + argBinary.ToHexString().Value;
            }
            else if (LanguageDefinition.PlaceHolders.SA.Equals(placeholder))
            {
                return argBinary.ToInt().ToString();
            }
            else if (LanguageDefinition.PlaceHolders.Code.Equals(placeholder))
            {
                return argBinary.ToHexString().Value.PadLeft(5, '0');
            }
            else
            {
                return null;
            }
        }
        public Instruction GetInstruction(BinaryString binaryString)
        {
            return LanguageDefinition.Instructions.Find(instruction =>
            {
                var isMatch = true;

                for(var i = 0; i < 32; i++)
                {
                    if(instruction.Mask[i] == '1' && instruction.Binary[i] != binaryString.Value[i])
                    {
                        isMatch = false;
                        break;
                    }
                }

                return isMatch;
            });
        }
    }
}
