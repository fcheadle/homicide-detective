using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class Item
    {
        //different pieces of evidence
        //or furniture or red herrings
        //or murder weapons

        string name;        //freetext, and unique identifier
        string Description; //freetext
        float hollowness;   //in percent
        int mass;           //in grams
        int volume;         //in cm squared

        string[] shapes;    //hardcoded specific values
        string[] classes;   //freetext
        string[] materials; //freetext
        string[] containers;//must be valid items from json
        string[] blocksViews;//specific hardcoded values
        string[] visiblSides;//specific hardcoded values
        
        public Item(int seed)
        {

        }
    }
}
