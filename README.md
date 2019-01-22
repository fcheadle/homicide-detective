# _Homicide Detective_

## A game about a homicide detective in the 1980s

_"When two objects meet, some evidence of that meeting can later be found and verified."_

Homicide Detective is a text-based game where you are a detective who investigates homicides. These homicides are generated from the ranges given in the json files. 

### Installation:

Unfortunately, the game must be hosted in iis for the time being. As my node.js skills improve, this method will hopefully simplify.

Go ahead and start iis and change the directory of your default website to the working directory.

Then, navigate to localhost:80 and interact with the prompt given there.

Good luck, stay sexy, and don't get murdered.

#### How it works:

_Gazing too closely upon the .json files may spoil some of the fun of the game_

scenes.json - areas in the game, i.e. where the body was found, where the murder happened, so on and so forth.	

murders.json - ways the victims could be killed, and the evidence each.

hiding_the_body.json - defines ways in which a victim's corpse might be hidden,	destroyed, or otherwise "gotten rid of", and the evidence that doing so might leave behind

people.json - defines people and their many variations

conversations.json - defines the things that people can say and tells that people might have

items.json - the types of items in the game, from furniture and murder weapons to signs of struggle and blood spatter