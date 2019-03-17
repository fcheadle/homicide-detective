using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace homicide_detective
{
    public class PersonTemplate
    {
        //define people physiologically and psychologically
        public string name;
        public string description;

        //following are percents
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

        public string[] motives;           //specific hardcoded values
        public string[] causeOfDeath;      //specific hardcoded values
        public string[] witnesses;         //people from json files
        public string[] murderWeapons;     //items from json files
         
        public List<PersonTemplate> family;
        public List<PersonTemplate> friends;
        public List<PersonTemplate> enemies;

        public PersonTemplate()
        {

        }

        public int GenerateAttributes(int seed)
        {
            throw new NotImplementedException();
        }
    }
}
