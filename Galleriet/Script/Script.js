"use strict"

var removeMSG = function () {
    var element = document.getElementById("sucessMSG");
    element.parentNode.removeChild(element);
}

window.onload = function () {
    var imageArr = document.getElementsByTagName('a');
    for (var i = 0; i < imageArr.length; i++) {
        if (imageArr[i] == window.location.href) {
            imageArr[i].style.backgroundColor = "limegreen";
        }
    }
}



