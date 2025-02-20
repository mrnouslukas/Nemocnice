using Hospital.DTO;
using Hospital.Models;
using Hospital;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class DepartmentService
    {
        private ApplicationDbContext _dbContext;

        public DepartmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DepartmentDto> GetDepartment()
        {
            var allDepartments = _dbContext.Departments;
            var departmentDtos = new List<DepartmentDto>();
            foreach (var department in allDepartments)
            {
                departmentDtos.Add(ModelToDto(department));
            }
            return departmentDtos;
        }

        public async Task AddDepartmentAsync(DepartmentDto departmentDto)
        {
            await _dbContext.Departments.AddAsync(
                DtoToModel(departmentDto)
            );
            await _dbContext.SaveChangesAsync();
        }

        private static Department DtoToModel(DepartmentDto departmentDto)
        {
            return new Department
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
            };
        }

        private async Task<Department> VerifyExistence(int id)
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(department => department.Id == id);
            if (department == null)
            {
                return null;
            }
            return department;
        }

        internal async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var department = await VerifyExistence(id);
            return ModelToDto(department);
        }

        private static DepartmentDto ModelToDto(Department department)
        {
            return new DepartmentDto
            {
                Name = department.Name,
                Id = department.Id,
            };
        }

        internal async Task UpdateAsync(int id, DepartmentDto departmentDto)
        {
            var department = await VerifyExistence(id);
            if (department != null)
            {
                department.Name = departmentDto.Name;
                _dbContext.Departments.Update(department);
                await _dbContext.SaveChangesAsync();
            }
        }

        internal async Task DeleteAsync(int id)
        {
            var departmentToDelete = await _dbContext.Departments.FirstOrDefaultAsync(Department => Department.Id == id);
            if (departmentToDelete != null)
            {
                _dbContext.Departments.Remove(departmentToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
