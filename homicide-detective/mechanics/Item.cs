using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
namespace homicide_detective
{
    public class ItemTemplate
    {
        public string name;             //freetext
        public string description;      //freetext
        public float hollowness;        //in percent

        public Range massRanges;   //for determining mass of the object
        public Range volumeRanges; //for determining volume of the object

        public List<Shape> shapes = new List<Shape>();          //hardcoded specific values
        public List<string> classes = new List<string>();       //freetext
        public List<string> materials = new List<string>();     //freetext
        public List<string> containers = new List<string>();    //must be valid items from json
        public List<string> blocksViews = new List<string>();   //specific hardcoded values
        public List<string> visibleSides = new List<string>();   //specific hardcoded values

        public ItemTemplate()
        {
            name = "";
            description = "";
            massRanges = new Range();
            volumeRanges = new Range();
        }
    }

    public class Item : ItemTemplate
    {
        int id;
        public int volume;                  //in cm^3
        public int mass;                    //in grams
        public Shape shape;                 //shape of the item
        public string aAn;                //"a" or "an"

        public bool murderWeapon;           //if it murdered the victim
        public bool bloodSpatter;           //if it contains bloodspatter of the victim

        internal static Random random;                      //passed in during generation, to keep things idempotent

        public Item()
        {

        }

        public Item(int seed)
        {
            id = seed;
            random = new Random(id);
            //item = GenerateItem(random.Next(), new Text().itemTemplates);
            List<ItemTemplate> templates = new Text().itemTemplates;
            ItemTemplate template;
            int itemType = random.Next(0, templates.Count() - 1);
            template = templates[itemType];
            name = template.name;
            massRanges = template.massRanges;
            volumeRanges = template.volumeRanges;
            volume = Range.GetIntFromRange(random.Next(), template.volumeRanges);
            mass = Range.GetIntFromRange(random.Next(), template.massRanges);

            description = template.description;
            description += AddVolumeDescriptor(volume, template.volumeRanges);
            description += AddMassDescriptor(mass, template.massRanges);

            classes = template.classes;
            blocksViews = template.blocksViews;
            bloodSpatter = false; //not yet implemented
            containers = template.containers;
        }

        public Item(int seed, ItemTemplate template)
        {
            id = seed;
            random = new Random(id);
            name = template.name;
            massRanges = template.massRanges;
            volumeRanges = template.volumeRanges;
            volume = Range.GetIntFromRange(random.Next(), template.volumeRanges);
            mass = Range.GetIntFromRange(random.Next(), template.massRanges);

            description = template.description;
            description += AddVolumeDescriptor(volume, template.volumeRanges);
            description += AddMassDescriptor(mass, template.massRanges);

            classes = template.classes;
            blocksViews = template.blocksViews;
            bloodSpatter = false; //not yet implemented
            containers = template.containers;
        }
        public Item GenerateMurderWeapon(int seed, List<ItemTemplate> templates)
        {
            List<ItemTemplate> possibleMurderWeapons = new List<ItemTemplate>();

            foreach (ItemTemplate itemTemplate in templates)
            {
                if (itemTemplate.classes.Contains("weapon")) possibleMurderWeapons.Add(itemTemplate);
            }

            int count = possibleMurderWeapons.Count;
            ItemTemplate murderWeapon = possibleMurderWeapons[random.Next(0, count)];
            Item item = new Item(seed, murderWeapon);
            item.bloodSpatter = true;
            return item;
        }

        private string AddVolumeDescriptor(int value, Range range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;
            //itemDecsription = JsonConvert.DeserializeObject(path);


            //todo: get strings from the json
            if (value < range.mode - tenPercent - tenPercent)
            {
                return " It is much smaller than average";
            }
            else if (value < range.mode - tenPercent)
            {
                return " It is smaller than average";
            }
            else if (value <= range.mode)
            {
                return " It is slightly smaller than average";
            }
            else if (value >= range.mode + tenPercent + tenPercent)
            {
                return " It is much larger than average";
            }
            else if (value >= range.mode + tenPercent)
            {
                return " It is larger than average";
            }
            else if (value >= range.mode)
            {
                return " It is slightly larger than average";
            }
            else
            {
                return " It is average in size";
            }
        }

        private string AddMassDescriptor(int value, Range range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;

            if (value < range.mode - tenPercent - tenPercent)
            {
                return " and much less heavy than average.";
            }
            else if (value < range.mode - tenPercent)
            {
                return " and less heavy than average.";
            }
            else if (value < range.mode)
            {
                return " and slightly less heavy than average.";
            }
            else if (value == range.mode)
            {
                return " and is exactly average in weight.";
            }
            else if (value > range.mode + tenPercent + tenPercent)
            {
                return " and much heavier than average.";
            }
            else if (value > range.mode + tenPercent)
            {
                return " and heavier than average.";
            }
            else if (value > range.mode)
            {
                return " and slightly heavier than average.";
            }
            else
            {
                return "";
            }
        }

        internal object GetFingerprints()
        {
            throw new NotImplementedException();
        }

        internal List<Person.FingerPrint> GetFingerPrints()
        {
            throw new NotImplementedException();
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }
    }
}
