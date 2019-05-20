using System;
using System.Collections.Generic;

namespace homicide_detective
{
    public class Item : Substantive
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
            Template template = new IO().GetRandomTemplate(SubstantiveType.item, random.Next());
            Generate(template, id);
        }

        public Item(int caseNumber, int id, Template template)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            Generate(template, id);
        }

        public override string ToString()
        {
            return name;
        }

        public override string Describe()
        {
            string output = "";
            output += aAn;
            output += name;
            output += ".";
            //output += volumeRange.GetVolumeDescription(volume);
            output += GetMassDescription(mass);
            output += ".";
            return output;
        }
    }
}
