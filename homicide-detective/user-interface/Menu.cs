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
        //MainMenu returns a true if the game is in session, and false if the game should quit
        public static bool MainMenu()
        {

            Console.WriteLine("Homicide Detective");
            Console.WriteLine("Whenever two objects interact, some evidence of that interaction can be found and verified.");
            Console.WriteLine("-Theory of Transfer");
            Console.WriteLine("new | load | exit");
            Console.WriteLine("at any time, press ? for help.");

            bool gameInSession = false;
            string command = Console.ReadLine();
            command = command.ToLower();

            switch (command)
            {
                case "new":

                    string detective = GetDetective();
                    Game game = new Game(detective);
                    gameInSession = true;

                    break;

                case "load":
                    
                    detective = GetDetective();
                    gameInSession = true;
                    game = Game.LoadGame(detective);
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

        #region active_commands
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