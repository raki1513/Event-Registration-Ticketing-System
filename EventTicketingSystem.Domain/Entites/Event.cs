using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Domain.Entites
{
    public class Event
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Location { get; set; } 
        public DateTime Time { get; set; }
        public int Capacity { get; set; }

        public int HostId { get; set; }
        public User Host { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
