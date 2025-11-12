using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_Tutorial.Models
{
    public class LoginDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
