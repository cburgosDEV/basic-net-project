using Application.DTOs;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using Application.Wrappers;

namespace Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<CustomResponse<AuthResponse>> Login(LoginAuthCommand command);
        Task<CustomResponse<AuthResponse>> Register(RegisterAuthCommand command);
        Task<PaginationResponse<List<UserDto>>> GetAll(GetAllQuery query);
        Task<CustomResponse<UserDto>> GetById(GetByIdQuery query);
    }
}
