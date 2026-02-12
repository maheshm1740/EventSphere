import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { organizerApi } from '../../api/organizerApi';
import { adminApi } from '../../api/adminApi';

const CreateEvent = () => {
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    categoryId: '',
    venue: '',
    date: '',
    location: '',
    imageUrl: '',
  });
  const [categories, setCategories] = useState([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const data = await adminApi.getCategories();
      setCategories(data);
    } catch (err) {
      console.error('Failed to fetch categories:', err);
    }
  };

  const validateForm = () => {
    if (!formData.title || !formData.description || !formData.categoryId || !formData.venue || !formData.date || !formData.location) {
      setError('All fields except image URL are required');
      return false;
    }

    // Validate future date
    const eventDate = new Date(formData.date);
    const now = new Date();
    if (eventDate <= now) {
      setError('Event date must be in the future');
      return false;
    }

    // Validate image URL if provided
    if (formData.imageUrl && !isValidUrl(formData.imageUrl)) {
      setError('Please enter a valid image URL');
      return false;
    }

    return true;
  };

  const isValidUrl = (url) => {
    try {
      new URL(url);
      return true;
    } catch {
      return false;
    }
  };

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!validateForm()) return;

    setLoading(true);
    try {
      await organizerApi.createEvent({
        ...formData,
        categoryId: parseInt(formData.categoryId),
      });
      alert('Event created successfully!');
      navigate('/organizer/my-events');
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to create event');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container py-5">
      <div className="row justify-content-center">
        <div className="col-md-10 col-lg-8">
          <div className="card shadow-lg border-0">
            <div className="card-body p-5">
              <h2 className="text-center fw-bold text-primary mb-4">Create New Event</h2>
              {error && <div className="alert alert-danger">{error}</div>}
              <form onSubmit={handleSubmit}>
                <div className="mb-3">
                  <label htmlFor="title" className="form-label fw-bold">Event Title</label>
                  <input
                    type="text"
                    className="form-control"
                    id="title"
                    name="title"
                    value={formData.title}
                    onChange={handleChange}
                    placeholder="Enter event title"
                    required
                  />
                </div>
                <div className="mb-3">
                  <label htmlFor="description" className="form-label fw-bold">Description</label>
                  <textarea
                    className="form-control"
                    id="description"
                    name="description"
                    rows="4"
                    value={formData.description}
                    onChange={handleChange}
                    placeholder="Enter event description"
                    required
                  ></textarea>
                </div>
                <div className="row">
                  <div className="col-md-6 mb-3">
                    <label htmlFor="categoryId" className="form-label fw-bold">Category</label>
                    <select
                      className="form-select"
                      id="categoryId"
                      name="categoryId"
                      value={formData.categoryId}
                      onChange={handleChange}
                      required
                    >
                      <option value="">Select a category</option>
                      {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                          {category.name}
                        </option>
                      ))}
                    </select>
                  </div>
                  <div className="col-md-6 mb-3">
                    <label htmlFor="venue" className="form-label fw-bold">Venue</label>
                    <input
                      type="text"
                      className="form-control"
                      id="venue"
                      name="venue"
                      value={formData.venue}
                      onChange={handleChange}
                      placeholder="Enter venue name"
                      required
                    />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-6 mb-3">
                    <label htmlFor="date" className="form-label fw-bold">Date & Time</label>
                    <input
                      type="datetime-local"
                      className="form-control"
                      id="date"
                      name="date"
                      value={formData.date}
                      onChange={handleChange}
                      required
                    />
                  </div>
                  <div className="col-md-6 mb-3">
                    <label htmlFor="location" className="form-label fw-bold">Location</label>
                    <input
                      type="text"
                      className="form-control"
                      id="location"
                      name="location"
                      value={formData.location}
                      onChange={handleChange}
                      placeholder="Enter location"
                      required
                    />
                  </div>
                </div>
                <div className="mb-3">
                  <label htmlFor="imageUrl" className="form-label fw-bold">Image URL (Optional)</label>
                  <input
                    type="url"
                    className="form-control"
                    id="imageUrl"
                    name="imageUrl"
                    value={formData.imageUrl}
                    onChange={handleChange}
                    placeholder="https://example.com/image.jpg"
                  />
                </div>
                <button
                  type="submit"
                  className="btn btn-primary w-100 py-2 fw-bold"
                  disabled={loading}
                >
                  {loading ? 'Creating...' : 'Create Event'}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreateEvent;
