using Microsoft.AspNetCore.Mvc;
using CarParkingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CarParkingSystem.Areas.Identity.Data;
using CarParkingSystem.Models;

[Authorize(Roles = "Student")]
public class FaultReportsController : Controller
{
    private readonly ApplicationDbContext _context;

    public FaultReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(FaultReport faultReport)
    {
        if (ModelState.IsValid)
        {
            _context.FaultReports.Add(faultReport);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "FaultReports"); // Redirect to a confirmation page or home
        }
        return View(faultReport);
    }
}
