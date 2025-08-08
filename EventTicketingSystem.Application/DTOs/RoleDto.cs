using EventTicketingSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.Application.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 2)]
        public RoleType Name { get; set; }
    }
}
