using System;
using System.Collections.Generic;

namespace homicide_detective
{
    //for generating mass and volume from ranges
    public class Range
    {
        public int minimum;    //minimum value for mass or volume
        public int maximum;    //maximum value for mass or volume
        public int median;       //mean of typical masses or volumes
        public int mode;       //mode of typical masses or volumes

        public int GenerateFromRange(int seed)
        {
            Random random = new Random(seed);
            int mean = 0;
            int totalRange = maximum - minimum;

            for (int i = 0; i < 5; i++)
            {
                mean += random.Next(-totalRange, totalRange);
            }

            mean += median;
            mean += median;
            mean += mode;
            mean += mode;
            mean += mode;

            mean /= 10;

            if (mean < minimum) mean = minimum;
            if (mean > maximum) mean = maximum;

            return mean;
        }
        
        public string GetVolumeDescription(int volume)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = maximum - minimum / 10;

            //todo: get strings from the json
            if (volume < mode - tenPercent - tenPercent)
            {
                return " much smaller than average";
            }
            else if (volume < mode - tenPercent)
            {
                return " smaller than average";
            }
            else if (volume <= mode)
            {
                return " slightly smaller than average";
            }
            else if (volume >= mode + tenPercent + tenPercent)
            {
                return " much larger than average";
            }
            else if (volume >= mode + tenPercent)
            {
                return " larger than average";
            }
            else if (volume >= mode)
            {
                return " slightly larger than average";
            }
            else
            {
                return " average in size";
            }
        }

        public string GetMassDescription(int mass)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = maximum - minimum / 10;

            if (mass < mode - tenPercent - tenPercent)
            {
                return " much less heavy than average";
            }
            else if (mass < mode - tenPercent)
            {
                return " less heavy than average";
            }
            else if (mass < mode)
            {
                return " slightly less heavy than average";
            }
            else if (mass == mode)
            {
                return " is exactly average in weight";
            }
            else if (mass > mode + tenPercent + tenPercent)
            {
                return " much heavier than average";
            }
            else if (mass > mode + tenPercent)
            {
                return " heavier than average";
            }
            else if (mass > mode)
            {
                return " slightly heavier than average";
            }
            else
            {
                return "";
            }
        }

        internal string GetHeightDescription(int height)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = maximum - minimum / 10;

            if (height < mode - tenPercent - tenPercent)
            {
                return " much shorter than average";
            }
            else if (height < mode - tenPercent)
            {
                return " shorter than average";
            }
            else if (height < mode)
            {
                return " slightly shorter than average";
            }
            else if (height == mode)
            {
                return " is exactly average in height";
            }
            else if (height > mode + tenPercent + tenPercent)
            {
                return " much taller than average";
            }
            else if (height > mode + tenPercent)
            {
                return " taller than average";
            }
            else if (height > mode)
            {
                return " slightly taller than average";
            }
            else
            {
                return "";
            }
        }
    }

    //shape of the object or room or whatever
    public class Shape
    {
        public string value;
        public int percentile;

        public Shape()
        {
        }

        public Shape(List<Shape> shapes, int seed)
        {
            Random random = new Random(seed);
            int total = 0;
            foreach(Shape shape in shapes)
            {
                total += shape.percentile;
            }

            int shapeIndex = random.Next(0, total);
            total = 0; 
            foreach(Shape shape in shapes)
            {
                if(shapeIndex > shape.percentile)
                {
                    shapeIndex -= shape.percentile;
                }
                else
                {
                    value = shape.value;
                    break;
                }
            }
        }
    }
}
