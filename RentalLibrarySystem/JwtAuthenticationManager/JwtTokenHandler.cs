using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        private JwtSecurityToken GetJwtToken(string username,string signingKey,string issuer,string audience,
        TimeSpan expiration,Claim[] additionalClaims)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub,username),
            // this guarantees the token is unique
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            if (additionalClaims is object)
            {
                var claimList = new List<Claim>(claims);
                claimList.AddRange(additionalClaims);
                claims = claimList.ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.Add(expiration),
                claims: claims,
                signingCredentials: creds
            );

        }

        public TokenResponse GenerateJwtToken(string username,List<string> roles, string signingKey, string issuer, string audience,
        TimeSpan expiration, Claim[] additionalClaims,string? memberNo=null)
        {
            var token = GetJwtToken(username,signingKey, issuer, audience, expiration, additionalClaims);

            var response = new TokenResponse()
            {
                Username = memberNo== null ? username : $"{username}({memberNo})",
                Roles = roles,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOnUtc = token.ValidTo
            };

            return response;
        }
    }
}
