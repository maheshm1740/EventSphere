using DLEventSphere.Abstract;
using DLEventSphere.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Repository
{
    public class EventRepo : IEventPartispantRepo
    {
        private readonly EventContext _context;

        public EventRepo(EventContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .AsNoTracking()
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
        }

        public async Task<EventRegistration?> GetExistingRegistration(long userId, long eventId)
        {
            return await _context.EventRegistrations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.EventId == eventId);
        }

        public async Task<EventRegistration> BookEventAsync(long userId, long eventId, string registrationCode)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var evnt = await _context.Events
                    .FirstOrDefaultAsync(e => e.EventId == eventId);

                if (evnt == null || evnt.AvailableSeats <= 0)
                    throw new InvalidOperationException("Event not found or no available seats.");

                if (await _context.EventRegistrations
                    .AnyAsync(r => r.UserId == userId && r.EventId == eventId))
                    throw new InvalidOperationException("User already registered for this event.");

                evnt.AvailableSeats--;

                var registration = new EventRegistration
                {
                    UserId = userId,
                    EventId = eventId,
                    RegisteredOn = DateTime.UtcNow,
                    RegistrationCode = registrationCode
                };

                _context.EventRegistrations.Add(registration);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return registration;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<List<Event>> GetUserRegisteredEvents(long userId)
        {
            return await _context.EventRegistrations
                .Where(r => r.UserId == userId)
                .Include(r => r.Event)
                    .ThenInclude(e => e.Category)
                .Include(r => r.Event)
                    .ThenInclude(e => e.Organizer)
                .Select(r => r.Event!)
                .AsNoTracking()
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
        }


        public async Task<bool> HasUserRegistered(long userId, long eventId)
        {
            return await _context.EventRegistrations
                .AnyAsync(r => r.UserId == userId && r.EventId == eventId);
        }

        public async Task<bool> HasUserGivenFeedback(long userId, long eventId)
        {
            return await _context.Feedbacks
                .AnyAsync(f => f.UserId == userId && f.EventId == eventId);
        }

        public async Task<Event?> GetEventByIdAsync(long eventId)
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<FeedBack> AddFeedbackAsync(FeedBack feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }
    }

}
