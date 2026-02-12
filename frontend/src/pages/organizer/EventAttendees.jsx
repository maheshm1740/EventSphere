import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { organizerApi } from '../../api/organizerApi';

const EventAttendees = () => {
  const { id } = useParams();
  const [attendees, setAttendees] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchAttendees();
  }, [id]);

  const fetchAttendees = async () => {
    setLoading(true);
    setError('');
    try {
      const data = await organizerApi.getEventAttendees(id);
      setAttendees(data);
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to load attendees');
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
      <h1 className="fw-bold text-primary mb-4">Event Attendees</h1>
      {error && <div className="alert alert-danger">{error}</div>}
      <div className="card shadow-sm">
        <div className="card-body">
          {attendees.length === 0 ? (
            <p className="text-muted">No attendees registered yet</p>
          ) : (
            <div className="table-responsive">
              <table className="table table-hover">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Registration Date</th>
                  </tr>
                </thead>
                <tbody>
                  {attendees.map((attendee) => (
                    <tr key={attendee.id}>
                      <td>{attendee.id}</td>
                      <td>{attendee.name}</td>
                      <td>{attendee.email}</td>
                      <td>
                        {attendee.registrationDate
                          ? new Date(attendee.registrationDate).toLocaleDateString()
                          : 'N/A'}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default EventAttendees;
