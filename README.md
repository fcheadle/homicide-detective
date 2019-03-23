# _Homicide Detective_

_"When two objects meet, some evidence of that meeting can later be found and verified."_

## A game about a homicide detective in the 1980s

Homicide Detective is a text-based game where you are a detective who investigates homicides. These homicides are generated from the ranges given in the .json files. 

[Check out our official website](https://homicide-detective.com)

[This project has a board on Trello for development](https://trello.com/invite/b/qngR0CGL/35e762327185af78bdd2959332b87e0d/homicide-detective)

[And a subreddit!](https://www.reddit.com/r/HomicideDetective)

### Installation:

The game is not yet at a point where it can be installed. To run it, you must open it in visual studio and run the debugger (f5).

Good luck, stay sexy, and don't get murdered.

### How it works:

For the most part, the game's mechanics are defined in the .json files in the objects\* folders. This is to support translations and modding in the future.

scenes - a scene is a place where something happened, or a place where you can go to talk to witnesses or other persons of interest. Scenes contain items and connect to other scenes.

persons - A person requires certain tissues to be functional and a certain amount of bodily fluids to keep from dying. They must also be kept at a decent temperature, or they can die from exposure.

speech - defines the things that people can say and tells that might indicate that someone is lying. Every spoken statement should be accompanied by one or more facial expressions and speech affectations.

items - the types of items in the game, from furniture and murder weapons to signs of struggle and blood spatter