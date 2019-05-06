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

        [TestMethod]
        public void GenerateItemTest()
        {
            Random random = new Random(44); //arbitrary random seed
            Item item = new Item(random.Next(), 0);
            Assert.AreEqual(" handgun", item.name); //idempotent
            Assert.IsTrue(item.mass <= item.massRange.maximum);
            Assert.IsTrue(item.mass >= item.massRange.minimum);
            Assert.IsTrue(item.volume <= item.volumeRange.maximum);
            Assert.IsTrue(item.volume >= item.volumeRange.minimum);
        }

        [TestMethod]
        public void GenerateMurderWeaponTest()
        {
            Random random = new Random();
            Item item = new Item(random.Next(), 55);
            Case _case = new Case(44, 33);
            for(int i = 0; i < 10; i++)
            {
                item = _case.GenerateMurderWeapon(random.Next(), 12, new IO().GetItemTemplates());
                Assert.IsTrue(item.classes.Contains("weapon"));
            }
        }
    } 
}
