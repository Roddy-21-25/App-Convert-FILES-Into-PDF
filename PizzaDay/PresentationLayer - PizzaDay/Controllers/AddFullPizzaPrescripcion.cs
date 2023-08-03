using AplicationDomainLayer___PizzaDay.DTOs;
using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer___PizzaDay.ApiResponse;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PresentationLayer___PizzaDay.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddFullPizzaPrescripcion : ControllerBase
    {
        private readonly IPizzaServices _pizzaServices;
        private readonly IMapper _mapper;
        public AddFullPizzaPrescripcion(IPizzaServices pizzaServices, IMapper mapper)
        {
            _pizzaServices = pizzaServices;
            _mapper = mapper;
        }
        /// <summary>
        /// ADD A FULL PRESCRIPTION OF A PIZZA!!!
        /// </summary>
        /// <param name="prescriptionPizzaDTO"></param>
        /// <returns></returns>
        [HttpPost("Post-A-New-Prescription")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<PrescriptionPizzaDTO>))]
        public IActionResult PostNewPrescription([FromQuery]PrescriptionPizzaDTO prescriptionPizzaDTO) 
        {
            var NewPizzaPrescription = _mapper.Map<Pizza>(prescriptionPizzaDTO);
            _pizzaServices.Insert(NewPizzaPrescription);

            var ApiResponsePizza = new PizzaApiResponse<PrescriptionPizzaDTO>(prescriptionPizzaDTO);
            return Ok(ApiResponsePizza);
        }
    }
}
