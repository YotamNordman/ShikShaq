﻿function myFunction() {
    var x = document.getElementById("myTopnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}

//window.onscroll = function () { myFunction2() };

var navbar = document.getElementById("myTopnav");

var sticky = navbar ? navbar.offsetTop : 0;

function myFunction2() {
    if (navbar) {
        if (window.pageYOffset >= sticky) {
            navbar.classList.add("sticky");
        } else {
            navbar.classList.remove("sticky")
        }
    }
}