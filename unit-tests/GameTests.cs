using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;

namespace unit_tests
{
    [TestClass]
    public class GameTests
    {
        //system variables
        string saveFolder = Directory.GetCurrentDirectory() + @"\save\";
        static string personFolder = Directory.GetCurrentDirectory() + @"\objects\person";
        static string itemFolder = Directory.GetCurrentDirectory() + @"\objects\item";
        static string sceneFolder = Directory.GetCurrentDirectory() + @"\objects\scene";

        string[] personPaths = Directory.GetFiles(personFolder);
        string[] itemPaths = Directory.GetFiles(itemFolder);
        string[] scenePaths = Directory.GetFiles(sceneFolder);

        //TODO: get an example file
        List<PersonTemplate> knownPersons = new List<PersonTemplate>();
        List<ItemTemplate> knownItems = new List<ItemTemplate>();
        List<SceneTemplate> knownScenes = new List<SceneTemplate>();
        List<Case> knownActiveCases = new List<Case>();
        List<Case> knownSolvedCases = new List<Case>();
        List<Case> knownColdCases = new List<Case>();
        
        GameText knownText = new GameText();

        int knownCaseIndex;
        int knownSeed = 1372205;
        string knownName = "test";
        string knownDescription;

        public GameTests()
        {
            foreach (string person in personPaths)
            {
                knownPersons.Add(JsonConvert.DeserializeObject<PersonTemplate>(File.ReadAllText(person)));
            }

            foreach (string item in itemPaths)
            {
                knownItems.Add(JsonConvert.DeserializeObject<ItemTemplate>(File.ReadAllText(item)));
            }

            foreach (string scene in scenePaths)
            {
                knownScenes.Add(JsonConvert.DeserializeObject<SceneTemplate>(File.ReadAllText(scene)));
            }
        }

        [TestMethod]
        public void NewGameWithName()
        {
            Random random = new Random();
            Game game = new Game(knownName);

            Assert.AreEqual(knownName, game.detective);
        }

        [TestMethod]
        public void SaveGame()
        {
            Random random = new Random();
            Game game = new Game();

            game.detective = knownName;
            game.seed = knownSeed;
            game.SaveGame();

            string json = JsonConvert.SerializeObject(game);

            //There is a file
            string saveFileLocation = Directory.GetCurrentDirectory() + @"\saves\" + knownName + ".json";

            Assert.IsTrue(File.Exists(saveFileLocation));
        }

        [TestMethod]
        public void LoadGame()
        {
            Game game = Game.LoadGame(knownName);
            Assert.AreEqual(knownName, game.detective);
            Assert.AreEqual(knownSeed, game.seed);
        }

        [TestMethod]
        public void SanitizeDetective()
        {
            Assert.AreEqual("MarjoryStJohnOneil", Game.SanitizeName("Marjory St. John-O'neil"));
        }
        
        [TestMethod]
        public void LoadPersonFiles()
        {
            Game game = new Game();

            bool testResult = false;
            game.personTemplates = game.LoadPersonFiles();
            foreach (PersonTemplate person in game.personTemplates)
            {
                foreach (PersonTemplate knownPerson in knownPersons)
                {
                    if (knownPerson.name == person.name)
                        if(knownPerson.description == person.description)
                            testResult = true;
                }
            }

            Assert.AreEqual(true, testResult);
        }

        [TestMethod]
        public void LoadSceneFiles()
        {
            Game game = new Game();

            bool testResult = false;
            game.sceneTemplates = game.LoadSceneFiles();
            foreach (SceneTemplate scene in game.sceneTemplates)
            {
                foreach (SceneTemplate knownScene in knownScenes)
                {
                    if (knownScene.name == scene.name)
                        if (knownScene.description == scene.description)
                            testResult = true;
                }
            }

            Assert.AreEqual(true, testResult);
        }

        [TestMethod]
        public void LoadItemFiles()
        {
            Game game = new Game();

            bool testResult = false;
            game.itemTemplates = game.LoadItemFiles();
            foreach (ItemTemplate item in game.itemTemplates)
            {
                foreach (ItemTemplate knownItem in knownItems)
                {
                    if (knownItem.name == item.name)
                        if (knownItem.description == item.description)
                            testResult = true;
                }
            }

            Assert.AreEqual(true, testResult);
        }

        [TestMethod]
        public void LoadTextFiles()
        {
            Game game = new Game();
            game.allText = game.LoadTextFiles();
            Assert.IsInstanceOfType(game.allText, knownText.GetType());
        }
    }
}
