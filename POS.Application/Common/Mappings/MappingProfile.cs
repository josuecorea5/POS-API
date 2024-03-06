using AutoMapper;
using POS.Application.DTOs.Clients;
using POS.Application.DTOs.Products;
using POS.Application.UseCases.Clients.Commands;
using POS.Application.UseCases.Products.Commands;
using POS.Domain.Entities;

namespace POS.Application.Common.Mappings
{
    public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Client, CreateClientCommand>().ReverseMap();
            CreateMap<Client, UpdateClientCommand>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
