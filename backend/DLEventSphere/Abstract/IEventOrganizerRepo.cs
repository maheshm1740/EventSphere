using DLEventSphere.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Abstract
{
    public interface IEventOrganizerRepo
    {
        Task<Event> CreateEventAsync(Event newEvent);
        Task<Event?> UpdateEventAsync(long eventId, Event updatedEvent);
         Task<Event?> GetEventByIdAsync(long eventId);
        Task<bool> DeleteEventAsync(long eventId);
        Task<IEnumerable<Event>> GetOrganizerEventsAsync(long organizerId);
        Task<bool> IsBuildingBooked(
            string location,
            string buildingName,
            string buildingNumber,
            DateTime startDate,
            DateTime endDate,
            long? eventIdToExclude = null);
    }
}
