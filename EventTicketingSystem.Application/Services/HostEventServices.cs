using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.IServices;
using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.Services
{


    public class HostEventServices: IHostEventService
    {

        private readonly IHostEventRepository _eventRepository;

        public HostEventServices(IHostEventRepository eventRepository)
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
            var evenDato=new CreateEventDto
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
               
            });
        }

        public async Task<List<CreateEventDto>> GetEventbyHostId(int hostId)
        {
            var events = await _eventRepository.GetEventByHostIdAsync(hostId);
            var newevent = events.Select(e => new CreateEventDto
            {
                Title = e.Title,
                Location = e.Location,
                Capacity = e.Capacity,
                HostEmail = e.Host.Email,
            });
            return newevent.ToList();
           
        }
        public async Task<CreateEventDto?> UpdateEventAsync(int hostId,string title, CreateEventDto dto)
        {
            var events = await _eventRepository.GetEventByHostIdAsync(hostId);
            var matchedEvent = events.FirstOrDefault(e => e.Title == title);

            if(matchedEvent==null)
            {
                return null;
            }
            matchedEvent.Title = dto.Title;
            matchedEvent.Location = dto.Location;
            matchedEvent.Capacity = dto.Capacity;

            await _eventRepository.UpdateEventAsync(matchedEvent);

            return new CreateEventDto
            {
                
                Title = matchedEvent.Title,
                Location = matchedEvent.Location,
                Capacity = matchedEvent.Capacity,
                HostEmail = matchedEvent.Host.Email,
               
            };
        }

        public async Task<bool> DeleteEventAsync(int hostId,string title)
        {
            var events = await _eventRepository.GetEventByHostIdAsync(hostId);

            var matchedvents = events.FirstOrDefault(e => e.Title == title);
  
            if (matchedvents == null) return false;

            await _eventRepository.DeleteEventAsync(matchedvents);
            return true;
        }






    }

}
