using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Clients.Commands
{
	public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DeleteClientHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var client = await _unitOfWork.ClientRepository.GetById(request.Id);
			_unitOfWork.ClientRepository.Delete(client);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result >  0)
			{
				response.Success = true;
				response.Message = "Client deleted successfully";
			}else
			{
				response.Message = "Something went wrong while deleting the register";
			}

			return response;
		}
	}
}
