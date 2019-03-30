using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

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
        static string saveFolder = Directory.GetCurrentDirectory() + @"\save\";
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
        
        //these classes must match the JSON exactly
        internal class MainMenuText
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

        internal class CaseMenuText
        {
            public class ReviewCaseText
            {
                public string verb;
                public string saveCase;
                public string nextCase;
                public string bodyFound;
            }

            public class SceneSelectionText
            {
                public string flavorText;
                public string where;
            }

            public ReviewCaseText reviewCaseText;
            public SceneSelectionText sceneSelectionText;
            public string take;
            public string nextCase;
            public string exitGame;
            public string closeCase;
            public string open;
            public string check;
            public string saveCase;
        }

        internal class CSIMenuText
        {
            public class LookText
            {
                public string inside;
                public string at;
                public string behind;
                public string under;
                public string on;
                public string query;
                public string verb;
            }

            public class PhotographText
            {
                public string photograph;
                public string scene;
                public string query;
                public string verb;
            }

            public class TakeText
            {
                public string take;
                public string note;
                public string evidence;
                public string verb;
            }

            public class DustText
            {
                public string query;
                public string verb;
            }

            public class LeaveText
            {
                public string photograph;
                public string scene;
                public string query;
                public string verb;
            }

            public class OpenText
            {
                public string verb;
                public string query;
            }

            public class CloseText
            {
                public string verb;
                public string query;
            }

            public class Checktext
            {
                public string verb;
                public string query;
                public string notes;
                public string photos;
                public string fingerprints;
                public string evidence;
            }

            public LookText look;
            public PhotographText photograph;
            public TakeText take;
            public DustText dust;
            public LeaveText leave;
            public OpenText open;
            public CloseText close;
            public Checktext check;
            public string record;
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


        //these classes are so unit tests can have an overrideable way to read console input
        public class IO
        {
            public bool debug = false;
            TextWriter textWriterOriginal = Console.Out;
            TextReader textReaderOriginal = Console.In;

            public IO(bool debugMode)
            {
                debug = debugMode;
            }

            private void ResetIn()
            {
                //Return the console output to the console and close the file
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamReader streamReader = new StreamReader(fileStream);
                Console.SetIn(textReaderOriginal);
                streamReader.Close();
            }

            private void SetIn()
            {
                //Set console.out to write to the file instead of the console
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Open);
                StreamReader streamReader = new StreamReader(fileStream);
                Console.SetIn(streamReader);
            }
            
            public virtual string Get(bool debugMode = false)
            {
                debug = debugMode;

                if (debug) SetIn();
                string input = Console.ReadLine();
                if (debug) ResetIn();
                return input;
            }

            public virtual void Send(string output, bool debugMode = false)
            {
                if (debug) SetOut();
                Console.Write(output);
                if (debug) ResetOut();
            }

            public virtual void SendLine(string output, bool debugMode = false)
            {
                if (debug) SetOut();
                Console.WriteLine(output);
                if (debug) ResetOut();
            }

            public virtual void SendLine(string output, string detective, bool debugMode = false)
            {
                if (debug) SetOut();
                Console.WriteLine(output,detective);
                if (debug) ResetOut();
            }

            internal void Send(string output, string aAn, string name, bool debugMode = false)
            {
                if (debug) SetOut();
                Console.WriteLine(output, aAn, name);
                if (debug) ResetOut();
            }
            
            public void SetOut()
            {
                //Set console.out to write to the file instead of the console
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(streamWriter);
            }

            public void ResetOut()
            {
                //Return the console output to the console and close the file
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(textWriterOriginal);
                streamWriter.Close();
            }
        }
        #endregion

        #region menu controllers
        public static Game MainMenu(bool debug = false)
        {
            PrintMainMenuCommands();
            IO input = new IO(false);
            Game game = new Game();
            game.state = 0;
            return EvaluateMainMenuCommand(input.Get(debug), game);
        }

        public static Game EvaluateMainMenuCommand(string command, Game game, bool debug = false)
        {
            IO io = new IO(debug);
            command = command.ToLower();

            if (command == mainMenuText.newGame)
            {
                string detective = GetDetective();      //ask for detective name

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

                string detective = GetDetective();

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
                return game;
            }
            else
            {
                io.SendLine(mainMenuText.commandNotFound, debug);
                return MainMenu();
            }
        }

        public static void PrintMainMenuCommands(IO io = null, bool debug = false)
        {
            if (io == null)
            {
                io = new IO(debug);
            }

            io.Send(mainMenuText.newGame, debug);
            io.Send(" | ", debug);
            io.Send(mainMenuText.loadGame, debug);
            io.Send(" | ", debug);
            io.SendLine(mainMenuText.exitGame, debug);
        }

        public static void PrintTitle(bool debug = false)
        {
            IO output = new IO(debug);
            output.SendLine(mainMenuText.title, debug);
            output.SendLine(mainMenuText.subtitle, debug);
        }
        
        private static string GetDetective(bool debug = false)
        {
            IO input = new IO(debug);
            IO output = new IO(debug);
            output.SendLine(mainMenuText.namePrompt, debug);
            return input.Get(debug);
        }
        
        public static int CaseMenu(Game game, string command = null, bool debug = false)
        {
            CreateCaseIfNull(game);

            IO io = new IO(debug);
            Case thisCase = game.activeCases[game.caseTaken];
            PrintCaseSynopsis(thisCase, debug);
            PrintCaseMenu(debug);
            if (command != null) return EvaluateCaseCommand(game, command);
            else return EvaluateCaseCommand(game, io.Get(debug));
            
        }

        public static int EvaluateCaseCommand(Game game, string command, bool debug = false)
        {
            IO io = new IO(debug);
            Case thisCase = game.activeCases[game.caseTaken];
            if (command == caseMenuText.reviewCaseText.verb)
            {
                PrintCaseIntroduction(thisCase);
                PrintCaseReviewMenu(game);
                return EvaluateCaseReviewCommand(game, io.Get(debug));
            }
            else if (command == caseMenuText.take)
            {
                PrintSceneSelection(game.activeCases[game.caseTaken]);
                return game.caseIndex;
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
                return CaseMenu(game);
            }
        }

        private static void PrintCaseIntroduction(Case thisCase, bool debug = false)
        {
            //todo: move these hardcoded strings to the json
            IO io = new IO(debug);
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
            IO io = new IO(debug);
            io.SendLine(caseMenuText.sceneSelectionText.flavorText, debug);
            io.SendLine(caseMenuText.sceneSelectionText.where, debug);
        }

        private static int EvaluateCaseReviewCommand(Game game, string command, bool debug = false)
        {
            IO io = new IO(debug);
            string review = caseMenuText.reviewCaseText.verb;
            string save = caseMenuText.saveCase;
            string next = caseMenuText.reviewCaseText.verb;
          
            if (command == review)
            {
                PrintCaseSynopsis(game.activeCases[game.caseIndex]);
                return game.state++;
            }
            else if (command == save)
            {
                game.bookmarkedCases.Add(game.activeCases[game.caseIndex]);
                game.SaveGame();
                return game.state;
            }
            else if (command == next)
            {
                game.caseIndex++;
                return game.state;
            }
            else
            {
                return 0;
            }
        }

        private static void PrintCaseReviewMenu(Game game, bool debug = false)
        {
            IO output = new IO(debug);
            output.Send(caseMenuText.reviewCaseText.saveCase, debug);
            output.Send(" | ", debug);
            output.Send(caseMenuText.take, debug);
            output.Send(" | ", debug);
            output.Send(caseMenuText.reviewCaseText.nextCase, debug);
            output.Send(" | ", debug);
            output.SendLine(caseMenuText.exitGame, debug);
        }

        public static void CreateCaseIfNull(Game game)
        {
            if (game.caseTaken == 0)
            {
                //case numbers start at 1
                game.caseTaken++;
            }

            if ((game.activeCases == null) || (game.activeCases.Count() == 0))
            {
                game.GenerateCase(game);
                game.GenerateCase(game);
            }

            int i = 0;

            while (game.activeCases.Count() <= game.caseTaken)
            {
                game.GenerateCase(game, game.caseTaken + i);
                i++;
            }
        }

        private static void PrintCaseMenu(bool debug = false)
        {
            IO output = new IO(debug);
            output.Send(caseMenuText.reviewCaseText.verb, debug);
            output.Send(" | ", debug);
            output.Send(caseMenuText.take, debug);
            output.Send(" | ", debug);
            output.Send(caseMenuText.nextCase, debug);
            output.Send(" | ", debug);
            output.SendLine(caseMenuText.exitGame, debug);
        }

        private static void PrintCaseSynopsis(Case thisCase, bool debug = false)
        {
            IO output = new IO(debug);
            output.Send(caseDescription.intro, debug);
            output.Send(thisCase.caseNumber.ToString(), debug);
            output.Send(", ", debug);
            output.SendLine(thisCase.victim.name, debug);
        }

        public static void Cheat(Case game, bool debug = false)
        {
            IO io = new IO(debug);
            string sentence = game.murderer.name;
            sentence += " killed ";
            sentence += game.victim.name;
            sentence += " at ";
            sentence += game.murderScene.name;
            sentence += " with ";
            sentence += game.murderWeapon.name;
            sentence += ".";
            io.SendLine(sentence, debug);
        }
        
        public static void PrintCSICommands(Game game, bool debug = false)
        {
            IO output = new IO(debug);

            output.Send(csiMenuText.look.verb, debug);
            output.Send(" | ", debug);
            output.Send(csiMenuText.photograph.verb, debug);
            output.Send(" | ", debug);
            output.Send(csiMenuText.dust.verb, debug);
            output.Send(" | ", debug);
            output.Send(csiMenuText.take.verb, debug);
            output.Send(" | ", debug);
            output.Send(csiMenuText.open.verb, debug);
            output.Send(" | ", debug);
            output.Send(csiMenuText.close.verb, debug);
            output.Send(" | ", debug);
            output.Send(csiMenuText.leave.verb, debug);
            //output.Send(" | ");
            //output.SendLine(caseMenuText.record);
            output.Send(" | ", debug);
            output.SendLine(csiMenuText.check.verb, debug);

            //game.state = 0;
            //return game.state;
        }

        public static int CSIMenu(Game game, bool debug = false)
        {
            IO io = new IO(debug);
            PrintCSICommands(game);
            return EvaluateCSICommand(game, io.Get(debug));
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
        #endregion

        #region print methods
        //All print methods should take only what they need and return void
        #endregion

        #region evaluate methods
        //all evaluate methods should return a state integer of some sort based on an input string
        //io.get should only be called in from the menu methods
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