using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Clients;
using POS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.UseCases.Clients.Queries.Clients
{
	public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, Response<IEnumerable<ClientDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetAllClientsHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<IEnumerable<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<IEnumerable<ClientDto>>();
			var clients = await _unitOfWork.ClientRepository.GetAll();

			if (clients is null)
			{
				response.Message = "Something went wrong with the request";
				return response;
			}
			response.Success = true;
			response.Data = _mapper.Map<IEnumerable<ClientDto>>(clients);
			response.Message = "Request successfully";

			return response;
		}
	}
}
