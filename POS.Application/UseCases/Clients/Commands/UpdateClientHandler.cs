using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Clients.Commands
{
	public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateClientHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var updatedClient = await _unitOfWork.ClientRepository.GetById(request.Id);
			_mapper.Map(request, updatedClient);
			_unitOfWork.ClientRepository.Update(updatedClient);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if (result > 0)
			{
				response.Success = true;
				response.Data = true;
				response.Message = "Client updated successfully";
			}
			else
			{
				response.Message = "Something went wrong while updateing the register";
			}

			return response;
		}
	}
}
