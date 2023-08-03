using AplicationDomainLayer___PizzaDay.DTOs;
using AplicationDomainLayer___PizzaDay.Entities;
using AutoMapper;

namespace InfrastructureLayer___PizzaDay.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Pizza, PizzaDTO>();
            CreateMap<PizzaDTO, Pizza>();

            CreateMap<Pizza, PizzaNutritionalInformationDTO>();
            CreateMap<PizzaNutritionalInformationDTO, Pizza>();

            CreateMap<Pizza, PizzaRamdonDTO>();
            CreateMap<PizzaRamdonDTO, Pizza>();

            CreateMap<Chef, ChefDTO>();
            CreateMap<ChefDTO, Chef>();

            CreateMap<Pizza, PrescriptionPizzaDTO>();
            CreateMap<PrescriptionPizzaDTO, Pizza>();
        }
    }
}
