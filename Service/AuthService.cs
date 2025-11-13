using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dot_Net_Core_Tutorial.DTOs;
using Dot_Net_Core_Tutorial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Dot_Net_Core_Tutorial.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration configuration;
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this.configuration = configuration;
        }
        public async Task<LoginDetails?> RegisterAsync(LoginDetailsDTO details)
        {
            if (await _context.LoginDetails.AnyAsync(a => a.UserName == details.UserName))
                return null;
            var loginDetails = new LoginDetails();
            loginDetails.UserName = details.UserName;
            loginDetails.PasswordHash = new PasswordHasher<LoginDetails>().HashPassword(loginDetails, details.Password);
            _context.LoginDetails.Add(loginDetails);
            await _context.SaveChangesAsync();
            return loginDetails;
        }

        public async Task<string?> LoginAsync(LoginDetailsDTO details)
        {
            LoginDetails? loginDetails = await _context.LoginDetails.FirstOrDefaultAsync(a => a.UserName == details.UserName);
            if (loginDetails == null)
                return null;

            if (new PasswordHasher<LoginDetails>().VerifyHashedPassword(loginDetails, loginDetails.PasswordHash, details.Password) == PasswordVerificationResult.Failed)
                return null;
            string token = CreateToken(details);
            return token;
        } 
        private string CreateToken(LoginDetailsDTO loginDetails)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginDetails.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:SecretKey")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("JWT:Issuer")!,
                audience : configuration.GetValue<string>("JWT:Audience")!,
                claims : claims,
                signingCredentials : creds,
                expires: DateTime.Now.AddHours(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
