
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part_3.Data;
using POE_Part_3.Models;
using System.Linq;
using System;
using Microsoft.Extensions.Hosting;
using POE_Part_3.Data.Data;

namespace POE_Part_3.Controllers
{
    public class UserController : Controller
    {
        private readonly Data.Data.AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public UserController(Data.Data.AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult ViewClaims()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var claims = _context.Claims.Where(c => c.UserId == userId).ToList();
            return View(claims);
        }

        public IActionResult AddClaim() => View();



        /*
          [HttpPost]
        public IActionResult AddClaim(Claim claim)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page or show an error message
                return RedirectToAction("Login", "Account");
            }
            claim.UserId = userId.Value;
            if (claim.HoursWorked <= 0 || claim.HourlyRate <= 0)
            {
                // Handle error: invalid hours worked or hourly rate
                return View(claim); // Return the view with error message or validation feedback
            }
            claim.TotalAmount = claim.HoursWorked * claim.HourlyRate;
            claim.Status = (claim.HoursWorked > 80 || claim.HourlyRate > 120) ? "Rejected" : "Accepted";

            // Handle file upload logic
            if (claim.FilePath != null && claim.FilePath.Length > 0)
            {
                // Specify a directory for the file
                var filePath = Path.Combine("wwwroot", "uploads", claim.FilePath.FileName); // Adjust the path as necessary
                claim.FilePath = filePath;

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            else
            {
                claim.FilePath = null; // Ensure it's null if no file is uploaded
            }

            try
            {
                _context.Claims.Add(claim);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the error: Log it or display a message
                // Example: _logger.LogError(ex, "Error adding claim");
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }

            return RedirectToAction("Claims");
        }
         */


        [HttpPost]
        public IActionResult SubmitClaim(Claim claim, IFormFile file)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) { 
            return RedirectToAction("Login", "Account");
        }

            if (ModelState.IsValid) {
                string filePath = "";

                if (file != null && (file.ContentType == "application/pdf" ||
                                     file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                                     file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    var uploadsDir = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var fileName = Path.GetFileName(file.FileName);
                    filePath = Path.Combine(uploadsDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    claim.FilePath = Path.Combine("uploads", file.FileName);
                    //filePath = "/uploads/" + fileName;
                }
                else
                {
                    claim.FilePath = null;
                }

                var claim1 = new Claim
                {
                    Id = _context.Claims.Count() + 1,
                    UserId = (int)userId,
                    HoursWorked = claim.HoursWorked,
                    HourlyRate = claim.HourlyRate,
                    Notes = claim.Notes,
                    FilePath = filePath,
                    Status = "Pending"
                };
                if (claim1.HoursWorked >= 80)
                {
                    if (claim1.HourlyRate >= 120)
                    {
                        claim1.Status = "Rejected";
                    }
                    else
                    {
                        claim1.Status = "Accepted";
                    }
                }
                _context.Claims.Add(claim1);
                _context.SaveChanges();

                return RedirectToAction("ViewClaims", claim1);
            }
            else
            {
                ViewBag.Message = "Error saving claim details";
                return View(AddClaim);

            }

        }

        /*
         *  public IActionResult ViewClaims()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var claims = _context.Claims.Where(c => c.UserId == userId).ToList();
            return View(claims);
        }
         */

    }
}

