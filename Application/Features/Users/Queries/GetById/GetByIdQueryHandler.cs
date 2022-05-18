using Application.Contracts.Identity;
using Application.DTOs;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CustomResponse<UserDto>>
    {
        private readonly IAuthService _authService;

        public GetByIdQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<CustomResponse<UserDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _authService.GetById(request);
        }
    }
}
