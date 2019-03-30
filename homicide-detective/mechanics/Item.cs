using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class ItemTemplate
    {
        //different pieces of evidence
        //or furniture or red herrings
        //or murder weapons

        public string id;               //unique identifier
        public string name;             //freetext
        public string description;      //freetext
        public float hollowness;        //in percent

        public PhysicalPropertyRange massRanges;   //for determining mass of the object
        public PhysicalPropertyRange volumeRanges; //for determining volume of the object

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
            massRanges = new PhysicalPropertyRange();
            volumeRanges = new PhysicalPropertyRange();
        }
    }

    public class Item : ItemTemplate
    {
        public int volume;                  //in cm^3
        public int mass;                    //in grams
        public Shape shape;                 //shape of the item

        public bool murderWeapon;           //if it murdered the victim
        public bool bloodSpatter;           //if it contains bloodspatter of the victim

        Random random;                      //passed in during generation, to keep things idempotent
        internal string aAn;                //"a" or "an"

        public Item()
        {

        }

        public Item(Random rand)
        {
            random = rand;
        }

        public Item GenerateItem(Random seedRandom, List<ItemTemplate> items)
        {
            random = seedRandom;
            int itemType = random.Next(0, items.Count() - 1);
            
            ItemTemplate template = items[itemType];
            name = template.name;
            //volume = PhysicalPropertyRange.GetFromRange(random, template.volumeRanges);
            //mass = PhysicalPropertyRange.GetIntFromRange(random, template.massRanges);

            description = template.description;
            description += AddVolumeDescriptor(template.volumeRanges);
            description += AddMassDescriptor(template.massRanges);

            return this;
        }

        public Item GenerateMurderWeapon(Random seedRandom, List<ItemTemplate> templates)
        {
            List<ItemTemplate> possibleMurderWeapons = new List<ItemTemplate>();

            foreach (ItemTemplate itemTemplate in templates)
            {
                if (!itemTemplate.classes.Contains("furniture")) possibleMurderWeapons.Add(itemTemplate);
            }

            random = seedRandom;
            int itemType = random.Next(0, possibleMurderWeapons.Count() - 1);

            ItemTemplate template = possibleMurderWeapons[itemType];
            name = template.name;
            //volume = PhysicalPropertyRange.GetIntFromRange(random, template.volumeRanges);
            //mass = PhysicalPropertyRange.GetIntFromRange(random, template.massRanges);

            description = template.description;
            description += AddVolumeDescriptor(template.volumeRanges);
            description += AddMassDescriptor(template.massRanges);

            return this;
        }

        private string AddVolumeDescriptor(PhysicalPropertyRange range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;
            //itemDecsription = JsonConvert.DeserializeObject(path);


            //todo: get strings from the json
            if (volume < range.mode - tenPercent - tenPercent)
            {
                return " It is much smaller than average";
            }
            else if (volume < range.mode - tenPercent)
            {
                return " It is smaller than average";
            }
            else if (volume < range.mode)
            {
                return " It is slightly smaller than average";
            }
            else if (volume > range.mode + tenPercent + tenPercent)
            {
                return " It is much larger than average";
            }
            else if (volume > range.mode + tenPercent)
            {
                return " It is larger than average";
            }
            else if (volume > range.mode)
            {
                return " It is slightly larger than average";
            }
            else
            {
                return " It is exactly average in size";
            }
        }

        private string AddMassDescriptor(PhysicalPropertyRange range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;

            if (volume < range.mode - tenPercent - tenPercent)
            {
                return " and much less heavy than average.";
            }
            else if (volume < range.mode - tenPercent)
            {
                return " and less heavy than average.";
            }
            else if (volume < range.mode)
            {
                return " and slightly less heavy than average.";
            }
            else if (volume == range.mode)
            {
                return " and is exactly average in weight.";
            }
            else if (volume > range.mode + tenPercent + tenPercent)
            {
                return " and much heavier than average.";
            }
            else if (volume > range.mode + tenPercent)
            {
                return " and heavier than average.";
            }
            else if (volume > range.mode)
            {
                return " and slightly heavier than average.";
            }
            else
            {
                return "";
            }
        }
    }
}
