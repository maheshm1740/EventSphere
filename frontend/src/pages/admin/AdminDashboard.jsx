import { useState, useEffect } from 'react';
import { adminApi } from '../../api/adminApi';

const AdminDashboard = () => {
  const [stats, setStats] = useState({
    totalEvents: 0,
    totalCategories: 0,
    totalUsers: 0,
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchStats();
  }, []);

  const fetchStats = async () => {
    setLoading(true);
    try {
      const [events, categories, users] = await Promise.all([
        adminApi.getEvents(),
        adminApi.getCategories(),
        adminApi.getUsers(),
      ]);
      setStats({
        totalEvents: events.length,
        totalCategories: categories.length,
        totalUsers: users.length,
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
      <h1 className="fw-bold text-primary mb-4">Admin Dashboard</h1>
      <div className="row">
        <div className="col-md-4 mb-4">
          <div className="card shadow-sm border-0 bg-primary text-white">
            <div className="card-body text-center p-4">
              <i className="bi bi-calendar-event" style={{ fontSize: '3rem' }}></i>
              <h2 className="fw-bold mt-3">{stats.totalEvents}</h2>
              <p className="mb-0">Total Events</p>
            </div>
          </div>
        </div>
        <div className="col-md-4 mb-4">
          <div className="card shadow-sm border-0 bg-secondary text-white">
            <div className="card-body text-center p-4">
              <i className="bi bi-tags" style={{ fontSize: '3rem' }}></i>
              <h2 className="fw-bold mt-3">{stats.totalCategories}</h2>
              <p className="mb-0">Total Categories</p>
            </div>
          </div>
        </div>
        <div className="col-md-4 mb-4">
          <div className="card shadow-sm border-0 bg-accent text-white">
            <div className="card-body text-center p-4">
              <i className="bi bi-people" style={{ fontSize: '3rem' }}></i>
              <h2 className="fw-bold mt-3">{stats.totalUsers}</h2>
              <p className="mb-0">Total Users</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
