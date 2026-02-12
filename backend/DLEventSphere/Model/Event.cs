using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Model
{
    public class Event
    {
        [Key]
        public long EventId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [MaxLength(255)]
        public string? Location { get; set; }
        
        [MaxLength(100)]
        public string? BuildingName { get; set; } 

        [MaxLength(50)]
        public string? BuildingNumber { get; set; }
        public int AvailableSeats { get; set; }

        public string? ImageUrl { get; set; }

        [ForeignKey("Organizer")]
        public long OrganizerId { get; set; }
        public User? Organizer { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<EventRegistration>? Registrations { get; set; }
        public ICollection<FeedBack>? Feedbacks { get; set; }
    }
}
