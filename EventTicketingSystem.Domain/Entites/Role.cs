using EventTicketingSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Domain.Entites
{
    public class Role
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public RoleType RoleName { get; set; } 

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
