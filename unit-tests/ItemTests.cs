using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace unit_tests
{
    [TestClass]
    public class ItemTests
    {
        static string itemFolder = Directory.GetCurrentDirectory() + @"\objects\";
        string[] itemPaths = Directory.GetFiles(itemFolder);
        Text text = new Text();
        List<ItemTemplate> itemTemplates = new List<ItemTemplate>();

        [TestMethod]
        public void GenerateItemTest()
        {
            Random random = new Random(44); //arbitrary random seed
            Item item = new Item();
            item = item.GenerateItem(random.Next(), text.itemTemplates);
            Assert.AreEqual("handgun", item.name); //idempotent
            Assert.IsTrue(item.mass <= item.massRanges.maximum);
            Assert.IsTrue(item.mass >= item.massRanges.minimum);
            Assert.IsTrue(item.volume <= item.volumeRanges.maximum);
            Assert.IsTrue(item.volume >= item.volumeRanges.minimum);
        }

        [TestMethod]
        public void GenerateMurderWeaponTest()
        {
            Random random = new Random();
            Item item = new Item(random.Next());
            for(int i = 0; i < 10; i++)
            {
                item = item.GenerateMurderWeapon(random.Next(), text.itemTemplates);
                Assert.IsTrue(item.classes.Contains("weapon"));
            }

        }
    } 
}
