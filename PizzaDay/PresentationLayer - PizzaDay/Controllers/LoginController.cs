using AplicationDomainLayer___PizzaDay.DTOs;
using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PresentationLayer___PizzaDay.ApiResponse;
using PresentationLayer___PizzaDay.ControllerOrder;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PresentationLayer___PizzaDay.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerControllerOrder(2)]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISignUpServices _signUpServices;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public LoginController(
            IConfiguration configuration, 
            ISignUpServices signUpServices, 
            IMapper mapper, 
            IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _signUpServices = signUpServices;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        /// <summary>
        /// Get the token using the Username and the Password That you create in the SignUp Method, for to login.
        /// </summary>
        /// <param name="SignUpDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<IActionResult> SignUp([FromQuery] ChefDTO SignUpDTO)
        {
            var SignUp = _mapper.Map<Chef>(SignUpDTO);

            var validation = await IsValidUser(SignUp);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);

                return Ok(new { token });
            }

            return NotFound();
        }

        private async Task<(bool, Chef)> IsValidUser(Chef SignUp)
        {
            var chef = await _signUpServices.SignUp(SignUp);

            var IsValid = _passwordHasher.Check(chef.UserPassword, SignUp.UserPassword);

            return (IsValid, chef);
        }

        private string GenerateToken(Chef SignUp)
        {
            var SecutiryKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));

            var SigningCredentiasl = new SigningCredentials(SecutiryKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(SigningCredentiasl);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, SignUp.UserName),
                new Claim(ClaimTypes.Email, "Chef_Admin@gmail.com123")
            };

            var payload = new JwtPayload(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(60));

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
