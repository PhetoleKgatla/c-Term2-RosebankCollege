using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using System.Threading.Tasks;

namespace RaceDay.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventApiService _eventApiService;

        public EventsController(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }

        // GET: /Events/
        public async Task<IActionResult> Index()
        {
            var events = await _eventApiService.GetAllEventsAsync();
            return View(events);
        }
    }
}