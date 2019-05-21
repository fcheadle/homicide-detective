using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Template
    {
        public string name;             //freetext
        public string description;      //freetext
        public SubstantiveType type;
        public Range massRange;
        public Range heightRange;
        public Range lengthRange;
        public Range widthRange;
        public List<Shape> shapes = new List<Shape>();          //hardcoded specific values
        public List<string> classes = new List<string>();       //freetext
        public List<string> materials = new List<string>();     //freetext
        public List<string> containers = new List<string>();    //must be valid items from json
        public List<string> blocksViews = new List<string>();   //specific hardcoded values
        public List<string> visibleSides = new List<string>();  //specific hardcoded values
        public int hollowness = 100;
        public List<string> connectionTypes = new List<string>();
        public List<string> containsTypes = new List<string>();
        public List<string> mustContainTypes = new List<string>();
    }

    //substantive is a synonym of noun. It is a person, a place, or a thing
    public abstract class Substantive : Template
    {
        public int caseNumber;
        public int id;
        public int length;
        public int width;
        public int height;
        public int volume;
        public int mass;
        public bool bloodSpatter;
        public string aAn = "";
        public Random random;
        Shape shape;

        public abstract string Describe();

        public override string ToString()
        {
            return name;
        }

        public void Generate(Template template, int seed)
        {
            Random random = new Random(seed);

            //text-based descriptions
            name = template.name;
            description = template.description;
            type = template.type;

            //physical properties
            shape = new Shape(template.shapes, random.Next());
            classes = template.classes;
            lengthRange = template.lengthRange;
            widthRange = template.widthRange;
            heightRange = template.heightRange;
            length = lengthRange.GenerateFromRange(random.Next());
            width = widthRange.GenerateFromRange(random.Next());
            height = heightRange.GenerateFromRange(random.Next());
            volume = length * width * height;
            hollowness = template.hollowness;

            float percentHollow = hollowness / 100;
            float v = volume * percentHollow;
            volume = (int) v;
            if (template.massRange != null)
            {
                massRange = template.massRange;
                mass = massRange.GenerateFromRange(random.Next());
            }

            blocksViews = template.blocksViews;
            bloodSpatter = false;
            containers = template.containers;

            List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
            if (vowels.Contains(name[0]))
            {
                aAn = "an";
            }
            else
            {
                aAn = "a";
            }
        }

        public string GetLengthDescription(int volume)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = lengthRange.maximum - lengthRange.minimum / 10;
            string output = "";
            //todo: get strings from the json
            if (volume < lengthRange.mode - tenPercent - tenPercent)
            {
                output += " much smaller than average";
            }
            else if (volume < lengthRange.mode - tenPercent)
            {
                output += " smaller than average";
            }
            else if (volume <= lengthRange.mode)
            {
                output += " slightly smaller than average";
            }
            else if (volume >= lengthRange.mode + tenPercent + tenPercent)
            {
                output += " much larger than average";
            }
            else if (volume >= lengthRange.mode + tenPercent)
            {
                output += " larger than average";
            }
            else if (volume >= lengthRange.mode)
            {
                output += " slightly larger than average";
            }
            return output;
        }

        public string GetMassDescription(int mass)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = massRange.maximum - massRange.minimum / 10;
            string output = "";
            if (mass < massRange.mode - tenPercent - tenPercent)
            {
                output += " much less heavy than average";
            }
            else if (mass < massRange.mode - tenPercent)
            {
                output += " less heavy than average";
            }
            else if (mass < massRange.mode)
            {
                output += " slightly less heavy than average";
            }
            else if (mass == massRange.mode)
            {
                output += " is exactly average in weight";
            }
            else if (mass > massRange.mode + tenPercent + tenPercent)
            {
                output += " much heavier than average";
            }
            else if (mass > massRange.mode + tenPercent)
            {
                output += " heavier than average";
            }
            else if (mass > massRange.mode)
            {
                output += " slightly heavier than average";
            }
            return output;
        }

        internal string GetHeightDescription(int height)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = heightRange.maximum - heightRange.minimum / 10;

            if (height < heightRange.mode - tenPercent - tenPercent)
            {
                return " much shorter than average";
            }
            else if (height < heightRange.mode - tenPercent)
            {
                return " shorter than average";
            }
            else if (height < heightRange.mode)
            {
                return " slightly shorter than average";
            }
            else if (height == heightRange.mode)
            {
                return " is exactly average in height";
            }
            else if (height > heightRange.mode + tenPercent + tenPercent)
            {
                return " much taller than average";
            }
            else if (height > heightRange.mode + tenPercent)
            {
                return " taller than average";
            }
            else if (height > heightRange.mode)
            {
                return " slightly taller than average";
            }
            else
            {
                return "";
            }
        }
    }
}
