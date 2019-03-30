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
        
        //public SceneTemplate[] placesOfInterest;               //All other relevant places

        public Item murderWeapon;                      //The item that killed the victim; may be blunt force trauma or drowning...?
        public Item allEvidence;                     //Every item including furniture at the scenes, the murder weapon, tire marks, wall damage, etc
        internal IEnumerable<Item> evidenceTaken;

        //public ItemTemplate[] evidenceTaken;                   //When an item is taken as evidence, it gets copied from all evidence to evidence taken

        public Case()
        {

        }

        public Case(Game game)
        {
            caseNumber = game.caseTaken;
            random = new Random(caseNumber + game.seed);
            GenerateCase(game);
        }

        public Case(Game game, int caseId)
        {
            caseNumber = caseId;
            random = new Random(caseNumber + game.seed);
            GenerateCase(game);
        }

        private void GenerateCase(Game game)
        {
            int seed = Base36.Decode(game.detective);
            victim = new Person(new Random(seed), game.allText);
            victim = victim.GeneratePerson(game.allText);
            murderer = new Person(new Random(seed + 1), game.allText);
            murderer = murderer.GeneratePerson(game.allText);

            murderScene = new Scene(random);
            murderScene = murderScene.GenerateScene(game.sceneTemplates);

            //whereTheyFoundTheBody = new SceneTemplate(random.Next());
            murderWeapon = new Item(random);
            murderWeapon = murderWeapon.GenerateMurderWeapon(random, game.itemTemplates);
            
            //personsOfInterest = GeneratePersonsOfInterest(random.Next());
            //placesOfInterest = GeneratePlacesOfInterest(random.Next());
            //allEvidence = GenerateAllEvidence(random.Next());
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
