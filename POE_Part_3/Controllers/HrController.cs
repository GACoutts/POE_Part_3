
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part_3.Models;
using POE_Part_3.Data;
using System.Linq;
using System;

namespace POE_Part_3.Controllers.Controllers
{
    public class HrController : Controller
    {
        private readonly Data.Data.AppDbContext _context;

        public HrController(Data.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult UserData() => View(_context.Users.ToList());

        public async Task<IActionResult> AcceptedClaimsReport()
        {
            // Fetch only accepted claims
            var acceptedClaims = await _context.Claims.Where(c => c.Status == "Accepted").ToListAsync();
            return View(acceptedClaims);

        }

        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Users.Any(u => u.Id == user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserData)); // Redirect to a list or another relevant view
            }
            return View(user);
        }

        public async Task<IActionResult> GenerateAcceptedClaimsReport()
        {
            var acceptedClaims = await _context.Claims
                                               .Where(c => c.Status == "Accepted")
                                               .ToListAsync();

            // Generate a CSV or other file format
            var csvContent = "Claim ID,User ID,Hours Worked,Hourly Rate,Total Amount,Status\n";
            foreach (var claim in acceptedClaims)
            {
                csvContent += $"{claim.Id},{claim.UserId},{claim.HoursWorked},{claim.HourlyRate},{claim.TotalAmount},{claim.Status}\n";
            }

            // Return the file as a downloadable response
            var fileName = "AcceptedClaimsReport.csv";
            var fileBytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(fileBytes, "text/csv", fileName);
        }

    }
}

