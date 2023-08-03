using AplicationDomainLayer___PizzaDay.DTOs;
using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Exceptions;
using AplicationDomainLayer___PizzaDay.Interfaces;
using AplicationDomainLayer___PizzaDay.PaginationEntity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PresentationLayer___PizzaDay.ApiResponse;
using PresentationLayer___PizzaDay.ControllerOrder;
using System.Net;

namespace PresentationLayer___PizzaDay.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerControllerOrder(3)]
    public class PizzaDayController : ControllerBase
    {
        private readonly IPizzaServices _pizzaServices;
        private readonly IMapper _mapper;
        private readonly IUrlServices _urlServices;
        private readonly PaginationDefaultOptions paginationDefaultOptions;
        private readonly IGetByServices _getByServices;

        public PizzaDayController(
            IPizzaServices pizzaServices, 
            IMapper mapper, 
            IUrlServices urlServices, 
            IOptions<PaginationDefaultOptions> options,
            IGetByServices getByServices) 
        {
            _pizzaServices = pizzaServices;
            _mapper = mapper;
            _urlServices = urlServices;
            paginationDefaultOptions = options.Value;
            _getByServices = getByServices;
        }
        /// <summary>
        /// Get all the Pizzas information
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaDTO>>))]
        public IActionResult GetAll([FromQuery] PaginationValues values)
        {
            values.ElementToShow = values.ElementToShow == 0 ? paginationDefaultOptions.DefaultElementToShow : values.ElementToShow;
            values.PageToShow = values.PageToShow == 0 ? paginationDefaultOptions.DefaultPageNumber : values.PageToShow;

            var Pizzas = _pizzaServices.GetAll(values);
            var PizzasDto = _mapper.Map<IEnumerable<PizzaDTO>>(Pizzas);

            var metadataOfPagination = new Metadata
            {
                TotalCountOfElements = Pizzas.TotalCount,
                ElementToShow = Pizzas.ElementToShow,
                CurrentPage = Pizzas.CurrentPage,
                TotalPages = Pizzas.TotalPages,
                HasNextPage = Pizzas.HasNextPage,
                HasPreviousPage = Pizzas.HasPreviousPage,
                NextPageUrl = _urlServices.UrlPagination(Url.RouteUrl(nameof(GetAll))).ToString(),
                PreviousPageUrl= _urlServices.UrlPagination(Url.RouteUrl(nameof(GetAll))).ToString()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadataOfPagination));

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaDTO>>(PizzasDto)
            {
                Meta = metadataOfPagination
            };

            return Ok(ApiResponsePizza);
        }

        /// <summary>
        /// Gell the information of a pizza by the id of it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<PizzaDTO>))]
        public async Task<IActionResult> GetById(int id)
        {
            var Pizza = await _pizzaServices.GetById(id);

            if (Pizza == null)
            {
                throw new GlobalBusinessExceptions("The Pizza that you are Looking isnt in the DataBase");
            }

            var PizzaDto = _mapper.Map<PizzaDTO>(Pizza);

            var ApiResponsePizza = new PizzaApiResponse<PizzaDTO>(PizzaDto);

            return Ok(ApiResponsePizza);
        }
        
        // Business Logic
        [HttpGet("Get-Pizzas-By-PreparationTime")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaDTO>>))]
        public IActionResult GetByPreparationTime([FromQuery]int PreparationTime)
        {
            var Pizzas = _getByServices.GetByPreparationTime(PreparationTime);
            var PizzasDto = _mapper.Map<IEnumerable<PizzaDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Pizzas-by-rating")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaDTO>>))]
        public IActionResult GetByRating([FromQuery]int Rating)
        {
            var Pizza = _getByServices.GetByRating(Rating);
            var PizzasDto = _mapper.Map<IEnumerable<PizzaDTO>>(Pizza);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Pizzas-By-ingredient")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaDTO>>))]
        public IActionResult GetByIngredient([FromQuery]string Ingredient) 
        {
            var Pizzas = _getByServices.GetByIngredient(Ingredient);
            var PizzasDto = _mapper.Map<IEnumerable<PizzaDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Most-Popular-Pizzas-By-Rating")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaDTO>>))]
        public IActionResult GetMostPopular()
        {
            var Pizzas = _getByServices.GetMostPopular();
            var PizzasDto = _mapper.Map<IEnumerable<PizzaDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Nutritional-Information-Of-Pizzas")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>))]
        public IActionResult GetNutritionalInformation()
        {
            var Pizzas = _getByServices.GetnutritionalInformation();

            var PizzasDto = _mapper.Map<IEnumerable<PizzaNutritionalInformationDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Nutritional-Information-By-id-Of-A-Pizza")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>))]
        public IActionResult GetNutritionalInformationById([FromQuery]int id)
        {
            var Pizzas = _getByServices.GetnutritionalInformationById(id);

            var PizzasDto = _mapper.Map<IEnumerable<PizzaNutritionalInformationDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }


        [HttpGet("Get-Nutritional-Information-By-Name-Of-A-Pizza")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>))]
        public IActionResult GetNutritionalInformationByName([FromQuery] string name)
        {
            var Pizzas = _getByServices.GetnutritionalInformationByName(name);

            var PizzasDto = _mapper.Map<IEnumerable<PizzaNutritionalInformationDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Nutritional-Information-By-Name-Of-All-Pizzas")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>))]
        public IActionResult GetNutritionalInformationByNameAll([FromQuery] string name)
        {
            var Pizzas = _getByServices.GetnutritionalInformationByNameOfAllPizza(name);

            var PizzasDto = _mapper.Map<IEnumerable<PizzaNutritionalInformationDTO>>(Pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaNutritionalInformationDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-A-Menu-Of-Pizza")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<IEnumerable<PizzaRamdonDTO>>))]
        public IActionResult GetAMenuOfPizza()
        {
            var pizzas = _getByServices.GetAMenuOfPizza();

            var PizzasDto = _mapper.Map<IEnumerable<PizzaRamdonDTO>>(pizzas);

            var ApiResponsePizza = new PizzaApiResponse<IEnumerable<PizzaRamdonDTO>>(PizzasDto);

            return Ok(ApiResponsePizza);
        }

        [HttpGet("Get-Ingredients-By-Name-Of-A-Pizza")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(object))]
        public IActionResult GetIngredientsByName([FromQuery]string name) 
        {
            var pizzas = _getByServices.GetIngredientsByPizzaName(name);
            return Ok(pizzas);
        }

        [HttpPut("Valorate-A-Pizza")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<PizzaDTO>))]
        public async Task<IActionResult> ValorateAPizza(int id, int NewRating)
        {
            var pizza = await _pizzaServices.GetById(id);

            _getByServices.ValorateAPizza(NewRating, pizza);

            var PizzaDto = _mapper.Map<PizzaDTO>(pizza);

            var ApiResponsePizza = new PizzaApiResponse<PizzaDTO>(PizzaDto);

            return Ok(ApiResponsePizza);
        }

        [HttpPut("Add-Comment-To-A-Pizza")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<PizzaDTO>))]
        public async Task<IActionResult> CommentAPizza(int id, string NewComment)
        {
            var pizza = await _pizzaServices.GetById(id);

            _getByServices.NewCommentPizza(NewComment, pizza);

            var PizzaDto = _mapper.Map<PizzaDTO>(pizza);

            var ApiResponsePizza = new PizzaApiResponse<PizzaDTO>(PizzaDto);

            return Ok(ApiResponsePizza);
        }
        /// <summary>
        /// Add a Pizza Prescription.
        /// </summary>
        /// <param name="pizzaDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<PizzaDTO>))]
        public async Task<IActionResult> Insert(PizzaDTO pizzaDTO)
        {
            var PizzaEntity = _mapper.Map<Pizza>(pizzaDTO);

            PizzaEntity.Calories = 210;
            PizzaEntity.Fats = 8;
            PizzaEntity.Carbohydrates = 70;
            PizzaEntity.Proteins = 9;

            _pizzaServices.Insert(PizzaEntity);

            var ApiResponsePizza = new PizzaApiResponse<PizzaDTO>(pizzaDTO);

            return Ok(ApiResponsePizza);
        }
        /// <summary>
        /// Update a pizza.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pizzaDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<PizzaDTO>))]
        public async Task<IActionResult> Update(int id, PizzaDTO pizzaDTO)
        {
            var pizzaEntity = _mapper.Map<Pizza>(pizzaDTO);
            pizzaEntity.Id = id;
            _pizzaServices.Update(pizzaEntity);

            var ApiResponsePizza = new PizzaApiResponse<PizzaDTO>(pizzaDTO);

            return Ok(ApiResponsePizza);
        }
        /// <summary>
        /// Delete a Pizza.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GlobalBusinessExceptions"></exception>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PizzaApiResponse<bool>))]
        public async Task<IActionResult> Delete(int id)
        {
            var Pizza = await _pizzaServices.GetById(id);

            if (Pizza == null)
            {
                throw new GlobalBusinessExceptions("The Pizza that you are Looking isnt in the DataBase");
            }

            var Result = _pizzaServices.Delete(Pizza);

            var ApiResponsePizza = new PizzaApiResponse<bool>(Result);

            return Ok(ApiResponsePizza);
        }
    }
}
