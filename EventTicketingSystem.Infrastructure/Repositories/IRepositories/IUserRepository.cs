using EventTicketingSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> FindUserExist(string email);
    }
}
