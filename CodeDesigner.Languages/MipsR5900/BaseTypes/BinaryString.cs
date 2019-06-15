using System;
using System.Collections.Generic;
using System.Text;
using CodeDesigner.Languages.Extensions;

namespace CodeDesigner.Languages.MipsR5900.BaseTypes
{
    public class BinaryString
    {
        private string _value { get; set; }
        public string Value { get { return _value; } }
        public BinaryString(string binaryString)
        {
            _value = binaryString;
        }
        public void Insert(int index, string binaryString)
        {
            _value = _value.ReplaceAtIndex(index, binaryString);
        }
        public int ToInt()
            => Convert.ToInt32(_value, 2);
        public HexString ToHexString()
        {
            var hexString = Convert.ToString(this.ToInt(), 16);
            return new HexString(hexString.PadLeft(_value.Length / 4, '0'));
        }
    }
}
