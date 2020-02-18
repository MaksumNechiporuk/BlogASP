// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on("scroll", function () {
    if ($(document).scrollTop() > 86) {
        $("#banner").addClass("shrink");
    } else {
        $("#banner").removeClass("shrink");
    }
});

$(".dropdown-toggle").on("click", function () {
    if ($(".dropdown-menu").hasClass("show")) {
        $(".dropdown-menu").removeClass("show");
    } else {
        $(".dropdown-menu").addClass("show");
    }
});

﻿   $(document).ready(function () {
                $('.custom-file-input').on("change", function () {
                    var fileName = $(this).val().split("\\").pop();
                    $(this).next('.custom-file-label').html(fileName);
                });
});

$(".search").on("click", function () {   
        //const URL = `https://newsapi.org/v2/top-headlines?country=${country}&category=${category}&apiKey=93395f34a1bd41ea945fea1ef380ff4c`;
        //await fetch(URL, {
        //    method: "GET"
        //})
        //    .then(response => {
        //        return response.json();
        //    })
        //    .then(data => {
        //        callback(data);
        //        console.log(URL);
        //    })
        //    .catch(err => {
        //        console.log("Catch => ", err);
        //    });
    
});
