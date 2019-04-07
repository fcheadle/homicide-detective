using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace homicide_detective
{
    public class Game
    {
        /*
         * One game object should be active when not on the main menu
         * This handles loading, saving, and how the objects interact with one another
         */

        //System Variables
        //todo: move to settings.config
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
        static string extension = ".json";

        public int state = 1;
        //state = 0;        //turn off game
        //state = 1;        //show main menu
        //state = 2;        //show case menu
        //state = 3;        //investigating a scene
        //state = 4;        //talking to persons of interest

        public int caseTaken = 0;
        public List<string> gameLog;

        //these variables need to be public so that JSONConvert can access them during static loadGame calls
        public string detective;                                //name of the detective, stored with dashes, periods, and hyphens
        public int seed;                                        //number generated from the detective's name
        public bool debugMode = false;                          //for testing purposes
        public List<Case> activeCases = new List<Case>();       //cases that are neither solved nor cold
        public List<Case> solvedCases = new List<Case>();       //when a case is added to the solved array, it must be removed from the active array
        public List<Case> coldCases = new List<Case>();         //when a case is added to the cold array, it must be removed from the active cases
        public List<Case> bookmarkedCases = new List<Case>();   //ones saved for later viewing
        public Case activeCase;                                 //the case currently being investigated
        Random random;
        public int menuState;

        //need a blank constructor because JSONConvert instantiates the object with no arguments
        public Game()
        {

        }

        //constructor - new game
        public Game(string name)
        {
            detective = name;

            if (detective != null)
            {
                seed = Base36.Decode(SanitizeName(name.ToLower()));
                random = new Random(seed);
            }

            CreateCaseIfNull();
            SaveGame();
        }

        //get the detective's name ready to be converted to a base36 number for the seed
        public static string SanitizeName(string detectiveName)
        {
            char[] separator = { ' ', '-', '\'', '.', ',', '?' };
            string[] afterSplit = detectiveName.Split(separator);
            string returnString = "";
            foreach(string s in afterSplit)
            {
                returnString = returnString + s; 
            }

            return returnString;
        }

        internal void CreateCaseIfNull()
        {
            Case thisCase = new Case();

            if (caseTaken == 0)
            {
                //case numbers start at 1
                caseTaken++;
            }

            if ((activeCases == null) || (activeCases.Count == 0))
            {
                activeCases.Add(new Case(random.Next(), caseTaken));
            }

            int i = 0;
            while (activeCases.Count <= caseTaken)
            {
                activeCases.Add(new Case(random.Next(), caseTaken + i));
                i++;
            }
            activeCase = activeCases[caseTaken];
        }

        //Check to see if a saved game exists for this string
        public static bool CheckFile(string name)
        {
            string path = saveFolder + name.ToLower() + extension;

            // Get current directory of binary and create a save directory if it doesn't exist.
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            return File.Exists(path);
        }

        //saves the game to a file
        public void SaveGame()
        {
            //allText = new Text.GameText();
            string path = saveFolder + SanitizeName(detective).ToLower() + extension;
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
            //allText = Text.LoadTextFiles();
        }

        //loads the game from a file
        public static Game LoadGame(string name)
        {
            name = SanitizeName(name);
            string path = Directory.GetCurrentDirectory() + @"\saves\" + name + ".json";
            string saveFileContents = File.ReadAllText(path);
            Game game = JsonConvert.DeserializeObject<Game>(saveFileContents);
            return game;
        }
        
        public void AddCase()
        {
            int i = activeCases.Count - 1;
            activeCases.Add(new Case(random.Next(), i));
            activeCase = activeCases[i];
        }
    }
}