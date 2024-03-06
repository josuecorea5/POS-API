using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Entities;

namespace POS.Application.UseCases.Products.Commands
{
	public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var newProduct = _mapper.Map<Product>(request);
			await _unitOfWork.ProductRepository.Insert(newProduct);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Product created successfully";
			}else
			{
				response.Message = "Something went wrong while creating the register";
			}

			return response;
		}
	}
}
