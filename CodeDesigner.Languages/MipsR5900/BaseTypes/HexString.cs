using CodeDesigner.Languages.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeDesigner.Languages.MipsR5900.BaseTypes
{
    public class HexString
    {
        private string _value { get; set; }
        public string Value { get { return _value; } }
        public HexString(string hexString)
        {
            _value = hexString;
        }
        public void Insert(int index, string hexString)
        {
            _value = _value.ReplaceAtIndex(index, hexString);
        }          
        public int ToInt()
            => Convert.ToInt32(_value, 16);
        public BinaryString ToBinaryString()
        {
            var binaryString = Convert.ToString(this.ToInt(), 2); 
            var test = new BinaryString(binaryString.PadLeft(_value.Length * 4, '0'));
            return test;
        }
    }
}
