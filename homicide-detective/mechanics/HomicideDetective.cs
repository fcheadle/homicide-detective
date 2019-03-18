using homicide_detective.user_interface;
using System;

namespace homicide_detective
{
    class Program
    {
        /*
         * Homicide Detective
         * by Forrest Cheadle
         * 03/11/2019 
         */
         
        static void Main()
        {
            Game game = new Game();
            while (game.state != 0)
            {
                switch (game.state)
                {
                    case 0:
                        break;

                    case 1:
                        game = Menu.MainMenu();
                        break;

                    case 2:
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
                        game = Menu.CrimeSceneMenu(game);
                        break;

                    case 4:
                        game = Menu.WitnessDialogueMenu(game);
                        break;

                    default:
                        game.state = 0;
                        break;
                }
            }
        }
    }
}
