using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendancesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Attend(AttendanceDto dto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var attendance = _unitOfWork.Attendances.GetAttendance(dto.GigId, userId);

            if (attendance != null)
                return BadRequest("The attendance exists.");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);

            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}