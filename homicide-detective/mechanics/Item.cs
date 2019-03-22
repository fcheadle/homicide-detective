﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Item
    {
        //an item is specific to a case
        public string name;                 //knife_003, bookshelf_005, etc
        public string description;          //generated from json

        public int volume;                  //in cm^3
        public int mass;                    //in grams
        public Shape shape;                 //shape of the item
        
        public bool murderWeapon;           //if it murdered the victim
        public bool bloodSpatter;           //if it contains bloodspatter of the victim

        Random random;                      //passed in during generation, to keep things idempotent

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
            volume = PhysicalPropertyRange.GetIntFromRange(random, template.volume_ranges);
            mass = PhysicalPropertyRange.GetIntFromRange(random, template.mass_ranges);

            description = template.description;
            description += AddVolumeDescriptor(template.volume_ranges);
            description += AddMassDescriptor(template.mass_ranges);

            return this;
        }

        private string AddVolumeDescriptor(PhysicalPropertyRange range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;

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
