import { Link } from "react-router-dom";

const Home = () => {
  return (
    <>
      {/* ===== Inline Styles (FIXED HERE) ===== */}
      <style>{`
        /* Hero Section */
        .hero-section {
          background: linear-gradient(135deg, #4f46e5, #7c3aed);
          color: #ffffff;
        }

        .hero-section h1,
        .hero-section p {
          color: #ffffff !important;
        }

        /* Accent Button */
        .btn-accent {
          background-color: #22c55e;
          color: #ffffff;
          border: none;
        }

        .btn-accent:hover,
        .btn-accent:focus,
        .btn-accent:active {
          background-color: #16a34a;
          color: #ffffff;
        }

        /* Fix ALL button states */
        .btn,
        .btn:hover,
        .btn:focus,
        .btn:active {
          text-decoration: none;
          color: inherit;
        }

        /* Outline light button fix */
        .btn-outline-light {
          color: #ffffff;
          border-color: #ffffff;
        }

        .btn-outline-light:hover,
        .btn-outline-light:focus,
        .btn-outline-light:active {
          background-color: #ffffff;
          color: #000000 !important;
        }

        /* Focus visibility (Accessible) */
        .btn:focus-visible {
          outline: 3px solid rgba(255,255,255,0.6);
          outline-offset: 2px;
        }

        /* Text selection fix */
        ::selection {
          background-color: #4f46e5;
          color: #ffffff;
        }

        /* Feature icon colors */
        .bg-accent {
          background-color: #22c55e !important;
        }
      `}</style>

      {/* ===== Page Content ===== */}
      <div>
        {/* Hero Section */}
        <div className="hero-section py-5">
          <div className="container text-center py-5">
            <h1 className="display-3 fw-bold mb-4">
              Welcome to EventHub
            </h1>
            <p className="lead mb-4">
              Discover, Create, and Manage Amazing Events
            </p>
            <div>
              <Link to="/events" className="btn btn-accent btn-lg me-3">
                Browse Events
              </Link>
              <Link to="/register" className="btn btn-outline-light btn-lg">
                Get Started
              </Link>
            </div>
          </div>
        </div>

        {/* Features Section */}
        <div className="container my-5">
          <div className="row text-center">
            <div className="col-md-4 mb-4">
              <div className="card h-100 shadow-sm border-0">
                <div className="card-body">
                  <div
                    className="bg-primary text-white rounded-circle mx-auto mb-3 d-flex align-items-center justify-content-center"
                    style={{ width: "80px", height: "80px" }}
                  >
                    <i className="bi bi-calendar-check fs-2"></i>
                  </div>
                  <h4 className="fw-bold text-primary">
                    Discover Events
                  </h4>
                  <p className="text-muted">
                    Browse through a wide variety of events tailored to your interests
                  </p>
                </div>
              </div>
            </div>

            <div className="col-md-4 mb-4">
              <div className="card h-100 shadow-sm border-0">
                <div className="card-body">
                  <div
                    className="bg-secondary text-white rounded-circle mx-auto mb-3 d-flex align-items-center justify-content-center"
                    style={{ width: "80px", height: "80px" }}
                  >
                    <i className="bi bi-people fs-2"></i>
                  </div>
                  <h4 className="fw-bold text-primary">
                    Easy Registration
                  </h4>
                  <p className="text-muted">
                    Register for events with just a few clicks and manage your schedule
                  </p>
                </div>
              </div>
            </div>

            <div className="col-md-4 mb-4">
              <div className="card h-100 shadow-sm border-0">
                <div className="card-body">
                  <div
                    className="bg-accent text-white rounded-circle mx-auto mb-3 d-flex align-items-center justify-content-center"
                    style={{ width: "80px", height: "80px" }}
                  >
                    <i className="bi bi-graph-up fs-2"></i>
                  </div>
                  <h4 className="fw-bold text-primary">
                    Organize Events
                  </h4>
                  <p className="text-muted">
                    Create and manage your own events with powerful tools
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* CTA Section */}
        <div className="bg-light py-5">
          <div className="container text-center">
            <h2 className="fw-bold text-primary mb-4">
              Ready to Get Started?
            </h2>
            <p className="lead text-muted mb-4">
              Join thousands of event enthusiasts today
            </p>
            <Link to="/register" className="btn btn-primary btn-lg">
              Create Your Account
            </Link>
          </div>
        </div>
      </div>
    </>
  );
};

export default Home;
