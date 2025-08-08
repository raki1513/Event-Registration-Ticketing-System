using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Domain.Entites
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string PasswordHash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
       
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Event> HostedEvents { get; set; }  
    }
}
