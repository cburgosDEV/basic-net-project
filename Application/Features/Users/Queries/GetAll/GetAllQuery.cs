using Application.DTOs;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.Queries.GetAll
{
    public class GetAllQuery : IRequest<PaginationResponse<List<UserDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
