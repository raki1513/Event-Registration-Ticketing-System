using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.API.Controllers
{
    [Authorize(Roles = "Attendee")]
    [ApiController]
    [Route("api/attendee")]
    public class AttendeDashboardController : ControllerBase
    {
        private readonly ITicketBookingService _ticketBookingService;

        public AttendeDashboardController(ITicketBookingService ticketBookingService)
        {
            _ticketBookingService = ticketBookingService;
        }

        [HttpPost("book-ticket")]
        
        public async Task<IActionResult> BookTicket(BookTicketDto dto)
        {
            var result = await _ticketBookingService.BookTicketAsync(dto);
            if (result == null) return BadRequest("Booking failed: User/Event not found or already booked.");
            return Ok(result);
        }
    }
}
