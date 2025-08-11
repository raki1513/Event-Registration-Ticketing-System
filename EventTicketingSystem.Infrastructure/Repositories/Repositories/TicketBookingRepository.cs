using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Infrastructure.Data;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace EventTicketingSystem.Infrastructure.Repositories.Repositories
{
    public class TicketBookingRepository:ITicketBookingRepository
    {
        private readonly TicketDbContext _ticketDbContext;

        public TicketBookingRepository(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<User?> FindUserExistAsync(string email)
        {
            return await _ticketDbContext.Users
                .Include(u => u.Tickets)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Event?> GetEventByTitleAsync(string eventTitle)
        {
            var eve= await _ticketDbContext.Events.FirstOrDefaultAsync(x => x.Title == eventTitle);
            return eve;
        }

        public async Task<Ticket?> GetUserTicketForEventAsync(int userId, int eventId)
        {
            return await _ticketDbContext.Tickets
                .FirstOrDefaultAsync(t => t.AttendeeId == userId && t.EventId == eventId);
        }

        public async Task<Ticket> BookTicketAsync(Ticket ticket)
        {
            _ticketDbContext.Tickets.Add(ticket);
            await _ticketDbContext.SaveChangesAsync();
            return ticket;
        }
    }
}
