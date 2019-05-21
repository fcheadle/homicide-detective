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
        #region variable declaration
        
        //system variables
        string saveFolder = Directory.GetCurrentDirectory() + @"\save\";
        static string objectsFolder = Directory.GetCurrentDirectory() + @"\objects\";
        string extension = ".json";

        string[] personPaths = Directory.GetFiles(objectsFolder);
        string[] itemPaths = Directory.GetFiles(objectsFolder);
        string[] scenePaths = Directory.GetFiles(objectsFolder);

        //TODO: get an example file
        List<Template> knownPersons = new List<Template>();
        List<Template> knownItems = new List<Template>();
        List<Template> knownScenes = new List<Template>();
        List<Case> knownActiveCases = new List<Case>();
        List<Case> knownSolvedCases = new List<Case>();
        List<Case> knownColdCases = new List<Case>();

        Case knownCase;
        Person knownVictim;
        Person knownMurderer;
        string knownName = "test";

        #endregion
        
        public GameTests()
        {
            foreach (string person in personPaths)
            {
                knownPersons.Add(JsonConvert.DeserializeObject<Template>(File.ReadAllText(person)));
            }

            foreach (string item in itemPaths)
            {
                knownItems.Add(JsonConvert.DeserializeObject<Template>(File.ReadAllText(item)));
            }

            foreach (string scene in scenePaths)
            {
                knownScenes.Add(JsonConvert.DeserializeObject<Template>(File.ReadAllText(scene)));
            }
            
            knownVictim = new Person(1,0);
            knownVictim.nameGiven = " Chieko";
            knownVictim.nameFamily = "Sutton";
            knownVictim.name = knownVictim.nameGiven + " " + knownVictim.nameFamily;
            knownMurderer = new Person(1, 1);
            knownMurderer.nameGiven = " Cathie";
            knownMurderer.nameFamily = "Carson";
            knownMurderer.name = knownMurderer.nameGiven + " " + knownMurderer.nameFamily;
            knownCase = new Case(12345, 99);
        }

        [TestMethod]
        public void NewGameWithName()
        {
            Random random = new Random();
            Game game = new Game(knownName);

            Assert.AreEqual(knownName, game.detectiveName);
        }

        [TestMethod]
        public void SanitizeNameTest()
        {
            Assert.AreEqual("MarjoryStJohnOneil", Game.Sanitize("Marjory St. John-O'neil"));
        }
        
        [TestMethod]
        public void LoadGame()
        {
            IO io = new IO();
            Game game = io.Load(knownName);
            Assert.AreEqual(knownName, game.detectiveName);
        }
    }
}
