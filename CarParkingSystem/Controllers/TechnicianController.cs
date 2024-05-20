using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarParkingSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using CarParkingSystem.Areas.Identity.Data;

namespace CarParkingSystem.Controllers
{
    [Authorize(Roles = "Technician")]
    public class TechnicianController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TechnicianController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Guard
        public async Task<IActionResult> Index()
        {
            var guardId = _userManager.GetUserId(User);
            var guard = await _userManager.FindByIdAsync(guardId);
            if (guard != null)
            {
                var assignedOrgNum = guard.orgNum;
                var faultReports = await _context.FaultReports
                    .Where(fr => fr.AssignedTech == assignedOrgNum)
                    .ToListAsync();
                return View(faultReports);
            }
            // Handle the case where the guard or orgNum is null
            return RedirectToAction("Error", "Home"); // Redirect to an error page
        }
        // GET: Guard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport == null)
            {
                return NotFound();
            }

            return View(faultReport);
        }


        // POST: Guard/ConfirmFault
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmFault(int id)
        {
            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport != null)
            {
                faultReport.Status = "  ";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Guard/DenyFault
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DenyFault(int id)
        {
            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport != null)
            {
                _context.FaultReports.Remove(faultReport);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
