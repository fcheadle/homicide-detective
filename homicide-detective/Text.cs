using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace homicide_detective
{
    public static class Text
    {
        public static string folder = Directory.GetCurrentDirectory() + @"\objects\";
        public static string extension = ".json";
        public static string menuRaw = File.ReadAllText(folder + "menu_english" + extension);
        public static string personNamesRaw = File.ReadAllText(folder + "names_default" + extension);
        public static string writtenRaw = File.ReadAllText(folder + "written_diary" + extension);
        public static string languageRaw = File.ReadAllText(folder + "language_english" + extension);

        //These objects are used to build the strings that are given to the user
        public static Menu menu = JsonConvert.DeserializeObject<Menu>(menuRaw);
        public static PersonName personNames = JsonConvert.DeserializeObject<PersonName>(personNamesRaw);
        public static Written written = JsonConvert.DeserializeObject<Written>(writtenRaw);
        public static Language language = JsonConvert.DeserializeObject<Language>(languageRaw);

    }
    

    public class Menu
    {
        public Main main;
        public Case _case;
        public CSI csi;
        public Response response;
        public LookType look;

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
            public string lookBehindQuery;
            public string lookAtQuery;
            public string lookInsideQuery;
            public string lookOnQuery;

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
    public class Description
    {
        internal string arch;
        internal string plain;
        internal string tented;
        internal string loop;
        internal string ulner;
        internal string radial;
        internal string whorl;
        internal string accidental;

        public string _it { get; internal set; }
        public string _is { get; internal set; }
        public string _double { get; internal set; }

        //get from json...

        public class Item
        {
            public string smaller;
            public string smallerAdverb;
            public string largerAdverb;
            public string larger;
            public string sizeDescription;
            public string exactly;
        }
        public class Case
        {
            public string intro;
        }

        public class Person
        {

        }
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
    public class Language
    {
        public Dictionary<string, string> nouns;
        public Dictionary<string, string> pronouns;
        public Dictionary<string, string> preopsitions;
        public Dictionary<string, string> articles;
        public Dictionary<string, string> adverbs;
        public Dictionary<string, string> adjectives;
        public Dictionary<string, string> conjunctions;
        public Dictionary<string, string> verbs;
        public Dictionary<string, string> prepositions;

        public static Dictionary<string, string> GetWords()
        {
            Dictionary<string, string> lang = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> entry in Text.language.pronouns)
            {
                lang.Add(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<string, string> entry in Text.language.nouns)
            {
                lang.Add(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<string, string> entry in Text.language.verbs)
            {
                lang.Add(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<string, string> entry in Text.language.adjectives)
            {
                lang.Add(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<string, string> entry in Text.language.conjunctions)
            {
                lang.Add(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<string, string> entry in Text.language.articles)
            {
                lang.Add(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<string, string> entry in Text.language.prepositions)
            {
                lang.Add(entry.Key, entry.Value);
            }
            return lang;
        }
    }

}
