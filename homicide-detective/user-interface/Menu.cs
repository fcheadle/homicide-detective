using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using static homicide_detective.TextClasses;

namespace homicide_detective
{
    public static class Menu
    {
        //The Menu class contains all call-response from the game to the user.
        //Any time the game writes to the console, it should be the menu that does it
        //because of this, the entire class and everything in it can be static
        //crime scene commands are not implemented yet

        #region variables
        //System Variables
        static string textFolder = Directory.GetCurrentDirectory() + @"\objects\text\";
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
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

        //These objects are used to build the strings that are given to the user
        static MainMenuText mainMenuText = JsonConvert.DeserializeObject<MainMenuText>(mainMenuRaw);
        static CaseMenuText caseMenuText = JsonConvert.DeserializeObject<CaseMenuText>(caseMenuRaw);
        static CSIMenuText csiMenuText = JsonConvert.DeserializeObject<CSIMenuText>(csiMenuRaw);

        static ItemDescription itemDescription = JsonConvert.DeserializeObject<ItemDescription>(itemDescriptionRaw);
        static CaseDescription caseDescription = JsonConvert.DeserializeObject<CaseDescription>(caseDescriptionRaw);
        #endregion

        //These call the necessary print and Evaluate commands
        #region menu controllers
        public static Game MainMenu(bool debug = false)
        {
            PrintMainMenuCommands(debug);
            IO input = new IO();
            Game game = new Game();
            game.state = 0;
            return EvaluateMainMenuCommand(input.Get(debug), game);
        }
        
        public static int CaseMenu(Game game, string command = null, bool debug = false)
        {
            CreateCaseIfNull(game);

            IO io = new IO();
            Case thisCase = game.activeCases[game.caseTaken];
            PrintCaseSynopsis(thisCase, debug);
            PrintCaseMenuCommands(debug);
            if (command != null) return EvaluateCaseCommand(game, command, debug);
            else return EvaluateCaseCommand(game, io.Get(debug), debug);

        }

        public static int CSIMenu(Game game, bool debug = false)
        {
            IO io = new IO();
            PrintCSIMenuCommands(debug);
            return EvaluateCSICommand(game, io.Get(debug));
        }

        public static string GetDetective(bool debug = false)
        {
            IO io = new IO();
            io.SendLine(mainMenuText.namePrompt, debug);
            return io.Get(debug);
        }

        public static Case CreateCaseIfNull(Game game)
        {
            Case thisCase = new Case();

            if (game.caseTaken == 0)
            {
                //case numbers start at 1
                game.caseTaken++;
            }

            if ((game.activeCases == null) || (game.activeCases.Count() == 0))
            {
                thisCase = game.GenerateCase(game, game.caseTaken);
            }

            int i = 0;

            while (game.activeCases.Count() <= game.caseTaken)
            {
                game.GenerateCase(game, game.caseTaken + i);
                i++;
            }

            return thisCase;
        }

        public static Game BookmarkCase(Case thisCase, Game game)
        {
            game.bookmarkedCases.Add(thisCase);
            return game;
        }
        #endregion


        //All print methods should take only what they need and return void
        //an optional debug parameter writes to a file instead of console
        #region print methods
        public static void PrintCSIMenuCommands(bool debug = false)
        {
            IO io = new IO();
            string output = csiMenuText.look.verb;
            output += " | ";
            output += csiMenuText.photograph.verb;
            output += " | ";
            output += csiMenuText.take.verb;
            output += " | ";
            output += csiMenuText.dust.verb;
            output += " | ";
            output += csiMenuText.leave.verb;
            output += " | ";
            output += csiMenuText.open.verb;
            output += " | ";
            output += csiMenuText.close.verb;
            output += " | ";
            output += csiMenuText.record;
            output += " | ";
            output += csiMenuText.check.verb;
            io.SendLine(output, debug);
        }
        
        public static void PrintCaseMenuCommands(bool debug = false)
        {
            IO io = new IO();
            string output = "";
            output += caseMenuText.nextCase;
            output += " | ";
            output += caseMenuText.take;
            output += " | ";
            output += caseMenuText.reviewCaseText.verb;
            output += " | ";
            output += caseMenuText.exitGame;
            io.SendLine(output, debug);
        }

        public static void PrintCaseSynopsis(Case thisCase, bool debug = false)
        {
            IO io = new IO();
            string output = caseDescription.intro;
            output += thisCase.caseNumber.ToString();
            output += ",";
            output += thisCase.victim.name;
            io.SendLine(output, debug);
        }

