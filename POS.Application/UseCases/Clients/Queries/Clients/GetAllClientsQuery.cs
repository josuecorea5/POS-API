using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Clients;

namespace POS.Application.UseCases.Clients.Queries.Clients
{
	public sealed record GetAllClientsQuery : IRequest<Response<IEnumerable<ClientDto>>>
	{
	}
}
