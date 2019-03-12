using System;

namespace homicide_detective
{
    

    class Program
    {
        static void Main()
        {
            bool gameInSession = false;
            UserInterface ui = new UserInterface();

            gameInSession = ui.MainMenu();

            while (gameInSession == true)
            {

            }
        }
    }
}
