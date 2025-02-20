using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hospital.DTO;
using Hospital.Services;

namespace Hospital.Controllers
{
    // [Authorize]
    public class DepartmentsController : Controller
    {
        private DepartmentService departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        public IActionResult Index()
        {
            IEnumerable<DepartmentDto> allDepartments = departmentService.GetDepartment();
            return View(allDepartments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DepartmentDto departmentDto)
        {
            await departmentService.AddDepartmentAsync(departmentDto);
            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var departmentToEdit = await departmentService.GetByIdAsync(id);
            if (departmentToEdit == null)
            {
                return View("NotFound");
            }
            return View(departmentToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentDto departmentDto, int id)
        {
            await departmentService.UpdateAsync(id, departmentDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var departmentToDelete = await departmentService.GetByIdAsync(id);
            if (departmentToDelete == null)
            {
                return View("NotFound");
            }
            await departmentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
