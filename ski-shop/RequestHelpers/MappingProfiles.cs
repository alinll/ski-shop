using AutoMapper;
using ski_shop.DTOs;
using ski_shop.Entities;

namespace ski_shop.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}