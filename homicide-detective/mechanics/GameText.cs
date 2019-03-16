using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            public List<string> givenMale;
            public List<string> givenFemale;
            public List<string> family;
            public List<string> title;
        }

        //things that have been written down
        public class WrittenText
        {
            public List<string> intro;
            public List<string> victim;
            public List<string> murderer;
            public List<string> accomplice;
            public List<string> unrelated;
        }

        //things people can say
        public class DialogueText
        {
            public List<string> greetings;
            public List<string> justification;
            public List<string> argument;
            public List<string> defense;
            public List<string> deflection;
            public List<string> smallTalk;
        }

        Name name;
        WrittenText written;
        DialogueText dialogue;

        //Constructor
        public GameText()
        {

        }

        public GameText(string[] texts)
        {

        }

        internal void Add(GameText gameText)
        {
            //foreach(PropertyInfo propertyInfo in name.GetType().GetProperties())
            {

            }
        }
    }
}
