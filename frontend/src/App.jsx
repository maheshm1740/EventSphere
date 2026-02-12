import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import Navbar from './components/Navbar';
import ProtectedRoute from './routes/ProtectedRoute';

// Public Pages
import Home from './pages/public/Home';
import Login from './pages/public/Login';
import Register from './pages/public/Register';
import Events from './pages/public/Events';
import AccessDenied from './pages/public/AccessDenied';

// Admin Pages
import AdminDashboard from './pages/admin/AdminDashboard';
import Categories from './pages/admin/Categories';
import AdminEvents from './pages/admin/AdminEvents';
import Users from './pages/admin/Users';
import AddOrganizer from './pages/admin/AddOrganizer';

// Organizer Pages
import OrganizerDashboard from './pages/organizer/OrganizerDashboard';
import MyEvents from './pages/organizer/MyEvents';
import CreateEvent from './pages/organizer/CreateEvent';
import EditEvent from './pages/organizer/EditEvent';
import EventAttendees from './pages/organizer/EventAttendees';

// Attendee Pages
import AttendeeDashboard from './pages/attendee/AttendeeDashboard';
import RegisteredEvents from './pages/attendee/RegisteredEvents';
import AttendedEvents from './pages/attendee/AttendedEvents';

function AppRoutes() {
  const { isAuthenticated, user } = useAuth();

  return (
    <Routes>
      {/* Public Routes */}
      <Route path="/" element={<Home />} />
      <Route path="/login" element={isAuthenticated ? <Navigate to={getRoleHomePage(user?.role)} replace /> : <Login />} />
      <Route path="/register" element={isAuthenticated ? <Navigate to={getRoleHomePage(user?.role)} replace /> : <Register />} />
      <Route path="/events" element={<Events />} />
      <Route path="/access-denied" element={<AccessDenied />} />

      {/* Admin Routes */}
      <Route
        path="/admin/dashboard"
        element={
          <ProtectedRoute allowedRoles={['ADMIN']}>
            <AdminDashboard />
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/categories"
        element={
          <ProtectedRoute allowedRoles={['ADMIN']}>
            <Categories />
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/events"
        element={
          <ProtectedRoute allowedRoles={['ADMIN']}>
            <AdminEvents />
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/users"
        element={
          <ProtectedRoute allowedRoles={['ADMIN']}>
            <Users />
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/add-organizer"
        element={
          <ProtectedRoute allowedRoles={['ADMIN']}>
            <AddOrganizer />
          </ProtectedRoute>
        }
      />

      {/* Organizer Routes */}
      <Route
        path="/organizer/dashboard"
        element={
          <ProtectedRoute allowedRoles={['ORGANIZER']}>
            <OrganizerDashboard />
          </ProtectedRoute>
        }
      />
      <Route
        path="/organizer/my-events"
        element={
          <ProtectedRoute allowedRoles={['ORGANIZER']}>
            <MyEvents />
          </ProtectedRoute>
        }
      />
      <Route
        path="/organizer/create-event"
        element={
          <ProtectedRoute allowedRoles={['ORGANIZER']}>
            <CreateEvent />
          </ProtectedRoute>
        }
      />
      <Route
        path="/organizer/edit-event/:id"
        element={
          <ProtectedRoute allowedRoles={['ORGANIZER']}>
            <EditEvent />
          </ProtectedRoute>
        }
      />
      <Route
        path="/organizer/event-attendees/:id"
        element={
          <ProtectedRoute allowedRoles={['ORGANIZER']}>
            <EventAttendees />
          </ProtectedRoute>
        }
      />

      {/* Attendee Routes */}
      <Route
        path="/attendee/dashboard"
        element={
          <ProtectedRoute allowedRoles={['ATTENDEE']}>
            <AttendeeDashboard />
          </ProtectedRoute>
        }
      />
      <Route
        path="/attendee/registered"
        element={
          <ProtectedRoute allowedRoles={['ATTENDEE']}>
            <RegisteredEvents />
          </ProtectedRoute>
        }
      />
      <Route
        path="/attendee/attended"
        element={
          <ProtectedRoute allowedRoles={['ATTENDEE']}>
            <AttendedEvents />
          </ProtectedRoute>
        }
      />

      {/* Fallback */}
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}

function getRoleHomePage(role) {
  switch (role) {
    case 'ADMIN':
      return '/admin/dashboard';
    case 'ORGANIZER':
      return '/organizer/my-events';
    case 'ATTENDEE':
      return '/events';
    default:
      return '/';
  }
}

function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="app">
          <Navbar />
          <AppRoutes />
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;
