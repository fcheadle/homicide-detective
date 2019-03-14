using System;
using System.IO;
using Newtonsoft.Json;

namespace homicide_detective
{
    class Game
    {
        /*
         * One game object should be active when not on the main menu
         * This handles loading, saving, and how the pbjects interact with one another
         * 
         */
        string detective;
        int seed;
        Case[] activeCases;
        Case[] solvedCases;
        Case[] coldCases;

        public Game(string name)
        {
            //set the new objects variables:
            detective = name;
            seed = Base36.Decode(SanitizeDetective(name));

            string rootDirectory = Directory.GetCurrentDirectory();
            string root = rootDirectory + @"\saves\";
            string extension = ".json";
            string path = root + name.ToLower() + extension;
            
            // Get current directory of binary and create a data directory if it doesn't exist.
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
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

        public static string SanitizeDetective(string detectiveName)
        {
            char[] separator = { ' ', '-' };
            string[] afterSplit = detectiveName.Split(separator);
            string returnString = "";
            foreach(string s in afterSplit)
            {
                returnString = returnString + s; 
            }

            return returnString;
        }

        void SaveGame()
        {
            string rootDirectory = Directory.GetCurrentDirectory();
            string root = rootDirectory + @"\saves\";
            string extension = ".json";
            string path = root + detective.ToLower() + extension;
            Game game = this;
            string saveText = JsonConvert.SerializeObject(game);
            File.WriteAllText(path, saveText);
        }

        public static Game LoadGame(string name)
        {
            //Location of the save game
            string rootDirectory = Directory.GetCurrentDirectory();
            string root = rootDirectory + @"\saves\";
            string extension = ".json";
            string path = root + name.ToLower() + extension;

            //Deserialize the save file contents to a Save object
            string saveFileContents = File.ReadAllText(path);
            Game game = JsonConvert.DeserializeObject<Game>(saveFileContents);

            return game;
        }
    }
}
