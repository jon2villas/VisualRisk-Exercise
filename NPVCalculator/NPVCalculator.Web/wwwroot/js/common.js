function ready(fn) {
  if (document.attachEvent ? document.readyState === 'complete' : document.readyState !== 'loading') {
    fn();
  } else {
    document.addEventListener('DOMContentLoaded', fn);
  }
}

function getJSON(url, fnSuccess, fnError) {
  var request = new XMLHttpRequest();

  request.open('GET', url, true);

  request.onload = function() {
    if (request.status >= 200 && request.status < 400) {
      fnSuccess(JSON.parse(request.responseText), request.status);
    } else {
      fnSuccess(null, request.status);
    }
  };

  request.onerror = function() {
    if (fnError) {
      fnError();
    }
  };

  request.send();
}

function postJSON(url, body, fnSuccess, fnError) {
  var request = new XMLHttpRequest();

  request.open('POST', url, true);
  request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');

  request.onload = function() {
    if (fnSuccess) {
      fnSuccess(request.responseText, request.status);
    }
  };

  request.onerror = function() {
    if (fnError) {
      fnError();
    }
  };

  request.send(JSON.stringify(body));
}

function formatNumber(num) {
    return num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
}

function hasClass(el, className) {
  return el.classList ? el.classList.contains(className) : !!el.className.match(new RegExp('(\\s|^)' + className + '(\\s|$)'));
}

function addClass(el, className) {
  if (el.classList) {
      el.classList.add(className);
  }
  else if (!hasClass(el, className)) {
      el.className += " " + className;
  }
}

function removeClass(el, className) {
  if (el.classList) {
      el.classList.remove(className);
  }
  else if (hasClass(el, className)) {
      var reg = new RegExp('(\\s|^)' + className + '(\\s|$)');
      el.className = el.className.replace(reg, ' ');
  }
}