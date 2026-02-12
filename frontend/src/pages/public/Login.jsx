import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { authApi } from '../../api/authApi';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const { login, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  // Clear error if user becomes authenticated
  useEffect(() => {
    if (isAuthenticated) {
      setError('');
    }
  }, [isAuthenticated]);

  const validateForm = () => {
    if (!email || !password) {
      setError('All fields are required');
      return false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      setError('Please enter a valid email address');
      return false;
    }

    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!validateForm()) return;

    setLoading(true);

    try {
      const response = await authApi.login(email, password);
      const { token, user } = response

      // Store auth details
      login(
        token,
        user.role.toUpperCase(),
        user.userId
      );

      setError(''); // ✅ CLEAR ERROR AFTER SUCCESS

      // Role-based navigation
      switch (user.role.toUpperCase()) {
        case 'ADMIN':
          navigate('/admin/dashboard');
          break;
        case 'ORGANIZER':
          navigate('/organizer/my-events');
          break;
        case 'ATTENDEE':
          navigate('/events');
          break;
        default:
          navigate('/');
      }
    } catch (err) {
      console.error('Login Error:', err);
      setError(
        err.response?.data?.message ||
        err.response?.data?.error ||
        'Invalid email or password'
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container py-5">
      <div className="row justify-content-center">
        <div className="col-md-6 col-lg-5">
          <div className="card shadow-lg border-0">
            <div className="card-body p-5">
              <h2 className="text-center fw-bold text-primary mb-4">
                Login
              </h2>

              {error && (
                <div className="alert alert-danger" role="alert">
                  {error}
                </div>
              )}

              <form onSubmit={handleSubmit}>
                <div className="mb-3">
                  <label className="form-label fw-bold">
                    Email
                  </label>
                  <input
                    type="email"
                    className="form-control"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="Enter your email"
                    required
                  />
                </div>

                <div className="mb-3">
                  <label className="form-label fw-bold">
                    Password
                  </label>
                  <input
                    type="password"
                    className="form-control"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="Enter your password"
                    required
                  />
                </div>

                <button
                  type="submit"
                  className="btn btn-primary w-100 py-2 fw-bold"
                  disabled={loading}
                >
                  {loading ? 'Logging in...' : 'Login'}
                </button>
              </form>

              <div className="text-center mt-3">
                <p className="text-muted">
                  Don't have an account?{' '}
                  <Link to="/register" className="fw-bold">
                    Register here
                  </Link>
                </p>
              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
