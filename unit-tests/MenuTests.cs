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
        #region variables
        //system variables
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
        static string personFolder = Directory.GetCurrentDirectory() + @"\objects\person";
        static string itemFolder = Directory.GetCurrentDirectory() + @"\objects\item";
        static string sceneFolder = Directory.GetCurrentDirectory() + @"\objects\scene";

        string[] personPaths = Directory.GetFiles(personFolder);
        string[] itemPaths = Directory.GetFiles(itemFolder);
        string[] scenePaths = Directory.GetFiles(sceneFolder);
        IO io = new IO();
        Game game = new Game("deacon-smythe");          //test detective
        Case knownCase;                                 //the first case
        #endregion

        #region set up
        public MenuTests()
        {
            knownCase = new Case(game, 1);
        }
        #endregion

        #region print tests
        [TestMethod]
        public void CheatTest()
        {
            Game game = new Game("test");
            game.GenerateCase(game);
            Case gameCase = game.activeCases[0];
            Menu.Cheat(gameCase, true);
            string answer = "killed";
            string result = io.Get(true);
            Assert.IsTrue(result.Contains(answer));

            answer = "with";
            Assert.IsTrue(result.Contains(answer));

            answer = "at";
            Assert.IsTrue((result.Contains(answer) || result.Contains("in")));
        }

        [TestMethod]
        public void PrintMainMenuCommandsTest()
        {
            Menu.PrintMainMenuCommands(true);
            Assert.AreEqual("new | load | exit", io.Get(true));
        }

        [TestMethod]
        public void PrintTitleTest()
        {
            Menu.PrintTitle(true);
            Assert.AreEqual("Whenever two objects interact, some evidence of that interaction can be found and verified.", io.Get(true));
        }

        [TestMethod]
        public void PrintCaseMenuTest()
        {
            Menu.PrintCaseMenuCommands(true);
            Assert.AreEqual("next | take | review | exit", io.Get(true));
        }

        [TestMethod]
        public void PrintCaseSynopsisTest()
        {
            Menu.PrintCaseSynopsis(knownCase, true);
            string result = io.Get(true);
            string answer = "The next case on the docket is case number 1, Bert Sanchez";
            Assert.AreEqual(answer, result);
        }
        #endregion

        #region evaluate tests
        [TestMethod]
        public void EvaluateCaseCommandTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 1;
            game.caseTaken = Menu.EvaluateCaseCommand(game, "next", true);
            Assert.AreEqual(2, game.caseTaken);
        }

        [TestMethod]
        public void EvaluateMainMenuCommandTest()
        {
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("new", game, true);
            Assert.AreEqual("What is your name, Detective?", game.detective, true);
        }
        #endregion

        [TestMethod]
        public void CreateCaseIfNullTest()
        {
            Game game = new Game("test");
            Menu.CreateCaseIfNull(game);
            Assert.AreEqual(2, game.activeCases.Count);
        }

        [TestMethod]
        public void CrimeSceneMenuTest()
        {
            Menu.PrintCSIMenuCommands(true);
            string answer = "look | photograph | take | ";
            string result = io.Get(true);
            Assert.IsTrue(result.Contains(answer));
        }
    }
}
