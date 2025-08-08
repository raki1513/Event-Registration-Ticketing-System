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
    public class UserRepository : IUserRepository
    {
        private readonly TicketDbContext _context;
        public UserRepository(TicketDbContext context) => _context = context;
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            const int attendeeRoleId = 2;

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = attendeeRoleId
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

        }

        public Task<User> FindUserExist(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
