namespace Hospital.DTO
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int InsuranceNumbeer { get; set; }
        public string Nationality { get; set; }
    }
}
