using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class Person
    {
        //define people physiologically and psychologically
        string name;
        string description;
        string dominantPersonalityTrait;

        //following are percents
        int jealousy; 
        int anger;
        int pride;
        int laziness;
        int ambition;
        int classiness;
        int creativity;
        int attentionToDetail;
        int intelligence;
        int wealth;

        string[] motives; //specific hardcoded values
        string[] causeOfDeath;//specific hardcoded values
        string[] witnesses; //people from json files
        string[] murderWeapons;//items from json files
        
        Scene sceneOfTheCrime;
        Scene locationBodyWasFound;
        Scene victimsBedroom;

        Person[] family;
        Person[] friends;
        Person[] enemies;

        public Person(int seed)
        {

        }
    }
}
