using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Domain.Enum;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using EventTicketingSystem.Infrastructure.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.Services
{


    public class AdminServices:IAdminServices
    {
        private readonly IAdminRepository _adminRepository;
        public AdminServices(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> AssignRole(AssignRoleDto assignDto)
        {
            var user = await _adminRepository.FindUserExistAsync(assignDto.Email);
            if (user == null) return false;

            if (!Enum.TryParse<RoleType>(assignDto.RoleName, true, out var roleType))
                return false;

            var role = await _adminRepository.GetRoleByNameAsync(roleType);
            if (role == null) return false;

            if (user.UserRoles.Any(ur => ur.RoleId == role.Id)) return false;

            var userrole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await _adminRepository.AddUserRoleAsync(userrole);
            return true;
        }

    }

}
