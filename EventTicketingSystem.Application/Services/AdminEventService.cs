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
    public class AdminEventService:IAdminEventService
    {

        private readonly IAdminEventRepository _eventRepository;

        public AdminEventService(IAdminEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<CreateEventDto?> CreateEventAsyc(CreateEventDto dto)
        {
            var host = await _eventRepository.FindHostByMailAsync(dto.HostEmail);
           

            if (host == null)
            {
                return null;
            }

            var newEvent = new Event
            {
                Title = dto.Title,
                Location = dto.Location,
                Time = DateTime.UtcNow,
                Capacity = dto.Capacity,
                HostId = host.Id,

            };

            await _eventRepository.CreateEventAsync(newEvent);
            var evenDato = new CreateEventDto
            {
                Title = newEvent.Title,
                Location = newEvent.Location,
                Capacity = newEvent.Capacity,
                HostEmail = host.Email
            };

            return evenDato;
        }


        public async Task<IEnumerable<CreateEventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return events.Select(e => new CreateEventDto
            {

                Title = e.Title,
                Location = e.Location,
                Capacity = e.Capacity,
                HostEmail = e.Host.Email,

            }).ToList();
        }

       
        public async Task<CreateEventDto?> UpdateEventAsync(string title, CreateEventDto dto)
        {
            var existingEvent = await _eventRepository.GetEventByNameAsync(title);
            if (existingEvent == null) return null;

            existingEvent.Title = dto.Title;
            existingEvent.Location = dto.Location;
            existingEvent.Capacity = dto.Capacity;

            await _eventRepository.UpdateEventAsync(existingEvent);

            return new CreateEventDto
            {

                Title = existingEvent.Title,
                Location = existingEvent.Location,
                Capacity = existingEvent.Capacity,
                HostEmail = existingEvent.Host.Email,

            };
        }

        public async Task<bool> DeleteEventAsync(string title)
        {
            var existingEvent = await _eventRepository.GetEventByNameAsync(title);
            if (existingEvent == null) return false;

            await _eventRepository.DeleteEventAsync(existingEvent);
            return true;
        }





    }
}
