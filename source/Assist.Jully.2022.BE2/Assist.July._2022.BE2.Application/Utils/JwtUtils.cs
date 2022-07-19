using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assist.July._2022.BE2.Application.Utils
{

    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings AppSetting;
        public JwtUtils(IOptions<AppSettings> appsettings)
        {
            AppSetting = appsettings.Value;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSetting.SecretCode);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString().ToUpper()),
                    new Claim("Email", user.Email), new Claim("Role", user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public UserToken? ValidateToken(string Token)
        {
            if (Token == null)
                return null;
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSetting.SecretCode);
            TokenHandler.ValidateToken(Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken ValidatedToken
                );
            var JwtToken = (JwtSecurityToken)ValidatedToken;
            var userToken = new UserToken();
            userToken.id= Guid.Parse(JwtToken.Claims.First(x => x.Type == "id").Value);
            userToken.Email = JwtToken.Claims.First(x => x.Type == "Email").Value;
            userToken.Role = byte.Parse(JwtToken.Claims.First(x => x.Type == "Role").Value);
            return userToken;
        }
    }
}
