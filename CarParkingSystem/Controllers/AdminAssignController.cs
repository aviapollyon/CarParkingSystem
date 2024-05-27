using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarParkingSystem.Areas.Identity.Data;
using CarParkingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CarParkingSystem.Controllers
{
    public class AdminAssignController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminAssignController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AdminAssign
        public async Task<IActionResult> Index()
        {
            var guardUsers = await _userManager.GetUsersInRoleAsync("Guard");
            var shifts = await _context.Shifts.ToListAsync();
            ViewBag.Guards = guardUsers;
            ViewBag.Shifts = shifts;
            return View();
        }

        // POST: AdminAssign/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int guardId, int shiftId)
        {
            // Check if the guard already has an active schedule
            var existingSchedule = await _context.Schedule.FirstOrDefaultAsync(s => s.orgNum == guardId);
            if (existingSchedule != null)
            {
                // If the guard already has an active schedule, you may want to handle this scenario accordingly
                // For example, you can return a view with an error message
                return RedirectToAction(nameof(Index)); // Redirect to Index for now, you can change this behavior as per your requirement
            }

            // If the guard does not have an active schedule, proceed with assigning the new schedule
            var schedule = new Schedule { orgNum = guardId, shiftId = shiftId };
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // Other actions like Edit, Delete, etc. can be added similarly
    }
}
