using Microsoft.AspNetCore.Mvc;
using EventTicketingSystem.Application.Services;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
namespace EventTicketingSystem.API.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IAuthService _authService;
        public AuthenticationController(IUserServices userServices,IAuthService authService)
        {
                _userServices = userServices;
                _authService = authService;
        }
        [HttpPost("Registering the User")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User Details cannot be empty");
            }
            bool isUser = await _userServices.RegisterUser(userDto);
            if (!isUser)
            {
                return BadRequest("User already Exist! Try Logging in ");
            }
            else
            {
                return Ok("User Details Entered Successfully");
            }

        }
        [HttpPost("Login the User")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null) {
                return BadRequest("User Credentials cannot be null");
            }
            string? token = await _authService.LoginUser(loginDTO);
            if (string.IsNullOrEmpty(token)) {
                return BadRequest("Enter Valid Credentials");
            }
            else
            {
                return Ok("Login Successfull, Here's the Token: " +token);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
