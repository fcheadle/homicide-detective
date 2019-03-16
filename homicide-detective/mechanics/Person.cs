using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace homicide_detective
{
    public class Person
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
         
        public List<Person> family;
        public List<Person> friends;
        public List<Person> enemies;

        public Person()
        {
            name = "";
            description = "";
            jealousy = 0;
            anger = 0;
            pride = 0;
            laziness = 0;
            ambition = 0;
            classiness = 0;
            creativity = 0;
            attentionToDetail = 0;
            intelligence = 0;
            wealth = 0;
            importanceOfFamily = 0;
        }

        public Person(int seed)
        {
            Random random = new Random(seed);

            //File[] files = Directory.GetFileSystemEntries();
            //jealousy = GenerateAttribute(seed);
        }

        private int GenerateAttributes(int seed)
        {
            throw new NotImplementedException();
        }
    }
}
