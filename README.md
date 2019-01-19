# _Homicide Detective_

## A game about a homicide detective in the 1980s

_"When two objects meet, some evidence of that meeting can later be found and verified."_
						-Theory of transfer
						
Homicide Detective is a text-based game where you are a detective who investigates homicides. The murders in questing are defined, generated, and described in the json files contained within. 
	

### Controls:

Always visible:
? 		displays the controls and options
ESC 	exits the game

```
look at <item>
look under <item>
look inside of <item>
look on top of <item>
look behind <item>
```

investigates an item and tells you about it, or if you find another item around/inside it. Replace <item> with the name of the item in the scene

```
leave
leave through <door>
```
exits a scene and takes you to another scene

```
photograph scene
photograph <item>
```
Photographs the scene. Saves the scene description for later, so that your looking for clues


_Gazing too closely upon the .json files may spoil some of the fun of the game_

scenes.json - areas in the game, i.e. where the body was found, where the murder happened, victim's house, so on and so forth.
	
murders.json - types of ways the victims could be killed, and the evidence each cause might leave. Also includes misleading false evidence
	
hiding_the_body.json - defines ways in which a victim's corpse might be hidden,	destroyed, or otherwise "gotten rid of", and the evidence that doing so might leave behind

people.json - defines people and their many variations

conversations.json - defines the things that people can say and tells that people might have

items.json - the types of items in the game, from furniture to tire marks to murder weapons
