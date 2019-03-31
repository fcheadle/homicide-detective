# _Homicide Detective_

_"When two objects meet, some evidence of that meeting can later be found and verified."_

## A game about a homicide detective in 1980

Homicide Detective is a text-based game where you are a detective who investigates homicides. These homicides are generated from the ranges given in the .json files. 

[Check out our official website](https://homicide-detective.com)

[This project has a board on Trello for development](https://trello.com/invite/b/qngR0CGL/35e762327185af78bdd2959332b87e0d/homicide-detective)

[And a subreddit!](https://www.reddit.com/r/HomicideDetective)

### Installation:

The game is not yet at a point where it can be installed. To run it, you must open it in visual studio and run the debugger (f5).

Homicide Detective creates a series of "Cases" based off of your detective's name and case number. All Detective Smith Case 40 should be the same; this will only change when new files are added to the .json definitions, which means that we should try to get a decent maximum number of files early on and try not to add them willy-nilly; only in known breaking updates.

Still, when loading an existing game, the existing cases shouldn't change just because there are new files.

### How it works:

For the most part, the game's outputs are defined in the .json files in the objects\* folders. This is to support translations and modding in the future.

#### text 
These are person names, written text, and menu options. Menu files are there to support translations.

#### scenes 
A scene is a place where something happened, or a place where you can go to talk to witnesses or other persons of interest. Scenes contain items and connect to other scenes. A scene's file will contain all printable strings, as well as size descriptions, what items are contained within, and what other scenes are connected to this scene. These will (hopefully) not expand in functionality very much beyond the initial game skeleton.

#### persons 
A person requires certain tissues to be functional and a certain amount of bodily fluids to keep from dying. They must also be kept at a decent temperature, or they can die from exposure. A person file in the json will contain skin colors, eye colors, special words that get inserted into conversation more often than others, facial feature descriptions, and more. These will become more complex as more features are added.

#### dialogue
This contains the markov corpus that powers speech, as well as truth value assessment words, speech affectations, and so on. For each language that the game must be translated into, you need a new corpus in that language.

#### items
the types of items in the game, from furniture and murder weapons to signs of struggle and blood spatter. Should not change very much after initial game outline is implemented

A 'Case' contains:
* A victim
* A murderer
* A murder weapon
* A scene where the body is found (probably victim's house or work)
* A scene where the murder happened (probably same as where the body is found)

A 'Person' contains:
* A name
* Facial Feature Descriptions
* A generated fingerprint
* A collection of scenes that is the person's home
* A collection of Scenes that is the person's work
* A collection of connections to other people (family)
* A collection of connections to other people (friends)
* A collection of connections to other people (coworkers)
* A collection of connections to other people (adversaries)
* A collection of connections to other people (lovers)
* A group of percentile values that represent how likely some trait is to become a motive
  * Jealousy, Rage, heartbreak, etc

A 'Scene' contains:
* A list of items contained within that are unrelated to the case, but generate based on the json definitions 
  * An office will contain a desk, chair, clutter, things to go on the desk, things to go in it's drawers, etc
* A list of connections to other scenes 
  * (i.e. a bedroom has a hallway, bathroom, and closet connections)

An 'Item' contains:
* The physical ranges (minimum, maximum, mean, mode) that the item type is normally
* the printable name and description of the item
* general information about hardness, hollowness, and points that an item contains
* information about what things can fit inside
* zero or more fingerprints of people who touched the item

The game's fun is achieved by dropping the player in a victim's bedroom and making the player use the commands avialable to find evidence that can lead them to an arrest.

This is done with several different menus:

A Main Menu
* new game
* load game
* settings
* exit

A Case Selection Menu
* Review this case
* Save this case for later
* Take this case
* view Next case
* Go to specific case
* exit

A Crime Scene Investigation (CSI) Menu
* Look at something
* Photograph scene or item
* Take item as evidence
* dust something for prints
* leave through connection
* open door or container
* close door or container
* record conversation
* check photograph or evidence

A witness dialogue menu
* input is freetext
* input is examined for truthfulness of statement
* input is examined for topics of conversation
* witness dialogue is generated by a markov chain
  * Witness dialogue must have a way to 'force' a conversation topic
  * Witness dialogue will have a way to determine whether the witness would lie about a topic
  * A way to generate a lie
  * A way to generate a truthful statement
  * A way to generate a lie by omission

A settings menu
* Blind mode / screen reader
* language selector
* view scoreboard
* graphics settings
* sound settings