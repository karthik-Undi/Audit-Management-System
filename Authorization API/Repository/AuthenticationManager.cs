using AuthorizationAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class AuthenticationManager : IAuthenticationManager
    {
        AuditManagementSystemContext _context = new AuditManagementSystemContext();
        private readonly string tokenKey;
        public AuthenticationManager(string TokenKey)
        {
            
            this.tokenKey = TokenKey;
            

        }
        public AuthenticationManager()
        {

        }
        

        public AuthenticationManager(string TokenKey,AuditManagementSystemContext context)
        {
            this.tokenKey = TokenKey;
            _context = context;
        }

        public string Authenticate(string email, string password)
        {
            if (!_context.Userdetails.Any(u => u.Email == email && u.Password == password))
            {
                return null;

            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public int GetUserid(string email)
        {
            int userId =  _context.Userdetails.Single(user => user.Email == email).Userid;
            return userId;
        }
    }
}
