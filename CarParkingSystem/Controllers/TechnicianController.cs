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
    [Authorize(Roles = "Technician,Admin")]
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
                    .Where(fr => fr.AssignedTech == assignedOrgNum && fr.Status != "VerifiedAsComplete")
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
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport != null)
            {
                faultReport.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Messages(int id)
        {
            var faultReport = _context.FaultReports.FirstOrDefault(f => f.Id == id);
            var messages = _context.FaultReportMessage.Where(m => m.FaultId == id).ToList();

            if (faultReport == null)
            {
                return NotFound();
            }

            var model = new Tuple<FaultReport, List<FaultReportMessage>>(faultReport, messages);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddMessage(int FaultId, string Title, string MessageReport, IFormFile? Image)
        {
            var newMessage = new FaultReportMessage
            {
                FaultId = FaultId,
                Title = Title,
                MessageReport = MessageReport,
                DateTime = DateTime.Now
            };

            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Image.CopyTo(memoryStream);
                    newMessage.Image = memoryStream.ToArray();
                }
            }

            _context.FaultReportMessage.Add(newMessage);
            _context.SaveChanges();

            return RedirectToAction("Messages", new { id = FaultId });
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
