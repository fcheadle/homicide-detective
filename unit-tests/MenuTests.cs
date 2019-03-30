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
        //system variables
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
        static string personFolder = Directory.GetCurrentDirectory() + @"\objects\person";
        static string itemFolder = Directory.GetCurrentDirectory() + @"\objects\item";
        static string sceneFolder = Directory.GetCurrentDirectory() + @"\objects\scene";

        string[] personPaths = Directory.GetFiles(personFolder);
        string[] itemPaths = Directory.GetFiles(itemFolder);
        string[] scenePaths = Directory.GetFiles(sceneFolder);
        Menu.IO io = new Menu.IO(true);

        #region file stream example
        /*
        [TestMethod]
        public void SetOutTest()
        {
            //The new textwriter and reader
            TextWriter textWriter;// = new StreamWriter(saveFolder + "test.txt");
            TextReader textReader;// = new StreamReader(saveFolder + "test.txt");

            //save the original text writer and reader
            TextWriter textWriterOriginal = Console.Out;// = new StreamWriter(saveFolder + "test.txt");
            TextReader textReaderOriginal = Console.In;// = new StreamReader(saveFolder + "test.txt");

            //Set console.out to write to the file instead of the console
            FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            Console.SetOut(streamWriter);

            //Write test string to the file
            Console.WriteLine("this is a test");

            //Return the console output to the console and close the file
            Console.SetOut(textWriterOriginal);
            streamWriter.Close();

            //set console.in to read from the file
            fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            Console.SetIn(streamReader);

            //get test string from the file
            string outcome = Console.ReadLine();

            //return console.in to the console and close the file
            Console.SetIn(textReaderOriginal);
            streamReader.Close();

            //and test
            Assert.AreEqual("this is a test",outcome);
        }
        */
        #endregion

        public MenuTests()
        {
            io.debug = true;
        }
        [TestMethod]
        public void IOTest()
        {
            io.Send("abcdefg", true);
            Assert.AreEqual("abcdefg", io.Get(true));
        }

        [TestMethod]
        public void EvaluateMainMenuCommandTest()
        {
            Game game = new Game();
            game.debugMode = true;
            game = Menu.EvaluateMainMenuCommand("new", game, game.debugMode);
            Assert.AreEqual("What is your name, Detective?", game.detective);
        }

        [TestMethod]
        public void PrintMainMenuCommandsTest()
        {
            Menu.PrintMainMenuCommands();
            Assert.AreEqual("new | load | exit", io.Get());
        }

        [TestMethod]
        public void PrintTitleTest()
        {
            Menu.PrintTitle();
            Assert.AreEqual("Homicide Detective", io.Get());
        }

        [TestMethod]
        public void CaseMenuTest()
        {
            Game game = new Game();
            game.state = Menu.CaseMenu(game);
            string answer = "next case on the docket is";
            Assert.AreEqual(true, io.Get().Contains(answer));
        }

        [TestMethod]
        public void EvaluateCaseCommandTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCaseIfNullTest()
        {
            Game game = new Game("test");
            Menu.CreateCaseIfNull(game);
            Assert.AreEqual(2, game.activeCases.Count);
        }

        [TestMethod]
        public void PrintCaseMenuTest()
        {
            Menu.PrintMainMenuCommands();
            Assert.AreEqual("review | take | next | exit", io.Get());
        }

        [TestMethod]
        public void PrintCaseSynopsisTest()
        {
            Menu.PrintMainMenuCommands();
            Assert.AreEqual("new | load | exit", io.Get());
        }

        [TestMethod]
        public void CheatTest()
        {
            Game game = new Game();
            game.GenerateCase(game);
            Case gameCase = game.activeCases[0];
            Menu.Cheat(gameCase, true);
            string answer = "killed with";
            Assert.IsTrue(io.Get(true).Contains(answer));
        }

        [TestMethod]
        public void CrimeSceneMenuTest()
        {
            Menu.PrintMainMenuCommands();
            string answer = "look | take | dust | ";
            Assert.AreEqual(answer, io.Get());
        }

        [TestMethod]
        public void WitnessDialogueTest()
        {
            //This is a long way off still
            throw new NotImplementedException();
        }
    }
}
