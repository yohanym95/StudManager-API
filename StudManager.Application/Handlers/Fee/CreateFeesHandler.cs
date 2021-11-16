using AutoMapper;
using MediatR;
using StudManager.Application.Commands.Fee;
using StudManager.Application.Responses.Fee;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Fee
{
    public class CreateFeesHandler : IRequestHandler<CreateFessCommand, FeesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFeesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeesResponse> Handle(CreateFessCommand request, CancellationToken cancellationToken)
        {
            var fees = _mapper.Map<CreateFessCommand, Fees>(request);

            await _unitOfWork.Fees.Add(fees);
            await _unitOfWork.CompleteAsync();
            var feeResult = _mapper.Map<Fees, FeesResponse>(fees);
            return feeResult;

        }
    }
}
