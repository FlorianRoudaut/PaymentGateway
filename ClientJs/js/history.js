var jwtToken = JSON.parse(document.cookie);


var _table_ = document.createElement('table'),
  _tr_ = document.createElement('tr'),
  _th_ = document.createElement('th'),
  _td_ = document.createElement('td');

ajaxGet("http://ec2-35-180-201-10.eu-west-3.compute.amazonaws.com:5051/api/history",buildHtmlTable)

function ajaxGet(url, callback) {
    var req = new XMLHttpRequest();
    req.open("GET", url);
    var authHeader = "Bearer "+jwtToken.token;
    req.setRequestHeader("Authorization", authHeader);
    req.addEventListener("load", function () {
        if (req.status >= 200 && req.status < 400) {
            callback(JSON.parse(req.responseText));
        } else {
            console.error(req.status + " " + req.statusText + " " + url);
        }
    });
    req.addEventListener("error", function () {
        console.error("Network error with URL " + url);
    });
    req.send(content);
}

function buildHtmlTable(arr) {
    var table = _table_.cloneNode(false),
      columns = addAllColumnHeaders(arr, table);
    for (var i = 0, maxi = arr.length; i < maxi; ++i) {
      var tr = _tr_.cloneNode(false);
      for (var j = 0, maxj = columns.length; j < maxj; ++j) {
        var td = _td_.cloneNode(false);
        cellValue = arr[i][columns[j]];
        td.appendChild(document.createTextNode(arr[i][columns[j]] || ''));
        tr.appendChild(td);
      }
      table.appendChild(tr);
    }
    document.getElementById("content").appendChild(table);
  }
  
  // Adds a header row to the table and returns the set of columns.
  // Need to do union of keys from all records as some records may not contain
  // all records
  function addAllColumnHeaders(arr, table) {
    var columnSet = [],
      tr = _tr_.cloneNode(false);
    for (var i = 0, l = arr.length; i < l; i++) {
      for (var key in arr[i]) {
        if (arr[i].hasOwnProperty(key) && columnSet.indexOf(key) === -1) {
          columnSet.push(key);
          var th = _th_.cloneNode(false);
          th.appendChild(document.createTextNode(key));
          tr.appendChild(th);
        }
      }
    }
    table.appendChild(tr);
    return columnSet;
  }

  document.getElementById('process').onclick = function(e){
    window.location.assign("processpayment.html");
  }