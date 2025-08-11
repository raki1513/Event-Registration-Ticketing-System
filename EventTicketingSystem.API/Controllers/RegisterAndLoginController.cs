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
                return BadRequest(new { message = "User Details cannot be empty" });
            }
            if (!userDto.Email.EndsWith("@gmail.com"))
            {
                return BadRequest(new { message = "Only Gmail addresses are allowed .ex(user@gamil.com)." });
            }

            bool isUser = await _userServices.RegisterUserAsync(userDto);
            if (!isUser)
            {
                return BadRequest(new { message = "User Already Exist! Try Logging in" });
            }
            else
            {
                return Ok(new { message = "User Details Entered Successfully" });
            }

        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null) {
                return BadRequest(new { message = "User credentials cannot be null" });
            }
            string? token = await _authService.GenerateJwtAsync(loginDTO);
            if (string.IsNullOrEmpty(token)) {
                return BadRequest(new { message = "Enter Valid Credentials" });
            }
            else
            {
                return Ok(new { token = token });
            }
        }
    }
}
