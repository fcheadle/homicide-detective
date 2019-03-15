using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using Newtonsoft.Json;
using System.IO;
using System;

namespace unit_tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameConstructorWithoutName()
        {
            homicide_detective.Game game = new Game();
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
            homicide_detective.Game game = new Game(detectiveName);

            Assert.AreEqual(detectiveName, game.detective);
        }

        [TestMethod]
        public void SaveGame()
        {
            Random random = new Random();
            string detectiveName = "test" + random.Next().ToString();
            homicide_detective.Game game = new Game(detectiveName);

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
            homicide_detective.Game game = homicide_detective.Game.LoadGame(detectiveName);

            Assert.AreEqual(detectiveName, game.detective);
            Assert.AreEqual(1372205, game.seed);
        }

        [TestMethod]
        public void SanitizeDetective()
        {
            Assert.AreEqual("MarjoryStJohnOneil", homicide_detective.Game.SanitizeName("Marjory St. John-O'neil"));
        }

        [TestMethod]
        public void LoadPersonFiles()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void LoadSceneFiles()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void LoadItemFiles()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void LoadTextFiles()
        {
            throw new NotImplementedException();
        }
    }
}
