// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on("scroll", function () {
    if ($(document).scrollTop() > 50) {
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

//$(".search").on("click", function () {   
//    const url = 'http://localhost:5000/Blog/Blog';
//    const data = { Name: $(".searchText").value };

//    try {
//        const response = await fetch(url, {
//            method: 'GET', 
//            body: JSON.stringify(data), 
//            headers: {
//                'Content-Type': 'application/json'
//            }
//        });
//        const json = await response.json();
//        console.log('Успех:', JSON.stringify(json));
//    } catch (error) {
//        console.error('Ошибка:', error);
//    }



    
