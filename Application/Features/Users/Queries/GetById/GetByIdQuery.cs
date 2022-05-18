using Application.DTOs;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.Queries.GetById
{
    public class GetByIdQuery : IRequest<CustomResponse<UserDto>>
    {
        public int Id { get; set; }
    }
}
