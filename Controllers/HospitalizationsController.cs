using Hospital.DTO;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace Hospital.Controllers
{
    public class HospitalizationsController : Controller
    {
        HospitalizationService hospitalizationService;

        public HospitalizationsController(HospitalizationService hospitalizationService)
        {
            this.hospitalizationService = hospitalizationService;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var hospitalizationVMs = await hospitalizationService.GetAllHospitalizationsAsync();
            return View(hospitalizationVMs);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            await FillSelects();
            return View();
        }

        private async Task FillSelects()
        {
            var dropdownsData = await hospitalizationService.GetDropdownsData();
            ViewBag.Patients = new SelectList(dropdownsData.Patients, "Id", "Name");
            ViewBag.Departments = new SelectList(dropdownsData.Departments, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(HospitalizationDto newHospitalization)
        {
            await hospitalizationService.CreateAsync(newHospitalization);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var hospitalizationToEdit = await hospitalizationService.GetByIdAsync(id);
            if (hospitalizationToEdit == null)
            {
                return View("NotFound");
            }
            await FillSelects();
            return View(hospitalizationToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(HospitalizationDto editedHospitalization)
        {
            await hospitalizationService.UpdateAsync(editedHospitalization);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await hospitalizationService.DeleteAsync(id);
            return RedirectToAction("Index", "Patients");
        }

    }
}
