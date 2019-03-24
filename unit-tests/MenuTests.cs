using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace unit_tests
{
    [TestClass]
    public class MenuTests
    {
        public class TestInputRetriever : Menu.InputRetriever
        {
            // This should give you the idea...
            private string[] names = new string[] { "Foo", "Foo2" };
            private int index = 0;
            public override string Get(int index)
            {
                return names[index++];
            }
        }

        public class TestOutputRetriever : Menu.OutputSender
        {
            // This should give you the idea...
            private string[] names = new string[] { "Foo", "Foo2" };
            private int index = 0;
            public override void Send(string output)
            {
                
            }
        }

        [TestMethod]
        public void MainMenuTest()
        {
            //Can this be tested?
        }

        [TestMethod]
        public void EvaluateMainMenuCommandTest()
        {
            Game game = new Game();
            Console.WriteLine("this is async test text");
        }

        [TestMethod]
        public void PrintMainMenuCommandsTest()
        {

        }

        [TestMethod]
        public void PrintTitleTest()
        {

        }

        [TestMethod]
        public void CaseMenuTest()
        {

        }

        [TestMethod]
        public void EvaluateCaseCommandTest()
        {

        }

        [TestMethod]
        public void CreateCaseIfNullTest()
        {

        }

        [TestMethod]
        public void PrintCaseMenuTest()
        {

        }

        [TestMethod]
        public void PrintCaseSynopsisTest()
        {

        }

        [TestMethod]
        public void CheatTest()
        {

        }

        [TestMethod]
        public void CrimeScenemenuTest()
        {

        }

        [TestMethod]
        public void WitnessDialogueTest()
        {

        }
    }
}
