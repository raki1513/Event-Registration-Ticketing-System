using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.API.Controllers
{
    [Authorize(Roles = "Attendee")]
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeeController : ControllerBase
    {
        [HttpGet("authorization")]
        public IActionResult Index()
        {
            return Ok("You are authorized as Attendee!");
        }
    }
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("authorization")]
        public IActionResult Index()
        {
            return Ok("You are authorized as Admin!");
        }
    }
}
