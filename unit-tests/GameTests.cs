using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using homicide_detective.mechanics;

namespace unit_tests
{
    [TestClass]
    public class GameTests
    {
        List<Person> knownPersons;
        List<Item> knownItems;
        List<Scene> knownScenes;
        List<Case> knownActiveCases;
        List<Person> knownSolvedCases;
        List<Person> knownColdCases;
        List<Person> allPersons;
        GameText knownText;

        int knownCaseIndex;
        int knownSeed;
        string knownName;
        string knownDescription;

        public GameTests()
        {

        }

        [TestMethod]
        public void GameConstructorWithoutName()
        {
            Game game = new Game();
            Assert.AreEqual(null, game.detective);
            Assert.AreEqual(0, game.seed);
            Assert.AreEqual(false, game.debugMode);
            Assert.AreEqual(0, game.caseIndex);
            Assert.AreEqual(null, game.activeCases);
            Assert.AreEqual(null, game.coldCases);
            Assert.AreEqual(null, game.solvedCases);
        }

        [TestMethod]
        public void GameConstructorWithName()
        {
            Random random = new Random();
            string detectiveName = "test" + random.Next().ToString();
            Game game = new Game(detectiveName);

            Assert.AreEqual(detectiveName, game.detective);
        }

        [TestMethod]
        public void SaveGame()
        {
            Random random = new Random();
            string detectiveName = "test" + random.Next().ToString();
            Game game = new Game();

            game.detective = detectiveName;
            game.SaveGame();

            string json = JsonConvert.SerializeObject(game);

            //There is a file
            string saveFileLocation = Directory.GetCurrentDirectory() + @"\saves\" + detectiveName + ".json";

            Assert.IsTrue(File.Exists(saveFileLocation));
        }

        [TestMethod]
        public void LoadGame()
        {
            string detectiveName = "test";
            Game game = Game.LoadGame(detectiveName);

            Assert.AreEqual(detectiveName, game.detective);
            Assert.AreEqual(1372205, game.seed);
        }

        [TestMethod]
        public void SanitizeDetective()
        {

            Assert.AreEqual("MarjoryStJohnOneil", Game.SanitizeName("Marjory St. John-O'neil"));

        }

        //Not Implemented
        [TestMethod]
        public void LoadPersonFiles()
        {
            Game game = new Game();

            int i = 0;
            int j = 0;
            bool testResult = false;

            foreach (Person person in game.allPersons)
            {
                foreach (Person personKnown in knownPersons)
                {
                    throw new NotImplementedException();
                }
            }
            game.detective = "test";
            game.LoadItemFiles();

            //Assert.AreEqual(, game.LoadItemFiles());
        }

        //not Implemented
        [TestMethod]
        public void LoadSceneFiles()
        {
            Game game = new Game();

            int i = 0;
            int j = 0;
            bool testResult = false;
            foreach (Scene scene in game.allScenes)
            {
                foreach (Scene sceneKnown in knownScenes)
                {
                    throw new NotImplementedException();
                }
            }
            game.detective = "test";
            game.LoadItemFiles();

        }

        //Not Implemented
        [TestMethod]
        public void LoadItemFiles()
        {
            Game game = new Game();

            int i = 0;
            int j = 0;
            bool testResult = false;
            foreach (Person person in game.allPersons)
            {
                foreach (Person personKnown in knownPersons)
                {
                    throw new NotImplementedException();
                }
            }
            game.detective = "test";
            game.LoadItemFiles();

            //Assert.AreEqual(, game.LoadItemFiles());
        }

        //Not Impleneted
        [TestMethod]
        public void LoadTextFiles()
        {
            Game game = new Game();

            int i = 0;
            int j = 0;
            bool testResult = false;
            foreach (Person person in game.allPersons)
            {
                foreach (Person personKnown in knownPersons)
                {
                    throw new NotImplementedException();
                    testResult = (personKnown == game.allPersons[i]);

                    if (testResult)
                    {
                        j += 1;
                        testResult = false;
                    }
                }
            }

            game.LoadTextFiles();

        }
    }
}
