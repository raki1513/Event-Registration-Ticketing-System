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
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignDto)
        {
            if (assignDto == null)
            {
                return BadRequest("Role details cannot be empty");
            }
            bool isRole = await _userServices.AssignRole(assignDto);
            if (!isRole)
            {
                return BadRequest("Falied to assign role, User may not be found, Role may not be found or User already has been assigned to the role");
             }
            return Ok("Role Assigned Successfully!");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
