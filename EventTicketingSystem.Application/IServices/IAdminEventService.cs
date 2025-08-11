using EventTicketingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.IServices
{
    public interface IAdminEventService
    {

        Task<CreateEventDto?> CreateEventAsyc(CreateEventDto dto);
        Task<IEnumerable<CreateEventDto?>> GetAllEventsAsync();

        Task<CreateEventDto?> UpdateEventAsync(string title, CreateEventDto dto);
        Task<bool> DeleteEventAsync(string title);
    }
}
