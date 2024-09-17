using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateToken(string UserID,string Email,string FullName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", UserID),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Name, FullName),
                }),
                Expires = DateTime.Now.AddDays(3),
                Issuer = "ProjectManagement",
                Audience = "ProjectManagement-Users",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
