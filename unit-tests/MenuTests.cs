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

        #region menu controllers
        [TestMethod]
        public void GetDetectiveTest()
        {
            string answer = "What is your name, Detective?";
            string result = Menu.GetDetective(true);
            Assert.AreEqual(answer, result);
        }

        [TestMethod]
        public void CreateCaseIfNullTest()
        {
            Game game = new Game("test");
            Menu.CreateCaseIfNull(game);
            Assert.AreEqual(2, game.activeCases.Count);
        }

        [TestMethod]
        public void BookmarkCaseTest()
        {
            Game game = new Game("deacon-smythe");
            Case thisCase = new Case();
            game = Menu.BookmarkCase(thisCase, game);
            Assert.AreEqual(1, game.bookmarkedCases.Count);
        }
        #endregion

        #region print tests
        [TestMethod]
        public void PrintCSIMenuCommandsTest()
        {
            Menu.PrintCSIMenuCommands(true);
            string answer = "look | photograph | take | ";
            string result = io.Get(true);
            Assert.IsTrue(result.Contains(answer));
        }

        [TestMethod]
        public void PrintCaseMenuCommandsTest()
        {
            Menu.PrintCaseMenuCommands(true);
            Assert.AreEqual("next | take | review | exit", io.Get(true));
        }

        [TestMethod]
        public void PrintCaseSynopsisTest()
        {
            Menu.PrintCaseSynopsis(knownCase, true);
            string result = io.Get(true);
            string answer = "The next case on the docket is case number 1, ";
            Assert.IsTrue(result.Contains(answer));
        }

        [TestMethod]
        public void PrintCaseIntroductionTest()
        {
            Menu.PrintCaseIntroduction(knownCase, true);
            string result = io.Get(true);
            string answer = " was found dead in";
            Assert.IsTrue(result.Contains(answer));
        }

        [TestMethod]
        public void PrintSceneSelectionTest()
        {
            Game game = new Game("deacon-smythe");
            Case thisCase = new Case(game, 1);
            Menu.PrintSceneSelection(thisCase, true);
            string answer = " Where will you investigate first?";
            Assert.AreEqual(answer, io.Get(true));
        }

        [TestMethod]
        public void PrintCrimeSceneReviewMenuTest()
        {
            Game game = new Game("deacon-smythe");
            Menu.PrintCaseReviewMenu(game, true);
            string answer = "bookmark | take | next | exit";
            Assert.AreEqual(answer, io.Get(true));
        }

        [TestMethod]
        public void CheatTest()
        {
            Game game = new Game("test");
            Case thisCase = game.GenerateCase(game,1);
            Menu.Cheat(thisCase, true);
            string answer = "killed";
            string result = io.Get(true);
            Assert.IsTrue(result.Contains(answer));

            answer = "with";
            Assert.IsTrue(result.Contains(answer));

            answer = "at";
            Assert.IsTrue((result.Contains(answer) || result.Contains("in")));
        }

        [TestMethod]
        public void PrintTitleTest()
        {
            Menu.PrintTitle(true);
            Assert.AreEqual("Whenever two objects interact, some evidence of that interaction can be found and verified.", io.Get(true));
        }

        [TestMethod]
        public void PrintMainMenuCommandsTest()
        {
            Menu.PrintMainMenuCommands(true);
            Assert.AreEqual("new | load | exit", io.Get(true));
        }
        #endregion

        #region evaluate tests
        [TestMethod]
        public void EvaluateMainMenuCommandNewTest()
        {
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("new", game, true);
            Assert.AreEqual("What is your name, Detective?", game.detective, true);
        }

        [TestMethod]
        public void EvaluateMainMenuCommandLoadTest()
        {
            //Hanging on some io.Get() call somewhere?
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("load", game, true);
            Assert.AreEqual("What is your name, Detective?", game.detective, true);
        }

        [TestMethod]
        public void EvaluateMainMenuCommandExitTest()
        {
            //not implemented
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("exit", game, true);
            Assert.AreEqual(0, game.state);
        }

        [TestMethod]
        public void EvaluateCaseCommandNextTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 1;
            game.caseTaken = Menu.EvaluateCaseCommand(game, "next", true);
            Assert.AreEqual(2, game.caseTaken);
        }

        [TestMethod]
        public void EvaluateCaseCommandTakeTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 1;
            game.caseTaken = Menu.EvaluateCaseCommand(game, "take", true);
            Assert.AreEqual(" Where will you investigate first?", io.Get(true));
        }

        [TestMethod]
        public void EvaluateCaseCommandReviewBookmarkTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 1;
            game = Menu.EvaluateCaseReviewCommand(game, "bookmark", true);
            Assert.AreEqual(1, game.bookmarkedCases.Count);
        }

        [TestMethod]
        public void EvaluateCaseCommandReviewNextTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 2;
            game = Menu.EvaluateCaseReviewCommand(game, "next", true);
            Assert.AreEqual(1, game.state);
            Assert.AreEqual(3, game.caseTaken);
        }

        [TestMethod]
        public void EvaluateCaseCommandReviewExitTest()
        {
            Game game = new Game("deacon-smythe");
            game.state = 5;
            game = Menu.EvaluateCaseReviewCommand(game, "exit", true);
            Assert.AreEqual(0, game.caseTaken);
        }

        [TestMethod]
        public void EvaluateCaseCommandExitTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 1;
            game.caseTaken = Menu.EvaluateCaseCommand(game, "exit", true);
            Assert.AreEqual(0, game.caseTaken);
        }
        #endregion
    }
}
