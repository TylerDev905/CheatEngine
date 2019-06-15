using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CodeDesigner.Library.Extensions;
using CodeDesigner.Library.Enums;

namespace CodeDesigner.Library
{
    public class CheatList
    {
        private string _cheatListText;

        private List<CheatBlock> _cheatList { get; set; } = new List<CheatBlock>();

        public CheatList() { }

        public CheatList(string cheatList)
        {
            _cheatListText = cheatList;
        }
        
        public List<CheatBlock> Parse(string cheatList = null)
        {
            _cheatListText = _cheatListText ?? cheatList;

            var blocks = _cheatListText.Split("\r\n\r\n");
            
            foreach(var block in blocks)
            {
                _cheatList.Add(CheatBlock.Parse(block));
            }
            return _cheatList;
        }
    }
}
