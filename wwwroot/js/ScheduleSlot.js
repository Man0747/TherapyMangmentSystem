
let DBslots = [];
$(function () {
    // All Evets are here
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
                    $('#editBtn').show();
                    $('#holiday-button').hide();
                    $("#selects-slot").attr("disabled", "disabled");
                    $("#submitButton").hide();
                    DBslots = response.slots;
                } else {
                    $('#editBtn').hide();
                    $('#holiday-button').show();
                    $("#selects-slot").removeAttr("disabled");
                    $("#submitButton").show();
                    alert("Message Not Sent, Please check details");
                }
                morninggenerateSlots();

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Error:", textStatus, errorThrown);
            }
        });
    });

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
            slotButton = $("<div>").addClass("slotavailable-button");
            slotButton.text(FinalSlotTime);
            $("#morningslotContainer").append(slotButton)
        } else {
            slotButton = $("<div>").addClass("slot-button");
            slotButton.text(FinalSlotTime);
            slotButton.click(function () {
                slotButton.toggleClass("selected");
            });
            $("#morningslotContainer").append(slotButton)
        }

        //slotButton.click(function () {
        //    slotButton.toggleClass("selected");
        //});

       // $("#morningslotContainer").append(slotButton);
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
