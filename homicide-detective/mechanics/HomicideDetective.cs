using System;

namespace homicide_detective
{
    class Program
    {
        /*
         * Homicide Detective
         * by Forrest Cheadle
         * with help from Jeremiah Hamilton
         * 03/11/2019 
         */
         
            /*
             * This game works by having a single game object 
             * it contains a random that is generated on based a seed: detective name
             * and it spawns new objects sending the random as the seed
             *                  
             * A Menu class is used for all writing to the console and reading menu inputs
             * Currently, this has many similar "PrintXXXCommand" or some similar
             * and soon, I want to try and refactor these to be a single class which reads and evaluates for whatever menu entry is in the json 
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



        static void Main()
        {
            //necessary variables - keep this section sparse
            Game game = new Game();


            //the game loop
            while (game.state != 0)
            {
                switch (game.state)
                {
                    case 0:
                        break;

                    case 1:                                     //case 1 is the main menu
                        Menu.PrintTitle();                      //
                        game = Menu.MainMenu();
                        break;

                    case 2:                                     //case 2 is the case menu
                        game.caseTaken = Menu.CaseMenu(game);

                        if (game.caseTaken == 0)
                        {
                            game.state = 0;
                        }
                        else
                        {
                            game.state = 3;
                        }

                        break;

                    case 3:
                        game.csiState = Menu.CSIMenu(game);   //case 3 is the crime scene menu / main game


                        break;

                    case 4:                                         //case 4 is the witness conversation mode
                        throw new NotImplementedException();
                        break;

                    default:
                        game.state = 0;
                        break;
                }
            }
        }
    }
}
