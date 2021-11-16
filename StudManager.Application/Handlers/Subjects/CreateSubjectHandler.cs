using AutoMapper;
using MediatR;
using StudManager.Application.Commands.Subjects;
using StudManager.Application.Responses.Common;
using StudManager.Core.Configuration;
using StudManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Subjects
{
    public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSubjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = _mapper.Map<CreateSubjectCommand, Subject>(request);

            await _unitOfWork.Subject.Add(subject);
            await _unitOfWork.CompleteAsync();

            return new Response { Status = "Success", Message = "Successfully Added the subject" };
        }
    }
}
