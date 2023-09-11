$(function () {
    // All Evets are here


    $("#submitButton").click(function () {

        
        
        $.ajax({
            type: "GET",
            url: "http://localhost:5255/WeatherForecast",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                alert("ajax working");

            }
           
        });
    });

});