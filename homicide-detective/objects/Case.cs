using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class Case
    {
        //non-static
        int caseNumber;                         //A short number that can be combined with the detective's name to regenerate the same case
        string status;                          //active, solved, or closed
        Random random;                          //Must be idempotent
        Person victim;                          //The person who was murdered
        Person murderer;                        //The person who killed the victim
        Person[] personsOfInterest;             //Murderer, Family of victim, witnesses, etc
        Scene murderScene;                      //Where the person was killed
        Scene whereTheyFoundTheBody;            //The first scene the detective arrives at
        Scene[] placesOfInterest;               //All other relevant places
        Item murderWeapon;                      //The item that killed the victim; may be blunt force trauma or drowning...?
        Item[] allEvidence;                     //Every item including furniture at the scenes, the murder weapon, tire marks, wall damage, etc
        Item[] evidenceTaken;                   //When an item is taken as evidence, it gets copied from all evidence to evidence taken

        public Case(int caseId, int seed)
        {
            caseNumber = caseId;
            random = new Random(caseNumber + seed);

            victim = new Person(random.Next());
            murderer = new Person(random.Next());
            murderScene = new Scene(random.Next());

            whereTheyFoundTheBody = new Scene(random.Next());
            murderWeapon = new Item(random.Next());

            personsOfInterest = GeneratePersonsOfInterest(random.Next());
            placesOfInterest = GeneratePlacesOfInterest(random.Next());
            allEvidence = GenerateAllEvidence(random.Next());
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
