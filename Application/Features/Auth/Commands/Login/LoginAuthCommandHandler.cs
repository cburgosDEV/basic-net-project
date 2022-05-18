using Application.Contracts.Identity;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, CustomResponse<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public LoginAuthCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CustomResponse<AuthResponse>> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Login(request);
        }
    }
}
