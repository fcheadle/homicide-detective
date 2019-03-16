using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Item
    {
        //different pieces of evidence
        //or furniture or red herrings
        //or murder weapons

        public string name;             //freetext, and unique identifier
        public string description;      //freetext
        public float hollowness;        //in percent
        public int mass;                //in grams
        public int volume;              //in cm cubed

        public PhysicalPropertyRange mass_ranges;  
        public PhysicalPropertyRange volume_ranges;

        public List<Shape> shapes = new List<Shape>();          //hardcoded specific values
        public List<string> classes = new List<string>();       //freetext
        public List<string> materials = new List<string>();     //freetext
        public List<string> containers = new List<string>();    //must be valid items from json
        public List<string> blocksViews = new List<string>();   //specific hardcoded values
        public List<string> visiblSides = new List<string>();   //specific hardcoded values

        public Item()
        {
            name = "";
            description = "";
            mass_ranges = new PhysicalPropertyRange();
            volume_ranges = new PhysicalPropertyRange();
        }

        public Item(int seed)
        {

        }
    }
}
