var form = document.querySelector("form");

document.getElementById('history').onclick = function(e){
    console.log("hisotrybutton");
    e.preventDefault();
    window.location.assign("history.html");
  }

var jwtToken = JSON.parse(document.cookie);

form.addEventListener("submit", function (e) {
    var card = form.elements.card.value;
    var cvv = form.elements.cvv.value;
    var expirymonth = form.elements.expirymonth.value;
    var expiryyear = form.elements.expiryyear.value;
    var cardholder = form.elements.cardholder.value;
    var amount = form.elements.amount.value;
    var currency = form.elements.currency.value;
    var json = {"CardNumber":card, "Cvv":cvv, "ExpiryMonth":expirymonth, "ExpiryYear":expiryyear,
    "CardHolderName":cardholder, "Amount":amount, "Currency":currency };
    ajaxJsonPost("http://localhost:5051/api/process",json,console.log);
    e.preventDefault();
});

function ajaxJsonPost(url, jsonContent, callback) {
    var req = new XMLHttpRequest();
    console.log("url : "+url);
    req.open("POST", url);
    req.setRequestHeader("Content-Type", "application/json");
    var authHeader = "Bearer "+jwtToken.token;
    req.setRequestHeader("Authorization", authHeader);
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