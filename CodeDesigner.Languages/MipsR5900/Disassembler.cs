using System;
using System.Collections.Generic;
using System.Text;
using CodeDesigner.Languages.Extensions;
using CodeDesigner.Languages.MipsR5900.BaseTypes;
using CodeDesigner.Languages.Interfaces;

namespace CodeDesigner.Languages.MipsR5900
{
    public class Disassembler : IDisassemblable
    {
        public string Disassemble(string operationString)
        {
            var operation = SplitOperationString(operationString);

            var instruction = LanguageDefinition.Instructions.Find(i => {
                return i.TextDisplay.ToLower() == operation[0].ToLower();
            });

            if(instruction == null)
            {
                throw new Exception("The operation supplied does not match any known instruction definition.");
            }

            var binary = new BinaryString(instruction.Binary);

            var syntaxDefinition = new string[0];
            var isMatch = false;
            foreach (var syntax in instruction.Syntax)
            {
                syntaxDefinition = SplitOperationString(syntax);
                if(operation.Length == syntaxDefinition.Length)
                {
                    isMatch = true;
                    break;
                }
            }

            if (!isMatch)
            {
                throw new Exception($"The operation does not match any known syntax definition for instruction {instruction.TextDisplay}");
            }

            var bitIndex = 0;
            
            foreach (var arg in instruction.Args)
            {
                var definitionIndex = Array.FindIndex(syntaxDefinition, def => def == arg.Value);

                if (definitionIndex != -1)
                {
                    var argBinary = GetArgByValue(arg.Value, operation[definitionIndex]);

                    if (argBinary != null)
                    {
                        binary.Insert(bitIndex, argBinary.Value);
                    }
                }
                bitIndex += arg.Size;
            }

            return binary.ToHexString().Value;
        }
        public string[] SplitOperationString(string operation)
            => operation.Replace(",", " ")
                .Replace("(", " ")
                .Replace(")", " ")
                .Replace("  ", " ")
                .Replace("$", " ")
                .Split(" ");
        public BinaryString GetArgByValue(string placeholder, string textDisplay)
        {
            if (LanguageDefinition.PlaceHolders.EERegisters.Contains(placeholder))
            {
                var binary = LanguageDefinition.EERegisters
                    .Find(r => r.TextDisplay == textDisplay).Binary;

                return new BinaryString(binary);
            }
            else if (LanguageDefinition.PlaceHolders.COP0Registers.Equals(placeholder))
            {
                var binary = LanguageDefinition.COP0Registers
                    .Find(r => r.TextDisplay == textDisplay).Binary;

                return new BinaryString(binary);
            }
            else if (LanguageDefinition.PlaceHolders.COP1Registers.Contains(placeholder))
            {
                var binary = LanguageDefinition.COP1Registers
                    .Find(r => r.TextDisplay == textDisplay).Binary;

                return new BinaryString(binary);
            }
            else if (LanguageDefinition.PlaceHolders.JType.Equals(placeholder))
            {
                var hexString = new HexString(textDisplay);
                return new BinaryString(Convert.ToString(hexString.ToInt() / 4, 2).PadLeft(26, '0'));
            }
            else if (LanguageDefinition.PlaceHolders.IType.Contains(placeholder))
            {
                return new HexString(textDisplay).ToBinaryString();
            }
            else if (LanguageDefinition.PlaceHolders.SA.Equals(placeholder))
            {
                return new BinaryString(Convert.ToString(int.Parse(textDisplay), 2).PadLeft(5, '0' ));
            }
            else if (LanguageDefinition.PlaceHolders.Code.Equals(placeholder))
            {
                return new BinaryString(Convert.ToString(int.Parse(textDisplay), 2).PadLeft(20, '0'));
            }
            else
            {
                return null;
            }
        }
    }
}
