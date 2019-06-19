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
        public Random random;

        //details about the case
        public List<Person> persons;              //start with the victim
        public List<Scene> scenes;                //start with the scene of the crime
        public List<Item> items;                  //start with the murderweapon
        public int victim = 0;
        public int murderer = 1;
        public int murderWeapon = 0;
        public int crime = 0;                                           //crime scene

        //details about the player's progress in the investigation
        public int currentScene;                  //id of the scene the player is currently investigating
        public int currentPerson;                 //id of the person being spoken to
        public List<int> evidence;                //int is the id of the item taken
        public List<List<int>> prints;            //first int is id of person they come from, second int is the id of the finger the print comes from

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
            items.Add(GenerateMurderWeapon(seed + caseNumber, 0));
            //scenes.Add(new Scene(seed + caseNumber, 0));

            GenerateFamilialRelationships(0); //Victim's Family
            GenerateFriendNetworks(0);
            GenerateHouse(0); //generate the victim's house
            GenerateWorkPlace(0); //generate victim's place of work
            currentScene = crime;
            
            victim = 0;
            murderer = random.Next(1, persons.Count);
            murderWeapon = 0;
            crime = random.Next(0, scenes.Count);
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
            //throw new NotImplementedException();
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

            foreach (string type in t.connectionTypes)
            {
                try
                {
                    Template connection = io.GetRandomTemplate(SubstantiveType.scene, type, random.Next());
                    scenes.Add(new Scene(caseNumber, index, connection));
                    rPersonScene.Add(new RPersonScene(basePerson, index, RPersonSceneType.owns));
                    rInterScene.Add(new RInterScene(i, index, RInterSceneType.connectedTo));
                    index++;
                }
                catch(Exception e)
                {

                }
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
            string output = persons[victim].pronounDescriptive;
            output += " was found dead in ";
            List<Person> owners = GetSceneOwners(scenes[crime]);
            for(int i = 0; i < owners.Count; i++)
            {
                Person owner = owners[i];
                if(i > 0)
                {
                    output += " and ";
                }

                if (owner.name == persons[victim].name)
                {
                    output += persons[victim].pronounPossessive;
                    output += " ";
                }
                else
                {
                    output += owner.name;
                    output += "'s ";
                }
            }
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
            IEnumerable<Person> query =
                from person in persons
                join relationship in rPersonScene
                    on person.id equals relationship._is
                where relationship._of == scene.id 
                select person;

            List<Person> owners = query.ToList();
            return owners;
        }
    }
}
