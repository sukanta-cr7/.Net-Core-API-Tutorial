using Dot_Net_Core_Tutorial.DTOs;
using Dot_Net_Core_Tutorial.Models;

namespace Dot_Net_Core_Tutorial.Service
{
    public interface IAuthService
    {
        public Task<string?> LoginAsync(LoginDetailsDTO details);
        public Task<LoginDetails?> RegisterAsync(LoginDetailsDTO details);
    }
}