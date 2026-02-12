using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Model
{
    [Table(name:"Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required (ErrorMessage = "Select role")]
        public Role role { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Event>? CreatedEvents { get; set; }
        public ICollection<EventRegistration>? Registrations { get; set; }
        public ICollection<FeedBack>? Feedbacks { get; set; }
    }

    public enum Role
    {
        Admin, Organizer, Attendee
    }
}
