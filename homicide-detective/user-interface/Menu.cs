using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace homicide_detective
{
    public static class Menu
    {
        //The Menu class contains all call-response from the game to the user.
        //when Menu is called, it will almost always return an integer gameState:
        //gameState = 0;        //turn off game
        //gameState = 1;        //show main menu
        //gameState = 2;        //show case menu
        //gameState = 3;        //investigating a scene
        //gameState = 4;        //talking to persons of interest
        //gameState = 5;        //escape menu


        #region variables
        static string textFolder = Directory.GetCurrentDirectory() + @"\objects\text\";
        static string extension = ".json";

        static string mainMenuPath = textFolder + "menu_main" + extension;
        static string caseMenuPath = textFolder + "menu_case" + extension;
        static string csiMenuPath = textFolder + "menu_csi" + extension;
        static string caseDescriptionPath = textFolder + "description_case" + extension;
        static string itemDescriptionPath = textFolder + "description_item" + extension;

        static string mainMenuRaw = File.ReadAllText(mainMenuPath);
        static string caseMenuRaw = File.ReadAllText(caseMenuPath);
        static string csiMenuRaw = File.ReadAllText(csiMenuPath);
        static string caseDescriptionRaw = File.ReadAllText(caseDescriptionPath);
        static string itemDescriptionRaw = File.ReadAllText(itemDescriptionPath);

        static MainMenuText mainMenuText = JsonConvert.DeserializeObject<MainMenuText>(mainMenuRaw);
        static CaseMenuText caseMenuText = JsonConvert.DeserializeObject<CaseMenuText>(caseMenuRaw);
        static CSIMenuText csiMenuText = JsonConvert.DeserializeObject<CSIMenuText>(csiMenuRaw);

        static ItemDescription itemDescription = JsonConvert.DeserializeObject<ItemDescription>(itemDescriptionRaw);
        static CaseDescription caseDescription = JsonConvert.DeserializeObject<CaseDescription>(caseDescriptionRaw);

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
            public string takeCase;
            public string reviewCase;
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

        internal class ItemDescription
        {
            public string smaller;
            public string smallerAdverb;
            public string largerAdverb;
            public string larger;
            public string sizeDescription;
            public string exactly;
        }

        internal class CaseDescription
        {
            public string intro;
        }

        public class InputRetriever
        {
            public virtual string Get()
            {
                return Console.ReadLine();
            }

            public virtual string Get(int index)
            {
                return Console.ReadLine();
            }
        }

        public class OutputSender
        {
            public virtual void Send(string output)
            {
                Console.Write(output);
            }

            internal void SendLine(string output)
            {
                Console.WriteLine(output);
            }

            internal void SendLine(string output, string detective)
            {
                Console.WriteLine(output, detective);
            }
        }
        #endregion

        #region menus
        public static Game MainMenu()
        {
            PrintMainMenuCommands();
            InputRetriever input = new InputRetriever();
            Game game = new Game();
            game.state = 0;
            return EvaluateMainMenuCommand(input.Get(), game);
        }

        private static Game EvaluateMainMenuCommand(string command, Game game)
        {
            InputRetriever input = new InputRetriever();
            OutputSender output = new OutputSender();
            command = command.ToLower();

            if (command == mainMenuText.newGame)
            {
                string detective = GetDetective();
                if (Game.CheckFile(detective))
                {
                    output.SendLine(mainMenuText.duplicateDetective, detective);

                    bool existConfirmation = false;
                    while (!existConfirmation)
                    {
                        string answer = input.Get();
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
                                output.SendLine(mainMenuText.yesNoOnly);
                            }
                        }
                        catch (Exception e)
                        {
                            output.SendLine(e.Message);
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
                    output.SendLine(e.Message);
                    return MainMenu();
                }
            }
            else if (command == mainMenuText.exitGame)
            {
                return game;
            }
            else
            {
                output.SendLine(mainMenuText.commandNotFound);
                return MainMenu();
            }
        }

        private static void PrintMainMenuCommands()
        {
            OutputSender output = new OutputSender();
            output.Send(mainMenuText.newGame);
            output.Send(" | ");
            output.Send(mainMenuText.loadGame);
            output.Send(" | ");
            output.SendLine(mainMenuText.exitGame);
        }

        public static void PrintTitle()
        {
            OutputSender output = new OutputSender();
            output.SendLine(mainMenuText.title);
            output.SendLine(mainMenuText.subtitle);
        }
        
        private static string GetDetective()
        {
            InputRetriever input = new InputRetriever();
            OutputSender output = new OutputSender();
            output.SendLine(mainMenuText.namePrompt);
            return input.Get();
        }
        
        public static int CaseMenu(Game game)
        {
            CreateCaseIfNull(game);

            InputRetriever input = new InputRetriever();
            Case thisCase = game.activeCases[game.caseTaken];
            int caseNumber = thisCase.caseNumber;
            string victimName = thisCase.victim.name;
            //string caseDescription = game.activeCases[game.caseTaken].victim.description;
            PrintCaseSynopsis(thisCase);
            PrintCaseMenu();
            return EvaluateCaseCommand(input.Get(), game);
            
        }

        private static int EvaluateCaseCommand(string command, Game game)
        {
            InputRetriever input = new InputRetriever();
            Case thisCase = game.activeCases[game.caseTaken];
            if (command == caseMenuText.reviewCase)
            {
                Cheat(thisCase);
                return CaseMenu(game);
            }
            else if (command == caseMenuText.takeCase)
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
            else if (command == "cheat")
            {
                Cheat(thisCase);
                return CaseMenu(game);
            }
            else
            {
                game.SaveGame();
                return CaseMenu(game);
            }
        }

        private static void CreateCaseIfNull(Game game)
        {
            if (game.caseTaken == 0)
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
        }

        private static void PrintCaseMenu()
        {
            OutputSender output = new OutputSender();
            output.Send(caseMenuText.reviewCase);
            output.Send(" | ");
            output.Send(caseMenuText.takeCase);
            output.Send(" | ");
            output.Send(caseMenuText.nextCase);
            output.Send(" | ");
            output.SendLine(caseMenuText.exitGame);
        }

        private static void PrintCaseSynopsis(Case thisCase)
        {
            OutputSender output = new OutputSender();
            output.Send(caseDescription.intro);
            output.Send(thisCase.caseNumber.ToString());
            output.Send(", ");
            output.SendLine(thisCase.victim.name);
        }

        private static void Cheat(Case game)
        {
            OutputSender output = new OutputSender();
            string sentence = game.murderer.name;
            sentence += " killed ";
            sentence += game.victim.name;
            sentence += " at ";
            sentence += game.murderScene.name;
            sentence += " with ";
            sentence += game.murderWeapon.name;
            sentence += ",";
            sentence += game.murderWeapon.description;
            output.SendLine(sentence);
        }
        
        internal static Game CrimeSceneMenu(Game game)
        {
            //Console.WriteLine(caseDescription);
            game.state = 0;
            return game;
        }
        
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