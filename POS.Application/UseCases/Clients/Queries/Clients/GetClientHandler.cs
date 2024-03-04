using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Clients;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Clients.Queries.Clients
{
	public class GetClientHandler : IRequestHandler<GetClientQuery, Response<ClientDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetClientHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<ClientDto>> Handle(GetClientQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<ClientDto>();

			var client = await _unitOfWork.ClientRepository.GetById(request.Id);

			if (client is null)
			{
				response.Message = "client was not found";
				return response;
			}

			response.Success = true;
			response.Data = _mapper.Map<ClientDto>(client);

			return response;
		}
	}
}
