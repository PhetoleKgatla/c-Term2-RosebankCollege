using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;

namespace RaceDayMVC.Controllers
{
    public class UserController : Controller
    {
        
            private readonly AuthApiService _authService;

            public UserController(AuthApiService authService)
            {
                _authService = authService;
            }

            [HttpGet]
            public IActionResult Login() => View();

            [HttpPost]
            public async Task<IActionResult> Login(LoginViewModel model)
            {
                if (!ModelState.IsValid) return View(model);

                var user = await _authService.LoginAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

            // Maintain user authenticated state and role across all pages using session management
            HttpContext.Session.SetInt32("UserId", user.UserID);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            // Redirect based on role
            if (user.Role == "Organiser")
                    return RedirectToAction("Dashboard", "Organiser");

                return RedirectToAction("Index", "Events");
            }

            [HttpGet]
            public IActionResult Register() => View();

            [HttpPost]
            public async Task<IActionResult> Register(RegisterViewModel model)
            {
                if (!ModelState.IsValid) return View(model);

                var success = await _authService.RegisterAsync(model);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, "Registration failed.");
                    return View(model);
                }

                return RedirectToAction("Login");
            }

            public IActionResult Logout()
            {
                HttpContext.Session.Clear(); // Clear session state
                return RedirectToAction("Index", "Home");
            }
        }
    }