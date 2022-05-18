using Application.Contracts.Identity;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    internal class RegisterAuthCommandHandler : IRequestHandler<RegisterAuthCommand, CustomResponse<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public RegisterAuthCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CustomResponse<AuthResponse>> Handle(RegisterAuthCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Register(request);
        }
    }
}
