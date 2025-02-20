using System.Diagnostics;
using Hospital.ViewModels;
using Hospital.DTO;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class HospitalizationService
    {
        ApplicationDbContext context;
        public HospitalizationService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        internal async Task<HospitalizationsDropdownsVM> GetDropdownsData()
        {
            var resultsDropdownsData = new HospitalizationsDropdownsVM()
            {
                Patients = await context.Patients.OrderBy(patient => patient.Name).ToListAsync(),
                Departments = await context.Departments.OrderBy(department => department.Name).ToListAsync(),
            };
            return resultsDropdownsData;
        }

        public async Task CreateAsync(HospitalizationDto newHospitalization)
        {
            Hospitalization resultToInsert = await DtoToModelAsync(newHospitalization);
            await context.Hospitalizations.AddAsync(resultToInsert);
            await context.SaveChangesAsync();
        }

        private async Task<Hospitalization> DtoToModelAsync(HospitalizationDto newHospitalizations)
        {
            return new Hospitalization
            {
                Id = newHospitalizations.Id,
                DayOfHospiptalization = newHospitalizations.DayOfHospiptalization,
                Description = newHospitalizations.Description,
                Patient = await context.Patients.FirstOrDefaultAsync(rs => rs.Id == newHospitalizations.PatientId),
                Department = await context.Departments.FirstOrDefaultAsync(dis => dis.Id == newHospitalizations.DepartmentId),
            };
        }

        public async Task<IEnumerable<HospitalizationsVM>> GetAllHospitalizationsAsync()
        {
            var hospitalizations = await context.Hospitalizations.Include(rs => rs.Patient).Include(di => di.Department).ToListAsync();
            List<HospitalizationsVM> hospitalizationsVMs = new List<HospitalizationsVM>();
            foreach (var hospitalization in hospitalizations)
            {
                hospitalizationsVMs.Add(new HospitalizationsVM
                {
                    Id = hospitalization.Id,
                    DateOfHospitalization = hospitalization.DayOfHospiptalization,
                    Description = hospitalization.Description,
                    PatientName = hospitalization.Patient.Name,
                    DepartmentName = hospitalization.Department.Name,
                });
            }
            return hospitalizationsVMs;
        }

        internal async Task<HospitalizationDto> GetByIdAsync(int id)
        {
            var hospitalizationToEdit = await context.Hospitalizations.Include(at => at.Patient).Include(di => di.Department).FirstOrDefaultAsync(rs => rs.Id == id);
            if (hospitalizationToEdit == null)
            {
                return null;
            }
            return ModelToDto(hospitalizationToEdit);
        }

        private HospitalizationDto ModelToDto(Hospitalization hospitalizationToEdit)
        {
            return new HospitalizationDto
            {
                Id = hospitalizationToEdit.Id,
                DayOfHospiptalization = hospitalizationToEdit.DayOfHospiptalization,
                Description = hospitalizationToEdit.Description,
                PatientId = hospitalizationToEdit.Patient.Id,
                DepartmentId = hospitalizationToEdit.Department.Id,
            };
        }

        internal async Task UpdateAsync(HospitalizationDto editedHospitalization)
        {
            context.Hospitalizations.Update(await DtoToModelAsync(editedHospitalization));
            await context.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id)
        {
            var hospitalizationToDelete = await context.Hospitalizations.FirstOrDefaultAsync(rs => rs.Id == id);
            context.Hospitalizations.Remove(hospitalizationToDelete);
            await context.SaveChangesAsync();
        }
    }
}
