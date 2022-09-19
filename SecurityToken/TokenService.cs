using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecurityToken
{
    public static class TokenService
    {
		const string Issuer = "http://localhost:52120";
		const string Audience = "http://localhost:33828";

		public static string GetToken(string userName, string gameId)
        {
			var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("GameId", gameId),
					new Claim("UserName", userName),
				}),
				Expires = DateTime.UtcNow.AddHours(2),
				Issuer = Issuer,
				Audience = Audience,
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
    }
}
