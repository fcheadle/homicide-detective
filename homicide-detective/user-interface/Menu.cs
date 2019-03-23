using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace homicide_detective.user_interface
{
    static class Menu
    {
        //The Menu class contains all call-response from the game to the user.
        //when Menu is called, it will almost always return an integer gameState:
        //gameState = 0;        //turn off game
        //gameState = 1;        //show main menu
        //gameState = 2;        //show case menu
        //gameState = 3;        //investigating a scene
        //gameState = 4;        //talking to persons of interest

        #region variables
        static string textFolder = Directory.GetCurrentDirectory() + @"\objects\text\";
        static string extension = ".json";

        static string mainMenuPath = textFolder + "menu_main" + extension;
        static string caseMenuPath = textFolder + "menu_case" + extension;
        static string csiMenuPath = textFolder + "menu_csi" + extension;

        static string mainMenuRaw = File.ReadAllText(mainMenuPath);
        static string caseMenuRaw = File.ReadAllText(caseMenuPath);
        static string csiMenuRaw = File.ReadAllText(csiMenuPath);

        static MainMenuText mainMenuText = JsonConvert.DeserializeObject<MainMenuText>(mainMenuRaw);
        static CaseMenuText caseMenuText = JsonConvert.DeserializeObject<CaseMenuText>(caseMenuRaw);
        static CSIMenuText csiMenuText = JsonConvert.DeserializeObject<CSIMenuText>(csiMenuRaw);
        
        class MainMenuText
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

        class CaseMenuText
        {
            public string caseDescription = LoadJson("case");


            private static string LoadJson(string v)
            {
                throw new NotImplementedException();
            }

            public string takeCase;
            public string nextCase;
            public string exitGame;
        }

        class CSIMenuText
        {
            public string look;
            public string inside;
            public string at;
            public string behind;
            public string under;
            public string on;
            public string photograph;
            public string scene;
            public string take;
            public string note;
            public string dust;
            public string leave;
            public string through;
            public string open;
            public string close;
            public string record;
            public string check;
            public string evidence;
        }
        #endregion

        #region menus
        //MainMenu returns an integer that correlates to gameState in Homicide-Detective.cs
        public static Game MainMenu()
        {
            Console.Write(mainMenuText.newGame);
            Console.Write(" | ");
            Console.Write(mainMenuText.loadGame);
            Console.Write(" | ");
            Console.WriteLine(mainMenuText.exitGame);
            
            Game game = new Game();
            game.state = 0;
            string command = Console.ReadLine();
            command = command.ToLower();

            if (command == mainMenuText.newGame)
            {
                string detective = GetDetective();
                if (Game.CheckFile(detective))
                {
                    Console.WriteLine(mainMenuText.duplicateDetective, detective);

                    bool existConfirmation = false;
                    while (!existConfirmation)
                    {
                        string answer = Console.ReadLine();
                        try
                        {
                            if (answer.Trim().ToLower() == mainMenuText.no)
                            {
                                game = new Game(detective);
                                game.state = 2;
                                existConfirmation = true;
                                break;
                            }
                            else if (answer.Trim().ToLower() == mainMenuText.yes)
                            {
                                game = Game.LoadGame(detective);
                                existConfirmation = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine(mainMenuText.yesNoOnly);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                {
                    game = new Game(detective);
                }

                game.state = 2;
                return game;
            }
            else if (command == mainMenuText.loadGame)
            {

                string detective = GetDetective();

                try
                {
                    game = Game.LoadGame(detective);
                    game.state = 3;
                    return game;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return MainMenu();
                }
            }
            else if (command == mainMenuText.exitGame)
            {
                return game;
            }
            else
            {
                Console.WriteLine(mainMenuText.commandNotFound);
                return MainMenu();
            }            
        }

        public static void PrintTitle()
        {
            //object menu;
            Console.WriteLine(mainMenuText.title);
            Console.WriteLine(mainMenuText.subtitle);
        }

        //GetDetective gets the name of the detective from the player
        private static string GetDetective()
        {
            Console.WriteLine(mainMenuText.namePrompt);
            return Console.ReadLine();
        }

        //CaseMenu asks the detective which case he wants to work on.
        public static int CaseMenu(Game game)
        {
            if(game.caseTaken == 0)
            {
                //case numbers start at 1
                game.caseTaken++;
            }

            if ((game.activeCases == null) || (game.activeCases.Count() == 0))
            {
                game.GenerateCase(game);
            }
            
            int i = 0;

            while (game.activeCases.Count() <= game.caseTaken)
            {
                game.GenerateCase(game, game.caseTaken + i);
                i++;
            }

            Console.WriteLine(caseMenuText.caseDescription, game.caseTaken, game.activeCases[game.caseTaken].victim.name);
            Console.Write(caseMenuText.takeCase);
            Console.Write(" | ");
            Console.Write(caseMenuText.nextCase);
            Console.Write(" | ");
            Console.WriteLine(caseMenuText.exitGame);

            string command = Console.ReadLine();
            if (command == caseMenuText.takeCase)
            {
                //return case number
                game.SaveGame();
                return game.caseTaken;
            }
            else if (command == caseMenuText.nextCase)
            {
                game.caseTaken++;
                return CaseMenu(game);
            }
            else if (command == caseMenuText.exitGame)
            {
                return 0; //return 0 to give the command to exit
            }
            else
            {
                game.SaveGame();
                return CaseMenu(game);
            }
        }

        //This is where the code for investigating a scene goes. Returns gameState
        internal static Game CrimeSceneMenu(Game game)
        {
            //string[] gameLog;            //the entire game log is saved to the file
            
            ////print the crime scene
            //in the future 
            string caseDescription = game.activeCases[game.caseTaken].murderer.name;
            caseDescription += " killed ";
            caseDescription += game.activeCases[game.caseTaken].victim.name;
            caseDescription += " at ";
            caseDescription += game.activeCases[game.caseTaken].murderScene.name;
            caseDescription += " with ";
            caseDescription += game.activeCases[game.caseTaken].murderWeapon.name;
            caseDescription += ",";
            caseDescription += game.activeCases[game.caseTaken].murderWeapon.description;
            Console.WriteLine(caseDescription);
            game.state = 0;
            return game;
        }

        //this is where the code for talking to witnesses belongs. Returns gameState
        internal static Game WitnessDialogueMenu(Game game)
        {
            throw new NotImplementedException();
        }
        #endregion 

        #region crime scene commands
        //EvaluateCommand reads the input during gameplay and figures out what method to call
        static void EvaluateCommand(string inputString)
        {
            var command = inputString.Split(' ');

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

                case "dust":
                    DustForPrints(command[1]);
                    break;

                case "leave":
                    switch (command[1])
                    {
                        case "through": LeaveThroughDoor(command[2]); break;
                        default: LeaveScene(); break;
                    }
                    break;

                case "open":
                    OpenDoor(command[1]);
                    break;

                case "close":
                    CloseDoor(command[1]);
                    break;

                case "check":
                    switch (command[1])
                    {
                        case "notes": CheckNotes(); break;
                        case "photographs": CheckPhotographs(); break;
                        case "evidence": CheckEvidence(); break;
                    }
                    break;

                case "record":
                    RecordConversation();
                    break;

                default:
                    Console.WriteLine("?");
                    break;
            }
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
        #endregion
    }
}