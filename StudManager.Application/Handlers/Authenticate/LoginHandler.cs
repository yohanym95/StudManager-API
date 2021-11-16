using AutoMapper;
using MediatR;
using StudManager.Application.Commands.Authenticate;
using StudManager.Application.Responses.Authenticate;
using StudManager.Core.Models;
using StudManager.Core.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace StudManager.Application.Handlers.Authenticate
{
    public class LoginHandler : IRequestHandler <LoginCommand, LoginResponse>
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IMapper _mapper;

        public LoginHandler(IAuthenticateService authenticateService, IMapper mapper)
        {
            _authenticateService = authenticateService;
            _mapper = mapper;


        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var loginModel = _mapper.Map<LoginCommand, LoginModel>(request);

            var result = await _authenticateService.userAuthenticate(loginModel);

            var results = new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(result),
                Expiration = result.ValidTo
            };

            return results;
        }
    }
}
