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

        internal void AddNames(Name text)
        {
            foreach (string maleGiven in text.givenMale)
            {
                name.givenMale.Add(maleGiven);
            }

            foreach (string femaleGiven in text.givenFemale)
            {
                name.givenFemale.Add(femaleGiven);
            }

            foreach (string familyName in text.family)
            {
                name.family.Add(familyName);
            }

            foreach (string importTitle in text.title)
            {
                name.title.Add(importTitle);
            }
        }

        internal void AddWrittenTexts(WrittenText text)
        {
            foreach (string introText in text.intro)
            {
                written.intro.Add(introText);
            }

            foreach (string victimText in text.victim)
            {
                written.victim.Add(victimText);
            }
        }

        internal void AddDialogueText(DialogueText text)
        {
            //Look through all the properties in dialogue
            //and all the properties in text
            //and add to the relevant list where the two properties match up
            int i = 0;
            int j = 0;
            foreach (PropertyInfo property in dialogue.GetType().GetProperties())
            {
                j = 0;
                foreach (PropertyInfo textProperty in dialogue.GetType().GetProperties())
                {
                    if(textProperty.Name == property.Name)
                    {
                        //add to the right variable
                    }
                    j++;
                }

                i++;
            }
        }
    }
}
