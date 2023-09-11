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
