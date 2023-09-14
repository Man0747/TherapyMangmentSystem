
let DBslots = [];
let DBBackupSlots = [];
$(function () {
    // All Evets are here
    var today = new Date();
    var yyyy = today.getFullYear();
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var dd = String(today.getDate()).padStart(2, '0');
    var formattedDate = yyyy + '-' + mm + '-' + dd;
    document.getElementById('start-date').value = formattedDate;



    //$("#submitButton").click(sumbitslots);

    $("#start-date").change(function () {

        const selectedSlotDate = $('#start-date').val();
        const requestData = {
            Date: selectedSlotDate
        };
        var status = selectedSlotDate
        $.ajax({
            type: "GET",
            url: "/Therapist/IsScheduleExist",
            data: { 'status': status },
            contentType: "application/json; charset=utf-8",
            success: function (response) {

                if (response.scheduleExists) {
                    //$('#editBtn').removeAttr("hidden");
                    //$('#holiday-button').hide();
                    //$("#selects-slot").attr("disabled", "disabled");
                    //$("#submitButton").hide();
                    //$("#selects-slot").val(response.slotduration);
                    DBslots = response.slots;
                } else {
                    //    $('#editBtn').attr("hidden", "hidden");
                    //    $('#holiday-button').show();
                    //    $("#selects-slot").removeAttr("disabled");
                    //    $("#submitButton").show();
                    alert("No Slots Available Please select another date")
                }
                morninggenerateSlots();
                //afternoongenerateSlots();
                //eveninggenerateSlots();
                DBBackupSlots = DBslots;
                DBslots = [];
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Error:", textStatus, errorThrown);
            }
        });
    });


    $("#start-date").trigger('change');
});

// All functions are here
function morninggenerateSlots() {


    const slotDuration = parseInt($("#selects-slot").val());


    $("#morningslotContainer").empty();


    const startTime = 6 * 60;
    const endTime = 12 * 60;



    for (let time = startTime; time < endTime; time += slotDuration) {
        const startHours = Math.floor(time / 60);
        const startMinutes = time % 60;
        const endHours = Math.floor((time + slotDuration) / 60);
        const endMinutes = (time + slotDuration) % 60;


        const startTimeSlot = formatTime(startHours, startMinutes);
        const endTimeSlot = formatTime(endHours, endMinutes);


        //const slotButton = $("<div>").addClass("slot-button");

        //slotButton.text(`${startTimeSlot} - ${endTimeSlot}`);



        let slotButton;
        var FinalSlotTime = startTimeSlot.concat(" - ", endTimeSlot);

        var IsSlotExists = DBslots.includes(FinalSlotTime)

        if (DBslots !== null && IsSlotExists == true) {
            //slotButton = $("<div>").addClass("slot-button selected");
            slotButton.click(function () {
                slotButton.toggleClass("selected");
            });

            slotButton.text(FinalSlotTime);

            $("#morningslotContainer").append(slotButton)
        } else {
            //slotButton = $("<div>").addClass("slot-button");
            //slotButton.text(FinalSlotTime);

            //slotButton.click(function () {
            //    slotButton.toggleClass("selected");
            //});
            //$("#morningslotContainer").append(slotButton)
        }


        //if (DBslots !== null && IsSlotExists == true) {
        //    slotButton = $("<div>").addClass("slot-button selected");
        //    //slotButton.click(function () {
        //    //    slotButton.toggleClass("selected");
        //    //});

        //    slotButton.text(FinalSlotTime);

        //    $("#morningslotContainer").append(slotButton)
        //} else {
        //    slotButton = $("<div>").addClass("slot-button");
        //    slotButton.text(FinalSlotTime);

        //    //slotButton.click(function () {
        //    //    slotButton.toggleClass("selected");
        //    //});
        //    $("#morningslotContainer").append(slotButton)
        //}

        //slotButton.click(function () {
        //    slotButton.toggleClass("selected");
        //});

        // $("#morningslotContainer").append(slotButton);
    }


}
function afternoongenerateSlots() {

    const slotDuration = parseInt($("#selects-slot").val());


    $("#afternoonslotContainer").empty();


    const startTime = 12 * 60;
    const endTime = 18 * 60;

    for (let time = startTime; time < endTime; time += slotDuration) {
        const startHours = Math.floor(time / 60);
        const startMinutes = time % 60;
        const endHours = Math.floor((time + slotDuration) / 60);
        const endMinutes = (time + slotDuration) % 60;


        const startTimeSlot = formatTime(startHours, startMinutes);
        const endTimeSlot = formatTime(endHours, endMinutes);




        let slotButton;
        var FinalSlotTime = startTimeSlot.concat(" - ", endTimeSlot);

        var IsSlotExists = DBslots.includes(FinalSlotTime)

        if ($('#editBtn').is(':hidden')) {
            if (DBslots !== null && IsSlotExists == true) {
                slotButton = $("<div>").addClass("slot-button selected");
                slotButton.click(function () {
                    slotButton.toggleClass("selected");
                });

                slotButton.text(FinalSlotTime);

                $("#afternoonslotContainer").append(slotButton)
            } else {
                slotButton = $("<div>").addClass("slot-button");
                slotButton.text(FinalSlotTime);

                slotButton.click(function () {
                    slotButton.toggleClass("selected");
                });
                $("#afternoonslotContainer").append(slotButton)
            }
        }
        else {
            if (DBslots !== null && IsSlotExists == true) {
                slotButton = $("<div>").addClass("slot-button selected");
                //slotButton.click(function () {
                //    slotButton.toggleClass("selected");
                //});

                slotButton.text(FinalSlotTime);

                $("#afternoonslotContainer").append(slotButton)
            } else {
                slotButton = $("<div>").addClass("slot-button");
                slotButton.text(FinalSlotTime);

                //slotButton.click(function () {
                //    slotButton.toggleClass("selected");
                //});
                $("#afternoonslotContainer").append(slotButton)
            }
        }
    }
}
function eveninggenerateSlots() {

    const slotDuration = parseInt($("#selects-slot").val());


    $("#eveningslotContainer").empty();


    const startTime = 18 * 60;
    const endTime = 22 * 60;


    for (let time = startTime; time < endTime; time += slotDuration) {
        const startHours = Math.floor(time / 60);
        const startMinutes = time % 60;
        const endHours = Math.floor((time + slotDuration) / 60);
        const endMinutes = (time + slotDuration) % 60;


        const startTimeSlot = formatTime(startHours, startMinutes);
        const endTimeSlot = formatTime(endHours, endMinutes);

        let slotButton;
        var FinalSlotTime = startTimeSlot.concat(" - ", endTimeSlot);

        var IsSlotExists = DBslots.includes(FinalSlotTime)
        if ($('#editBtn').is(':hidden')) {
            if (DBslots !== null && IsSlotExists == true) {
                slotButton = $("<div>").addClass("slot-button selected");
                slotButton.click(function () {
                    slotButton.toggleClass("selected");
                });

                slotButton.text(FinalSlotTime);

                $("#eveningslotContainer").append(slotButton)
            } else {
                slotButton = $("<div>").addClass("slot-button");
                slotButton.text(FinalSlotTime);

                slotButton.click(function () {
                    slotButton.toggleClass("selected");
                });
                $("#eveningslotContainer").append(slotButton)
            }
        }
        else {
            if (DBslots !== null && IsSlotExists == true) {
                slotButton = $("<div>").addClass("slot-button selected");
                //slotButton.click(function () {
                //    slotButton.toggleClass("selected");
                //});

                slotButton.text(FinalSlotTime);

                $("#eveningslotContainer").append(slotButton)
            } else {
                slotButton = $("<div>").addClass("slot-button");
                slotButton.text(FinalSlotTime);

                //slotButton.click(function () {
                //    slotButton.toggleClass("selected");
                //});
                $("#eveningslotContainer").append(slotButton)
            }
        }
    }
}

