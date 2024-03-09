﻿using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Sales.Commands
{
	public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var client = await _unitOfWork.ClientRepository.GetById(request.ClientId);
			if(client is null)
			{
				response.Message = "Client not found";
				return response;
			}

			var sale = await _unitOfWork.SaleRepository.GetById(request.Id);

			_mapper.Map(request, sale);

			_unitOfWork.SaleRepository.Update(sale);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Sale updated successfully";
			}
			else
			{
				response.Message = "Something went wrong while updateing the register";
			}

			return response;
		}
	}
}
