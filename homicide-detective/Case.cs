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

        //we need a blank constructor for JSONConvert :(
        public Case()
        {

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
            persons.Add(new Person(caseNumber, 0));
            items.Add(GenerateMurderWeapon(caseNumber, 0, io.GetItemTemplates()));
            scenes.Add(new Scene(caseNumber, 0));

            for (int i = 1; i < 5; i++)
            {
                persons.Add(new Person(caseNumber, i));
                items.Add(new Item(caseNumber, i));
                scenes.Add(new Scene(caseNumber, i));
            }

            currentScene = crime;
        }
        
        //returns an item that could be used to murder someone
        public Item GenerateMurderWeapon(int seed, int id, List<ItemTemplate> templates)
        {
            random = new Random(seed);
            List<ItemTemplate> possibleMurderWeapons = new List<ItemTemplate>();

            foreach (ItemTemplate itemTemplate in templates)
            {
                if (itemTemplate.classes.Contains("weapon")) possibleMurderWeapons.Add(itemTemplate);
            }

            int count = possibleMurderWeapons.Count;
            ItemTemplate murderWeapon = possibleMurderWeapons[random.Next(0, count)];
            Item item = new Item(caseNumber, items.Count, murderWeapon);
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
            output += GetSceneOwner(scenes[crime]).ToString();
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
            string output = Text.caseDescription.intro;
            output += caseNumber.ToString();
            output += ",";
            output += persons[victim].name;
            return output;
        }

        //tells you who owns a particular scene
        private List<Person> GetSceneOwner(Scene scene)
        {
            throw new NotImplementedException();
        }
    }
}
