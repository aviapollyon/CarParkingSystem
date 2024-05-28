using Microsoft.AspNetCore.Mvc;

namespace CarParkingSystem.Controllers
{
    public class TIAScheduleController : Controller
    {
        public IActionResult admin()
        {
            return View();
        }
        public IActionResult guard()
        {
            return View();
        }
    }
}
