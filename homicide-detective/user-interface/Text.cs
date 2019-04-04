using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace homicide_detective
{
    public class Text
    {
        /*
         * These classes exist as the framework that the json files in objects\folders
         * This is so that we can support translating the game to other languages
         * 
         * 
         */


        //System Variables
        private static string folder = Directory.GetCurrentDirectory() + @"\objects\";
        private static string extension = ".json";
        
        private static string mainMenuPath = folder + "menu_main" + extension;
        private static string caseMenuPath = folder + "menu_case" + extension;
        private static string csiMenuPath = folder + "menu_csi" + extension;
        private static string caseDescriptionPath = folder + "description_case" + extension;
        private static string itemDescriptionPath = folder + "description_item" + extension;
        private static string personNamesPath = folder + "names_default" + extension;
        private static string writtenPath = folder + "written_diary" + extension;
        private static string dialoguePath = folder + "dialogue_default" + extension;

        private static string mainMenuRaw = File.ReadAllText(mainMenuPath);
        private static string caseMenuRaw = File.ReadAllText(caseMenuPath);
        private static string csiMenuRaw = File.ReadAllText(csiMenuPath);
        private static string caseDescriptionRaw = File.ReadAllText(caseDescriptionPath);
        private static string itemDescriptionRaw = File.ReadAllText(itemDescriptionPath);
        private static string personNamesRaw = File.ReadAllText(personNamesPath);
        private static string writtenRaw = File.ReadAllText(writtenPath);
        private static string dialogueRaw = File.ReadAllText(dialoguePath);
        private string itemTemplateRaw;
        private string sceneTemplateRaw;
        private string personTemplateRaw;

        //These objects are used to build the strings that are given to the user
        public MainMenu mainMenuText;
        public CaseMenu caseMenuText;
        public CSIMenu csiMenuText;
        public ItemDescription itemDescription;
        public CaseDescription caseDescription;
        public PersonName personNames;
        public Written written;
        public Dialogue dialogue;
        public List<ItemTemplate> itemTemplates = new List<ItemTemplate>();
        public List<SceneTemplate> sceneTemplates = new List<SceneTemplate>();
        public List<PersonTemplate> personTemplates = new List<PersonTemplate>();

        public Text()
        {
            mainMenuText = JsonConvert.DeserializeObject<MainMenu>(mainMenuRaw);
            caseMenuText = JsonConvert.DeserializeObject<CaseMenu>(caseMenuRaw);
            csiMenuText = JsonConvert.DeserializeObject<CSIMenu>(csiMenuRaw);
            itemDescription = JsonConvert.DeserializeObject<ItemDescription>(itemDescriptionRaw);
            caseDescription = JsonConvert.DeserializeObject<CaseDescription>(caseDescriptionRaw);
            personNames = JsonConvert.DeserializeObject<PersonName>(personNamesRaw);
            written = JsonConvert.DeserializeObject<Written>(writtenRaw);
            dialogue = JsonConvert.DeserializeObject<Dialogue>(dialogueRaw);

            foreach(string path in Directory.GetFiles(folder))
            {
                if (path.Contains("item_"))
                {
                    itemTemplateRaw = File.ReadAllText(path);
                    ItemTemplate item = JsonConvert.DeserializeObject<ItemTemplate>(itemTemplateRaw);
                    itemTemplates.Add(item);
                }
                else if (path.Contains("scene_"))
                {
                    sceneTemplateRaw = File.ReadAllText(path);
                    SceneTemplate scene = JsonConvert.DeserializeObject<SceneTemplate>(sceneTemplateRaw);
                    sceneTemplates.Add(scene);
                }
                else if (path.Contains("person_"))
                {
                    personTemplateRaw = File.ReadAllText(path);
                    PersonTemplate person = JsonConvert.DeserializeObject<PersonTemplate>(personTemplateRaw);
                    personTemplates.Add(person);
                }
            }
        }

        #region objects
        public class MainMenu
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
        public class CaseMenu
        {
            public class ReviewCaseText
            {
                public string verb;
                public string bookmarkCase;
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
            public string bookmarkCase;
        }
        public class CSIMenu
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
        public class PersonName
        {
            public List<string> givenMale;
            public List<string> givenFemale;
            public List<string> family;
            public PersonTitle title;
        }
        public class PersonTitle
        {
            public List<string> male;
            public List<string> female;
            public List<string> professional;
            public List<string> status;
        }
        public class Written
        {
            public Written()
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
        public class Dialogue
        {
            public List<string> greetings;
            public List<string> justification;
            public List<string> argument;
            public List<string> defense;
            public List<string> deflection;
            public List<string> smallTalk;
        }
        #endregion
    }
}
