namespace TherapyMangmentSystem.Models
{
    public class ScheduleResponse
    {
        public bool scheduleExists { get; set; }
        public List<String> Slots { get; set; }
        public int slotduration { get; set; }
    }

}
