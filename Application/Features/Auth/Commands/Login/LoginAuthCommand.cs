using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginAuthCommand : IRequest<CustomResponse<AuthResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
