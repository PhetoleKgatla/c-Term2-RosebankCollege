
using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;

namespace RaceDayMVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly EventApiService _eventApiService;

        public ParticipantController(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }

        // GET: Participant/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Check whether the user is logged in as a Participant
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "User");
            }

            if (role != "Participant")
            {
                return RedirectToAction("Login", "User");
            }

            // Get all events from the API
            // Get the participant's email from Session
            ViewBag.ParticipantEmail =
                HttpContext.Session.GetString("UserEmail");

            // Get all events from the API
            var events = await _eventApiService.GetAllEventsAsync();

            // Display the events on the Participant Dashboard
            return View(events);
        }


        // GET: Participant/Details/1
        public async Task<IActionResult> Details(int id)
        {
            // Check whether the user is logged in as a Participant
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "User");
            }

            if (!string.Equals(role, "Participant",
                StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login", "User");
            }

            // Retrieve the selected event
            var raceEvent = await _eventApiService.GetEventByIdAsync(id);

            if (raceEvent == null)
            {
                return NotFound("Event not found.");
            }

            // Retrieve categories belonging to this event
            var categories = await _eventApiService
                .GetCategoriesByEventIdAsync(id);

            // Send categories to the Details page
            ViewBag.Categories = categories;

            return View(raceEvent);
        }

    }
}
