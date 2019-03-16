using System;
using System.IO;
using Newtonsoft.Json;
using homicide_detective.mechanics;
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
        string rootDirectory = Directory.GetCurrentDirectory();
        string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
        string extension = ".json";

        //these variables need to be public so that JSONConvert can access them during static loadGame calls
        public string detective;        //name of the detective, stored with dashes, periods, and hyphens
        public int seed;                //number generated from the detective's name
        public int caseIndex;           //case currently being reviewed (always exactly one case in review)
        public bool debugMode = false;  //for testing purposes
        public Case[] activeCases;      //cases that are neither solved nor cold
        public Case[] solvedCases;      //when a case is added to the solved array, it must be removed from the active array
        public Case[] coldCases;        //when a case is added to the cold array, it must be removed from the active cases
        public string[] gameLog;        //the entire game log is saved to the file

        public List<Person> allPersons = new List<Person>();    //keep the persons from the person folder in memory
        public List<Item> allItems = new List<Item>();          //keep the items from the item folder in memory
        public List<Scene> allScenes = new List<Scene>();       //keep the scenes from the scene folder in memory
        public GameText allText;                                //keep the text from the text folder in memory. There is only item for all game text 

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
                seed = Base36.Decode(SanitizeName(name));
            }

            string path = saveFolder + name.ToLower() + extension;
            
            // Get current directory of binary and create a data directory if it doesn't exist.
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            //create a new save file
            if (!File.Exists(path))
            {
                SaveGame();
            }
            else if (File.Exists(path))
            {
                bool existConfirmation = false;

                while (!existConfirmation)
                {
                    Console.WriteLine("Warning! There is already a detective named " + detective + ". Would you like to load that game instead?");
                    string answer = Console.ReadLine();

                    try
                    {
                        List<string> No = new List<string>();
                        No.Add("no");
                        No.Add("nope");
                        No.Add("nah");
                        No.Add("oh hell no");
                        List<string> Yes = new List<string>();
                        Yes.Add("yes");
                        Yes.Add("yep");
                        Yes.Add("yeah");
                        Yes.Add("oh hell yeah");
                        if (No.Contains(answer.Trim().ToLower()))
                        {
                            SaveGame();
                            existConfirmation = true;
                            break;
                        }
                        else if (Yes.Contains(answer.Trim().ToLower()))
                        {
                            LoadGame(detective);
                            existConfirmation = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Umm... That didn't make sense. Yes/No answers ony!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Holy Smokes! There was a problem: " + ex.Message);
                    }
                }
            }
        }

        //get the detective's name ready to be converted to a base36 number for the seed
        public static string SanitizeName(string detectiveName)
        {
            char[] separator = { ' ', '-', '\'', '.'};
            string[] afterSplit = detectiveName.Split(separator);
            string returnString = "";
            foreach(string s in afterSplit)
            {
                returnString = returnString + s; 
            }

            return returnString;
        }

        //saves the game to a file
        public void SaveGame()
        {
            //Delete these objects before saving so that they don't take up a whole lot of disk space
            if (allItems != null) allItems = new List<Item>();
            if (allPersons != null) allPersons = new List<Person>();
            if (allScenes != null) allScenes = new List<Scene>();
            if (allText != null) allText = new GameText();

            string path = saveFolder + detective.ToLower() + extension;
            File.WriteAllText(path, JsonConvert.SerializeObject(this));

            //Load these files back so that they are in memory again
            LoadPersonFiles();
            LoadItemFiles();
            LoadSceneFiles();
            LoadTextFiles();
        }

        //loads the game from a file
        public static Game LoadGame(string name)
        {

            string path = Directory.GetCurrentDirectory() + @"\saves\" + name.ToLower() + ".json";

            //Deserialize the save file contents to a Game object
            string saveFileContents = File.ReadAllText(path);
            Game game = JsonConvert.DeserializeObject<Game>(saveFileContents);

            return game;
        }

        public void LoadPersonFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\person\";
            string[] persons = Directory.GetFiles(fileDirectory);
            int i = 0;
            foreach(string json in persons)
            {
                allPersons.Add(JsonConvert.DeserializeObject<Person>(File.ReadAllText(persons[i])));
            }
        }

        public void LoadItemFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\item\";
            string[] items = Directory.GetFiles(fileDirectory);
            int i = 0;
            foreach (string json in items)
            {
                allItems.Add(JsonConvert.DeserializeObject<Item>(File.ReadAllText(items[i])));
            }
        }

        public void LoadSceneFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\scene\";
            string[] scenes = Directory.GetFiles(fileDirectory);
            int i = 0;
            foreach (string json in scenes)
            {
                allScenes.Add(JsonConvert.DeserializeObject<Scene>(File.ReadAllText(scenes[i])));
            }
        }

        public void LoadTextFiles()
        {
            string fileDirectory = rootDirectory + @"\objects\text\";
            string[] texts = Directory.GetFiles(fileDirectory);

            //we only have one instance of allText
            allText = new GameText(texts);
        }
    }
}
