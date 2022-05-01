using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreReactDemo.Services
{
    public class BearerTokenAuthenticationManager : IAuthenticationManager
    {
        private readonly string _tokenKey;

        public BearerTokenAuthenticationManager(IOptions<JwtTokenOptions> options)
        {
            _tokenKey = options.Value.Secret;
        }

        public string Authenticate(UserCredential credential)
        {
            // always allowed
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Upn, credential.Upn)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
