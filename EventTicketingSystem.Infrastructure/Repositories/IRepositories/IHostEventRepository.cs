using EventTicketingSystem.Domain.Entites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Repositories.IRepositories
{
    public interface IHostEventRepository
    {
        Task<Event> CreateEventAsync(Event ev);

        Task<User>FindHostByMailAsync( string email);
        Task<List<Event>> GetAllEventsAsync();

        Task<Event?> GetEventByNameAsync(string name);
        Task<List<Event>>GetEventByHostIdAsync(int hostId);
        Task UpdateEventAsync(Event eve);
        Task DeleteEventAsync(Event eve);
    }
}
