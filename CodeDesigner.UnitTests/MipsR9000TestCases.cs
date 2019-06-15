using CodeDesigner.Languages.MipsR5900;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.UnitTests
{
    [TestClass]
    public class MipsR9000TestCases
    {
        private Assembler _assembler { get; } = new Assembler();
        private Disassembler _disassembler { get; } = new Disassembler();

        private bool RunTest(string operationText)
        {
            var disassemblerOperation = _disassembler.Disassemble(operationText);
            var assembledOperation = _assembler.Assemble(disassemblerOperation);
            return operationText == assembledOperation;
        }

        [TestMethod]
        public void IType()
        {
            Assert.IsTrue(RunTest("lui a0, $0045"));
            Assert.IsTrue(RunTest("addiu a0, t0, $a0c0"));
            Assert.IsTrue(RunTest("sd ra, $0050(sp)"));
            Assert.IsTrue(RunTest("beq s4, zero, $0020"));
        }
        [TestMethod]
        public void JType()
        {
            Assert.IsTrue(RunTest("jal $01234568"));
            Assert.IsTrue(RunTest("j $01234568"));
            Assert.IsTrue(RunTest("jr ra"));
            Assert.IsTrue(RunTest("jalr v0"));
        }
        [TestMethod]
        public void RType()
        {
            Assert.IsTrue(RunTest("daddu s0, zero, zero"));
            Assert.IsTrue(RunTest("addu a0, v0, v1"));
            Assert.IsTrue(RunTest("slt v0, s0, s1"));
            Assert.IsTrue(RunTest("movn a1, v0, v1"));
        }
    }
}
