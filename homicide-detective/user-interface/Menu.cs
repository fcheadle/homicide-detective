using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region menus
        //MainMenu returns an integer that correlates to gameState in Homicide-Detective.cs
        public static int MainMenu()
        {
            //TODO: load these strings from the JSON to support future translations
            Console.WriteLine("Homicide Detective");
            Console.WriteLine("Whenever two objects interact, some evidence of that interaction can be found and verified.");
            Console.WriteLine("-Theory of Transfer");
            Console.WriteLine("new | load | exit");
            Console.WriteLine("at any time, press ? for help.");

            Game game;
            int gameState = 0;
            string command = Console.ReadLine();
            command = command.ToLower();

            switch (command)
            {
                case "new":

                    string detective = GetDetective();
                    if (Game.CheckFile(detective))
                    {

                        bool existConfirmation = false;
                        while (!existConfirmation)
                        {
                            Console.WriteLine("There is already a detective named " + detective + ". Would you like to load that game instead?");
                            string answer = Console.ReadLine();

                            try
                            {
                                List<string> No = new List<string>();
                                No.Add("no");
                                No.Add("No");
                                No.Add("nO");
                                No.Add("NO");
                                List<string> Yes = new List<string>();
                                Yes.Add("yes");
                                Yes.Add("Yes");
                                Yes.Add("YES");

                                if (No.Contains(answer.Trim().ToLower()))
                                {
                                    game = new Game(detective);
                                    existConfirmation = true;
                                    break;
                                }
                                else if (Yes.Contains(answer.Trim().ToLower()))
                                {
                                    Game.LoadGame(detective);
                                    existConfirmation = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Umm... That didn't make sense. Yes/No answers ony!");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Holy Smokes! There was a problem: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        game = new Game(detective);
                    }

                    gameState = 2;
                    break;

                case "load":
                    
                    detective = GetDetective();
                    gameState = 3;
                    game = Game.LoadGame(detective);
                    break;

                case "exit":
                    return gameState;

                default:
                    Console.WriteLine("Command not recognized.");
                    gameState = MainMenu();
                    break;
            }

            return gameState;
        }

        //GetDetective gets the name of the detective from the player
        private static string GetDetective()
        {
            Console.WriteLine("What is your name, Detective?");
            return Console.ReadLine();
        }

        //CaseMenu asks the detective which case he wants to work on.
        public static int CaseMenu(int caseNumber)
        {
            Console.WriteLine("The next case on the docket is number " + caseNumber +", VICTIM NAME");
            Console.WriteLine("take | next | exit");
            string command = Console.ReadLine();
            switch (command)
            {
                case "take":
                    //return case number
                    return caseNumber; 

                case "next":
                    return CaseMenu(caseNumber + 1);

                case "exit":
                    return 0; //return  0 to give the command to exit

                default:
                    return CaseMenu(caseNumber);
            }
        }

        //This is where the code for investigating a scene goes. Returns gameState
        internal static int CrimeSceneMenu(int caseNumber)
        {
            string[] gameLog;            //the entire game log is saved to the file
            //print the crime scene
            throw new NotImplementedException();
        }

        //this is where the code for talking to witnesses belongs. Returns gameState
        internal static int WitnessDialogueMenu()
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