        public static void PrintCaseIntroduction(Case thisCase, bool debug = false)
        {
            //todo: move these hardcoded strings to the json
            IO io = new IO();
            Person victim = thisCase.victim;
            string output = victim.name + victim.description;
            output += victim.pronounDescriptive + " was found dead in" + victim.pronounDescriptive + thisCase.murderScene.name + ".";
            if (thisCase.evidenceTaken == null)
            {
                output += " There has been no evidence taken yet.";
            }
            else
            {
                output += " Evidence taken includes";
                foreach (Item evidence in thisCase.evidenceTaken)
                {
                    output += "{0}{1}" + evidence.aAn + evidence.name;
                }
            }

            io.SendLine(output, debug);
        }

        public static void PrintSceneSelection(Case thisCase, bool debug = false)
        {
            IO io = new IO();
            io.SendLine(caseMenuText.sceneSelectionText.flavorText, debug);
            io.SendLine(caseMenuText.sceneSelectionText.where, debug);
        }

        public static void PrintCaseReviewMenu(Game game, bool debug = false)
        {
            IO io = new IO();
            string output = caseMenuText.reviewCaseText.bookmarkCase;
            output += " | ";
            output += caseMenuText.take;
            output += " | ";
            output += caseMenuText.reviewCaseText.nextCase;
            output += " | ";
            output += caseMenuText.exitGame;
            io.SendLine(output, debug);
        }

        public static void Cheat(Case thisCase, bool debug = false)
        {
            IO io = new IO();
            string sentence = thisCase.murderer.name;
            sentence += " killed";
            sentence += thisCase.victim.name;
            sentence += " at";
            sentence += thisCase.murderScene.name;
            sentence += " with";
            sentence += thisCase.murderWeapon.name;
            sentence += ".";
            io.SendLine(sentence, debug);
        }

        public static void PrintTitle(bool debug = false)
        {
            IO output = new IO();
            output.SendLine(mainMenuText.title, debug);
            output.SendLine(mainMenuText.subtitle, debug);
        }

        public static void PrintMainMenuCommands(bool debug = false)
        {
            IO io = new IO();
            string output = mainMenuText.newGame;
            output += " | ";
            output += mainMenuText.loadGame;
            output += " | ";
            output += mainMenuText.exitGame;
            io.SendLine(output, debug);
        }
        #endregion

        #region evaluate methods
        //all evaluate methods should return a state integer of some sort based on an input string
        //io.get should only be called in from the menu methods

