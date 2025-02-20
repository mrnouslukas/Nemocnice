using Hospital.DTO;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private PatientService patientService;

        public PatientsController(PatientService service)
        {
            patientService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allPatients = patientService.GetAllPatients();
            return View(allPatients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PatientDto patientDto)
        {
            await patientService.CreateStudentAsync(patientDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var patientToEdit = await patientService.GetByIdAsync(id);
            if (patientToEdit == null)
            {
                return View("NotFound");
            }
            return View(patientToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, PatientDto patient)
        {
            await patientService.UpdateAsync(id, patient);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var patientToDelete = await patientService.GetByIdAsync(id);
            if (patientToDelete == null)
            {
                return View("NotFound");
            }
            await patientService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
