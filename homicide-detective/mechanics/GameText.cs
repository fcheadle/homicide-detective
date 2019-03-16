using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class GameText
    {
        //This class contains the written text from object\text in-memory.
        //Anything that can be printed to the screen belongs in objects\text, even menu prompts (though they aren't in there yet)

        //potential names of persons
        public class Name
        {
            public string[] givenMale;
            public string[] givenFemale;
            public string[] family;
            public string[] title;
        }

        //things that have been written down
        public class WrittenText
        {
            public string[] intro;
            public string[] victim;
            public string[] murderer;
            public string[] accomplice;
            public string[] unrelated;
        }

        //things people can say
        public class DialogueText
        {
            public string[] greetings;
            public string[] justification;
            public string[] argument;
            public string[] defense;
            public string[] deflection;
        }

        //Constructor
        public GameText()
        {

        }

        public GameText(string[] texts)
        {
            int i = 0;

            foreach(string json in texts)
            {

            }
        }
    }
}
