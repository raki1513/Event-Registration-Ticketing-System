using EventTicketingSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Application.DTOs
{
    public class AssignRoleDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(30,MinimumLength =2)]
        public RoleType RoleName { get; set; }
    }
}
