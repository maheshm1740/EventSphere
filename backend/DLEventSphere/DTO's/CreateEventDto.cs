using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.DTO_s
{
    public class CreateEventDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(5000)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [MaxLength(255)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? BuildingName { get; set; }

        [MaxLength(50)]
        public string? BuildingNumber { get; set; }
        public int AvailableSeats { get; set; }
        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
