using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Item
    {
        //an item is specific to a case
        public string name;             //knife_003, bookshelf_005, etc
        public string description;      //generated from json

        public int volume;              //in cm^3
        public int mass;                //in grams

        public bool murderWeapon;       //if it murdered the victim

        public List<string> bloodSpatter;   //names of people whose blood is on the item;

        public Item()
        {

        }
    }
}
