using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(User user);
        Task<User> FindUserExist(string email);
        Task<Role> GetRoleByName(RoleType roleType);
        Task SaveChangesAsync();
    }
}
