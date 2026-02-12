    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Model
{
    public class FeedBack
    {
        [Key]
        public long FeedbackId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [ForeignKey("Event")]
        public long EventId { get; set; }
        public Event? Event { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User? User { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}
