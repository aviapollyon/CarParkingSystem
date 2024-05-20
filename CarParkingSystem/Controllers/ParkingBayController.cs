using CarParkingSystem.Areas.Identity.Data;
using CarParkingSystem.Models;
using CarParkingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParkingSystem.Controllers
{
    public class ParkingBayController : Controller
    {
        private readonly ParkingBayService _parkingBayService;
        private readonly ApplicationDbContext _context;

        public ParkingBayController(ParkingBayService parkingBayService, ApplicationDbContext context)
        {
            _parkingBayService = parkingBayService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parkingBays = await _context.ParkingBays.ToListAsync();
            return View(parkingBays);
        }

        [HttpGet]
        public IActionResult UpdateOccupancy(string baySection)
        {
            var model = new UpdateOccupancyViewModel
            {
                BaySection = baySection
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOccupancy(UpdateOccupancyViewModel model)
        {
            var specificBayNumbers = model.SpecificBayNumbers?.Split(',')
                .Select(x => x.Trim()).ToList();
            await _parkingBayService.UpdateOccupancyAsync(model.BaySection, model.NewStatus, specificBayNumbers);
            return RedirectToAction("Index");
        }
    }
}
