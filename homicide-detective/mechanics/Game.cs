using System;
using System.IO;
using Newtonsoft.Json;

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

        //need a blank constructor because JSONConvert instantiates the object with no arguments
        public Game()
        {

        }

        //constructor
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
                Console.WriteLine("Warning! There is already a detective named " + detective + ". Would you like to load that game instead?");
                string answer = Console.ReadLine();

                if ((answer == "no") || (answer == "No") || (answer == "NO"))
                {
                    SaveGame();
                }
                else
                {
                    LoadGame(detective);
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

            string path = saveFolder + detective.ToLower() + extension;

            File.WriteAllText(path, JsonConvert.SerializeObject(this));
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
    }
}
