using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GigHub.Core.Models;
using GigHub.Data;
using GigHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers.Api
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
        public IActionResult Attend(AttendanceDto dto)
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

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var attendance = _context.Attendances
                .SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);

            _context.SaveChanges();

            return Ok(id);
        }
    }
}