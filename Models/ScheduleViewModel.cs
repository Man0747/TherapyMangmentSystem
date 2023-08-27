using System.ComponentModel.DataAnnotations;

namespace TherapyMangmentSystem.Models
{
    public class ScheduleViewModel
    {
        public int Schedule_Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Slot { get; set; }
        public string Isholiday { get; set; }
        public int Therapist_Id { get; set; }



        public int ScheduledSlots_Id { get; set; }

        [DataType(DataType.Time)]
        public DateTime FromTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime ToTime { get; set; }
        public string Day { get; set; }
        public string Shift { get; set; }
       
    }
}
