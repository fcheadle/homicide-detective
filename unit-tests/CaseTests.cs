using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;

namespace unit_tests
{
    [TestClass]
    public class CaseTests
    {
        [TestMethod]
        public void CaseConstructor()
        {
            int caseId = 99;
            int seed = 12345;

            homicide_detective.Case testCase = new Case(caseId, seed);

            homicide_detective.Person testVictim = new Person(); //get known values
            
            homicide_detective.Scene testMurderScene = new Scene(); // get known value

            homicide_detective.Item testMurderWeapon = new Item();

            Assert.AreEqual(caseId, testCase.caseNumber);
            //Assert.AreEqual(testVictim, testCase.victim);
            //Assert.AreEqual(testMurderScene, testCase.murderScene);
            //Assert.AreEqual(testMurderWeapon, testCase.murderWeapon);
        }

        [TestMethod]
        public void GenerateAllEvidence()
        {
            //Not yet Implemented
        }

        [TestMethod]
        public void GeneratePlacesOfInterest()
        {
            //not yet implemented
        }

        [TestMethod]
        public void GeneratePersonsOfInterest()
        {
            //not yet implemented
        }

    }
}
