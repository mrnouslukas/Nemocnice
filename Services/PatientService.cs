
using Hospital.DTO;
using Hospital.Models;
using Hospital;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class PatientService
    {
        private ApplicationDbContext dbContext;

        public PatientService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal IEnumerable<PatientDto> GetAllPatients()
        {
            var allPatients = dbContext.Patients;

            var patientsDtos = new List<PatientDto>();
            foreach (var patient in allPatients)
            {
                patientsDtos.Add(ModelToDto(patient));
            }
            return patientsDtos;
        }

        private static PatientDto ModelToDto(Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                DateOfBirth = patient.DateOfBirth,
                Name = patient.Name,
                Nationality = patient.Nationality,
                InsuranceNumbeer=patient.InsuranceNumbeer,
            };
        }

        public async Task CreateStudentAsync(PatientDto patientDto)
        {
            await dbContext.Patients.AddAsync(DtoToModel(patientDto));
            await dbContext.SaveChangesAsync();
        }

        private Patient DtoToModel(PatientDto patientDto)
        {
            return new Patient
            {
                Id = patientDto.Id,
                DateOfBirth = patientDto.DateOfBirth,
                Name = patientDto.Name,
                Nationality = patientDto.Nationality,
                InsuranceNumbeer = patientDto.InsuranceNumbeer,
            };
        }

        internal async Task<PatientDto> GetByIdAsync(int id)
        {
            var patientToEdit = await dbContext.Patients.FirstOrDefaultAsync(Patient => Patient.Id == id);
            if (patientToEdit == null)
            {
                return null;
            }
            return ModelToDto(patientToEdit);
        }

        internal async Task UpdateAsync(int id, PatientDto updatedPatient)
        {
            dbContext.Patients.Update(DtoToModel(updatedPatient));
            await dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id)
        {
            var patientToDelete = dbContext.Patients.FirstOrDefault(Patient => Patient.Id == id);
            dbContext.Patients.Remove(patientToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}
