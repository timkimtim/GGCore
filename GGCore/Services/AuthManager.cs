using GGCore.DTOs;
using GGCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GGCore.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private User _user;

        public AuthManager(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            _user = await _userManager.FindByEmailAsync(userDTO.Email);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("GGKEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["GGKey"]));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _config.GetSection("JWTSettings");
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("Lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
                );

            return token;
        }
    }
}
