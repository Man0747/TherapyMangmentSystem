﻿
function morninggenerateSlots() {

    const slotDuration = parseInt($("#selects-slot").val());


    $("#morningslotContainer").empty();

    
    const startTime = 6 * 60; 
    const endTime = 12 * 60 ;   


    for (let time = startTime; time < endTime; time += slotDuration) {
        const startHours = Math.floor(time / 60);
        const startMinutes = time % 60;
        const endHours = Math.floor((time + slotDuration) / 60);
        const endMinutes = (time + slotDuration) % 60;


        const startTimeSlot = formatTime(startHours, startMinutes);
        const endTimeSlot = formatTime(endHours, endMinutes);


        const slotButton = $("<div>").addClass("slot-button");

        slotButton.text(`${startTimeSlot} - ${endTimeSlot}`);


        slotButton.click(function () {
            slotButton.toggleClass("selected");
        });

        $("#morningslotContainer").append(slotButton);
    }
}

function afternoongenerateSlots() {

    const slotDuration = parseInt($("#selects-slot").val());


    $("#afternoonslotContainer").empty();


    const startTime = 12 * 60; 
    const endTime = 18 * 60 ;   

    for (let time = startTime; time < endTime; time += slotDuration) {
        const startHours = Math.floor(time / 60);
        const startMinutes = time % 60;
        const endHours = Math.floor((time + slotDuration) / 60);
        const endMinutes = (time + slotDuration) % 60;


        const startTimeSlot = formatTime(startHours, startMinutes);
        const endTimeSlot = formatTime(endHours, endMinutes);

        const slotButton = $("<div>").addClass("slot-button");

        slotButton.text(`${startTimeSlot} - ${endTimeSlot}`);

   
        slotButton.click(function () {
            slotButton.toggleClass("selected");
        });

        $("#afternoonslotContainer").append(slotButton);
    }
}



function eveninggenerateSlots() {
    
    const slotDuration = parseInt($("#selects-slot").val());

    
    $("#eveningslotContainer").empty();

   
    const startTime = 18 * 60; 
    const endTime = 24 * 60;   

    
    for (let time = startTime; time < endTime; time += slotDuration) {
        const startHours = Math.floor(time / 60);
        const startMinutes = time % 60;
        const endHours = Math.floor((time + slotDuration) / 60);
        const endMinutes = (time + slotDuration) % 60;

        
        const startTimeSlot = formatTime(startHours, startMinutes);
        const endTimeSlot = formatTime(endHours, endMinutes);

        const slotButton = $("<div>").addClass("slot-button");


        slotButton.text(`${startTimeSlot} - ${endTimeSlot}`);


        slotButton.click(function () {
            slotButton.toggleClass("selected");
        });

        $("#eveningslotContainer").append(slotButton);
    }
}


function formatTime(hours, minutes) {
    let ampm = "AM";
    if (hours == 24) {
        ampm = "AM";
        if (hours > 12) {
            hours -= 12;
        }
    }
    else if (hours >= 12) {
        ampm = "PM";
        if (hours > 12) {
            hours -= 12;
        }

    } else if (hours === 0) {
        hours = 12;
    }
    return `${hours}:${minutes.toString().padStart(2, "0")} ${ampm}`;
}




function Holiday() {
    
    let isHoliday = false;
    const holidayButton = $(this);
    const selectsSlot = $("#selects-slot");

    if (holidayButton.text() == "Undeclare Holiday") {
        isHoliday = true; 
    }
    else {
        isHoliday = false;
    } 
    if (isHoliday) {

        morninggenerateSlots();
        afternoongenerateSlots();
        eveninggenerateSlots();
        selectsSlot.removeAttr("disabled");
        $("#holiday-message").remove();
        $("h2").show();
        holidayButton.text("Declare Holiday");
        isHoliday = false;
    } else {

        $("#morningslotContainer").empty();
        $("#afternoonslotContainer").empty();
        $("#eveningslotContainer").empty();
        selectsSlot.attr("disabled", "disabled");
        $("h2").hide();
        holidayButton.text("Undeclare Holiday");
        isHoliday = true;
        
        const holidayMessage = $("<h3>").attr("id", "holiday-message").text("Declared Holiday. Please select another Date .");
        $("#morningslotContainer").append(holidayMessage);
    }
};
function collectSelectedSlots(containerId) {
    const selectedSlots = [];
    $(`#${containerId} .slot-button.selected`).each(function () {
        const slotText = $(this).text();
        selectedSlots.push(slotText);
    });
    return selectedSlots;

}


function sumbitslots() {

   
    const morningSlots = collectSelectedSlots("morningslotContainer");
    
    const afternoonSlots = collectSelectedSlots("afternoonslotContainer");
    const eveningSlots = collectSelectedSlots("eveningslotContainer");


    const selectedSlots = [...morningSlots, ...afternoonSlots, ...eveningSlots];


    const selectedDate = $("#start-date").val();
    const slotDuration = parseInt($("#selects-slot").val());
    //const isHoliday = isHoliday; // Use the variable that tracks the holiday status


    const requestData = {
        date: selectedDate,
        slot: slotDuration,
       /* isHoliday: isHoliday,*/
        slots: selectedSlots
    };

    $.ajax({
        type: "POST",
        url: "/Therapist/Schedule",
        data: JSON.stringify(requestData),
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            console.log(response);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error:", textStatus, errorThrown);
        }
    });
};












