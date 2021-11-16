using MediatR;
using StudManager.Application.Queries.Subjects;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Subjects
{
    public class GetAllSubejctHandler : IRequestHandler<GetAllSubjectQuery, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSubejctHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> Handle(GetAllSubjectQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _unitOfWork.Subject.All();
            return subjects;
        }
    }
}
