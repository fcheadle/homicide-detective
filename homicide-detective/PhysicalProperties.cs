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
    }

    //shape of the object or room or whatever
    public class Shape
    {
        public string name;
        public int probability;

        public Shape()
        {
        }

        public Shape(List<Shape> shapes, int seed)
        {
            Random random = new Random(seed);
            int total = 0;
            foreach(Shape shape in shapes)
            {
                total += shape.probability;
            }

            int shapeIndex = random.Next(0, total);
            total = 0; 
            foreach(Shape shape in shapes)
            {
                if(shapeIndex > shape.probability)
                {
                    shapeIndex -= shape.probability;
                }
                else
                {
                    name = shape.name;
                    break;
                }
            }
        }
    }
}
