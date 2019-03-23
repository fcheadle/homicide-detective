using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Person : PersonTemplate
    {
        public int height;          //in centimeters
        public int mass;            //in grams

        public List<string> family;             //names
        public List<string> friends;            //names
        public List<string> enemies;            //names
        
        public List<string> motives;            //generated from percents
        public List<string> causeOfDeath;       //specific hardcoded values... for now
    }
}