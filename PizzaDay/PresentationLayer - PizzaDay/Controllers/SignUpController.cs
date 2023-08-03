using AplicationDomainLayer___PizzaDay.DTOs;
using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer___PizzaDay.ApiResponse;
using PresentationLayer___PizzaDay.ControllerOrder;
using System.Net;

namespace PresentationLayer___PizzaDay.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerControllerOrder(1)]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpServices _signUpServices;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public SignUpController(ISignUpServices signUpServices,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _signUpServices = signUpServices;
            _passwordHasher = passwordHasher;
        }
        /// <summary>
        /// Complete the box for to create a Chef User for to Login in the Login method.
        /// </summary>
        /// <param name="chefDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<ChefDTO>))]
        public async Task<IActionResult> SignUp([FromQuery]ChefDTO chefDTO)
        {
            var chef = _mapper.Map<Chef>(chefDTO);

            chef.UserPassword = _passwordHasher.Hash(chef.UserPassword);

            await _signUpServices.RegisterChef(chef);

            chefDTO = _mapper.Map<ChefDTO>(chef);

            var ApiResponsePizza = new PizzaApiResponse<ChefDTO>(chefDTO);
            return Ok(ApiResponsePizza);
        }
    }
}
