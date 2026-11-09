using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly EventApiService _eventApiService;

        public OrganiserController(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }

        // GET: Organiser/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Verify session role is Organiser
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Organiser")
            {
                return RedirectToAction("Login", "User");
            }

            // Call the correct method name defined in your EventApiService
            var events = await _eventApiService.GetAllEventsAsync();

            return View(events);
        }
    }
}