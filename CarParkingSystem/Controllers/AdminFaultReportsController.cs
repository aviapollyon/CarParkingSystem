using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarParkingSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using CarParkingSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = "Admin")]
public class AdminFaultReportsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminFaultReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Admin
    public async Task<IActionResult> Index()
    {
        var faultReports = await _context.FaultReports.ToListAsync();
        return View(faultReports);
    }

    // GET: Admin/Details/5
    //public async Task<IActionResult> Details(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var faultReport = await _context.FaultReports.FindAsync(id);
    //    if (faultReport == null)
    //    {
    //        return NotFound();
    //    }

    //    ApplicationUser assignedTechUser = null;
    //    if (faultReport.AssignedTech != null)
    //    {
    //        assignedTechUser = await ApplicationUser.FindByOrgNumAsync(_userManager, faultReport.AssignedTech.Value);
    //    }

    //    ViewBag.AssignedTechUser = assignedTechUser;

    //    return View(faultReport);
    //}
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

        // Get all users with the role "Guard"
        var guardUsers = await _userManager.GetUsersInRoleAsync("Technician");

        // Create a SelectList with users' orgNum as value and Name as text
        ViewBag.GuardUsers = new SelectList(
            guardUsers.Select(u => new { orgNum = u.orgNum, Name = $"{u.FirstName} {u.LastName}" }),
            "orgNum",
            "Name"
        );

        return View(faultReport);
    }


    // POST: Admin/AssignTech
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignTech(int id, long? AssignedTech)
    {
        if (AssignedTech == null)
        {
            ModelState.AddModelError("", "Please select a technician.");
            return RedirectToAction(nameof(Details), new { id });
        }

        var faultReport = await _context.FaultReports.FindAsync(id);
        if (faultReport == null)
        {
            return NotFound();
        }

        faultReport.AssignedTech = AssignedTech;
        _context.Update(faultReport);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}
