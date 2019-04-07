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
          * This game works by having a single "game" object 
          * it contains a random that is generated on based a seed: detective name
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
             
        static void Main()
        {
            //necessary variables - keep this section sparse
            Game game = new Game();
            Text text = new Text();
            IO io = new IO();

            //game.state
            //0:    quit
            //1:    main menu
            //2:    case menu
            //3:    crime scene investigation mode
            //4:    dialogue

            //game.menuState
            //0: show the menu introduction / case synopsis / so on
            //higher: special depending on what menu you're in

            //the game loop
            while (game.state != 0)
            {
                switch (game.state)
                {
                    case 1:                                
                        //main menu
                        Menu.PrintTitle();
                        while (game.state == 1)
                        {
                            Menu.PrintMenuCommands(text.menu.main.ToList());
                            game = Menu.EvaluateMainMenuCommand(io.Get());
                        }
                        break;

                    case 2:
                        //case selection menu
                        //Menu.PrintCaseSynopsis();
                        while (game.state == 2)
                        {
                            switch(game.menuState)
                            {
                                case 0:
                                    game.CreateCaseIfNull();
                                    Menu.PrintCaseSynopsis(game.activeCase);
                                    //game.menuState++;
                                    break;
                                case 1:
                                    Menu.PrintCaseReview(game.activeCase);
                                    game.menuState = 0;
                                    break;
                            }

                            Menu.PrintMenuCommands(text.menu._case.ToList());
                            game = Menu.EvaluateCaseCommand(game, io.Get());

                            if (game.caseTaken == 0)
                            {
                                game.state = 0;
                            }
                        }
                        game.menuState = 0;
                        break;

                    case 3:
                        //CSI menu / main game
                        while (game.state == 3)
                        {
                            switch (game.menuState)
                            {
                                case 0:
                                    break;
                                case 1: // "look"
                                    Menu.PrintQuery(text.menu.response.lookUnderQuery);
                                    game = Menu.EvaluateLookQuery(game, io.Get());
                                    break;
                                case 2: // "open"
                                    Menu.PrintQuery(text.menu.response.openQuery);
                                    game = Menu.EvaluateOpenQuery(game, io.Get());
                                    break;
                                case 3: // "close"
                                    Menu.PrintQuery(text.menu.response.closeQuery);
                                    game = Menu.EvaluateCloseQuery(game, io.Get());
                                    break;
                                case 4: // "take"
                                    Menu.PrintQuery(text.menu.response.takeQuery);
                                    game = Menu.EvaluateTakeQuery(game, io.Get());
                                    break;
                                case 5: // "dust"
                                    Menu.PrintQuery(text.menu.response.dustQuery);
                                    game = Menu.EvaluateDustQuery(game, io.Get());
                                    break;
                                case 6: // "leave"
                                    Menu.PrintQuery(text.menu.response.leaveQuery);
                                    game = Menu.EvaluateLeaveQuery(game, io.Get());
                                    break;
                                case 7: // "record"
                                    break;
                                case 8: // "check"
                                    Menu.PrintQuery(text.menu.response.checkQuery);
                                    game = Menu.EvaluateCheckQuery(game, io.Get());
                                    break;
                                case 9: // "photograph"
                                    Menu.PrintQuery(text.menu.response.photographQuery);
                                    game = Menu.EvaluatePhotographQuery(game, io.Get());
                                    break;
                                case 10: // LookAt
                                    Menu.PrintQuery(text.menu.response.lookAtQuery);
                                    Menu.LookAt(game.activeCase.activeItem);
                                    game.menuState = 0;
                                    break;
                                case 11: // LookBehind
                                    Menu.PrintQuery(text.menu.response.lookBehindQuery);
                                    Menu.LookBehind(game.activeCase.activeItem);
                                    game.menuState = 0;
                                    break;
                                case 12: // LookInside
                                    Menu.PrintQuery(text.menu.response.lookInsideQuery);
                                    Menu.LookInsideOf(game.activeCase.activeItem);
                                    game.menuState = 0;
                                    break;
                                case 13: // LookOnTopOf
                                    Menu.PrintQuery(text.menu.response.lookOnQuery);
                                    Menu.LookOnTopOf(game.activeCase.activeItem);
                                    game.menuState = 0;
                                    break;
                                case 14: // LookUnderneath
                                    Menu.PrintQuery(text.menu.response.lookUnderQuery);
                                    Menu.LookUnder(game.activeCase.activeItem);
                                    game.menuState = 0;
                                    break;
                            }
                            Menu.PrintSceneDescription(game.activeCase.currentScene);
                            Menu.PrintMenuCommands(text.menu.csi.ToList());
                            game = Menu.EvaluateCSICommand(game, io.Get());
                        }
                        break;

                    case 4:                                         
                        //case 4 is the witness conversation mode
                        throw new NotImplementedException();
                        break;

                    default:
                        game.state = 0;
                        break;
                }//end switch
            }//end while
        }//end main
    }//end program
}//end namespace
