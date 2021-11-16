using AutoMapper;
using MediatR;
using StudManager.Application.Queries.Fee;
using StudManager.Application.Responses.Fee;
using StudManager.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Fee
{
    public class GetFeesHandler : IRequestHandler<GetFeesQuery, GetFeesReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetFeesReponse> Handle(GetFeesQuery request, CancellationToken cancellationToken)
        {
            var fees = await _unitOfWork.Fees.GetById(request.Id);

            if (fees == null)
                return null;

            var student = await _unitOfWork.Student.GetStudent(fees.StuId);

            var feesModel = new GetFeesReponse
            {
                Id = fees.Id,
                FeesType = fees.FeesType,
                FeesDescription = fees.FeesDescription,
                AmountofFees = fees.AmountofFees,
                RecieptNo = fees.RecieptNo,
                studName = student.FullName,
                StuId = fees.StuId,
                studRegNo = student.StudRegNo
            };

            return feesModel;
        }
    }
}
