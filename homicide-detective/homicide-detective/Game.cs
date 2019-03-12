using System;
using System.IO;
using Newtonsoft.Json;

namespace homicide_detective
{
    class Game
    {
        //make this whole class static
        static string detective = "";
        static string command = "";
        static bool gameInSession = false;
        //static Save save = new Save();

        //MainMenu returns a true if the game is in session, and false if the game should quit
        public static bool MainMenu()
        {
            
            Console.WriteLine("Homicide Detective");
            Console.WriteLine("Whenever two objects interact, some evidence of that interaction can be found and verified.");
            Console.WriteLine("-Theory of Transfer");
            Console.WriteLine("new | load | exit");
            Console.WriteLine("at any time, press ? for help.");

            command = Console.ReadLine();
            command = command.ToLower();

            switch (command)
            {
                case "new":
                    gameInSession = true;
                    NewGame();
                    break;
                case "load":
                    gameInSession = true;
                    LoadGame();
                    break;
                case "exit":
                    return gameInSession;
                default:
                    Console.WriteLine("Command not recognized.");
                    gameInSession = MainMenu();
                    break;
            }

            return gameInSession;
        }

        static void NewGame()
        {
            Console.WriteLine("What is your name, Detective?");
            detective = Console.ReadLine();
            detective = detective.ToLower();
            Console.WriteLine("It's nice you meet you, Detective " + detective + ".");
            
            string root = @"D:\homicide-detective\";
            string extension = ".json";
            string path = root + detective + extension;
            Save newGame = new Save(detective);

            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(newGame));
            }
            else if (File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(newGame));
            }

        }

        static void SaveGame()
        {
            throw new NotImplementedException();
        }

        static void LoadGame()
        {
            throw new NotImplementedException();
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


        static void LookAt(string item)
        {
            throw new NotImplementedException();
        }

        static void LookUnder(string item)
        {
            throw new NotImplementedException();
        }

        static void LookInsideOf(string item)
        {
            throw new NotImplementedException();
        }

        static void LookOnTopOf(string item)
        {
            throw new NotImplementedException();
        }

        static void LookBehind(string item)
        {
            throw new NotImplementedException();
        }

        static void PhotographScene()
        {
            throw new NotImplementedException();
        }

        static void PhotographItem(string item)
        {
            throw new NotImplementedException();
        }

        static void TakeNote()
        {
            throw new NotImplementedException();
        }

        static void TakeEvidence(string item)
        {
            throw new NotImplementedException();
        }

        static void DustForPrints(string item)
        {
            throw new NotImplementedException();
        }

        static void LeaveScene()
        {
            throw new NotImplementedException();
        }

        static void LeaveThroughDoor(string door)
        {
            throw new NotImplementedException();
        }

        static void RecordConversation()
        {
            throw new NotImplementedException();
        }

        static void CheckEvidence()
        {
            throw new NotImplementedException();
        }

        static void CheckPhotographs()
        {
            throw new NotImplementedException();
        }

        static void CheckNotes()
        {
            throw new NotImplementedException();
        }

        static void CloseDoor(string v)
        {
            throw new NotImplementedException();
        }

        static void OpenDoor(string v)
        {
            throw new NotImplementedException();
        }

        static void EvaluateCommand(string input_string)
        {
            var command = input_string.Split(' ');

            switch (command[0])
            {
                case "look":
                    switch (command[1])
                    {
                        case "at": LookAt(command[2]); break;
                        case "under": LookUnder(command[2]); break;
                        case "inside": LookInsideOf(command[2]); break;
                        case "on": LookOnTopOf(command[4]); break;
                        case "behind": LookBehind(command[2]); break;
                    }
                    break;
                case "photograph":
                    switch (command[1])
                    {
                        case "scene": PhotographScene(); break;
                        default: PhotographItem(command[1]); break;
                    }
                    break;
                case "take":
                    switch (command[1])
                    {
                        case "note": TakeNote(); break;
                        default: TakeEvidence(command[1]); break;
                    }
                    break;
                case "dust": DustForPrints(command[1]); break;
                case "leave":
                    switch (command[1])
                    {
                        case "through": LeaveThroughDoor(command[2]); break;
                        default: LeaveScene(); break;
                    }
                    break;
                case "open": OpenDoor(command[1]); break;
                case "close": CloseDoor(command[1]); break;
                case "check":
                    switch (command[1])
                    {
                        case "notes": CheckNotes(); break;
                        case "photographs": CheckPhotographs(); break;
                        case "evidence": CheckEvidence(); break;
                    }
                    break;
                case "record": RecordConversation(); break;
                default: Console.WriteLine("?"); break;
            }
        }
    }
}
