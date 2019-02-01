//game.js

var murderer;
var victim;
var causeOfDeath;
var crimeScene;

function gameLog(outputText){
	var log = document.getElementById("game_output").innerHTML;
	log = log + "\n" + outputText;
	document.getElementById("game_output").innerHTML = log;
}

function saveGame(){
	localStorage.setItem("murderer", murderer);
	localStorage.setItem("victim", victim);
	localStorage.setItem("causeOfDeath", causeOfDeath);
	localStorage.setItem("crimeScene", crimeScene);
}

function loadGame(){
	murderer = localStorage.getItem("murderer");
	victim = localStorage.getItem("victim");
	causeOfDeath = localStorage.getItem("causeOfDeath");
	crimeScene = localStorage.getItem("crimeScene");
}

function lookAt(item){
	gameLog("look at " + item + ". ");	
}

function lookUnder(item){
	gameLog("looking underneath " + item + ". ");	
}

function lookInsideOf(item){
	gameLog("looking inside of " + item + ". ");
}

function lookOnTopOf(item){
	gameLog("looking on top of " + item + ". ");	
}

function lookBehind(item){
	gameLog("looking behind " + item + ". ");
}

function photographScene(){
	gameLog("photographing scene. ");
}

function photographItem(item){
	gameLog("photographing " + item + ". ");
}

function takeNote(){
	gameLog("taking a note. ");
}

function takeEvidence(item){
	gameLog("taking " + item + " as evidence. ");
}

function dustForPrints(item){
	gameLog("dusting " + item + " for prints. ");
}

function leaveScene(){
	gameLog("Goodbye, detective. ");
}

function leaveThroughDoor(door){
	gameLog("leaving through " + door + ". ");
}

function evaluateCommand(input_string){
	var command = input_string.split(" ");
	switch(command[0]){
		case "look": 
			switch(command[1]){
				case "at": 			lookAt(command[2]); break;
				case "under": 		lookUnder(command[2]); break;
				case "inside": 		lookInsideOf(command[2]); break;
				case "on": 			loonOnTopOf(command[4]); break;			
				case "behind": 		lookBehind(command[2]); break;
			} break;
		case "photograph": 
			switch(command[1]){
				case "scene": 		photographScene(); break;
				default: 			photographItem(command[1]);break;
			}
			break;
		case "take":
			switch(command[1]){
				case "note": 		takeNote(); break;
				default: 			takeEvidence(command[1]); break;
			} break;
		case "dust": 				dustForPrints(command[1]); break;
		case "leave":
			switch(command[1]){
				case "through": 	leaveThroughDoor(command[2]); break;
				default: 			leaveScene(); break;
			} break;
		case "open": 				openDoor(command[1]); break;
		case "close": 				closeDoor(comand[1]); break;
		case "check":
			switch(comand[1]){
				case "notes": 		checkNotes(); break;
				case "photographs":	checkPhotographs(); break;
				case "evidence":	checkEvidence(); break;
			} break;
		case "record": 				recordConversation(); break;
		default: gameLog("?"); break;
	}
}

document.getElementById("submit_button").addEventListener("click", function(){
	evaluateCommand(document.getElementById("game_input").value);
	document.getElementById("game_input").value = "";
});

document.getElementById("game_input").addEventListener("keypress", function (e) {
	var key = e.which || e.keyCode;
	if (key === 13) { // 13 is enter
		evaluateCommand(document.getElementById("game_input").value);
		document.getElementById("game_input").value = "";
	}
});

function generateHomicide(){
	murderer = "generic murderer";
	victim = "generic victim";
	causeOfDeath = "generic cause of death";
	crimeScene = "scene of the crime";
	saveGame();
}

if(localStorage.getItem("game") != "inProgress"){
	generateHomicide();
	localStorage.setItem("game", "inProgress");
	saveGame();
}