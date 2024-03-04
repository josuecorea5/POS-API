using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Entities;

namespace POS.Application.UseCases.Clients.Commands
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateClientHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var newClient = _mapper.Map<Client>(request);
            await _unitOfWork.ClientRepository.Insert(newClient);
            var result = await _unitOfWork.SaveChanges(cancellationToken);

            if (result > 0)
            {
                response.Success = true;
                response.Data = true;
                response.Message = "Client created successfully";
            }else
			{
				response.Message = "Something went wrong while creating the register";
			}

			return response;
        }
    }
}
