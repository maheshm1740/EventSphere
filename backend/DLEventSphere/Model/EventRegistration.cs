using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Model
{
    public class EventRegistration
    {
        [Key]
        public long RegistrationId { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Event")]
        public long EventId { get; set; }
        public Event? Event { get; set; }
        public DateTime RegisteredOn { get; set; }

        [Required]
        [MaxLength(100)]
        public string RegistrationCode { get; set; }
        
    }
}
