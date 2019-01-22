//homicide_detective.js

function gameLog(outputText){
	var log = document.getElementById("game_output").innerHTML;
	log = log + "\n" + outputText;
	document.getElementById("game_output").innerHTML = log;
}

function lookAt(item){
	gameLog("look at method, item is " + item);	
}

function lookUnder(item){
	gameLog("look underneath of method, item is " + item);	
}

function lookInsideOf(item){
	gameLog("look inside of method, item is " + item);
}

function lookOnTopOf(item){
	gameLog("look on top of method, item is " + item);	
}

function lookBehind(item){
	gameLog("look behind method, item is " + item);
}

function photographScene(){
	gameLog("photograph method");
}

function photographItem(item){
	gameLog("photographItem method, item is " + item);
}

function evaluateCommand(mystring){
	var command = mystring.split(" ");
	switch(command[0]){
		case "look": 
			switch(command[1]){
				case "at": 		lookAt(command[2]); break;
				case "under": 	lookUnder(command[2]); break;
				case "inside": 	lookInsideOf(command[2]); break;
				case "on": 		loonOnTopOf(command[4]); break;			
				case "behind": 	lookBehind(command[2]); break;
			}
			break;
		case "photograph": 
			switch(command[1]){
				case "scene": 	photographScene(); break;
				default: 		photographItem(command[1]);break;
			}
			break;
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