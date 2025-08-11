using EventTicketingSystem.Domain.Entites;
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
    public class AuthenticationRepository : IAuthentication
    {
        private readonly TicketDbContext _ticketDbContext;
        public AuthenticationRepository(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }
        public async Task<User> FindUserExistAsync(string username)
        {
            var user=await _ticketDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }
        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            return await _ticketDbContext.UserRoles
                .Where(ur => ur.User.Id == userId)
                .Select(ur => ur.Role.RoleName.ToString())
                .ToListAsync();
        }
    }
}
