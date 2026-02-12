using DLEventSphere.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Abstract
{
    public interface IEventPartispantRepo
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<EventRegistration?> GetExistingRegistration(long userId, long eventId);
        Task<EventRegistration> BookEventAsync(long userId, long eventId, string registrationCode);
        Task<List<Event>> GetUserRegisteredEvents(long userId);
        Task<bool> HasUserRegistered(long userId, long eventId);
        Task<bool> HasUserGivenFeedback(long userId, long eventId);
        Task<Event?> GetEventByIdAsync(long eventId);
        Task<FeedBack> AddFeedbackAsync(FeedBack feedback);
    }
}
