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
    public class EventOrganizerRepo : IEventOrganizerRepo
    {
        private readonly EventContext _context;

        public EventOrganizerRepo(EventContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .FirstAsync(e => e.EventId == newEvent.EventId);
        }

        public async Task<Event?> UpdateEventAsync(long eventId, Event updatedEvent)
        {
            await _context.SaveChangesAsync();

            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .FirstAsync(e => e.EventId == eventId);
        }

        public async Task<Event?> GetEventByIdAsync(long eventId)
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<bool> DeleteEventAsync(long eventId)
        {
            var existing = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (existing == null)
                return false;

            if (existing.Registrations != null && existing.Registrations.Any())
                throw new InvalidOperationException(
                    "Cannot delete event with existing registrations.");

            _context.Events.Remove(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Event>> GetOrganizerEventsAsync(long organizerId)
        {
            return await _context.Events
                .Where(e => e.OrganizerId == organizerId)
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .ToListAsync();
        }

        public async Task<bool> IsBuildingBooked(
            string location,
            string buildingName,
            string buildingNumber,
            DateTime startDate,
            DateTime endDate,
            long? eventIdToExclude = null)
        {
            return await _context.Events.AnyAsync(e =>
                e.Location == location &&
                e.BuildingName == buildingName &&
                e.BuildingNumber == buildingNumber &&
                e.StartDate < endDate &&
                e.EndDate > startDate &&
                (eventIdToExclude == null || e.EventId != eventIdToExclude)
            );
        }
    }
}
