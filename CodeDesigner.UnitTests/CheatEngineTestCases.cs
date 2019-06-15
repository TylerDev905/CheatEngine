using System;
using CodeDesigner.Library;
using CodeDesigner.Library.Editors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDesigner.UnitTests
{
    [TestClass]
    public class CheatEngineTestCases
    {
        public MockMemoryEditor MockMemoryEditor { get; set; }
        public CheatEngine CheatEngine { get; set; }
        
        public CheatEngineTestCases()
        {
            MockMemoryEditor = new MockMemoryEditor();
            CheatEngine = new CheatEngine(MockMemoryEditor, 0x00000000);
        }

        public void IsCheatApplied(byte[] cheatBytes, byte[] appliedBytes)
        {
            for(var i = 0; i < cheatBytes.Length; i++)
            {
                if (cheatBytes[i] != appliedBytes[i])
                {
                    throw new Exception("The bytes do not match");
                }
            }
        }

        [TestMethod]
        public void _8BitWrite()
        {
            var cheatBlock = CheatBlock.Parse("00000000 00000011");

            CheatEngine.PatchMemory(cheatBlock);

            var buffer = MockMemoryEditor.Read(0x00000000, 0x04);

            IsCheatApplied(cheatBlock.Cheats[0].Data, buffer);
        }

        [TestMethod]
        public void _16BitWrite()
        {
            var cheatBlock = CheatBlock.Parse("10000010 00001122");

            CheatEngine.PatchMemory(cheatBlock);

            var buffer = MockMemoryEditor.Read(0x00000010, 0x04);

            IsCheatApplied(cheatBlock.Cheats[0].Data, buffer);
        }

        [TestMethod]
        public void _32BitWrite()
        {
            var cheatBlock = CheatBlock.Parse("20000020 11223344");

            CheatEngine.PatchMemory(cheatBlock);

            var buffer = MockMemoryEditor.Read(0x00000020, 0x04);

            IsCheatApplied(cheatBlock.Cheats[0].Data, buffer);
        }

        [TestMethod]
        public void CopyBytes()
        {
            var cheatBlock = CheatBlock.Parse("20000020 11223344\n\r" + "20000024 55667788");
            CheatEngine.PatchMemory(cheatBlock);

            CheatEngine.PatchMemory(CheatBlock.Parse("50000020 00000008\n\r" + "00000030 00000000"));

            var buffer = MockMemoryEditor.Read(0x00000030, 0x08);

            IsCheatApplied(cheatBlock.Cheats[0].Data, buffer);
        }

        [TestMethod]
        public void _16BitCondition()
        {
            CheatEngine.PatchMemory(CheatBlock.Parse("20000020 00001111"));

            var cheatBlock = CheatBlock.Parse("D0000020 00001111\n\r" + "20000024 55667788" + "20000028 11223344");

            CheatEngine.PatchMemory(cheatBlock);

            var buffer = MockMemoryEditor.Read(0x00000024, 0x04);
            IsCheatApplied(cheatBlock.Cheats[1].Data, buffer);

            buffer = MockMemoryEditor.Read(0x00000028, 0x04);
            IsCheatApplied(cheatBlock.Cheats[2].Data, buffer);
        }

        [TestMethod]
        public void _32BitCondition()
        {
            CheatEngine.PatchMemory(CheatBlock.Parse("20000020 11111111"));

            var cheatBlock = CheatBlock.Parse("C0000020 11111111\n\r" + "20000024 55667788" + "20000028 11223344");

            CheatEngine.PatchMemory(cheatBlock);

            var buffer = MockMemoryEditor.Read(0x00000024, 0x04);
            IsCheatApplied(cheatBlock.Cheats[1].Data, buffer);

            buffer = MockMemoryEditor.Read(0x00000028, 0x04);
            IsCheatApplied(cheatBlock.Cheats[2].Data, buffer);
        }
    }
}
