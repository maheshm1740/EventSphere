using DLEventSphere.Abstract;
using DLEventSphere.DTO_s;
using DLEventSphere.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLEventSphere
{
    public class BlEvent
    {
        private readonly IEventPartispantRepo _repo;

        public BlEvent(IEventPartispantRepo repo)
        {
            _repo = repo;
        }

        // ===================== GET ALL EVENTS =====================
        public async Task<ServiceResponse<IEnumerable<EventResponseDto>>> GetAllEvents()
        {
            var response = new ServiceResponse<IEnumerable<EventResponseDto>>();

            try
            {
                var events = await _repo.GetAllEventsAsync();

                response.Data = events.Select(e => new EventResponseDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Location = e.Location,
                    BuildingName = e.BuildingName,
                    BuildingNumber = e.BuildingNumber,
                    AvailableSeats = e.AvailableSeats,
                    ImageUrl = e.ImageUrl,

                    CategoryId = e.Category.CategoryId,
                    CategoryName = e.Category.Name,

                    OrganizerId = e.Organizer.UserId,
                    OrganizerName = e.Organizer.Name
                }).ToList();

                response.Message = response.Data.Any()
                    ? "Events retrieved successfully."
                    : "No events found.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error while fetching events: {ex.Message}";
                response.Data = Enumerable.Empty<EventResponseDto>();
            }

            return response;
        }


        // ===================== REGISTER FOR EVENT =====================
        public async Task<ServiceResponse<EventRegistrationResponseDto>> RegisterForEvent(
            long userId,
            long eventId)
        {
            try
            {
                var registrationCode = GenerateRegistrationCode();

                var registration = await _repo.BookEventAsync(
                    userId,
                    eventId,
                    registrationCode
                );

                return new ServiceResponse<EventRegistrationResponseDto>
                {
                    Message = "Event registered successfully.",
                    Data = new EventRegistrationResponseDto
                    {
                        RegistrationId = registration.RegistrationId,
                        RegistrationCode = registration.RegistrationCode,
                        EventId = registration.EventId,
                        RegisteredOn = registration.RegisteredOn
                    }
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EventRegistrationResponseDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // ===================== USER REGISTERED EVENTS =====================
        public async Task<ServiceResponse<IEnumerable<Event>>> GetUserRegisteredEvents(long userId)
        {
            var response = new ServiceResponse<IEnumerable<Event>>();

            try
            {
                var events = await _repo.GetUserRegisteredEvents(userId);

                response.Data = events;
                response.Message = events.Any()
                    ? "Registered events fetched successfully."
                    : "User has not registered for any events.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to fetch registered events: {ex.Message}";
                response.Data = Enumerable.Empty<Event>();
            }

            return response;
        }

        // ===================== ADD FEEDBACK =====================
        public async Task<ServiceResponse<string>> AddFeedback(
            long userId,
            long eventId,
            int rating,
            string? comment)
        {
            try
            {
                var evnt = await _repo.GetEventByIdAsync(eventId);

                if (evnt == null)
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Event not found."
                    };

                if (DateTime.UtcNow < evnt.EndDate)
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Feedback can be given only after event completion."
                    };

                if (!await _repo.HasUserRegistered(userId, eventId))
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Only registered users can give feedback."
                    };

                if (await _repo.HasUserGivenFeedback(userId, eventId))
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Feedback already submitted."
                    };

                await _repo.AddFeedbackAsync(new FeedBack
                {
                    UserId = userId,
                    EventId = eventId,
                    Rating = rating,
                    Comment = comment
                });

                return new ServiceResponse<string>
                {
                    Message = "Feedback submitted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // ===================== REGISTRATION CODE GENERATOR =====================
        private string GenerateRegistrationCode()
        {
            return $"REG-{Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("/", "")
                .Replace("+", "")
                .Substring(0, 12)
                .ToUpper()}";
        }
    }
}
