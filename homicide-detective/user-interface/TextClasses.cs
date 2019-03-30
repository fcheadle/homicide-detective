using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class TextClasses
    {
        /*
         * These classes exist as the framework that the json files in objects\test
         * This is so that we can support translating the game to other languages
         * 
         * 
         */

        //these classes must match the JSON exactly
        public class MainMenuText
        {
            public string title;
            public string subtitle;
            public string newGame;
            public string loadGame;
            public string exitGame;
            public string namePrompt;
            public string yes;
            public string no;
            public string duplicateDetective;
            public string yesNoOnly;
            public string commandNotFound;
        }

        public class CaseMenuText
        {
            public class ReviewCaseText
            {
                public string verb;
                public string saveCase;
                public string nextCase;
                public string bodyFound;
            }

            public class SceneSelectionText
            {
                public string flavorText;
                public string where;
            }

            public ReviewCaseText reviewCaseText;
            public SceneSelectionText sceneSelectionText;
            public string take;
            public string nextCase;
            public string exitGame;
            public string closeCase;
            public string open;
            public string check;
            public string saveCase;
        }

        public class CSIMenuText
        {
            public class LookText
            {
                public string inside;
                public string at;
                public string behind;
                public string under;
                public string on;
                public string query;
                public string verb;
            }

            public class PhotographText
            {
                public string photograph;
                public string scene;
                public string query;
                public string verb;
            }

            public class TakeText
            {
                public string take;
                public string note;
                public string evidence;
                public string verb;
            }

            public class DustText
            {
                public string query;
                public string verb;
            }

            public class LeaveText
            {
                public string photograph;
                public string scene;
                public string query;
                public string verb;
            }

            public class OpenText
            {
                public string verb;
                public string query;
            }

            public class CloseText
            {
                public string verb;
                public string query;
            }

            public class Checktext
            {
                public string verb;
                public string query;
                public string notes;
                public string photos;
                public string fingerprints;
                public string evidence;
            }

            public LookText look;
            public PhotographText photograph;
            public TakeText take;
            public DustText dust;
            public LeaveText leave;
            public OpenText open;
            public CloseText close;
            public Checktext check;
            public string record;
        }

        public class ItemDescription
        {
            public string smaller;
            public string smallerAdverb;
            public string largerAdverb;
            public string larger;
            public string sizeDescription;
            public string exactly;
        }

        public class CaseDescription
        {
            public string intro;
        }
    }
}
