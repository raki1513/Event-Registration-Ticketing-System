using EventTicketingSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Repositories.IRepositories
{
    public  interface ITicketBookingRepository
    {
        Task<User?> FindUserExistAsync(string email);
        Task<Event?> GetEventByTitleAsync(string  eventTitle);
        Task<Ticket?> GetUserTicketForEventAsync(int userId, int eventId);
        Task<Ticket> BookTicketAsync(Ticket ticket);
    }
}
