using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Domain.Enum;
using EventTicketingSystem.Infrastructure.Data;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Repositories.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly TicketDbContext _ticketDbContext;

        public AdminRepository(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<User?> FindUserExistAsync(string email)
        {
            return await _ticketDbContext.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Role?> GetRoleByNameAsync(RoleType roleType)
        {
            return await _ticketDbContext.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleType);
        }

        public async Task<bool> AddUserRoleAsync(UserRole userRole)
        {
            _ticketDbContext.UserRoles.Add(userRole);
            return await _ticketDbContext.SaveChangesAsync() > 0; // returns true if saved
        }

        public async  Task<List<string>> GetUserRolesAsync(int userId)
        {
            return await _ticketDbContext.UserRoles
                .Where(ur => ur.User.Id == userId)
                .Select(ur => ur.Role.RoleName.ToString())
                .ToListAsync();
        }
    }

}
