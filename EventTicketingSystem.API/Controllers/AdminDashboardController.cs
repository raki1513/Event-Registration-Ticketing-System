using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventTicketingSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/")]
    public class AdminDashboardController : ControllerBase
    {

        private readonly IAdminServices _adminServices;
        private readonly IAdminEventService _adminEventService;
        public AdminDashboardController(IAdminServices adminServices, IAdminEventService adminEventService  )
        {
            _adminServices = adminServices;
            _adminEventService = adminEventService;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignDto)
        {
            var result = await _adminServices.AssignRole(assignDto);

            if (!result)
                return BadRequest("User not found, role not found, or already assigned.");

            return Ok("Role assigned successfully.");
        }


        [HttpPost("create")]
       
        public async Task<IActionResult> CreateEventAsync(CreateEventDto dto)
        {
            var createdEvent = await _adminEventService.CreateEventAsyc(dto);
            if (createdEvent == null)
            {
                return BadRequest("Host not found or something went wrong");
            }

            return Ok(new
            {
                Message = "Event created successfully",
                Event = createdEvent
            });
        }


        [HttpGet("allEvent")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _adminEventService.GetAllEventsAsync();
            return Ok(events);
        }



        [HttpPut("GetEventby{title}")]

        public async Task<IActionResult> UpdateEvent(string title, CreateEventDto dto)
        {
            var updated = await _adminEventService.UpdateEventAsync(title, dto);
            if (updated == null) return NotFound("Event not found.");
            return Ok(updated);
        }

        [HttpDelete("DeletEvetby{title}")]

        public async Task<IActionResult> DeleteEvent(string title)
        {
            var deleted = await _adminEventService.DeleteEventAsync(title);
            if (!deleted) return NotFound("Event not found.");
            return Ok("Event deleted successfully.");
        }
    }
}
