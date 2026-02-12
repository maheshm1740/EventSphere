import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Navbar = () => {
  const { user, isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary sticky-top shadow">
      <div className="container">
        <Link className="navbar-brand fw-bold" to="/">
          <i className="bi bi-calendar-event me-2"></i>
          EventHub
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto">
            {!isAuthenticated && (
              <>
                <li className="nav-item">
                  <Link className="nav-link" to="/">Home</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/events">Events</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/login">Login</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link btn btn-accent text-white ms-2" to="/register">Register</Link>
                </li>
              </>
            )}

            {isAuthenticated && user.role === 'ADMIN' && (
              <>
                <li className="nav-item">
                  <Link className="nav-link" to="/admin/dashboard">Dashboard</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/admin/categories">Categories</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/admin/events">Events</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/admin/users">Users</Link>
                </li>
                <li className="nav-item">
                  <button className="nav-link btn btn-link text-white" onClick={handleLogout}>
                    Logout
                  </button>
                </li>
              </>
            )}

            {isAuthenticated && user.role === 'ORGANIZER' && (
              <>
                <li className="nav-item">
                  <Link className="nav-link" to="/organizer/my-events">My Events</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/organizer/create-event">Create Event</Link>
                </li>
                <li className="nav-item">
                  <button className="nav-link btn btn-link text-white" onClick={handleLogout}>
                    Logout
                  </button>
                </li>
              </>
            )}

            {isAuthenticated && user.role === 'ATTENDEE' && (
              <>
                <li className="nav-item">
                  <Link className="nav-link" to="/events">Events</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/attendee/registered">My Registrations</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/attendee/attended">Attended Events</Link>
                </li>
                <li className="nav-item">
                  <button className="nav-link btn btn-link text-white" onClick={handleLogout}>
                    Logout
                  </button>
                </li>
              </>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
