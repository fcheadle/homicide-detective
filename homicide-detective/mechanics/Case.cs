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

        public PersonTemplate victim;                          //The person who was murdered
        public PersonTemplate murderer;                        //The person who killed the victim
        public PersonTemplate[] personsOfInterest;             //Murderer, Family of victim, witnesses, etc

        public SceneTemplate murderScene;                      //Where the person was killed
        public SceneTemplate whereTheyFoundTheBody;            //The first scene the detective arrives at
        public SceneTemplate[] placesOfInterest;               //All other relevant places

        public ItemTemplate murderWeapon;                      //The item that killed the victim; may be blunt force trauma or drowning...?
        public ItemTemplate[] allEvidence;                     //Every item including furniture at the scenes, the murder weapon, tire marks, wall damage, etc
        public ItemTemplate[] evidenceTaken;                   //When an item is taken as evidence, it gets copied from all evidence to evidence taken

        public Case(int caseId, int seed)
        {
            caseNumber = caseId;
            random = new Random(caseNumber + seed);

            //victim = new PersonTemplate(random.Next());
            //murderer = new PersonTemplate(random.Next());
            //
            //murderScene = new SceneTemplate(random.Next());
            //whereTheyFoundTheBody = new SceneTemplate(random.Next());
            //
            //murderWeapon = new ItemTemplate(random.Next());

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
