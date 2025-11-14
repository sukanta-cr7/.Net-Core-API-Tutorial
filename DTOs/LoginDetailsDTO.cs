using System.ComponentModel.DataAnnotations;

namespace Dot_Net_Core_Tutorial.DTOs
{
    public class LoginDetailsDTO
    {
        [Required,MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required,MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
