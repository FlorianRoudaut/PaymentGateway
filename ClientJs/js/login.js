var form = document.querySelector("form");

form.addEventListener("submit", function (e) {
    var login = form.elements.login.value;
    var pwd = form.elements.pwd.value;
    var json = {"login":login, "password":pwd};
    ajaxJsonPost("http://ec2-35-180-201-10.eu-west-3.compute.amazonaws.com:5052/login",json,storeJwtToken);
    e.preventDefault();
	setTimeout(showHistory, 1000);
});



function ajaxJsonPost(url, jsonContent, callback) {
    var req = new XMLHttpRequest();
    console.log("url : "+url);
    req.open("POST", url);
    req.setRequestHeader("Content-Type", "application/json");
    req.addEventListener("load", function () {
        if (req.status >= 200 && req.status < 400) {
            callback(req.responseText);
        } else {
            console.error(req.status + " " + req.statusText + " " + url);
        }
    });
    req.addEventListener("error", function () {
        console.error("Network error with URL " + url);
    });
    var content = JSON.stringify(jsonContent);
    console.log("content : "+content);
    req.send(content);
}

function storeJwtToken(jwtToken)
{
    document.cookie = jwtToken;
    console.log(jwtToken);
}

function showHistory()
{
    console.log("Show History");
	window.location.assign("history.html");
}