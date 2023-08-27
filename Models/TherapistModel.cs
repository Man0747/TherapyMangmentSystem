using System.Net;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace TherapyMangmentSystem.Models
{
    public class TherapistModel
    {
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
