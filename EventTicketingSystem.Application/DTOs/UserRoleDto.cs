using EventTicketingSystem.Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.Application.DTOs
{
    public class UserRoleDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
       
    }
}
