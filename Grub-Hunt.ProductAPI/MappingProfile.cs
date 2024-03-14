using AutoMapper;
using Grub_Hunt.ProductAPI.DTOs;
using Grub_Hunt.ProductAPI.Models;

namespace Grub_Hunt.ProductAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
