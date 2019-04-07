﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    //for generating mass and volume from ranges
    public class Range
    {
        public int minimum;    //minimum value for mass or volume
        public int maximum;    //maximum value for mass or volume
        public int median;       //mean of typical masses or volumes
        public int mode;       //mode of typical masses or volumes

        public static int GetIntFromRange(int seed, Range range)
        {
            Random random = new Random();
            int mean = 0;
            int totalRange = range.maximum - range.minimum;

            for (int i = 0; i < 5; i++)
            {
                mean += random.Next(-totalRange, totalRange);
            }

            mean += range.median;
            mean += range.median;
            mean += range.mode;
            mean += range.mode;
            mean += range.mode;

            mean /= 10;
            
            if (mean < range.minimum) mean = range.minimum;
            if (mean > range.maximum) mean = range.maximum;

            return mean;
        }
    }
}
