namespace TimeTrackingApi.Models
{
    public class TimeTrackingItem
    {
        public long Id { get; set;}
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public DateTime Data { get; set; }
        public int NumarOre { get; set; }
        public string? Secret { get; set; }
        public ICollection<TimeTrackingSubitem> TimeTrackingSubitems { get; set; }
    }
}