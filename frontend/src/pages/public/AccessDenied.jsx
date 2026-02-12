import { Link } from 'react-router-dom';

const AccessDenied = () => {
  return (
    <div className="container py-5">
      <div className="row justify-content-center">
        <div className="col-md-6 text-center">
          <div className="card shadow-lg border-0 p-5">
            <i className="bi bi-shield-exclamation text-danger" style={{ fontSize: '5rem' }}></i>
            <h1 className="fw-bold text-danger mt-4">Access Denied</h1>
            <p className="text-muted mt-3">
              You do not have permission to access this page.
            </p>
            <Link to="/" className="btn btn-primary mt-3">
              Go to Home
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AccessDenied;
