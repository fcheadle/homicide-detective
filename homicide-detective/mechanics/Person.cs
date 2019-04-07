using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class PersonTemplate
    {
        public string type;
        public string descrtiption;

        //following are added to percents during person generation
        public int jealousy;
        public int anger;
        public int pride;
        public int laziness;
        public int ambition;
        public int creativity;
        public int attentionToDetail;
        public int familialImportance;

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
                                            
        
        //define people physiologically and psychologically
        public string name;
        public int id;
        public string firstName;                
        public string lastName;
        public string description;
        public List<string> family;             //id
        public List<string> friends;            //id
        public List<string> enemies;            //id
        
        public List<string> motives;            //generated from percents
        public List<string> causeOfDeath;       //specific hardcoded values... for now

        public Person()
        {

        }

        public Person(int seed)
        {
            id = seed;
            Text text = new Text();
            Random random = new Random(id);
            gender = (random.Next(0,2));

            int givenNameIndex;
            int familyNameIndex = random.Next(0, text.personNames.family.Count());

            if (gender == 0)
            {
                givenNameIndex = random.Next(0, text.personNames.givenMale.Count());
                firstName = " " + text.personNames.givenMale[givenNameIndex];
            }
            else
            {
                givenNameIndex = random.Next(0, text.personNames.givenMale.Count());
                firstName = " " + text.personNames.givenFemale[givenNameIndex];
            }

            lastName = text.personNames.family[familyNameIndex];

            name = firstName + " " + lastName;
        }

        internal class FingerPrint
        {
            public class Digits
            {
                //"this is a fingerprint of someone's index finger. It is an accidental loop, centered high on the finger. It has a small scar in the shape of a line on it's bottom left side."
                public string index;
                public string middle;
                public string ring;
                public string pinky;
                public string thumb;
            }

            //public Digits left;
            //public Digits right;
            //string archPlain;
            //string archTented;
            //string loopUlnar;
            //string loopRadial;
            //string loopDouble;
            //string whorlCentralPocketLoop;
            //string whorlPlain;
            //string whorlAccidental;
        }
    }
}