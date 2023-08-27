using System.ComponentModel.DataAnnotations;

namespace TherapyMangmentSystem.Models
{
    public class PatientModel
    {
        public int Patient_Id { get; set; }

        public string Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
    }
}