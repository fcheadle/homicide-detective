//game.js

//requirements
var fs = require('fs'); //file reader
var http = require('http'); //for hosting webserver locally

//host the webserver in you browser
http.createServer(function (req, res) {
	res.writeHead(200, {'Content-Type': 'text/html'});
	//res.write(req.url);
	var contents = fs.readFileSync('./index.html', 'utf8');
	res.end(contents);
}).listen(8080);

