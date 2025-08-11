using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.Services
{
    public class TicketBookingService:ITicketBookingService
    {
        private readonly ITicketBookingRepository _ticketRepository;

        public TicketBookingService(ITicketBookingRepository ticketBookingRepository)
        {
            _ticketRepository=ticketBookingRepository;
        }

        public async Task<BookTicketDto?> BookTicketAsync(BookTicketDto dto)
        {

            var user = await _ticketRepository.FindUserExistAsync(dto.AttendeeEmail);
            if (user == null) return null;


            var eventEntity = await _ticketRepository.GetEventByTitleAsync(dto.EventTitle);
            if (eventEntity == null) return null;


            var existingTicket = await _ticketRepository.GetUserTicketForEventAsync(user.Id, eventEntity.Id);
            if (existingTicket != null) return null;


            var ticket = new Ticket
            {
                AttendeeId = user.Id,
                EventId = eventEntity.Id,
                TicketNumber = Guid.NewGuid().ToString()
            };

            await _ticketRepository.BookTicketAsync(ticket);

            return new BookTicketDto
            {
                AttendeeEmail = user.Email,
                EventTitle = eventEntity.Title,
                
            };

        }
    }
}
