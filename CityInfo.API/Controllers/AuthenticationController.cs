using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // Without Database

        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBodyDto authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody.Username, authenticationRequestBody.Password);

            if (user == null) 
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("UserID", user.UserID.ToString()));
            claimsForToken.Add(new Claim("FirstName", user.FirstName.ToString()));
            claimsForToken.Add(new Claim("City", user.City.ToString()));
            //claimsForToken.Add(new Claim(ClaimTypes.Email, user.Email.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredential
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private CityInfoUserDto ValidateUserCredentials(string? username, string? password) 
        {
            return new CityInfoUserDto (1, username ?? "", "Erfan", "Farhanian", "Tehran");
        }
    }
}
