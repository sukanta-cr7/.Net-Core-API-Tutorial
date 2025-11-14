using System.ComponentModel.DataAnnotations;

namespace Dot_Net_Core_Tutorial.DTOs
{
    public class CustomersDTO
    {
        [Required,MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required,EmailAddress,MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required,Phone,MaxLength(10)]
        public string Phone { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

    }
}
