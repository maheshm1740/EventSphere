const EventCard = ({ event, onClick }) => {
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  };

  return (
    <div className="col-md-6 col-lg-4 mb-4">
      <div className="card event-card h-100 shadow-sm" onClick={onClick} style={{ cursor: 'pointer' }}>
        <img
          src={event.imageUrl || 'https://via.placeholder.com/400x250?text=Event+Image'}
          className="card-img-top"
          alt={event.title}
          style={{ height: '200px', objectFit: 'cover' }}
        />
        <div className="card-body">
          <h5 className="card-title text-primary fw-bold">{event.title}</h5>
          <p className="card-text">
            <span className="badge bg-secondary me-2">{event.categoryName || 'Uncategorized'}</span>
          </p>
          <p className="card-text">
            <i className="bi bi-calendar-event text-accent me-2"></i>
            {formatDate(event.startDate)}
          </p>
          <p className="card-text">
            <i className="bi bi-geo-alt text-accent me-2"></i>
            {event.location}
          </p>
        </div>
      </div>
    </div>
  );
};

export default EventCard;
