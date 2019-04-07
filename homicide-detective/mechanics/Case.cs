using System;
using System.Collections.Generic;
using System.Linq;

namespace homicide_detective
{
    public class Case
    {
        //non-static
        public int caseNumber;                         //A short number that can be combined with the detective's name to regenerate the same case
        public string status = "active";               //active, solved, or closed
        public Random random;                          //Must be idempotent
        public string description;                      //generated

        public Person victim;                          //The person who was murdered
        public Person murderer;                        //The person who killed the victim
        //public Person[] personsOfInterest;             //Murderer, Family of victim, witnesses, etc

        public Scene murderScene;                      //Where the person was killed
        public Scene whereTheyFoundTheBody;            //The first scene the detective arrives at
        public Scene currentScene;

        //public SceneTemplate[] placesOfInterest;               //All other relevant places

        public Item murderWeapon;                      //The item that killed the victim; may be blunt force trauma or drowning...?
        public Item allEvidence;                     //Every item including furniture at the scenes, the murder weapon, tire marks, wall damage, etc
        internal List<Item> evidenceTaken;
        internal List<Person.FingerPrint> printsTaken;
        public Item activeItem;                                 //the item currently in question
        public Person activePerson;                             //the person currently being looked at or something
        public Scene activeScene;                               //the scene currently being looked at


        public Case()
        {

        }

        public Case (int seed, int caseNum)
        {
            caseNumber = caseNum;
            GenerateCase(seed + caseNum);
        }

        public Case(Game game)
        {
            caseNumber = game.caseTaken;
            GenerateCase(caseNumber + game.seed);
        }

        public Case(Game game, int caseId)
        {
            caseNumber = caseId;
            GenerateCase(caseNumber + game.seed);
        }

        private void GenerateCase(int seed)
        {
            random = new Random(seed);
            victim = new Person(random.Next());
            murderer = new Person(random.Next());
            murderScene = new Scene(random.Next());
            murderWeapon = new Item(random.Next());
            activeScene = murderScene;
        }

        private ItemTemplate[] GenerateAllEvidence(int seed)
        {
            throw new NotImplementedException();
        }

        private SceneTemplate[] GeneratePlacesOfInterest(int seed)
        {
            throw new NotImplementedException();
        }

        private PersonTemplate[] GeneratePersonsOfInterest(int seed)
        {
            throw new NotImplementedException();
        }
    }
}
