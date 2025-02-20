namespace Hospital.Models
{
    public class Hospitalization
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Department Department { get; set; }
        public string Description { get; set; }
        public DateOnly DayOfHospiptalization { get; set; }
    }
}
