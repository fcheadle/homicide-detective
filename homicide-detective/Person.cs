using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Person : Substantive
    {
        public Gender? gender;          //2 for now
        
        public string nameGiven;                
        public string nameFamily;
        public string pronounDescriptive;       //he, she
        public string pronounPossessive;        //his, her

        //percents 
        public int jealousy;
        public int anger;
        public int pride;
        public int laziness;
        public int ambition;
        public int creativity;
        public int attentionToDetail;
        public int familialImportance;

        public List<string> motives;            //generated from percents
        public List<string> causeOfDeath;       //specific hardcoded values... for now
        public List<BodyMark> bodyMarks;

        private Random random;

        public Person() { }

        public Person(int caseNumber, int id)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            IO io = new IO();
            Template template = io.GetRandomTemplate(SubstantiveType.person, random.Next());
            Generate(template, id);
            Personalize();
        }

        public Person(int caseNumber, int id, Gender gender)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            IO io = new IO();
            Template template = io.GetRandomTemplate(SubstantiveType.person, random.Next());
            this.gender = gender;
            Generate(template, id);
            Personalize();
        }

        private void Personalize()
        {
            //Names depend on gender so we generate that first - only two for now
            if(gender == null) gender = (Gender) random.Next(Enum.GetNames(typeof(Gender)).Length);

            PersonName names = Text.personNames;

            if (gender == Gender.female)
            {
                nameGiven = names.givenFemale[random.Next(0, names.givenFemale.Count)];
                pronounDescriptive = "she";
                pronounPossessive = "her";
                height *= (int) 0.9;
            }
            else
            {
                nameGiven = names.givenMale[random.Next(0, names.givenMale.Count)];
                pronounDescriptive = "he";
                pronounPossessive = "his";
            }

            nameFamily = names.family[random.Next(0, names.family.Count)];
            name = nameGiven + " " + nameFamily;
            
            bodyMarks = new List<BodyMark>();
            int bodymarkAmount = 0;

            for (int i = 0; i < 10; i++)
            {
                bodymarkAmount += random.Next(0, 10);
            }

            for (int i = 0; i < bodymarkAmount; i++)
            {
                bodyMarks.Add(new BodyMark());
            }
        }

        public override string Describe()
        {
            string output = "";
            output += name;
            output += " is";
            output += GetHeightDescription(height);
            output += " and";
            output += GetMassDescription(mass);
            output += ".";
            return output;
        }
    }

    public class BodyMark
    {
        public string type = "";       //cut? puncture mark? rope burn? tattoo? birthmark? dimples?
        public string descriptor = ""; //deep? winding? faint? hot? sensitive? octopus?
        public string color = "";      //can be blank
        public int size = 0;           //centimeters squared
    }

    public class FacialFeature
    {
        
    }

    //todo: make body parts, then give prints to digits to hands to arms to torse, and associated tissue
    public class FingerPrint
    {
        public string description;
        Random random = new Random();
        private PrintType printType;

        public FingerPrint(int seed)
        {

        }

        public override string ToString()
        {
            Description _ = new Description();
            string output = "";
            switch (printType)
            {
                case PrintType.archPlain:
                    output = _.arch + _.plain;
                    return output;

                case PrintType.archTented:
                    output = _.arch + _.tented;
                    return output;


                case PrintType.loopUlnar:
                    output = _.loop + _.ulner;
                    return output;


                case PrintType.loopRadial:
                    output = _.loop + _.radial;
                    return output;


                case PrintType.whorlCentralPocketLoop:
                    output = _.loop + _._double;
                    return output;


                case PrintType.whorlPlain:
                    output = _.whorl + _.plain;
                    return output;


                case PrintType.whorlAccidental:
                    output = _.whorl + _.accidental;
                    return output;
            }
            return output;
        }
    }

    public enum PrintType
    {
        archPlain,
        archTented,
        loopUlnar,
        loopRadial,
        loopDouble,
        whorlCentralPocketLoop,
        whorlPlain,
        whorlAccidental
    }

    public enum FacialFeatureType
    {
        nose,
        eye,
        ear,
        eyeBrow,
        lip,
        cheek,

    }
}