import { useState, useEffect } from 'react';
import { organizerApi } from '../../api/organizerApi';
import { Link } from 'react-router-dom';

const OrganizerDashboard = () => {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchEvents();
  }, []);

  const fetchEvents = async () => {
    setLoading(true);
    try {
      const data = await organizerApi.getMyEvents();
      setEvents(data);
    } catch (err) {
      console.error('Failed to fetch events:', err);
    } finally {
      setLoading(false);
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
      <h1 className="fw-bold text-primary mb-4">Organizer Dashboard</h1>
      <div className="row">
        <div className="col-md-4 mb-4">
          <div className="card shadow-sm border-0 bg-primary text-white">
            <div className="card-body text-center p-4">
              <i className="bi bi-calendar-event" style={{ fontSize: '3rem' }}></i>
              <h2 className="fw-bold mt-3">{events.length}</h2>
              <p className="mb-0">My Events</p>
            </div>
          </div>
        </div>
      </div>
      <div className="mt-4">
        <Link to="/organizer/create-event" className="btn btn-accent btn-lg">
          <i className="bi bi-plus-circle me-2"></i>
          Create New Event
        </Link>
      </div>
    </div>
  );
};

export default OrganizerDashboard;
