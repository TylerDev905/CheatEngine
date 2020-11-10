using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.UnitTests
{
    [TestClass]
    public class ConsoleAppTestCases
    {
        private Process _process { get; set; }
            = new Process()
            {
                StartInfo =
                    {
                        FileName = Environment.CurrentDirectory + @"\..\..\..\CodeDesigner.ConsoleApp\bin\Debug\CodeDesigner.ConsoleApp.exe",
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        RedirectStandardError = true
                    }
            };
        [TestMethod]
        public void MipsR9000Assemble()
        {
            _process.StartInfo.Arguments = "MipsR9500 -a -h 24080001";
            _process.Start();
            var output = _process.StandardOutput.ReadLine();
            _process.Close();
            Assert.IsTrue(output == "addiu t0, zero, $0001");
        }
        [TestMethod]
        public void MipsR9000Disassemble()
        {
            _process.StartInfo.Arguments = @"MipsR9500 -d -o ""addiu t0, zero, $0001""";
            _process.Start();
            var output = _process.StandardOutput.ReadLine();
            _process.Close();
            Assert.IsTrue(output == "24080001");
        }
    }   
}