function formatTime(hours, minutes) {
    let ampm = "AM";
    let hourstring;
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
    if (hours < 10) {
        hourstring = "0".concat(hours.toString());
        return `${hourstring}:${minutes.toString().padStart(2, "0")} ${ampm}`;
    }
    else {
        return `${hours}:${minutes.toString().padStart(2, "0")} ${ampm}`;
    }

}



function generateSlots() {
    morninggenerateSlots();
    afternoongenerateSlots();
    eveninggenerateSlots();
}
//function Holiday() {

//    let isHoliday = false;
//    const holidayButton = $(this);
//    const selectsSlot = $("#selects-slot");

//    if (holidayButton.text() == "Undeclare Holiday") {
//        isHoliday = true;
//    }
//    else {
//        isHoliday = false;
//    }
//    if (isHoliday) {

//        morninggenerateSlots();
//        afternoongenerateSlots();
//        eveninggenerateSlots();
//        selectsSlot.removeAttr("disabled");
//        $("#holiday-message").remove();
//        $("h2").show();
//        $("#submitButton").show();
//        holidayButton.text("Declare Holiday");
//        isHoliday = false;
//    } else {

//        $("#morningslotContainer").empty();
//        $("#afternoonslotContainer").empty();
//        $("#eveningslotContainer").empty();
//        selectsSlot.attr("disabled", "disabled");
//        $("h2").hide();
//        $("#submitButton").hide();
//        holidayButton.text("Undeclare Holiday");
//        isHoliday = true;

//        const holidayMessage = $("<h3>").attr("id", "holiday-message").text("Declared Holiday. Please select another Date .");
//        $("#morningslotContainer").append(holidayMessage);
//    }
//};



//function EditMode() {
//    //$("#start-date").trigger('change');
//    DBslots = DBBackupSlots;
//    $('#editBtn').attr("hidden", "hidden");
//    $('#holiday-button').show();
//    $("#selects-slot").removeAttr("disabled");
//    $("#submitButton").show();
//    generateSlots();
//    DBslots = [];
//}




//function collectSelectedSlots(containerId) {
//    const selectedSlots = [];
//    $(`#${containerId} .slot-button.selected`).each(function () {
//        const slotText = $(this).text();
//        selectedSlots.push(slotText);
//    });
//    return selectedSlots;

//}

//function sumbitslots() {


//    const morningSlots = collectSelectedSlots("morningslotContainer");

//    const afternoonSlots = collectSelectedSlots("afternoonslotContainer");
//    const eveningSlots = collectSelectedSlots("eveningslotContainer");


//    const selectedSlots = [...morningSlots, ...afternoonSlots, ...eveningSlots];


//    const selectedDate = $("#start-date").val();
//    const slotDuration = parseInt($("#selects-slot").val());
//    //const isHoliday = isHoliday; // Use the variable that tracks the holiday status

//    let holiday = "true";
//    if ($("#holiday-button").text() == "Declare Holiday") {
//        holiday = "false";
//    }
//    else {
//        holiday = "true";
//    }
//    const requestData = {
//        date: selectedDate,
//        slot: slotDuration,
//        Isholiday: holiday,
//        slots: selectedSlots
//    };

//    $.ajax({
//        type: "POST",
//        url: "/Therapist/Schedule",
//        data: JSON.stringify(requestData),
//        contentType: "application/json; charset=utf-8",
//        success: function (response) {

//            console.log(response);
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            console.error("Error:", textStatus, errorThrown);
//        }
//    });
//};