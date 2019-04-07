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
        public void BookmarkCaseTest()
        {
            //not working
            Game game = new Game("deacon-smythe");
            Case thisCase = new Case(33, 4); //arbitrarily chose seed / casenumber
            game = Menu.BookmarkCase(thisCase, game);
            Assert.AreEqual(1, game.bookmarkedCases.Count);
            Assert.AreEqual(" Cara Niles", game.bookmarkedCases[0].victim.name);
            Assert.AreEqual(" Joseph Yarborough", game.bookmarkedCases[0].murderer.name);
        }
        #endregion

        #region print tests
        [TestMethod]
        public void PrintCSIMenuCommandsTest()
        {
            Menu.PrintMenuCommands(new Text.Menu(true).csi.ToList(), true);
            string result = io.Get(true);
            Assert.AreEqual("dust | leave | open | close | record | check | look | photograph | take", result);
        }

        [TestMethod]
        public void PrintCaseMenuCommandsTest()
        {
            Menu.PrintMenuCommands(new Text.Menu(true)._case.ToList(), true);
            string result = io.Get(true);
            Assert.AreEqual("take | review | next | bookmark | case", result);
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
        public void PrintCaseReviewTest()
        {
            Menu.PrintCaseReview(knownCase, true);
            string result = io.Get(true);
            string answer = " was found dead in";
            Assert.IsTrue(result.Contains(answer));
        }

        [TestMethod]
        public void CheatTest()
        {
            Game game = new Game("test");
            game.AddCase();
            Case thisCase = game.activeCase;
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
            Menu.PrintMenuCommands(new Text.Menu(true).main.ToList(),true);
            Assert.AreEqual("new | load | exit", io.Get(true));
        }
        #endregion

        #region evaluate tests
        [TestMethod]
        public void EvaluateMainMenuCommandNewTest()
        {
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("new", true);
            Assert.AreEqual("What is your name, Detective?", game.detective, true);
        }

        [TestMethod]
        public void EvaluateMainMenuCommandLoadTest()
        {
            //Hanging on some io.Get() call somewhere?
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("load", true);
            Assert.AreEqual("What is your name, Detective?", game.detective, true);
        }

        [TestMethod]
        public void EvaluateMainMenuCommandExitTest()
        {
            //not implemented
            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("exit", true);
            Assert.AreEqual(0, game.state);
        }

        [TestMethod]
        public void EvaluateCaseCommandNextTest()
        {
            Game game = new Game("deacon-smythe");
            game.caseTaken = 1;
            game = Menu.EvaluateCaseCommand(game, "next", true);
            Assert.AreEqual(2, game.caseTaken);
        }

        [TestMethod]
        public void EvaluateCaseCommandTakeTest()
        {
            Game game = new Game("deacon-smythe");
            game.state = 2; //case menu
            game.caseTaken = 1;
            game = Menu.EvaluateCaseCommand(game, "take", true);
            Assert.AreEqual(3, game.state);
            Assert.AreEqual(game.activeCase, game.activeCases[game.caseTaken]);
        }
        
        //[TestMethod]
        //public void EvaluateCSICommandLookTest()
        //{
        //    game = Menu.EvaluateCSICommand(game, "look");
        //    Assert.AreEqual(1, game.case);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandOpenTest()
        //{
        //    game = Menu.EvaluateCSICommand(game,"open");
        //    Assert.AreEqual(2, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandCloseTest()
        //{
        //    int result = Menu.EvaluateCSICommand("close");
        //    Assert.AreEqual(3, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandTakeTest()
        //{
        //    int result = Menu.EvaluateCSICommand("take");
        //    Assert.AreEqual(4, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandDustTest()
        //{
        //    int result = Menu.EvaluateCSICommand("dust");
        //    Assert.AreEqual(5, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandLeaveTest()
        //{
        //    int result = Menu.EvaluateCSICommand("leave");
        //    Assert.AreEqual(6, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandRecordTest()
        //{
        //    int result = Menu.EvaluateCSICommand("record");
        //    Assert.AreEqual(7, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandCheckTest()
        //{
        //    int result = Menu.EvaluateCSICommand("check");
        //    Assert.AreEqual(8, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandPhotographTest()
        //{
        //    int result = Menu.EvaluateCSICommand("photograph");
        //    Assert.AreEqual(9, result);
        //}

        //[TestMethod]
        //public void EvaluateCSICommandNegativeTest()
        //{
        //    int result = Menu.EvaluateCSICommand("negative");
        //    Assert.AreEqual(0, result);
        //}
        #endregion
    }
}
