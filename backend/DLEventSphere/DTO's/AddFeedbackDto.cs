using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.DTO_s
{
    public class AddFeedbackDto
    {

        [Required(ErrorMessage = "UserId is required.")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "EventId is required.")]
        public long EventId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string? Comment { get; set; }
    }
}
