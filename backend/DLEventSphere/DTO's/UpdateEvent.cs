using System;
using System.ComponentModel.DataAnnotations;

namespace DLEventSphere.DTO_s
{
    public class UpdateEvent
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [MaxLength(255, ErrorMessage = "Location cannot exceed 255 characters.")]
        public string? Location { get; set; }

        [MaxLength(100, ErrorMessage = "Building name cannot exceed 100 characters.")]
        public string? BuildingName { get; set; }

        [MaxLength(50, ErrorMessage = "Building number cannot exceed 50 characters.")]
        public string? BuildingNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Available seats must be greater than 0.")]
        public int AvailableSeats { get; set; }
    }
}
