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


    public class HostEventRepository:IHostEventRepository
    {

        private readonly TicketDbContext _ticketDbContext;

        public HostEventRepository(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }


       public async Task<Event> CreateEventAsync(Event eve)
        {
           _ticketDbContext.Events.Add(eve);
            await _ticketDbContext.SaveChangesAsync();
            return eve;



        }

        public async Task<User> FindHostByMailAsync(string email)
        {
            var found= await _ticketDbContext.Users.FirstOrDefaultAsync(e=>e.Email == email);
            if (found != null)
            {
                return found;
            }
            return null;

            
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _ticketDbContext.Events
                .Include(e => e.Host)
                .ToListAsync();
        }
        // Correct repository method signature:
        public async Task<List<Event>> GetEventByHostIdAsync(int hostId)
        {
            return await _ticketDbContext.Events
                .Where(e => e.HostId == hostId)
                .Include(e => e.Host)
                .ToListAsync();
        }
        public async Task<Event?> GetEventByNameAsync(string title)
        {
            return await _ticketDbContext.Events.Include(e => e.Host).FirstOrDefaultAsync(e => e.Title == title);
        }



        public async Task UpdateEventAsync(Event eve)
        {
            _ticketDbContext.Events.Update(eve);
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Event eve)
        {
            _ticketDbContext.Events.Remove(eve);
            await _ticketDbContext.SaveChangesAsync();
        }


    } 
}
