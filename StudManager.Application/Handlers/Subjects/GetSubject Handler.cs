using AutoMapper;
using MediatR;
using StudManager.Application.Queries.Subjects;
using StudManager.Application.Responses.Subjects;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Subjects
{
    public class GetSubject_Handler : IRequestHandler<GetSubjectQuery, SubjectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubject_Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  async Task<SubjectResponse> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subject.GetById(request.Id);

            if (subject == null)
                return null;

            var subjectResult = _mapper.Map<Subject, SubjectResponse>(subject);

            return subjectResult;

        }
    }
}
