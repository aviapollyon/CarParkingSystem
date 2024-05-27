using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarParkingSystem.Areas.Identity.Data;
using CarParkingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CarParkingSystem.Controllers
{
    public class AdminShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminShiftsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AdminShifts
        public async Task<IActionResult> Index()
        {
            var shifts = await _context.Shifts.ToListAsync();
            return View(shifts);
        }

        // GET: AdminShifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminShifts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,inTime,outTime,inBreak,outBreak")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }
        // GET: AdminShifts/Assigned
        public async Task<IActionResult> Assigned()
        {
            var assignedShifts = await _context.Schedule.Include(s => s.Shift).ToListAsync();
            return View(assignedShifts);
        }

        // POST: AdminShifts/RemoveAssigned
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAssigned(int scheduleId)
        {
            var schedule = await _context.Schedule.FindAsync(scheduleId);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Assigned));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // Other actions like Edit, Delete, etc. can be added similarly
    }
}
