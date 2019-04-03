using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using static homicide_detective.Text;

namespace homicide_detective
{
    public static class Menu
    {
        //The Menu class contains all call-response from the game to the user.
        //Any time the game writes to the console, it should be the menu that does it
        //because of this, the entire class and everything in it can be static
        //crime scene commands are not implemented yet

        #region variables
        static Text text = new Text();
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
            return EvaluateCSICommand(io.Get(debug));
        }

        public static string GetDetective(bool debug = false)
        {
            IO io = new IO();
            io.SendLine(text.mainMenuText.namePrompt, debug);
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
                thisCase = Game.AddCase(game);
            }

            int i = 0;

            while (game.activeCases.Count() <= game.caseTaken)
            {
                thisCase = Game.AddCase(game);
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
            string output = text.csiMenuText.look.verb;
            output += " | ";
            output += text.csiMenuText.photograph.verb;
            output += " | ";
            output += text.csiMenuText.take.verb;
            output += " | ";
            output += text.csiMenuText.dust.verb;
            output += " | ";
            output += text.csiMenuText.leave.verb;
            output += " | ";
            output += text.csiMenuText.open.verb;
            output += " | ";
            output += text.csiMenuText.close.verb;
            output += " | ";
            output += text.csiMenuText.record;
            output += " | ";
            output += text.csiMenuText.check.verb;
            io.SendLine(output, debug);
        }
        
        public static void PrintCaseMenuCommands(bool debug = false)
        {
            IO io = new IO();
            string output = "";
            output += text.caseMenuText.nextCase;
            output += " | ";
            output += text.caseMenuText.take;
            output += " | ";
            output += text.caseMenuText.reviewCaseText.verb;
            output += " | ";
            output += text.caseMenuText.exitGame;
            io.SendLine(output, debug);
        }

        public static void PrintCaseSynopsis(Case thisCase, bool debug = false)
        {
            IO io = new IO();
            string output = text.caseDescription.intro;
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
            io.SendLine(text.caseMenuText.sceneSelectionText.flavorText, debug);
            io.SendLine(text.caseMenuText.sceneSelectionText.where, debug);
        }

        public static void PrintCaseReviewMenu(Game game, bool debug = false)
        {
            IO io = new IO();
            string output = text.caseMenuText.reviewCaseText.bookmarkCase;
            output += " | ";
            output += text.caseMenuText.take;
            output += " | ";
            output += text.caseMenuText.reviewCaseText.nextCase;
            output += " | ";
            output += text.caseMenuText.exitGame;
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
            output.SendLine(text.mainMenuText.title, debug);
            output.SendLine(text.mainMenuText.subtitle, debug);
        }

        public static void PrintMainMenuCommands(bool debug = false)
        {
            IO io = new IO();
            string output = text.mainMenuText.newGame;
            output += " | ";
            output += text.mainMenuText.loadGame;
            output += " | ";
            output += text.mainMenuText.exitGame;
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

            if (command == text.mainMenuText.newGame)
            {
                string detective = GetDetective(debug);      //ask for detective name

                if (Game.CheckFile(detective))
                {
                    io.SendLine(text.mainMenuText.duplicateDetective, detective, debug);

                    bool existConfirmation = false;
                    while (!existConfirmation)
                    {
                        string answer = io.Get(debug);
                        try
                        {
                            if (answer.Trim().ToLower() == text.mainMenuText.no)
                            {
                                game = new Game(detective);
                                game.state = 2;
                                existConfirmation = true;
                                break;
                            }
                            else if (answer.Trim().ToLower() == text.mainMenuText.yes)
                            {
                                game = Game.LoadGame(detective);
                                existConfirmation = true;
                                break;
                            }
                            else
                            {
                                io.SendLine(text.mainMenuText.yesNoOnly, debug);
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
            else if (command == text.mainMenuText.loadGame)
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
            else if (command == text.mainMenuText.exitGame)
            {
                game.state = 0;
                return game;
            }
            else
            {
                io.SendLine(text.mainMenuText.commandNotFound, debug);
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

            if (command == text.caseMenuText.reviewCaseText.verb)
            {
                PrintCaseIntroduction(thisCase, debug);
                PrintCaseReviewMenu(game, debug);
                game = EvaluateCaseReviewCommand(game, io.Get(debug),  debug);
                return game.caseTaken;
            }
            else if (command == text.caseMenuText.take)
            {
                PrintSceneSelection(game.activeCases[game.caseTaken], debug);
                return game.caseIndex;
            }
            else if (command == text.caseMenuText.nextCase)
            {
                game.caseTaken++;
                return game.caseTaken;
            }
            else if (command == text.caseMenuText.exitGame)
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

            if (command == text.caseMenuText.bookmarkCase)
            {
                game = BookmarkCase(thisCase, game);
                return game;
            }
            else if (command == text.caseMenuText.take)
            {
                PrintSceneSelection(game.activeCases[game.caseTaken], debug);
                return game;
            }
            else if (command == text.caseMenuText.nextCase)
            {
                game.caseTaken++;
                return game;
            }
            else if (command == text.caseMenuText.exitGame)
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

        public static int EvaluateCSICommand(string inputString)
        {
            if (inputString == text.csiMenuText.look.verb)
            {
                return 1;
            }
            else if (inputString == text.csiMenuText.open.verb)
            {
                return 2;
            }
            else if (inputString == text.csiMenuText.close.verb)
            {
                return 3;
            }
            else if (inputString == text.csiMenuText.take.verb)
            {
                return 4;
            }
            else if (inputString == text.csiMenuText.dust.verb)
            {
                return 5;
            }
            else if (inputString == text.csiMenuText.leave.verb)
            {
                return 6;
            }
            else if (inputString == text.csiMenuText.record)
            {
                return 7;
            }
            else if (inputString == text.csiMenuText.check.verb)
            {
                return 8;
            }
            else if (inputString == text.csiMenuText.photograph.verb)
            {
                return 9;
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