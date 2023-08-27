using Common.Controllers;
using Identity.DTOs;
using Identity.Repositories.Interfaces;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountController(IUserRepository userRepository,JwtTokenHandler jwtTokenHandler)
        {
            _userRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthRequest userLogin)
        {
            var candidate = await _userRepository.AuthenticateUser(userLogin.UserName, userLogin.Password);

            if (candidate != null)
            {
                var loginUser = await _userRepository.GetUserById(candidate.UserId);
                var userRoles = await _userRepository.GetRolesByUserIdAsync(loginUser.UserId);

                var claims = new List<Claim>();
                claims.Add(new Claim("UserName", loginUser.Username));
                claims.Add(new Claim("Email", loginUser.Email));
                claims.Add(new Claim("UserId", loginUser.UserId.ToString()));
                claims.Add(new Claim("MemberId", Convert.ToInt32(loginUser.MemberId).ToString()));
                claims.Add(new Claim("MemberName", loginUser.MemberName?? ""));
                claims.Add(new Claim("MemberNo", loginUser.MemberNo ?? ""));

                // Add roles as multiple claims
                var roles = new List<string>();
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    roles.Add(role.Name);
                }

                var tokenresponse = _jwtTokenHandler.GenerateJwtToken(
                                loginUser.Username,
                                roles,
                                JWTSettings.Secret,
                                JWTSettings.Issuer,
                                JWTSettings.Audience,
                                TimeSpan.FromMinutes(JWTSettings.TokenTimeoutMinutes),
                                claims.ToArray(),
                                loginUser.MemberNo);

                return Ok(tokenresponse);
            }
            else
            {
                return Unauthorized("Invalid Login Credentials!");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("GetRoles")]
        
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _userRepository.GetRolesAsync());
        }
    }
}
