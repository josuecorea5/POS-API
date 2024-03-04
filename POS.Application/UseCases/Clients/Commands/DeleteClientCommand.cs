using MediatR;
using POS.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.UseCases.Clients.Commands
{
	public class DeleteClientCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
	}
}
