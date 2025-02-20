namespace Hospital.DTO
{
    public class HospitalizationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DepartmentId { get; set; }
        public string Description { get; set; }
        public DateOnly DayOfHospiptalization { get; set; }
        public int DoctorId { get; set; }
    }
}
