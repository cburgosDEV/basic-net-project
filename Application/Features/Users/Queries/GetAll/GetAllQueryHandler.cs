using Application.Contracts.Identity;
using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginationResponse<List<UserDto>>>
    {
        private readonly IAuthService _authService;

        public GetAllQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<PaginationResponse<List<UserDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _authService.GetAll(request);
        }
    }
}
