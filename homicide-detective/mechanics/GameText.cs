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
            public Name()
            {
                givenMale = new List<string>();
                givenFemale = new List<string>();
                family = new List<string>();
                title = new Titles();
            }
            public List<string> givenMale;
            public List<string> givenFemale;
            public List<string> family;
            public Titles title;
        }

        public class Titles
        {
            public Titles()
            {
                male = new List<string>();
                female = new List<string>();
                professional = new List<string>();
                status = new List<string>();
            }
            public List<string> male;
            public List<string> female;
            public List<string> professional;
            public List<string> status;
        }

        //things that have been written down
        public class WrittenText
        {
            public WrittenText()
            {
                this.intro = new List<string>();
                this.victim = new List<string>();
                this.murderer = new List<string>();
                this.accomplice = new List<string>();
                this.unrelated = new List<string>();
            }
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
            this.name = new Name();
            this.written = new WrittenText();
        }

        internal void AddNames(Name text)
        {
            this.name = text;
        }

        internal void AddWrittenTexts(WrittenText text)
        {
            this.written = text;
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

        internal void Add(object gameText)
        {
            
            //this.AddNames(gameText.name);
            //this.AddDialogueText(gameText.dialogue);
        }
    }
}
