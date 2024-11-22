
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part_3.Data;
using POE_Part_3.Models;
using System.Linq;
using System;

namespace POE_Part_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly Data.Data.AppDbContext _context;

        public AccountController(Data.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Redirect based on role
            switch (user.Role)
            {
                case "Admin":
                    return RedirectToAction("AllClaims", "Admin"); // Admin-specific action
                case "HR":
                    return RedirectToAction("UserData", "HR"); // HR-specific action
                default:
                    return RedirectToAction("ViewClaims", "User"); // Default action for regular users
            }
        }

        public IActionResult Signup() => View();

        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}

