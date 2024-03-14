using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class CreateNewRechargeSaleHandler : IRequestHandler<CreateNewRechargeSaleCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CreateNewRechargeSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(CreateNewRechargeSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var rechargeSale = await _unitOfWork.RechargeSaleRepository.GetById(request.RechargeId);

			if(rechargeSale.RechargeSaleStatus != RechargeSaleStatus.OVERDUE)
			{
				response.Message = "The recharge sale state is not overdue";
				return response;
			}

			var transaction = _unitOfWork.BeginTransaction();

			try
			{
				var newRechargeSale = _mapper.Map<RechargeSale>(request);
				newRechargeSale.Percentage = rechargeSale.Percentage;
				newRechargeSale.SaleId = rechargeSale.SaleId;
				newRechargeSale.NewTotal = rechargeSale.NewTotal + (rechargeSale.NewTotal * rechargeSale.Percentage);

				await _unitOfWork.RechargeSaleRepository.Insert(newRechargeSale);
				await _unitOfWork.SaveChanges(cancellationToken);
				_unitOfWork.RechargeSaleRepository.Delete(rechargeSale);
				await _unitOfWork.SaveChanges(cancellationToken);

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				response.Message = ex.Message;
				return response;
			}
			
			response.Success = true;
			response.Message = "New recharge sale created successfully";

			return response;
		}
	}
}
