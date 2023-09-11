$(function () {
   
    $("#symptom").change(function () {

        const selectedSlotDate = $('#symptom').val();
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
                    $('#editBtn').removeAttr("hidden");
                    $('#holiday-button').hide();
                    $("#selects-slot").attr("disabled", "disabled");
                    $("#submitButton").hide();
                    $("#selects-slot").val(response.slotduration);
                    DBslots = response.slots;
                } else {
                    $('#editBtn').attr("hidden", "hidden");
                    $('#holiday-button').show();
                    $("#selects-slot").removeAttr("disabled");
                    $("#submitButton").show();
                    alert("Message Not Sent, Please check details");
                }
                morninggenerateSlots();
                afternoongenerateSlots();
                eveninggenerateSlots();
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




// JavaScript for search functionality
const searchButton = document.getElementById("search-button");
const symptomInput = document.getElementById("symptom");
const doctorResults = document.getElementById("doctor-results");

searchButton.addEventListener("click", searchDoctors);

function searchDoctors() {
    const symptom = symptomInput.value;
    // TODO: Fetch doctor data based on the symptom from a database or API
    // Replace the following dummy data with real data
    const dummyData = [
        {
            name: "Dr. John Doe",
            specialty: "Cardiology",
            experience: "10 years",
            contact: "john.doe@example.com",
        },
        {
            name: "Dr. Jane Smith",
            specialty: "Orthopedics",
            experience: "8 years",
            contact: "jane.smith@example.com",
        },
    ];

    displayDoctors(dummyData);
}

function displayDoctors(doctorData) {
    doctorResults.innerHTML = ""; // Clear previous results

    if (doctorData.length === 0) {
        doctorResults.innerHTML = "<p>No doctors found.</p>";
    } else {
        doctorData.forEach((doctor) => {
            const listItem = document.createElement("li");
            listItem.innerHTML = `
                <h3>${doctor.name}</h3>
                <p>Specialty: ${doctor.specialty}</p>
                <p>Experience: ${doctor.experience}</p>
                <p>Contact: ${doctor.contact}</p>
            `;
            doctorResults.appendChild(listItem);
        });
    }
}
