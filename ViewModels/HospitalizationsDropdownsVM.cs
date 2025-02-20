using Hospital.Models;

namespace Hospital.ViewModels
{
    public class HospitalizationsDropdownsVM
    {
        public List<Patient> Patients { get; set; }
        public List<Department> Departments { get; set; }

        public HospitalizationsDropdownsVM()
        {
            Patients = new List<Patient>();
            Departments = new List<Department>();
        }
    }
}
