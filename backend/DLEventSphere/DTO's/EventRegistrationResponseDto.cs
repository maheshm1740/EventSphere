using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.DTO_s
{
    public class EventRegistrationResponseDto
    {
        public long RegistrationId { get; set; }
        public string RegistrationCode { get; set; }
        public long EventId { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
