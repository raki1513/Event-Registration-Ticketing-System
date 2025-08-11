using EventTicketingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.IServices
{
    public interface IUserServices
    {
        Task<bool> UserLoginAsync(LoginDTO loginDTO);
        Task<bool> RegisterUserAsync(UserDto userDTO);
        //Task<bool> AssignRole(AssignRoleDto assignDto);
    }
}
