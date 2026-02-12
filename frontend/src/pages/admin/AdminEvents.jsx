import { useState, useEffect } from 'react';
import { adminApi } from '../../api/adminApi';
import EventCard from '../../components/EventCard';
import EventModal from '../../components/EventModal';

const AdminEvents = () => {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    fetchEvents();
  }, []);

  const fetchEvents = async () => {
    setLoading(true);
    setError('');
    try {
      const data = await adminApi.getEvents();
      setEvents(data);
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to load events');
    } finally {
      setLoading(false);
    }
  };

  const handleEventClick = (event) => {
    setSelectedEvent(event);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedEvent(null);
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
      <h1 className="fw-bold text-primary mb-4">All Events</h1>
      {error && <div className="alert alert-danger">{error}</div>}
      {events.length === 0 ? (
        <div className="text-center py-5">
          <i className="bi bi-calendar-x" style={{ fontSize: '4rem', color: '#ccc' }}></i>
          <p className="text-muted mt-3">No events available</p>
        </div>
      ) : (
        <div className="row">
          {events.map((event) => (
            <EventCard key={event.id} event={event} onClick={() => handleEventClick(event)} />
          ))}
        </div>
      )}
      {selectedEvent && (
        <EventModal
          event={selectedEvent}
          show={showModal}
          onHide={handleCloseModal}
          isRegistered={false}
        />
      )}
    </div>
  );
};

export default AdminEvents;
