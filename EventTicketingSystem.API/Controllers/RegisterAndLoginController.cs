using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Application.Services;
using EventTicketingSystem.Domain.Entites;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace EventTicketingSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegisterAndLoginController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IAuthService _authService;
        public RegisterAndLoginController(IUserServices userServices,IAuthService authService)
        {
                _userServices = userServices;
                _authService = authService;
        }
        [HttpPost("UserRegister")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {

            if (userDto == null)
            {
                return BadRequest("User Details cannot be empty");
            }
            if (!userDto.Email.EndsWith("@gmail.com"))
            {
                return BadRequest("Only Gmail addresses are allowed .ex(user@gamil.com).");
            }

            bool isUser = await _userServices.RegisterUserAsync(userDto);
            if (!isUser)
            {
                return BadRequest("User already Exist! Try Logging in ");
            }
            else
            {
                return Ok("User Details Entered Successfully");
            }

        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null) {
                return BadRequest("User Credentials cannot be null");
            }
            string? token = await _authService.GenerateJwtAsync(loginDTO);
            if (string.IsNullOrEmpty(token)) {
                return BadRequest("Enter Valid Credentials");
            }
            else
            {
                return Ok("Login Successfull: " + token);
            }
        }
    }
}
