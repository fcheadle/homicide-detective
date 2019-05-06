using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using System.IO;

namespace unit_tests
{
    [TestClass]
    public class IOTests
    {
        IO io = new IO();
        static string saveFolder = Directory.GetCurrentDirectory() + @"\save\";
        static string testFile = saveFolder + "test";

        [TestMethod]
        public void IOGetTest()
        {
            File.WriteAllText(testFile, "this is my io.Get() Test");
            string result = io.Read(true); // debug = true
            Assert.AreEqual("this is my io.Get() Test", result);
        }

        [TestMethod]
        public void IOSendTest()
        {
            io.Write("this is my io.send() test", true); // true = debug
            Assert.AreEqual("this is my io.send() test", File.ReadAllText(testFile));
        }

        [TestMethod]
        public void IOSendLineOutputTest()
        {
            io.WriteLine("this is my io.send() test", true); // true = debug
            Assert.AreEqual("this is my io.send() test", File.ReadAllText(testFile));
        }

        [TestMethod]
        public void IOSendLineOutputNameTest()
        {
            io.WriteLine("this is my {0} test", "io.send()", true); // true = debug
            Assert.AreEqual("this is my io.send() test", File.ReadAllText(testFile));
        }

        [TestMethod]
        public void IOSendLineOutputAAnNameTest()
        {
            io.WriteLine("this is my {0} {1}", "io.send()", "test", true); // true = debug
            Assert.AreEqual("this is my io.send() test", File.ReadAllText(testFile));
        }
    }
}
