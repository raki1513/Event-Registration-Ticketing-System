using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.IServices
{
    public interface IHostEventService
    {
         Task<CreateEventDto?> CreateEventAsyc(CreateEventDto dto);
        Task<IEnumerable<CreateEventDto?>> GetAllEventsAsync();

        Task<List<CreateEventDto>> GetEventbyHostId(int hostId);
        Task<CreateEventDto?> UpdateEventAsync(int hostid,string title, CreateEventDto dto);
        Task<bool> DeleteEventAsync(int hostid,string title);
    }
}
