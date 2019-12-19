var form = document.querySelector("form");

form.addEventListener("submit", function (e) {
    var login = form.elements.login.value;
    var pwd = form.elements.pwd.value;
    var json = {"login":login, "password":pwd};
    ajaxJsonPost("http://localhost:5056/login",json,storeJwtToken);
    e.preventDefault();
    window.location.assign("history.html");
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
    req.send(jsonContent);
}

function storeJwtToken(jwtToken)
{
    document.cookie = jwtToken;
    console.log(jwtToken);
}