using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Products.Commands
{
	public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var product = await _unitOfWork.ProductRepository.GetById(request.Id);
			_unitOfWork.ProductRepository.Delete(product);

			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Product deleted successfully";
			}
			else
			{
				response.Message = "Something went wrong while deleting the register";
			}

			return response;
		}
	}
}
