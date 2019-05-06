using System;
using System.Collections.Generic;

namespace homicide_detective
{
    //A base class for items to be read from json templates
    public class ItemTemplate
    {
        public string name;             //freetext
        public string description;      //freetext
        public Range massRange;
        public Range volumeRange;
        public List<Shape> shapes = new List<Shape>();          //hardcoded specific values
        public List<string> classes = new List<string>();       //freetext
        public List<string> materials = new List<string>();     //freetext
        public List<string> containers = new List<string>();    //must be valid items from json
        public List<string> blocksViews = new List<string>();   //specific hardcoded values
        public List<string> visibleSides = new List<string>();  //specific hardcoded values
        public int hollowness = 100;                            //percent
    }

    public class Item : ItemTemplate
    {
        public int id;
        public int caseNumber;
        internal string aAn;                  //"a" or "an"
        internal Shape shape;                 //shape of the item
        internal bool murderWeapon;           //if it murdered the victim
        internal bool bloodSpatter;           //if it contains bloodspatter of the victim
        internal Random random;               //passed in during generation, to keep things idempotent
        public int mass;
        public int volume;

        public Item()
        {

        }

        public Item(int caseNumber, int id)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            ItemTemplate template = new IO().GetRandomItemTemplate(random.Next());
            Generate(template);
        }

        public Item(int caseNumber, int id, ItemTemplate template)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            Generate(template);
        }

        internal void Generate(ItemTemplate template)
        {
            name = template.name;
            massRange = template.massRange;
            volumeRange = template.volumeRange;
            volume = volumeRange.GenerateFromRange(random.Next());
            mass = massRange.GenerateFromRange(random.Next());
            
            classes = template.classes;
            blocksViews = template.blocksViews;
            bloodSpatter = false; //not yet implemented
            containers = template.containers;
            List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
            if (vowels.Contains(name[1]))
            {
                aAn = " an";
            }
            else
            {
                aAn = " a";
            }
        }

        public override string ToString()
        {
            return name;
        }

        public string Describe()
        {
            string output = "";
            output += aAn;
            output += name;
            output += ".";
            output += volumeRange.GetVolumeDescription(volume);
            output += massRange.GetMassDescription(mass);
            output += ".";
            return output;
        }
    }
}
