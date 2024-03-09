using AutoMapper;
using POS.Application.DTOs.Clients;
using POS.Application.DTOs.Products;
using POS.Application.DTOs.SaleDetails;
using POS.Application.DTOs.Sales;
using POS.Application.UseCases.Clients.Commands;
using POS.Application.UseCases.Products.Commands;
using POS.Application.UseCases.SaleDetails.Commands;
using POS.Application.UseCases.Sales.Commands;
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
            CreateMap<Sale, CreateSaleCommand>().ReverseMap().ForMember(s => s.SaleDetails, opt => opt.Ignore());
            CreateMap<SaleDetail, CreateSaleDetailCommand>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<SaleDetail, SaleDetailDto>().ReverseMap();
            CreateMap<UpdateSaleCommand, Sale>();
            CreateMap<UpdateSaleDetailCommand, SaleDetail>();
            CreateMap<DeleteSaleDetailCommand, SaleDetail>();
        }
    }
}
