using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersAPI.Models;

namespace UsersAPI.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> identityUser)
        {
            var userClaims = new Claim[]
            {
                new Claim("username", identityUser.UserName),
                new Claim("id", identityUser.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djdsadjsadjsadjsd09asjd09sajcnzxn"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: userClaims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return  new Token(tokenString);
        }
    }
}
