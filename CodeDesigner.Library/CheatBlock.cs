using CodeDesigner.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeDesigner.Library
{
    public class CheatBlock
    {
        public string Label { get; set; }
        public List<Cheat> Cheats { get; set; } = new List<Cheat>();

        public static CheatBlock Parse(string block)
        {
            var label = Regex.Match(block, "(#.{1,})\r\n", RegexOptions.IgnoreCase).Groups[1]?.Value;

            var cheatBlock = new CheatBlock() { Label = label };

            var cheatMatches = Regex.Matches(block, "([a-f0-9]{8}) ([a-f0-9]{8})", RegexOptions.IgnoreCase);

            var i = 0;

            foreach (Match match in cheatMatches)
            {
                var address = match.Groups[1].Value;

                var data = Enumerable.Range(0, match.Groups[2].Value.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(match.Groups[2].Value.Substring(x, 2), 16))
                    .ToArray();

                Array.Reverse(data);

                cheatBlock.Cheats.Add(new Cheat()
                {
                    Position = i,
                    Address = Convert.ToInt32(address.Substring(2, 6), 16),
                    CheatType = (CheatType)Convert.ToInt32(address.Substring(0, 2), 16),
                    Data = data
                });

                i++;
            }
            return cheatBlock;
        }
    }
}
