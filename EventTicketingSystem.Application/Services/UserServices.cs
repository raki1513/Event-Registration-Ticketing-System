using EventTicketingSystem.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
namespace EventTicketingSystem.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> RegisterUser(UserDto userDTO)
        {
            var isAlreadyUser = await _userRepository.FindUserExist(userDTO.Email);
            if (isAlreadyUser != null) {
                return false;
            }
            var newUser = new User
            {
                Email = userDTO.Email,
                Username = userDTO.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),

            };
            await _userRepository.AddUserAsync(newUser);
            return true;
            throw new NotImplementedException();
        }

        public async Task<bool> UserLogin(LoginDTO loginDTO)
        {

            throw new NotImplementedException();
        }
    }

}
