using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.Clients.Commands
{
    public sealed record CreateClientCommand : IRequest<Response<bool>>
    {
        public string FullName { get; set; }
    }
}
