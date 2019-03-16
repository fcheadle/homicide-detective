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
    }
}
