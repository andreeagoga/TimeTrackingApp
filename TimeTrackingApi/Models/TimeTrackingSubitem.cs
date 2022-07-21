namespace TimeTrackingApi.Models
{
    public class TimeTrackingSubitem
    {
        public long Id { get; set;}
        public string Descriere { get; set; }
        public int NumarOre { get; set; }
        public string Dificultate { get; set; }
        public TimeTrackingItem Parent { get; set; }
    }
}