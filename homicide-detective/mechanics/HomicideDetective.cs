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

        static int gameState = 1;
        //gameState = 0;        //turn off game
        //gameState = 1;        //show main menu
        //gameState = 2;        //show case menu
        //gameState = 3;        //investigating a scene
        //gameState = 4;        //talking to persons of interest
        static int caseTaken;

        static void Main()
        {
            while (gameState != 0)
            {
                switch (gameState)
                {
                    case 0:
                        break;

                    case 1:
                        gameState = Menu.MainMenu();
                        break;

                    case 2:
                        caseTaken = Menu.CaseMenu(0);

                        if (caseTaken == 0)
                        {
                            gameState = 0;
                        }
                        else
                        {
                            gameState = 3;
                        }

                        break;

                    case 3:
                        gameState = Menu.CrimeSceneMenu(caseTaken);
                        break;

                    case 4:
                        gameState = Menu.WitnessDialogueMenu();
                        break;

                    default:
                        gameState = 0;
                        break;
                }
            }
        }
    }
}
