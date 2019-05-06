using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace homicide_detective
{
    /*
     * Homicide Detective
     * by Forrest Cheadle
     * with help from Jeremiah Hamilton
     * 03/11/2019 
     */

    /*
     * This game works by having a single "game" object 
     * it contains a random that is generated on based a seed: detective name
     * so that two games with the same detective name will produce the exact same cases, evidence, etc
     * and it spawns new objects sending the next random  int as the ID of the thing being generated
     *                  
     * A Menu class is used for all writing to the console and reading menu inputs
     * 
     * The main mechanics are that a game object contains many lists of cases, and some utility methods, such as saving and loading.
     * 
     * Cases contain a victim, a murderer, a murderweapon, all persons involved, even tangentially, all relevant scenes including things written of only once
     * 
     * Persons are complicated and implemented yet beyond a name. They will have relationships, personalities, appearances, and voices as complex objects in future releases of their own
     * 
     * Scenes are collections of various properties that contain a number and are related to one or more persons, connected to one or more scenes, and contain large numbers of items
     * 
     * A user interface group of classes get and write from the console or file to the game, and should be used everytime something of that nature happens
     */
     
    class Program
    { 
        static void Main()
        {
            Game game = new Game();
            while (game.state != 0)
            {
                game.Loop();
            }
        }
    }

    public class Game
    {
        #region game variables, constructors, and functions
        public string detectiveName;
        public int seed;
        public List<string> gameLog;
        public int caseIndex = 0;                               //the case currently being investigated
        public List<Case> cases = new List<Case>();       //cases that are neither solved nor cold. Generated one at a time during the case menu
        public List<int> bookmarkedCases = new List<int>();   //ones saved for later viewing

        private IO io = new IO();
        private Random random;

        public int state = 1;
        //state = 0;        //turn off game
        //state = 1;        //show main menu
        //state = 2;        //show case menu
        //state = 3;        //investigating a scene
        //state = 4;        //talking to persons of interest

        //need a blank constructor because JSONConvert instantiates the object with no arguments
        public Game()
        {

        }

        //constructor - new game
        public Game(string name)
        {
            detectiveName = name;

            if (detectiveName != null)
            {
                seed = ToInt(name);
                random = new Random(seed);
            }

            CreateCaseIfNull();
        }

        //the main GameLoop
        internal void Loop()
        {
            IO io = new IO();
            switch (state)
            {
                case 1:
                    MainMenu();
                    break;

                case 2:
                    CaseMenu();
                    break;

                case 3:
                    CsiMenu();
                    break;

                case 4:
                    WitnessDialogue();
                    break;

                default:
                    state = 0;
                    io.Write("Unknown Game State. Press any key to exit");
                    io.Read();
                    break;
            }
        }//end while
        #endregion

        #region menu
        private void MainMenu()
        {
            PrintTitle();
            while (state == 1)
            {
                PrintMenuCommands(Text.menu.main.ToList());
                state = EvaluateMainMenuCommand(io.Read());
            }
        }

        public int EvaluateMainMenuCommand(string command, bool debug = false)
        {
            Game game = new Game();
            IO io = new IO();
            command = command.ToLower();

            if (command == Text.menu.main.newGame)
            {
                string detective = GetDetective(debug);      //ask for detective name

                if (io.CheckThatFileExists(detective))
                {
                    io.WriteLine(Text.menu.response.duplicateDetective, detective, debug);

                    bool existConfirmation = false;
                    while (!existConfirmation)
                    {
                        string answer = io.Read(debug);
                        try
                        {
                            if (answer.Trim().ToLower() == Text.menu.response.no)
                            {
                                game = new Game(detective);
                                game.state = 2;
                                existConfirmation = true;
                                break;
                            }
                            else if (answer.Trim().ToLower() == Text.menu.response.yes)
                            {
                                game = io.Load(detective);
                                existConfirmation = true;
                                break;
                            }
                            else
                            {
                                io.WriteLine(Text.menu.response.yesNoOnly, debug);
                            }
                        }
                        catch (Exception e)
                        {
                            io.WriteLine(e.Message, debug);
                        }
                    }
                }
                else
                {
                    game = new Game(detective);
                }
                seed = ToInt(detective);
                random = new Random(seed);
                game.state = 2;
                return game.state;
            }
            else if (command == Text.menu.main.loadGame)
            {

                string detective = GetDetective(debug);

                try
                {
                    game = io.Load(detective);
                    game.state = 3;
                    return game.state;
                }
                catch (Exception e)
                {
                    io.WriteLine(e.Message, debug);
                    return game.state;
                }
            }
            else if (command == Text.menu.main.exitGame)
            {
                game.state = 0;
                return game.state;
            }
            else
            {
                io.WriteLine(Text.menu.response.commandNotFound, debug);
                return game.state;
            }
        }

        public static void PrintMenuCommands(List<string> commands, bool debug = false)
        {
            IO io = new IO();
            string output = commands[0];
            for (int i = 1; i < commands.Count; i++)
            {
                output += " | ";
                output += commands[i];
            }

            io.WriteLine(output, debug);
        }

        public void Cheat(Case _, bool debug = false)
        {
            //this is an unsustainable form, and should be replaced with some REAL cheat codes soon
            string sentence = _.persons[_.murderer].name;
            sentence += " killed";
            sentence += _.persons[_.victim].name;
            sentence += " at";
            //sentence += thisCase.murderScene.owners[0].name;
            //sentence += "'s";
            sentence += _.scenes[_.murderer].name;
            sentence += " with";
            sentence += _.items[_.murderWeapon].name;
            sentence += ".";
            io.WriteLine(sentence, debug);
        }

        internal void PrintQuery(string query, bool debug = false)
        {
            io.WriteLine(query);
        }

        public static void PrintTitle(bool debug = false)
        {
            IO output = new IO();
            output.WriteLine(Text.menu.main.title, debug);
            output.WriteLine(Text.menu.main.subtitle, debug);
        }

        public void PrintSceneDescription(Scene scene, bool debug = false)
        {
            IO io = new IO();
            string output = scene.ToString();
            io.WriteLine(output, debug);
        }
        
        public int EvaluateCaseCommand(Case thisCase, string command, bool debug = false)
        {
            IO io = new IO();
            
            //for debugging
            if (command.Contains(' '))
            {
                command = command.Split(' ')[0];
            }

            if (command == Text.menu._case.review)
            {
                io.WriteLine(thisCase.Review(), debug);
                return caseIndex;
            }
            else if (command == Text.menu._case.take)
            {
                state++;
                return caseIndex;
            }
            else if (command == Text.menu._case.next)
            {
                caseIndex++;
                return caseIndex;
            }
            else
            {
                return caseIndex;
            }
        }

        private void WitnessDialogue()
        {
            throw new NotImplementedException();
        }

        private void CaseMenu()
        {
            int menuState = 1;
            CreateCaseIfNull();
            while (menuState != 0)
            {
                PrintMenuCommands(Text.menu._case.ToList());
                caseIndex = EvaluateCaseCommand(cases[caseIndex], io.Read());
                if (caseIndex == 0)
                {
                    state = 0;
                }
            }
        }

        private void CsiMenu()
        {
            while (state == 3)
            {
                //switch (menuState)
                //{ }
                //    throw new NotImplementedException();
                //case 0:
                //    break;
                //case 1: // "look"
                //    Menu.PrintQuery(text.menu.response.lookUnderQuery);
                //    game = Menu.EvaluateLookQuery(game, io.Get());
                //    break;
                //case 2: // "open"
                //    Menu.PrintQuery(text.menu.response.openQuery);
                //    game = Menu.EvaluateOpenQuery(game, io.Get());
                //    break;
                //case 3: // "close"
                //    Menu.PrintQuery(text.menu.response.closeQuery);
                //    game = Menu.EvaluateCloseQuery(game, io.Get());
                //    break;
                //case 4: // "take"
                //    Menu.PrintQuery(text.menu.response.takeQuery);
                //    game = Menu.EvaluateTakeQuery(game, io.Get());
                //    break;
                //case 5: // "dust"
                //    Menu.PrintQuery(text.menu.response.dustQuery);
                //    game = Menu.EvaluateDustQuery(game, io.Get());
                //    break;
                //case 6: // "leave"
                //    Menu.PrintQuery(text.menu.response.leaveQuery);
                //    game = Menu.EvaluateLeaveQuery(game, io.Get());
                //    break;
                //case 7: // "record"
                //    break;
                //case 8: // "check"
                //    Menu.PrintQuery(text.menu.response.checkQuery);
                //    game = Menu.EvaluateCheckQuery(game, io.Get());
                //    break;
                //case 9: // "photograph"
                //    Menu.PrintQuery(text.menu.response.photographQuery);
                //    game = Menu.EvaluatePhotographQuery(game, io.Get());
                //    break;
                //case 10: // LookAt
                //    Menu.PrintQuery(text.menu.response.lookAtQuery);
                //    Menu.LookAt(game.activeCase.activeItem);
                //    game.menuState = 0;
                //    break;
                //case 11: // LookBehind
                //    Menu.PrintQuery(text.menu.response.lookBehindQuery);
                //    Menu.LookBehind(game.activeCase.activeItem);
                //    game.menuState = 0;
                //    break;
                //case 12: // LookInside
                //    Menu.PrintQuery(text.menu.response.lookInsideQuery);
                //    Menu.LookInsideOf(game.activeCase.activeItem);
                //    game.menuState = 0;
                //    break;
                //case 13: // LookOnTopOf
                //    Menu.PrintQuery(text.menu.response.lookOnQuery);
                //    Menu.LookOnTopOf(game.activeCase.activeItem);
                //    game.menuState = 0;
                //    break;
                //case 14: // LookUnderneath
                //    Menu.PrintQuery(text.menu.response.lookUnderQuery);
                //    Menu.LookUnder(game.activeCase.activeItem);
                //    game.menuState = 0;
                //    break;
                //}

                //todo: take out the game from this, and re-implement
                //Menu.PrintSceneDescription(game.activeCase.currentScene);
                //Menu.PrintMenuCommands(text.menu.csi.ToList());
                //game = Menu.EvaluateCSICommand(game, io.Get());
            }
        }
        #endregion

        #region csi
        internal void EvaluateCsiCommand(Game game, string input, bool debug = false)
        {
            IO io = new IO();

            if (input == Text.menu.csi.look)
            {
                io.WriteLine(Text.menu.response.lookUnderQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else if (input == Text.menu.csi.open)
            {
                io.WriteLine(Text.menu.response.openQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else if (input == Text.menu.csi.close)
            {
                io.WriteLine(Text.menu.response.closeQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else if (input == Text.menu.csi.take)
            {
                io.WriteLine(Text.menu.response.takeQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else if (input == Text.menu.csi.dust)
            {
                io.WriteLine(Text.menu.response.dustQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else if (input == Text.menu.csi.leave)
            {
                io.WriteLine(Text.menu.response.leaveQuery);
                EvaluateLeaveQuery(io.Read(debug));
            }
            //else if (input == text.menu.csi.record)
            //{
            //}
            else if (input == Text.menu.csi.check)
            {
                io.WriteLine(Text.menu.response.checkQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else if (input == Text.menu.csi.photograph)
            {
                io.WriteLine(Text.menu.response.photographQuery);
                EvaluateLookQuery(io.Read(debug));
            }
            else
            {
            }
        }

        private void EvaluateLeaveQuery(string v)
        {
            throw new NotImplementedException();
        }

        internal Game EvaluatePhotographQuery(Game game, string command)
        {
            throw new NotImplementedException();
        }

        internal Game EvaluateCheckQuery(Game game, string command)
        {
            throw new NotImplementedException();
        }

        internal void EvaluateLookQuery(string command)
        {
            if (command == Text.menu.look.at)
            {
                Item item = io.GetItemOrScene(command);
                LookAt(item);
            }
            else if (command == Text.menu.look.behind)
            {
                Item item = io.GetItemOrScene(command);
                LookBehind(item);
            }
            if (command == Text.menu.look.insideOf)
            {
                Item item = io.GetItemOrScene(command);
                LookInsideOf(item);
            }
            if (command == Text.menu.look.onTopOf)
            {
                Item item = io.GetItemOrScene(command);
                LookOnTopOf(item);
            }
            if (command == Text.menu.look.underneath)
            {
                Item item = io.GetItemOrScene(command);
                LookUnder(item);
            }
        }

        internal Game EvaluateOpenQuery(Game game, string command)
        {
            //foreach (Item item in game.activeCase.currentScene.contains)
            //{
            //    if (command == item.name)
            //    {
            //        item.Open();
            //    }
            //}
            //game.menuState = 0;
            return game;
        }

        internal Game EvaluateCloseQuery(Game game, string command)
        {
            //foreach (Item item in game.activeCase.currentScene.contains)
            //{
            //    if (command == item.name)
            //    {
            //        item.Close();
            //    }
            //}
            //game.menuState = 0;
            return game;
        }

        internal Game EvaluateTakeQuery(Game game, string command)
        {
            //foreach (Item item in game.activeCase.currentScene.contains)
            //{
            //    if (command == item.name)
            //    {
            //        game.activeCase.evidenceTaken.Add(item);
            //    }
            //}
            //game.menuState = 0;
            return game;
        }

        internal Game EvaluateDustQuery(Game game, string command)
        {
            List<FingerPrint> prints = new List<FingerPrint>();
            //foreach (Item item in game.activeCase.currentScene.contains)
            //{
            //    if (command == item.name)
            //    {
            //        prints = item.GetFingerPrints();
            //    }
            //}
            foreach (FingerPrint print in prints)
            {
                //game.caseIndex.printsTaken.Add(print);
            }
            //game.menuState = 0;
            return game;
        }

        internal Game EvaluateLeaveQuery(Game game, string command)
        {
            //foreach (Scene scene in game.activeCase.currentScene.connections)
            //{
            //    if (command == scene.name)
            //    {
            //        game.activeCase.Current(scene);
            //    }
            //}
            return game;
        }

        internal void LookAt(Item item, bool debug = false)
        {
            IO io = new IO();
            string output = " This is a ";
            output += item.name;
            //todo: item description
            io.WriteLine(output);
        }

        internal void LookUnder(Item item)
        {
            IO io = new IO();
            string output = " Below the item is ";
            //output += item.name;
            //todo
            io.WriteLine(output);
        }

        internal void LookInsideOf(Item item)
        {
            IO io = new IO();
            string output = " This is a ";
            //output += item.name;
            //todo: item description
            io.WriteLine(output);
        }

        internal void LookOnTopOf(Item item)
        {
            IO io = new IO();
            string output = " This is a ";
            output += item.name;
            //todo: item description
            io.WriteLine(output);
        }

        internal void LookBehind(Item item)
        {
            IO io = new IO();
            string output = " This is a ";
            output += item.name;
            //todo: item description
            io.WriteLine(output);
        }

        internal void PhotographScene()
        {
            throw new NotImplementedException();
        }

        internal void PhotographItem(string item)
        {
            throw new NotImplementedException();
        }

        internal void TakeNote()
        {
            throw new NotImplementedException();
        }

        internal void TakeEvidence(string item)
        {
            throw new NotImplementedException();
        }

        internal void DustForPrints(string item)
        {
            throw new NotImplementedException();
        }

        internal void LeaveScene()
        {
            throw new NotImplementedException();
        }

        internal void LeaveThroughDoor(string door)
        {
            throw new NotImplementedException();
        }

        internal void RecordConversation()
        {
            throw new NotImplementedException();
        }

        internal void CheckEvidence()
        {
            throw new NotImplementedException();
        }

        internal void CheckPhotographs()
        {
            throw new NotImplementedException();
        }

        internal void CheckNotes()
        {
            throw new NotImplementedException();
        }

        internal void CloseDoor(string whichDoor)
        {
            throw new NotImplementedException();
        }

        internal void OpenDoor(string whichDoor)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region utitlities
        // From base36 to base10
        private static int ToInt(string input)
        {
            string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";
            input = Sanitize(input);
            var reversed = input.ToLower().Reverse();
            int result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += CharList.IndexOf(c) * (int) Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
        
        //get the detective's name ready to be converted to a base36 number for the seed
        public static string Sanitize(string input)
        {
            char[] separator = { ' ', '-', '\'', '.', ',', '?' };
            string[] afterSplit = input.Split(separator);
            string output = "";
            foreach (string s in afterSplit)
            {
                output = output + s;
            }

            return output;
        }

        private void CreateCaseIfNull()
        {
            if (caseIndex == 0)
            {
                //case numbers start at 1
                caseIndex++;
            }

            if ((cases == null) || (cases.Count == 0))
            {
                cases.Add(new Case(random.Next(), caseIndex));
            }

            int i = 0;
            while (cases.Count <= caseIndex)
            {
                cases.Add(new Case(random.Next(), caseIndex + i));
                i++;
            }
        }
        
        public static string GetDetective(bool debug = false)
        {
            IO io = new IO();
            io.WriteLine(Text.menu.response.namePrompt, debug);
            return io.Read(debug);
        }
        #endregion
    }
}