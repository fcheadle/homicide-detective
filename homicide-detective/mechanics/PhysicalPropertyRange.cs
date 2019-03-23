using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    //for generating mass and volume from ranges
    public class PhysicalPropertyRange
    {
        public int minimum;    //minimum value for mass or volume
        public int maximum;    //maximum value for mass or volume
        public int mean;       //mean of typical masses or volumes
        public int mode;       //mode of typical masses or volumes

        /*public static int SetStatsFromRange(Random random, PhysicalPropertyRange range)
        {
            int mean = 0;
            int totalRange = range.maximum - range.minimum;

            for (int i = 0; i < 10; i++)
            {
                mean += random.Next(-totalRange, totalRange);
            }

            mean /= 10;

            mean += range.mode;

            if (mean < range.minimum) mean = range.minimum;
            if (mean > range.maximum) mean = range.maximum;

            return mean;
        }*/
    }

    public class PhysichalPropertyRange
    {
    }
}
