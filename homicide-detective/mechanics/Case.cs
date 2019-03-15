using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Case
    {
        //non-static
        public int caseNumber;                         //A short number that can be combined with the detective's name to regenerate the same case
        public string status = "active";               //active, solved, or closed
        public Random random;                          //Must be idempotent

        public Person victim;                          //The person who was murdered
        public Person murderer;                        //The person who killed the victim
        public Person[] personsOfInterest;             //Murderer, Family of victim, witnesses, etc

        public Scene murderScene;                      //Where the person was killed
        public Scene whereTheyFoundTheBody;            //The first scene the detective arrives at
        public Scene[] placesOfInterest;               //All other relevant places

        public Item murderWeapon;                      //The item that killed the victim; may be blunt force trauma or drowning...?
        public Item[] allEvidence;                     //Every item including furniture at the scenes, the murder weapon, tire marks, wall damage, etc
        public Item[] evidenceTaken;                   //When an item is taken as evidence, it gets copied from all evidence to evidence taken

        public Case(int caseId, int seed)
        {
            caseNumber = caseId;
            random = new Random(caseNumber + seed);

            victim = new Person(random.Next());
            murderer = new Person(random.Next());

            murderScene = new Scene(random.Next());
            whereTheyFoundTheBody = new Scene(random.Next());

            murderWeapon = new Item(random.Next());

            //personsOfInterest = GeneratePersonsOfInterest(random.Next());
            //placesOfInterest = GeneratePlacesOfInterest(random.Next());
            //allEvidence = GenerateAllEvidence(random.Next());
        }

        private Item[] GenerateAllEvidence(int seed)
        {
            throw new NotImplementedException();
        }

        private Scene[] GeneratePlacesOfInterest(int seed)
        {
            throw new NotImplementedException();
        }

        private Person[] GeneratePersonsOfInterest(int seed)
        {
            throw new NotImplementedException();
        }
    }
}
