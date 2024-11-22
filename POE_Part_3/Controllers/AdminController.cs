using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part_3.Models;
using System;
using POE_Part_3.Data;

namespace POE_Part_3.Controllers.Controllers
{
    public class AdminController : Controller
    {
        private readonly Data.Data.AppDbContext _context;

        public AdminController(Data.Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AllClaims()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }
    }
}
