﻿using Collectors_Corner_Backend.Models.DataBase;
using Collectors_Corner_Backend.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Collectors_Corner_Backend.Services
{
	public class TokenService
	{
		private readonly TokenSettings _tokenSettings;
		public TokenService(IOptions<TokenSettings> jwtSettings)
		{
			_tokenSettings = jwtSettings.Value;
		}
		public string GenerateJwtToken(string username, string email, out DateTime expires)
		{
			expires = DateTime.Now.AddMinutes(_tokenSettings.TokenLifeTime);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.Email, email),
				new Claim(ClaimTypes.Role, "User")
			};
			var jwt = new JwtSecurityToken(
			issuer: _tokenSettings?.Issuer,
			audience: _tokenSettings?.Audience,
			claims: claims,
				expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
				signingCredentials: new SigningCredentials(_tokenSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToHexString(randomNumber);
		}
	}
}
