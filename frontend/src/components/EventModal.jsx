import { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { attendeeApi } from '../api/attendeeApi';

const EventModal = ({ event, show, onHide, isRegistered, onRegisterSuccess }) => {
  const { user, isAuthenticated } = useAuth();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const formatDateTime = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  const handleRegister = async () => {
    setLoading(true);
    setError('');
    try {
      await attendeeApi.registerForEvent(event.id);
      if (onRegisterSuccess) {
        onRegisterSuccess();
      }
      alert('Successfully registered for the event!');
      onHide();
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to register for event');
    } finally {
      setLoading(false);
    }
  };

  if (!event) return null;

  return (
    <div className={`modal fade ${show ? 'show d-block' : ''}`} tabIndex="-1" style={{ backgroundColor: show ? 'rgba(0,0,0,0.5)' : 'transparent' }}>
      <div className="modal-dialog modal-lg modal-dialog-centered">
        <div className="modal-content">
          <div className="modal-header bg-primary text-white">
            <h5 className="modal-title fw-bold">{event.title}</h5>
            <button type="button" className="btn-close btn-close-white" onClick={onHide}></button>
          </div>
          <div className="modal-body">
            <img
              src={event.imageUrl || 'https://via.placeholder.com/800x400?text=Event+Image'}
              className="img-fluid rounded mb-3"
              alt={event.title}
              style={{ width: '100%', maxHeight: '300px', objectFit: 'cover' }}
            />
            <div className="mb-3">
              <h6 className="text-primary fw-bold">Description</h6>
              <p>{event.description}</p>
            </div>
            <div className="row">
              <div className="col-md-6 mb-3">
                <h6 className="text-primary fw-bold">Category</h6>
                <span className="badge bg-secondary">{event.category?.name || 'Uncategorized'}</span>
              </div>
              <div className="col-md-6 mb-3">
                <h6 className="text-primary fw-bold">Organizer</h6>
                <p>{event.organizer?.name || 'N/A'}</p>
              </div>
            </div>
            <div className="row">
              <div className="col-md-6 mb-3">
                <h6 className="text-primary fw-bold">Venue</h6>
                <p>{event.venue}</p>
              </div>
              <div className="col-md-6 mb-3">
                <h6 className="text-primary fw-bold">Date & Time</h6>
                <p>{formatDateTime(event.date)}</p>
              </div>
            </div>
            <div className="mb-3">
              <h6 className="text-primary fw-bold">Location</h6>
              <p><i className="bi bi-geo-alt text-accent me-2"></i>{event.location}</p>
            </div>
            {error && (
              <div className="alert alert-danger" role="alert">
                {error}
              </div>
            )}
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={onHide}>
              Close
            </button>
            {isAuthenticated && user?.role === 'ATTENDEE' && (
              <button
                type="button"
                className="btn btn-accent"
                onClick={handleRegister}
                disabled={loading || isRegistered}
              >
                {loading ? 'Registering...' : isRegistered ? 'Already Registered' : 'Register for Event'}
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventModal;
