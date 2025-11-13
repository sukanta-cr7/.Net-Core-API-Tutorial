using Dot_Net_Core_Tutorial.DTOs;
using Dot_Net_Core_Tutorial.Models;
using Dot_Net_Core_Tutorial.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;
        public AuthController(IAuthService authService) 
        {
            _authservice = authService;
        }
        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult<LoginDetails>> Register(LoginDetailsDTO details)
        {
            var user = await _authservice.RegisterAsync(details);
            if (user == null)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDetailsDTO details)
        {
            var token = await _authservice.LoginAsync(details);
            if (token == null)
            {
                return BadRequest("Username/Password is wrong.");
            }
            return Ok(token);
        }
        [HttpGet("Auth-endpoint")]
        [Authorize]
        public ActionResult AuthCheck()
        {
            return Ok("Authentication Successful.");
        }
    }
}
