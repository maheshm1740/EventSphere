import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { organizerApi } from '../../api/organizerApi';
import EventCard from '../../components/EventCard';

const MyEvents = () => {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchEvents();
  }, []);

  const fetchEvents = async () => {
    setLoading(true);
    setError('');
    try {
      const data = await organizerApi.getMyEvents();
      setEvents(data);
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to load events');
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteEvent = async (id) => {
    if (!window.confirm('Are you sure you want to delete this event?')) return;

    try {
      await organizerApi.deleteEvent(id);
      alert('Event deleted successfully');
      fetchEvents();
    } catch (err) {
      alert(err.response?.data?.message || 'Failed to delete event');
    }
  };

  if (loading) {
    return (
      <div className="container py-5 text-center">
        <div className="spinner-border text-primary" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      </div>
    );
  }

  return (
    <div className="container py-5">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1 className="fw-bold text-primary">My Events</h1>
        <Link to="/organizer/create-event" className="btn btn-accent">
          <i className="bi bi-plus-circle me-2"></i>
          Create Event
        </Link>
      </div>
      {error && <div className="alert alert-danger">{error}</div>}
      {events.length === 0 ? (
        <div className="text-center py-5">
          <i className="bi bi-calendar-x" style={{ fontSize: '4rem', color: '#ccc' }}></i>
          <p className="text-muted mt-3">You haven't created any events yet</p>
          <Link to="/organizer/create-event" className="btn btn-primary mt-3">
            Create Your First Event
          </Link>
        </div>
      ) : (
        <div className="row">
          {events.map((event) => (
            <div key={event.id} className="col-md-6 col-lg-4 mb-4">
              <div className="card event-card h-100 shadow-sm">
                <img
                  src={event.imageUrl || 'https://via.placeholder.com/400x250?text=Event+Image'}
                  className="card-img-top"
                  alt={event.title}
                  style={{ height: '200px', objectFit: 'cover' }}
                />
                <div className="card-body">
                  <h5 className="card-title text-primary fw-bold">{event.title}</h5>
                  <p className="card-text">
                    <span className="badge bg-secondary me-2">{event.category?.name || 'Uncategorized'}</span>
                  </p>
                  <p className="card-text">
                    <i className="bi bi-calendar-event text-accent me-2"></i>
                    {new Date(event.date).toLocaleDateString()}
                  </p>
                  <p className="card-text">
                    <i className="bi bi-geo-alt text-accent me-2"></i>
                    {event.location}
                  </p>
                  <div className="d-flex gap-2 mt-3">
                    <Link to={`/organizer/edit-event/${event.id}`} className="btn btn-sm btn-primary flex-fill">
                      <i className="bi bi-pencil"></i> Edit
                    </Link>
                    <Link to={`/organizer/event-attendees/${event.id}`} className="btn btn-sm btn-secondary flex-fill">
                      <i className="bi bi-people"></i> Attendees
                    </Link>
                    <button
                      className="btn btn-sm btn-danger"
                      onClick={() => handleDeleteEvent(event.id)}
                    >
                      <i className="bi bi-trash"></i>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default MyEvents;
