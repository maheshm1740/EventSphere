public class EventResponseDto
{
    public long EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Location { get; set; }
    public string BuildingName { get; set; }
    public string BuildingNumber { get; set; }
    public int AvailableSeats { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public long OrganizerId { get; set; }
    public string OrganizerName { get; set; }
}
