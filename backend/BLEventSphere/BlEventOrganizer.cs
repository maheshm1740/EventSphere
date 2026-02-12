using AutoMapper;
using Azure.Core;
using DLEventSphere.Abstract;
using DLEventSphere.DTO_s;
using DLEventSphere.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEventSphere
{
    public class BlEventOrganizer
    {
        private readonly IEventOrganizerRepo _repo;
        private readonly IMapper _mapper;

        public BlEventOrganizer(IEventOrganizerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // ===================== GET ORGANIZER EVENTS =====================
        public async Task<ServiceResponse<IEnumerable<EventResponseDto>>> GetOrganizerEvents(long organizerId)
        {
            var response = new ServiceResponse<IEnumerable<EventResponseDto>>();

            try
            {
                var events = await _repo.GetOrganizerEventsAsync(organizerId);

                response.Data = _mapper.Map<IEnumerable<EventResponseDto>>(events);
                response.Message = response.Data.Any()
                    ? "Events retrieved successfully"
                    : "No events found for organizer";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error while fetching events: {ex.Message}";
                response.Data = Enumerable.Empty<EventResponseDto>();
            }

            return response;
        }

        // ===================== GET EVENT BY ID =====================
        public async Task<ServiceResponse<EventResponseDto>> GetEventById(long eventId)
        {
            var response = new ServiceResponse<EventResponseDto>();

            try
            {
                var evnt = await _repo.GetEventByIdAsync(eventId);

                if (evnt == null)
                {
                    response.Success = false;
                    response.Message = "Event not found";
                    return response;
                }

                response.Data = _mapper.Map<EventResponseDto>(evnt);
                response.Message = "Event fetched successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error while fetching event: {ex.Message}";
            }

            return response;
        }

        // ===================== CREATE EVENT =====================
        public async Task<ServiceResponse<EventResponseDto>> RegisterEvent(
    CreateEventDto eventDetails,
    string? imageUrl,
    long organizerId)
        {
            var response = new ServiceResponse<EventResponseDto>();

            try
            {
                if (eventDetails == null)
                    throw new ArgumentException("Invalid event details provided.");

                bool isBooked = await _repo.IsBuildingBooked(
                    eventDetails.Location ?? string.Empty,
                    eventDetails.BuildingName ?? string.Empty,
                    eventDetails.BuildingNumber ?? string.Empty,
                    eventDetails.StartDate,
                    eventDetails.EndDate,
                    null);

                if (isBooked)
                    throw new InvalidOperationException(
                        "The selected location or hall is already booked for the given date and time.");

                var newEvent = _mapper.Map<Event>(eventDetails);

                newEvent.OrganizerId = organizerId;
                newEvent.ImageUrl = imageUrl;
                newEvent.CreatedAt = DateTime.UtcNow;

                var createdEvent = await _repo.CreateEventAsync(newEvent);

                response.Data = _mapper.Map<EventResponseDto>(createdEvent);
                response.Message = "Event registered successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = ex.Message;
            }

            return response;
        }



        // ===================== UPDATE EVENT =====================
        public async Task<ServiceResponse<EventResponseDto>> UpdateEvent(
            long eventId,
            UpdateEvent updateEvent)
        {
            var response = new ServiceResponse<EventResponseDto>();

            try
            {
                if (updateEvent == null)
                    throw new ArgumentException("Invalid event details.");

                var existingEvent = await _repo.GetEventByIdAsync(eventId)
                    ?? throw new InvalidOperationException("Event not found.");

                bool isBooked = await _repo.IsBuildingBooked(
                    updateEvent.Location ?? string.Empty,
                    updateEvent.BuildingName ?? string.Empty,
                    updateEvent.BuildingNumber ?? string.Empty,
                    updateEvent.StartDate,
                    updateEvent.EndDate,
                    eventId);

                if (isBooked)
                    throw new InvalidOperationException(
                        "Already booked for the selected date and time.");

                _mapper.Map(updateEvent, existingEvent);
                existingEvent.UpdatedAt = DateTime.UtcNow;

                var updatedEvent = await _repo.UpdateEventAsync(eventId, existingEvent);

                response.Data = _mapper.Map<EventResponseDto>(updatedEvent);
                response.Message = "Event updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ===================== DELETE EVENT =====================
        public async Task<ServiceResponse<bool>> DeleteEvent(
    long eventId,
    long organizerId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var ev = await _repo.GetEventByIdAsync(eventId);

                if (ev == null)
                    throw new KeyNotFoundException("Event not found.");

                if (ev.OrganizerId != organizerId)
                    throw new UnauthorizedAccessException(
                        "You are not allowed to delete this event.");

                var deleted = await _repo.DeleteEventAsync(eventId);

                if (!deleted)
                    throw new InvalidOperationException("Failed to delete event.");

                response.Data = true;
                response.Message = "Event deleted successfully.";
            }
            catch (UnauthorizedAccessException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }

}
