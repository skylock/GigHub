using GigHub.Data;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GigHub.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Attend(AttendaceDto dto)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_context.Attendances.Any(a => a.AttendeeId == currentUserId && a.GigId == dto.GigId))
                return BadRequest("The attendance exists.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = currentUserId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

        // GET: api/Gigs
        [HttpGet]
        public IEnumerable<Gig> GetGigs()
        {
            return _context.Gigs;
        }
    }
}