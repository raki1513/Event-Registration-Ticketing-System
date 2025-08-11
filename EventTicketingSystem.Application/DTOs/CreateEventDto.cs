using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.Application.DTOs
{
    public class CreateEventDto
    {

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title {  get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Capacity { get; set; }



        [Required]
        [EmailAddress]
        public string HostEmail { get; set; }
    }
}
