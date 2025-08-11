using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Repositories.IRepositories
{



    public interface IAdminRepository
    {
      
        Task<List<string>> GetUserRolesAsync(int userId);
 

        Task<User?> FindUserExistAsync(string email);
        Task<Role?> GetRoleByNameAsync(RoleType roleType);
        Task<bool> AddUserRoleAsync(UserRole userRole);
    }
}