        //Returns a game 
        public static Game EvaluateMainMenuCommand(string command, Game game, bool debug = false)
        {
            IO io = new IO();
            command = command.ToLower();

            if (command == mainMenuText.newGame)
            {
                string detective = GetDetective(debug);      //ask for detective name

                if (Game.CheckFile(detective))
                {
                    io.SendLine(mainMenuText.duplicateDetective, detective, debug);

                    bool existConfirmation = false;
                    while (!existConfirmation)
                    {
                        string answer = io.Get(debug);
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
                                io.SendLine(mainMenuText.yesNoOnly, debug);
                            }
                        }
                        catch (Exception e)
                        {
                            io.SendLine(e.Message, debug);
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

                string detective = GetDetective(debug);

                try
                {
                    game = Game.LoadGame(detective);
                    game.state = 3;
                    return game;
                }
                catch (Exception e)
                {
                    io.SendLine(e.Message, debug);
                    return MainMenu();
                }
            }
            else if (command == mainMenuText.exitGame)
            {
                game.state = 0;
                return game;
            }
            else
            {
                io.SendLine(mainMenuText.commandNotFound, debug);
                return MainMenu();
            }
        }

        //returns the case number of the taken case
        public static int EvaluateCaseCommand(Game game, string command, bool debug = false)
        {
            IO io = new IO();
            CreateCaseIfNull(game);
            Case thisCase = game.activeCases[game.caseTaken];

            //for debugging
            if (command.Contains(' ')) 
            {
                command = command.Split(' ')[0];
            }

            if (command == caseMenuText.reviewCaseText.verb)
            {
                PrintCaseIntroduction(thisCase, debug);
                PrintCaseReviewMenu(game, debug);
                game = EvaluateCaseReviewCommand(game, io.Get(debug),  debug);
                return game.caseTaken;
            }
            else if (command == caseMenuText.take)
            {
                PrintSceneSelection(game.activeCases[game.caseTaken], debug);
                return game.caseIndex;
            }
            else if (command == caseMenuText.nextCase)
            {
                game.caseTaken++;
                return game.caseTaken;
            }
            else if (command == caseMenuText.exitGame)
            {
                return 0; //return 0 to give the command to exit
            }
            else if (command == "cheat")
            {
                Cheat(thisCase, debug);
                return CaseMenu(game, null, debug);
            }
            else
            {
                return CaseMenu(game, null, debug);
            }
        }

        //sets caseTaken and bookmarkedCases and returns the game
        public static Game EvaluateCaseReviewCommand(Game game, string command, bool debug = false)
        {

            IO io = new IO();
            CreateCaseIfNull(game);
            Case thisCase = game.activeCases[game.caseTaken];

            //for debugging
            if (command.Contains(' '))
            {
                command = command.Split(' ')[0];
            }

            if (command == caseMenuText.bookmarkCase)
            {
                game = BookmarkCase(thisCase, game);
                return game;
            }
            else if (command == caseMenuText.take)
            {
                PrintSceneSelection(game.activeCases[game.caseTaken], debug);
                return game;
            }
            else if (command == caseMenuText.nextCase)
            {
                game.caseTaken++;
                return game;
            }
            else if (command == caseMenuText.exitGame)
            {
                game.caseTaken = 0;
                return game; //return 0 to give the command to exit
            }
            else if (command == "cheat")
            {
                Cheat(thisCase, debug);
                game.caseTaken = CaseMenu(game, null, debug);
                return game;
            }
            else
            {
                game.caseTaken = CaseMenu(game, null, debug);
                return game;
            }
        }

        public static int EvaluateCSICommand(Game game, string inputString)
        {
            if (inputString == csiMenuText.look.verb)
            {
                return 1;
            }
            else if (inputString == csiMenuText.open.verb)
            {
                return 2;
            }
            else if (inputString == csiMenuText.close.verb)
            {
                return 3;
            }
            else if (inputString == csiMenuText.take.verb)
            {
                return 4;
            }
            else if (inputString == csiMenuText.dust.verb)
            {
                return 5;
            }
            else if (inputString == csiMenuText.leave.verb)
            {
                return 6;
            }
            else if (inputString == csiMenuText.record)
            {
                return 7;
            }
            else if (inputString == csiMenuText.check.verb)
            {
                return 8;
            }
            else return 0;
            //      switch (inputString)
            //      {
            //          case "at": LookAt(command[2]); break;
            //          case "under": LookUnder(command[2]); break;
            //          case "inside": LookInsideOf(command[2]); break;
            //          case "on": LookOnTopOf(command[4]); break;
            //          case "behind": LookBehind(command[2]); break;
            //      }
            //      break;
            //
            //  case "photograph":
            //      switch (command[1])
            //      {
            //          case "scene": PhotographScene(); break;
            //          default: PhotographItem(command[1]); break;
            //      }
            //      break;
            //
            //  case "take":
            //      switch (command[1])
            //      {
            //          case "note": TakeNote(); break;
            //          default: TakeEvidence(command[1]); break;
            //      }
            //      break;
            //
            //  case "dust":
            //      DustForPrints(command[1]);
            //      break;
            //
            //  case "leave":
            //      switch (command[1])
            //      {
            //          case "through": LeaveThroughDoor(command[2]); break;
            //          default: LeaveScene(); break;
            //      }
            //      break;
            //
            //  case "open":
            //      OpenDoor(command[1]);
            //      break;
            //
            //  case "close":
            //      CloseDoor(command[1]);
            //      break;
            //
            //  case "check":
            //      switch (command[1])
            //      {
            //          case "notes": CheckNotes(); break;
            //          case "photographs": CheckPhotographs(); break;
            //          case "evidence": CheckEvidence(); break;
            //      }
            //      break;
            //
            //  case "record":
            //      RecordConversation();
            //      break;
            //
            //  default:
            //      Console.WriteLine("?");
            //      break;
            //}//
        }

        public static int EvaluateSceneSelectionCommand(Case thisCase, string inputSting)
        {
            return 3;
        }
        #endregion

        #region crime scene commands
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

        static void CloseDoor(string whichDoor)
        {
            throw new NotImplementedException();
        }

        static void OpenDoor(string whichDoor)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}