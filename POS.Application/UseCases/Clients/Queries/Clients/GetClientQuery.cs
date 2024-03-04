using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.UseCases.Clients.Queries.Clients
{
	public sealed record GetClientQuery : IRequest<Response<ClientDto>>
	{
		public int Id { get; set; }
	}
}
