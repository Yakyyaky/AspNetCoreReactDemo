using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreReactDemo.Services
{
    public class BearerTokenAuthenticationManager : IAuthenticationManager
    {
        private readonly IUserService _userService;
        private readonly string _tokenKey;

        public BearerTokenAuthenticationManager(IOptions<JwtTokenOptions> options, IUserService userService)
        {
            _userService = userService;
            _tokenKey = options.Value.Secret;
        }

        public async Task<AuthenticatedUser> SignIn(SignInCredential credential)
        {
            var user = await _userService.GetUser(credential);
            if (user == null)
            {
                return null;
            }

            // always allowed
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Upn, credential.Upn)
                }),
                Expires = DateTime.UtcNow.AddHours(1),  // after an hour the user will have to re-authenticate.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticatedUser(user, tokenHandler.WriteToken(token));
        }

        public Task<bool> SignOut(string upn)
        {
            // TODO: perform sign out logic, currently access token is handled by client and is not actually revoked on the server.
            return Task.FromResult(true);
        }
    }
}
