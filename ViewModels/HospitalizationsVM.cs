namespace Hospital.ViewModels
{
    public class HospitalizationsVM
    {
            public int Id { get; set; }
            public string PatientName { get; set; }
            public string DepartmentName { get; set; }
            public string Description { get; set; }
            public DateOnly DateOfHospitalization { get; set; }
        
    }
}
