import { useState, useEffect } from 'react';
import { attendeeApi } from '../../api/attendeeApi';

const AttendeeDashboard = () => {
  const [stats, setStats] = useState({
    registered: 0,
    attended: 0,
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchStats();
  }, []);

  const fetchStats = async () => {
    setLoading(true);
    try {
      const [registered, attended] = await Promise.all([
        attendeeApi.getRegisteredEvents(),
        attendeeApi.getAttendedEvents(),
      ]);
      setStats({
        registered: registered.length,
        attended: attended.length,
      });
    } catch (err) {
      console.error('Failed to fetch stats:', err);
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
      <h1 className="fw-bold text-primary mb-4">Attendee Dashboard</h1>
      <div className="row">
        <div className="col-md-6 mb-4">
          <div className="card shadow-sm border-0 bg-primary text-white">
            <div className="card-body text-center p-4">
              <i className="bi bi-calendar-check" style={{ fontSize: '3rem' }}></i>
              <h2 className="fw-bold mt-3">{stats.registered}</h2>
              <p className="mb-0">Registered Events</p>
            </div>
          </div>
        </div>
        <div className="col-md-6 mb-4">
          <div className="card shadow-sm border-0 bg-secondary text-white">
            <div className="card-body text-center p-4">
              <i className="bi bi-calendar-event" style={{ fontSize: '3rem' }}></i>
              <h2 className="fw-bold mt-3">{stats.attended}</h2>
              <p className="mb-0">Attended Events</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AttendeeDashboard;
