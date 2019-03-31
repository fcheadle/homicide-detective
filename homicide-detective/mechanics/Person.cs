using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class PersonTemplate
    {
        public string id;
        public string descrtiption;
        //following are added to percents during person generation
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

        public PersonTemplate()
        {

        }
    }

    public class Person : PersonTemplate
    {
        public int height;          //in centimeters
        public int mass;            //in grams
        public int gender;          //2 for now
        public string pronounDescriptive;       //he, she
        public string pronounPossessive;        //his, her
        public int type;                    //general collections of mannerism / descriptions
                                            
        
        //define people physiologically and psychologically
        public string name;
        public string firstName;                
        public string lastName;
        public string description;
        public List<string> family;             //id
        public List<string> friends;            //id
        public List<string> enemies;            //id
        
        public List<string> motives;            //generated from percents
        public List<string> causeOfDeath;       //specific hardcoded values... for now
        Random random;

        public Person(int seed, GameText text)
        {
            random = new Random(seed);
            gender = (random.Next(0,2));

            int givenNameIndex;
            int familyNameIndex = random.Next(0, text.names.family.Count());

            if (gender == 0)
            {
                givenNameIndex = random.Next(0, text.names.givenMale.Count());
                firstName = " " + text.names.givenMale[givenNameIndex];
            }
            else
            {
                givenNameIndex = random.Next(0, text.names.givenMale.Count());
                firstName = " " + text.names.givenFemale[givenNameIndex];
            }

            lastName = text.names.family[familyNameIndex];

            name = firstName + " " + lastName;
        }

        /*
        public Person GeneratePerson(GameText text)
        {
            Person person = new Person(random, text);
            int givenNameIndex;
            int familyNameIndex = random.Next(0, text.names.family.Count() - 1);

            if (gender == 0)
            {
                givenNameIndex = random.Next(0, text.names.givenMale.Count() - 1);
                firstName = text.names.givenMale[givenNameIndex];
            }
            else
            {
                givenNameIndex = random.Next(0, text.names.givenMale.Count() - 1);
                firstName = text.names.givenFemale[givenNameIndex];
            }
            
            lastName = text.names.family[familyNameIndex];
           
            name = firstName + " " + lastName;
            return person;
        }
        */
    }
}