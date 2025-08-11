using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Validations;
using System.Security.Claims;

namespace EventTicketingSystem.API.Controllers
{
    [Authorize(Roles = "EventHost")]
    [ApiController]
    [Route("api/eventhost")]
    public class EvenHostDashboardController : ControllerBase
    {
        private readonly IHostEventService _eventService;

        public EvenHostDashboardController(IHostEventService eventService) {

            _eventService = eventService;
        
        }

      

        [HttpPost("Create an Event")]

        public async Task<IActionResult> CreateEventAsync(CreateEventDto dto)
        {
            var createdEvent = await _eventService.CreateEventAsyc(dto);
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

        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetEventsOfHost()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            int host = Convert.ToInt32((userId));
            var events = await _eventService.GetEventbyHostId(host);
            if (events == null)
            {
                return BadRequest("Host Has No Events");

            }
            return Ok(events);
        }

        [HttpPut("GetEventBy{title}")]
        
        public async Task<IActionResult> UpdateEvent(string title, CreateEventDto dto)
        {
            var id = User.FindFirst(ClaimTypes.Sid)?.Value;
            int hostid = Convert.ToInt32(id);
            //var events = await _eventService.GetEventbyHostId(hostid);
            var updated = await _eventService.UpdateEventAsync(hostid,title, dto);
            if (updated == null) return NotFound("Event not found.");
            return Ok(updated);
        }

        [HttpDelete("DeletEventBy{title}")]
       
        public async Task<IActionResult> DeleteEvent(string title)
        {
            var id = User.FindFirst(ClaimTypes.Sid)?.Value;

            int hostid = Convert.ToInt32(id);
          

            var deleted = await _eventService.DeleteEventAsync(hostid,title);
            if (!deleted) return NotFound("Event not found.");
            return Ok("Event deleted successfully.");
        }


    }
}
