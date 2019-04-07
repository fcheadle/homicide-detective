using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        //All print methods should take only what they need and return void
        //an optional debug parameter writes to a file instead of console
        #region print methods
        public static void PrintMenuCommands(List<string> commands, bool debug = false)
        {
            IO io = new IO();
            string output = commands[0];
            for(int i = 1; i < commands.Count; i++)
            {
                output += " | ";
                output += commands[i];
            }

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

        public static void PrintCaseReview(Case thisCase, bool debug = false)
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

        public static void Cheat(Case thisCase, bool debug = false)
        {
            IO io = new IO();
            string sentence = thisCase.murderer.name;
            sentence += " killed";
            sentence += thisCase.victim.name;
            sentence += " at";
            //sentence += thisCase.murderScene.owners[0];
            //sentence += "'s";
            sentence += thisCase.murderScene.name;
            sentence += " with";
            sentence += thisCase.murderWeapon.name;
            sentence += ".";
            io.SendLine(sentence, debug);
        }

        internal static void PrintQuery(string query, bool debug = false)
        {
            IO io = new IO();
            io.SendLine(query);
        }

        public static void PrintTitle(bool debug = false)
        {
            IO output = new IO();
            output.SendLine(text.menu.main.title, debug);
            output.SendLine(text.menu.main.subtitle, debug);
        }

        internal static void PrintSceneDescription(Scene scene)
        {
            IO io = new IO();
            string output = " This is";
            //output += scene.owners[0];
            output += "'s ";
            output += scene.name;
            output += ".";
        }

        public static string GetDetective(bool debug = false)
        {
            IO io = new IO();
            io.SendLine(text.menu.response.namePrompt, debug);
            return io.Get(debug);
        }
        #endregion

        #region main menu
        //Returns a game 
        public static Game EvaluateMainMenuCommand(string command, bool debug = false)
        {
            Game game = new Game();
            IO io = new IO();
            command = command.ToLower();

            if (command == text.menu.main.newGame)
            {
                string detective = GetDetective(debug);      //ask for detective name

                if (Game.CheckFile(detective))
                {
                    io.SendLine(text.menu.response.duplicateDetective, detective, debug);

                    bool existConfirmation = false;
                    while (!existConfirmation)
                    {
                        string answer = io.Get(debug);
                        try
                        {
                            if (answer.Trim().ToLower() == text.menu.response.no)
                            {
                                game = new Game(detective);
                                game.state = 2;
                                existConfirmation = true;
                                break;
                            }
                            else if (answer.Trim().ToLower() == text.menu.response.yes)
                            {
                                game = Game.LoadGame(detective);
                                existConfirmation = true;
                                break;
                            }
                            else
                            {
                                io.SendLine(text.menu.response.yesNoOnly, debug);
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
            else if (command == text.menu.main.loadGame)
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
                    return game;//MainMenu();
                }
            }
            else if (command == text.menu.main.exitGame)
            {
                game.state = 0;
                return game;
            }
            else
            {
                io.SendLine(text.menu.response.commandNotFound, debug);
                return game;
            }
        }

        #endregion

        #region case menus
        //returns the case number of the taken case
        public static Game EvaluateCaseCommand(Game game, string command, bool debug = false)
        {
            IO io = new IO();
            Case thisCase = game.activeCases[game.caseTaken];

            //for debugging
            if (command.Contains(' ')) 
            {
                command = command.Split(' ')[0];
            }

            if (command == text.menu._case.review)
            {
                game.menuState = 1;
                return game;
            }
            else if (command == text.menu._case.take)
            {
                game.state++;
                game.activeCase = game.activeCases[game.caseTaken];
                return game;
            }
            else if (command == text.menu._case.next)
            {
                game.menuState = 0;
                game.caseTaken++;
                return game;
            }
            else if (command == "cheat")
            {
                Cheat(thisCase, debug);
                game.menuState = 1;
                return game;
            }
            else
            {
                return game;
            }
        }

        public static Game BookmarkCase(Case thisCase, Game game)
        {
            game.bookmarkedCases.Add(thisCase);
            return game;
        }
        #endregion

        #region crime scene menu
        public static Game EvaluateCSICommand(Game game, string input, bool debug = false)
        {
            IO io = new IO();

            if (input == text.menu.csi.look)
            {
                io.SendLine(text.menu.response.lookUnderQuery);
                game.menuState = 1;
                return game;
            }
            else if (input == text.menu.csi.open)
            {
                io.SendLine(text.menu.response.openQuery);
                game.menuState = 2;
                return game;
            }
            else if (input == text.menu.csi.close)
            {
                io.SendLine(text.menu.response.closeQuery);
                game.menuState = 3;
                return game;
            }
            else if (input == text.menu.csi.take)
            {
                io.SendLine(text.menu.response.takeQuery);
                game.menuState = 4;
                return game;
            }
            else if (input == text.menu.csi.dust)
            {
                io.SendLine(text.menu.response.dustQuery);
                game.menuState = 5;
                return game;
            }
            else if (input == text.menu.csi.leave)
            {
                io.SendLine(text.menu.response.leaveQuery);
                game.menuState = 6;
                return game;
            }
            else if (input == text.menu.csi.record)
            {
                game.menuState = 7;
                return game;
            }
            else if (input == text.menu.csi.check)
            {
                io.SendLine(text.menu.response.checkQuery);
                game.menuState = 8;
                return game;
            }
            else if (input == text.menu.csi.photograph)
            {
                io.SendLine(text.menu.response.photographQuery);
                game.menuState = 9;
                return game;
            }
            else
            {
                return game;
            }
        }

        internal static Game EvaluatePhotographQuery(Game game, string command)
        {
            throw new NotImplementedException();
        }

        internal static Game EvaluateCheckQuery(Game game, string command)
        {
            throw new NotImplementedException();
        }

        internal static Game EvaluateLookQuery(Game game, string command)
        {
            if (command == text.menu.look.at)
            {
                game.menuState = 10;
            }
            else if (command == text.menu.look.behind)
            {
                game.menuState = 11;
            }
            if (command == text.menu.look.insideOf)
            {
                game.menuState = 12;
            }
            if (command == text.menu.look.onTopOf)
            {
                game.menuState = 13;
            }
            if (command == text.menu.look.underneath)
            {
                game.menuState = 14;
            }
            return game;
        }

        internal static Game EvaluateOpenQuery(Game game, string command)
        {
            foreach (Item item in game.activeCase.currentScene.contains)
            {
                if (command == item.name)
                {
                    item.Open();
                }
            }
            game.menuState = 0;
            return game;
        }

        internal static Game EvaluateCloseQuery(Game game, string command)
        {
            foreach (Item item in game.activeCase.currentScene.contains)
            {
                if (command == item.name)
                {
                    item.Close();
                }
            }
            game.menuState = 0;
            return game;
        }

        internal static Game EvaluateTakeQuery(Game game, string command)
        {
            foreach (Item item in game.activeCase.currentScene.contains)
            {
                if (command == item.name)
                {
                    game.activeCase.evidenceTaken.Add(item);
                }
            }
            game.menuState = 0;
            return game;
        }

        internal static Game EvaluateDustQuery(Game game, string command)
        {
            List<Person.FingerPrint> prints = new List<Person.FingerPrint>();
            foreach (Item item in game.activeCase.currentScene.contains)
            {
                if (command == item.name)
                {
                    prints = item.GetFingerPrints();
                }
            }
            foreach (Person.FingerPrint print in prints)
            {
                game.activeCase.printsTaken.Add(print);
            }
            game.menuState = 0;
            return game;
        }

        internal static Game EvaluateLeaveQuery(Game game, string command)
        {
            foreach (Scene scene in game.activeCase.currentScene.connections)
            {
                if (command == scene.name)
                {
                    game.activeCase.currentScene = scene;
                }
            }
            return game;
        }

        public static void LookAt(Item item, bool debug = false)
        {
            IO io = new IO();
            string output = " This is a ";
            output += item.name;
            //todo: item description
            io.SendLine(output);
        }

        public static void LookUnder(Item item)
        {
            IO io = new IO();
            string output = " Below the item is ";
            //output += item.name;
            //todo
            io.SendLine(output);
        }

        public static void LookInsideOf(Item item)
        {
            IO io = new IO();
            string output = " This is a ";
            //output += item.name;
            //todo: item description
            io.SendLine(output);
        }

        public static void LookOnTopOf(Item item)
        {
            IO io = new IO();
            string output = " This is a ";
            output += item.name;
            //todo: item description
            io.SendLine(output);
        }

        public static void LookBehind(Item item)
        {
            IO io = new IO();
            string output = " This is a ";
            output += item.name;
            //todo: item description
            io.SendLine(output);
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