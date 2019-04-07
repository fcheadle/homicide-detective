using Newtonsoft.Json;
using System;
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

        #region variables
        //System Variables
        private static string folder = Directory.GetCurrentDirectory() + @"\objects\";
        private static string extension = ".json";
        
        private static string menuPath = folder + "menu_english" + extension;
        private static string caseDescriptionPath = folder + "description_case" + extension;
        private static string itemDescriptionPath = folder + "description_item" + extension;
        private static string personNamesPath = folder + "names_default" + extension;
        private static string writtenPath = folder + "written_diary" + extension;
        private static string dialoguePath = folder + "dialogue_default" + extension;

        private static string menuRaw = File.ReadAllText(menuPath);
        private static string caseDescriptionRaw = File.ReadAllText(caseDescriptionPath);
        private static string itemDescriptionRaw = File.ReadAllText(itemDescriptionPath);
        private static string personNamesRaw = File.ReadAllText(personNamesPath);
        private static string writtenRaw = File.ReadAllText(writtenPath);
        private static string dialogueRaw = File.ReadAllText(dialoguePath);
        private string itemTemplateRaw;
        private string sceneTemplateRaw;
        private string personTemplateRaw;

        //These objects are used to build the strings that are given to the user
        public Text.Menu menu;
        public ItemDescription itemDescription;
        public CaseDescription caseDescription;
        public PersonName personNames;
        public Written written;
        public Dialogue dialogue;
        public List<ItemTemplate> itemTemplates = new List<ItemTemplate>();
        public List<SceneTemplate> sceneTemplates = new List<SceneTemplate>();
        public List<PersonTemplate> personTemplates = new List<PersonTemplate>();
        #endregion

        public Text()
        {
            menu = JsonConvert.DeserializeObject<Text.Menu>(menuRaw);
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
        public class Menu
        {
            public Main main;
            public Case _case;
            public CSI csi;
            public Response response;
            public LookType look;

            public Menu(bool load = false)
            {
                if (load)
                {
                    Menu _menu = new Menu();
                    _menu = JsonConvert.DeserializeObject<Text.Menu>(menuRaw);
                    main = _menu.main;
                    _case = _menu._case;
                    csi = _menu.csi;
                }
            }

            public class Main
            {
                public string title;
                public string subtitle;
                public string newGame;
                public string loadGame;
                public string exitGame;

                public List<string> ToList()
                {
                    List<string> list = new List<string>();
                    list.Add(newGame);
                    list.Add(loadGame);
                    list.Add(exitGame);
                    return list;
                }
            }

            public class Case
            {
                public string take;
                public string review;
                public string next;
                public string bookmark;
                public string takeCaseNumber;

                public List<string> ToList()
                {
                    List<string> list = new List<string>();
                    list.Add(take);
                    list.Add(review);
                    list.Add(next);
                    list.Add(bookmark);
                    list.Add(takeCaseNumber);
                    return list;
                }
            }

            public class CSI
            {
                public string dust;
                public string leave;
                public string open;
                public string close;
                public string record;
                public string check;
                public string look;
                public string photograph;
                public string take;

                public List<string> ToList()
                {
                    List<string> list = new List<string>();
                    list.Add(dust);
                    list.Add(leave);
                    list.Add(open);
                    list.Add(close);
                    list.Add(record);
                    list.Add(check);
                    list.Add(look);
                    list.Add(photograph);
                    list.Add(take);
                    return list;
                }
            }

            public class Response
            {
                public string duplicateDetective;
                public string yes;
                public string no;
                public string yesNoOnly;
                public string commandNotFound;
                public string namePrompt;
                public string sceneSelection;
                public string caseMenuIntro;
                public string dustQuery;
                public string checkQuery;
                public string openQuery;
                public string closeQuery;
                public string leaveQuery;
                public string takeQuery;
                public string lookUnderQuery;
                public string photographQuery;
                internal string lookBehindQuery;
                internal string lookAtQuery;
                internal string lookInsideQuery;
                internal string lookOnQuery;

                public List<string> ToList()
                {
                    List<string> list = new List<string>();
                    list.Add(duplicateDetective);
                    list.Add(yes);
                    list.Add(no);
                    list.Add(yesNoOnly);
                    list.Add(commandNotFound);
                    list.Add(namePrompt);
                    list.Add(sceneSelection);
                    list.Add(caseMenuIntro);
                    list.Add(dustQuery);
                    list.Add(checkQuery);
                    list.Add(openQuery);
                    list.Add(closeQuery);
                    list.Add(leaveQuery);
                    list.Add(takeQuery);
                    list.Add(photographQuery);
                    return list;
                }
            }                
                 
            public class LookType
            {
                public string at;
                public string behind;
                public string underneath;
                public string onTopOf;
                public string insideOf;

                public List<string> ToList()
                {
                    List<string> list = new List<string>();
                    list.Add(at);
                    list.Add(behind);
                    list.Add(underneath);
                    list.Add(onTopOf);
                    list.Add(insideOf);
                    return list;
                }
            }
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
