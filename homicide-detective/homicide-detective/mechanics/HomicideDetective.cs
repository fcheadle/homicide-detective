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
            bool gameInSession = false;

            gameInSession = Menu.MainMenu();

            while (gameInSession == true)
            {
                int caseTaken = Menu.CaseMenu(1);
                if (caseTaken == 0) gameInSession = false;
            }
        }
    }
}
