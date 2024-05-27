using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarParkingSystem.Areas.Identity.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarParkingSystem.Controllers
{
    public class GuardShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuardShiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GuardShifts
        public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the guard user based on the user's ID
            var guard = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            // Retrieve the schedules assigned to the guard
            var schedules = await _context.Schedule
                .Where(s => s.orgNum == guard.orgNum)
                .Include(s => s.Shift)
                .ToListAsync();

            return View(schedules);
        }
    }
}
