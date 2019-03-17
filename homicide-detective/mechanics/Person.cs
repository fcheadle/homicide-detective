using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class Person
    {
        public string name;         //John Doe, Nancy Reagan, Samuel Clemmons, etc
        public string description;  //generated
        public int height;          //in centimeters
        public int mass;            //in grams

        //in percent
        public int jealousy;
        public int anger;
        public int pride;
        public int laziness;
        public int ambition;
        public int classiness;
        public int creativity;
        public int attentionToDetail;
        public int intelligence;
        public int wealth;
        public int importanceOfFamily;

        public List<string> family;             //names
        public List<string> friends;            //names
        public List<string> enemies;            //names
        
        public List<string> motives;            //generated from percents
        public List<string> causeOfDeath;       //specific hardcoded values... for now
    }
}
