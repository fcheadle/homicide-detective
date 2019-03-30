using System;

namespace homicide_detective
{
    class Program
    {
        /*
         * Homicide Detective
         * by Forrest Cheadle
         * and Jeremiah Hamilton
         * 03/11/2019 
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
