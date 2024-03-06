using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Products.Commands
{
	public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var product = await _unitOfWork.ProductRepository.GetById(request.Id);
			_mapper.Map(request, product);
			_unitOfWork.ProductRepository.Update(product);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if (result > 0)
			{
				response.Success = true;
				response.Data = true;
				response.Message = "Product updated successfully";
			}
			else
			{
				response.Message = "Something went wrong while updateing the register";
			}

			return response;
		}
	}
}
