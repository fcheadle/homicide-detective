using System;
using System.Collections.Generic;
using System.Linq;

namespace homicide_detective
{
    public class Case
    {
        /*
         * The CASE object is one murder.
         * It contains the who, what, and where
         * and their relationship to one another
         * 
         * It has overrides of ToString() that build the descriptions
         * Mostly, these should just call the ToString()s of the contained objects and return that.
         * 
         * These are idempotent based on seed, which is also it's id. 
         * once set, it should not be changed
         * 
         * */
         
        //public properties are saved to detective.json
        public int seed;
        public int caseNumber;
        public bool active = true;
        public bool cold = false;
        public bool solved = false;
        private Random random;

        //details about the case
        public List<Person> persons { get; internal set; }              //start with the victim
        public List<Scene> scenes { get; internal set; }                //start with the scene of the crime
        public List<Item> items { get; internal set; }                  //start with the murderweapon
        public int victim = 0;
        public int murderer = 1;
        public int murderWeapon = 0;
        public int crime = 0;                                           //crime scene

        //details about the player's progress in the investigation
        public int currentScene { get; internal set; }                  //id of the scene the player is currently investigating
        public int currentPerson { get; internal set; }                 //id of the person being spoken to
        public List<int> evidence { get; internal set; }                //int is the id of the item taken
        public List<List<int>> prints { get; internal set; }            //first int is id of person they come from, second int is the id of the finger the print comes from

        public List<RInterPerson> rInterPersonal = new List<RInterPerson>();    //all interpersonal relationships
        public List<RInterScene> rInterScene = new List<RInterScene>();         //all relationships between two scenes
        public List<RInterItem> rInterItem = new List<RInterItem>();            //all relationships between two items
        public List<RPersonScene> rPersonScene = new List<RPersonScene>();      //all relationships between people and scenes
        public List<RPersonItem> rPersonItem = new List<RPersonItem>();         //all relationships between people and items
        public List<RSceneItem> rSceneItem = new List<RSceneItem>();            //all relationships between scenes and items

        public Case()
        {
            //we need a blank constructor for JSONConvert
        }

        public Case (int seed, int caseNumber)
        {
            this.seed = seed;
            this.caseNumber = caseNumber;
            scenes = new List<Scene>();
            items = new List<Item>();
            persons = new List<Person>();
            Generate();
        }

        private void Generate()
        {
            IO io = new IO();
            if(random == null) random = new Random(seed);
            persons.Add(new Person(seed + caseNumber, 0));
            persons.Add(new Person(seed + caseNumber, 1));
            items.Add(GenerateMurderWeapon(seed + caseNumber, 0));
            scenes.Add(new Scene(seed + caseNumber, 0));

            victim = 0;
            murderer = 1;
            murderWeapon = 0;
            crime = 0;
            currentScene = 0;

            GenerateFamilialRelationships(0); //Victim's Family
            GenerateFriendNetworks(0);
            GenerateFamilialRelationships(1); //murderer's family
            GenerateHouse(0); //generate the victim's house
            GenerateWorkPlace(0); //generate victim's place of work
            currentScene = crime;
        }

        private void GenerateFriendNetworks(int basePerson)
        {
            int index = persons.Count;

            //coworkers
            while (random.Next() % 10 != 0)
            {
                persons.Add(new Person(seed + caseNumber, index));
                rInterPersonal.Add(new RInterPerson(basePerson, index, RInterPersonType.coworker));
                index++;
            }

            //friends
            while (random.Next() % 10 != 0)
            {
                persons.Add(new Person(seed + caseNumber, index));
                rInterPersonal.Add(new RInterPerson(basePerson, index, RInterPersonType.friend));
                index++;
            }
        }

        private void GenerateWorkPlace(int basePerson)
        {
            throw new NotImplementedException();
        }

        private void GenerateHouse(int basePerson)
        {
            int index = scenes.Count;
            int i = index;
            IO io = new IO();
            Template t = io.GetRandomTemplate(SubstantiveType.scene, "parlor", random.Next());
            Scene parlor = new Scene(seed + caseNumber, index, t);
            scenes.Add(parlor);
            rPersonScene.Add(new RPersonScene(basePerson, index, RPersonSceneType.owns));

            index++;

            foreach (string _c in t.classes)
            {
                Template connection = io.GetRandomTemplate(SubstantiveType.scene, _c, random.Next());
                rPersonScene.Add(new RPersonScene(basePerson, index, RPersonSceneType.owns));
                rInterScene.Add(new RInterScene(i, index, RInterSceneType.connectedTo));
                index++;
            }
        }

        private void GenerateFamilialRelationships(int basePerson)
        {
            int index = persons.Count;

            //mother
            persons.Add(new Person(seed + caseNumber, index, Gender.female));
            rInterPersonal.Add(new RInterPerson(basePerson, index, RInterPersonType.child));
            index++;

            //father
            persons.Add(new Person(seed + caseNumber, index, Gender.male));
            rInterPersonal.Add(new RInterPerson(basePerson, index, RInterPersonType.child));
            index++;

            //siblings
            while (random.Next() % 2 == 0)
            {
                persons.Add(new Person(seed + caseNumber, index));
                rInterPersonal.Add(new RInterPerson(basePerson, index, RInterPersonType.sibling));
                index++;
            }

            //aunt/uncle/cousin
            while(random.Next() % 10 != 0)
            {
                persons.Add(new Person(seed + caseNumber, index));
                rInterPersonal.Add(new RInterPerson(basePerson, index, RInterPersonType.distantFamily));
                index++;
            }
        }

        //returns an item that could be used to murder someone
        public Item GenerateMurderWeapon(int seed, int id)
        {
            random = new Random(seed);
            List<Template> possibleMurderWeapons = new List<Template>();
            IO io = new IO();
            foreach (Template _this in io.GetTemplates(SubstantiveType.item))
            {
                if (_this.classes.Contains("weapon")) possibleMurderWeapons.Add(_this);
            }

            int count = possibleMurderWeapons.Count;
            Template template = possibleMurderWeapons[random.Next(0, count)];
            Item item = new Item(caseNumber, items.Count, template);
            item.bloodSpatter = true;
            return item;
        }

        // returns something like, Alice was found dead in Mike's garage.
        public string Review()
        {
            //todo: move these hardcoded strings to the json
            string output = persons[victim].ToString();
            output += ", ";
            output += persons[victim].Describe();
            output += persons[victim].pronounDescriptive;
            output += " was found dead in";
            output += GetSceneOwners(scenes[crime]).ToString();
            output += "'s";
            output += scenes[crime].ToString();
            output += ".";

            if (evidence == null)
            {
                output += " There has been no evidence taken yet.";
            }
            else
            {
                output += " Evidence taken includes";
                foreach (int evidence in evidence)
                {
                    output += "{0}{1}" + items[evidence].aAn + items[evidence].name;
                }
            }

            return output;
        }

        // returns something like, Next on the docket is case number 000, Gracey Anderson. 
        public string Synopsis()
        {
            return "";
        }

        //tells you who owns a particular scene
        private List<Person> GetSceneOwners(Scene scene)
        {
            List<Person> owners = new List<Person>();
            var query =
                from person in persons
                join relationship in rInterPersonal
                    on person.id equals relationship._is
                select new { person };
            throw new NotImplementedException();
        }
    }
}
