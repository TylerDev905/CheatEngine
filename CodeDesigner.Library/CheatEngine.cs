using CodeDesigner.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeDesigner.Library
{
    public class CheatEngine
    {
        private CheatList _cheatList { get; set; }
        private IByteEditor _byteEditor { get; set; }
        private int _memoryOffset { get; set; } = 0x20000000;
        private List<CheatTimer> _cheatTimers { get; } = new List<CheatTimer>();

        public CheatEngine(IByteEditor byteEditor)
        {
            _byteEditor = byteEditor;
        }

        public CheatEngine(IByteEditor byteEditor, int memoryOffset = 0x00000000)
        {
            _byteEditor = byteEditor;
            _memoryOffset = memoryOffset;
        }

        public CheatEngine(IByteEditor byteEditor, CheatList cheatList)
        {
            _byteEditor = byteEditor;
            _cheatList = cheatList;
        }

        public CheatEngine(IByteEditor byteEditor, CheatList cheatList, int memoryOffset = 0x00000000)
        {
            _byteEditor = byteEditor;
            _cheatList = cheatList;
            _memoryOffset = memoryOffset;
        }

        public void PatchMemory(CheatList cheatList = null)
        {
            _cheatList = _cheatList ?? cheatList;

            foreach(var cheatBlock in _cheatList.Parse())
            {
                PatchMemory(cheatBlock);
            }
        }

        public void PatchMemory(CheatBlock cheatBlock)
        {
            for (var i = 0; i < cheatBlock.Cheats.Count;)
            {
                i = i + ApplyCheat(cheatBlock, cheatBlock.Cheats[i]);
            }
        }

        public int ApplyCheat(CheatBlock cheatBlock, Cheat cheat)
        {
            switch (cheat.CheatType)
            {
                case CheatType._8BitWrite:
                    return _8BitWrite(cheat);
                case CheatType._16BitWrite:
                    return _16BitWrite(cheat);
                case CheatType._32BitWrite:
                    return _32BitWrite(cheat);
                case CheatType._copyBytes:
                    return CopyBytes(cheatBlock, cheat);
                case CheatType._pointerWrite:
                    return PointerWrite(cheatBlock, cheat);
                case CheatType._timer:
                    return Timer(cheatBlock, cheat);
                case CheatType._32BitCondition:
                    return _32BitCondition(cheatBlock, cheat);
                case CheatType._16BitCondition:
                    return _16BitCondition(cheatBlock, cheat);
            }
            return 0;
        }

        public int CopyBytes(CheatBlock cheatBlock, Cheat cheat)
        {
            var copyLength = BitConverter.ToInt32(cheat.Data, 0);
            var bytesToCopy = _byteEditor.Read(cheat.Address + _memoryOffset, copyLength);
            _byteEditor.Write(cheatBlock.Cheats[cheat.Position + 1].Address + _memoryOffset, bytesToCopy);
            return 2;
        }

        public int Timer(CheatBlock cheatBlock, Cheat cheat)
        {
            var cheatTimer = _cheatTimers.SingleOrDefault(c => c.Cheat == cheat);

            if (cheatTimer == null)
            {
                var bytes = cheat.Data
                    .Take(2)
                    .ToArray();

                var timerIntervalType = (CheatTimerIntervalType)BitConverter.ToInt32(bytes, 0);

                var timerInterval = BitConverter.ToInt32(cheat.Data
                    .Skip(1)
                    .Take(3)
                    .ToArray(), 0);

                _cheatTimers.Add(new CheatTimer(cheat, timerIntervalType, timerInterval));
            }

            if (cheatTimer.IsIntervalCriteriaMet())
            {
                return 1;
            }

            var currentPosition = cheatBlock.Cheats.IndexOf(cheat);
            return cheatBlock.Cheats.Count;
        }

        public int PointerWrite(CheatBlock cheatBlock, Cheat cheat)
        {
            var pointer = Convert.ToInt32(_byteEditor.Read(cheat.Address + _memoryOffset, 0x08));

            return 0;
        }

        public int _8BitWrite(Cheat cheat)
        {
            _byteEditor.Write(cheat.Address + _memoryOffset, cheat.Data
                .Take(1)
                .ToArray());
            return 1;
        }

        public int _16BitWrite(Cheat cheat)
        {
            _byteEditor.Write(cheat.Address + _memoryOffset, cheat.Data
                .Take(2)
                .ToArray());
            return 1;
        }

        public int _32BitWrite(Cheat cheat)
        {
            _byteEditor.Write(cheat.Address + _memoryOffset, cheat.Data);
            return 1;
        }

        public int _16BitCondition(CheatBlock cheatBlock, Cheat cheat)
        {
            var data = _byteEditor
                .Read(cheat.Address + _memoryOffset, 0x04)
                .Take(2);

            var IsCriteriaMet = cheat.Data
                .Take(2)
                .SequenceEqual(data);

            return IsCriteriaMet ? 1 : cheatBlock.Cheats.Count;
        }

        public int _32BitCondition(CheatBlock cheatBlock, Cheat cheat)
        {
            var data = _byteEditor
                .Read(cheat.Address + _memoryOffset, 0x04);

            var IsCriteriaMet = cheat.Data
                .SequenceEqual(data);

            return IsCriteriaMet ? 1 : cheatBlock.Cheats.Count;
        }
    }
}
