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
        static string rootDirectory = Directory.GetCurrentDirectory();
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
        static string extension = ".json";

        public int state = 1;
        //state = 0;        //turn off game
        //state = 1;        //show main menu
        //state = 2;        //show case menu
        //state = 3;        //investigating a scene
        //state = 4;        //talking to persons of interest
        //state = 5;        //case review menu

        public int caseTaken = 0;
        public List<string> gameLog;

        //these variables need to be public so that JSONConvert can access them during static loadGame calls
        public string detective;            //name of the detective, stored with dashes, periods, and hyphens
        public int seed;                    //number generated from the detective's name
        public int caseIndex;               //case currently being reviewed (always exactly one case in review)
        public bool debugMode = false;      //for testing purposes
        public List<Case> activeCases = new List<Case>();      //cases that are neither solved nor cold
        public List<Case> solvedCases = new List<Case>();      //when a case is added to the solved array, it must be removed from the active array
        public List<Case> coldCases = new List<Case>();        //when a case is added to the cold array, it must be removed from the active cases
        public List<Case> bookmarkedCases = new List<Case>();  //ones saved for later viewing

        public List<PersonTemplate> personTemplates = new List<PersonTemplate>();    //keep the persons from the person folder in memory
        public List<ItemTemplate> itemTemplates = new List<ItemTemplate>();          //keep the items from the item folder in memory
        public List<SceneTemplate> sceneTemplates = new List<SceneTemplate>();       //keep the scenes from the scene folder in memory
        public GameText allText;                                //keep the text from the text folder in memory. There is only item for all game text 
        internal int csiState;

        //need a blank constructor because JSONConvert instantiates the object with no arguments
        public Game()
        {
            personTemplates = LoadPersonFiles();
            itemTemplates = LoadItemFiles();
            sceneTemplates = LoadSceneFiles();
            allText = LoadTextFiles();
        }

        //constructor - new game
        public Game(string name)
        {
            detective = name;

            if (detective != null)
            {
                seed = Base36.Decode(SanitizeName(name.ToLower()));
            }
            
            personTemplates = LoadPersonFiles();
            itemTemplates = LoadItemFiles();
            sceneTemplates = LoadSceneFiles();
            allText = LoadTextFiles();

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
            allText = new GameText();
            string path = saveFolder + SanitizeName(detective).ToLower() + extension;
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
            allText = LoadTextFiles();
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

        public List<PersonTemplate> LoadPersonFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\person\";
            string[] persons = Directory.GetFiles(fileDirectory);

            List<PersonTemplate> returnList = new List<PersonTemplate>();
            foreach(string path in persons)
            {
                returnList.Add(JsonConvert.DeserializeObject<PersonTemplate>(File.ReadAllText(path)));
            }

            return returnList;
        }

        public List<ItemTemplate> LoadItemFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\item\";
            string[] items = Directory.GetFiles(fileDirectory);

            List<ItemTemplate> returnList = new List<ItemTemplate>();
            foreach (string path in items)
            {
                returnList.Add(JsonConvert.DeserializeObject<ItemTemplate>(File.ReadAllText(path)));
            }

            return returnList;
        }

        public List<SceneTemplate> LoadSceneFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\scene\";
            string[] scenes = Directory.GetFiles(fileDirectory);

            List<SceneTemplate> returnList = new List<SceneTemplate>();
            foreach (string path in scenes)
            {
                returnList.Add(JsonConvert.DeserializeObject<SceneTemplate>(File.ReadAllText(path)));
            }
            
            return returnList;
        }

        public GameText LoadTextFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\text\";
            string[] texts = Directory.GetFiles(fileDirectory);

            GameText gameText = new GameText();

            foreach(string text in texts)
            {
                if(text.Contains("names_"))
                {
                    gameText.AddNames(JsonConvert.DeserializeObject<GameText.Name>(File.ReadAllText(text)));
                }                
                if(text.Contains("written_"))
                {
                    gameText.AddWrittenTexts(JsonConvert.DeserializeObject<GameText.WrittenText>(File.ReadAllText(text)));
                }
            }

            return gameText;
        }

        public static Case AddCase(Game game)
        {
            game.activeCases.Add(new Case(game));
            int i = game.activeCases.Count;
            return game.activeCases[i];
        }

        public Case GenerateCase(Game game, int caseNumber)
        {
            game.activeCases.Add(new Case(game, caseNumber));
            int i = game.activeCases.Count - 1;
            return game.activeCases[i];
        }
    }
}