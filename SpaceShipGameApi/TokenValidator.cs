using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipGameApi
{
	public static class TokenValidator
	{
		const string Issuer = "http://localhost:52120";
		const string Audience = "http://localhost:33828";

		public static bool ValidateToken(string token)
		{
			var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

			var tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = Issuer,
					ValidAudience = Audience,
					IssuerSigningKey = mySecurityKey
				}, out SecurityToken validatedToken);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static (string, string) GetClaims(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

			string gameId = securityToken.Claims.First(claim => claim.Type == "GameId").Value;
			string userName = securityToken.Claims.First(claim => claim.Type == "UserName").Value;

			return (gameId, userName);
		}
	}
}
