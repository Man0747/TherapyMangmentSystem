namespace TherapyMangmentSystem.Models
{
    public class TherapistLoginModel
    {
        public string Login_Id { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int User_Id { get; set; }


        public int Therapist_Id { get; set; }
        public string Name { get; set; }
        public string BusinessAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ClinicName { get; set; }
        public string IsActive { get; set; }
        public string GeneralPracticeArea { get; set; }
        public string SpecialityArea { get; set; }
        public string Certifications { get; set; }
    }
}
