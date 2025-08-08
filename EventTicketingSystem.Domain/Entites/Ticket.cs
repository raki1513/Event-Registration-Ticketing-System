using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Domain.Entites
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int AttendeeId { get; set; }
        public User Attendee { get; set; }

        public string TicketNumber { get; set; } =Guid.NewGuid().ToString();
        public DateTime BookingTime { get; set; }=DateTime.UtcNow;
    }
}
