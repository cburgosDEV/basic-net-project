using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using Application.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameter query)
        {
            return Ok(await _mediator.Send(new GetAllQuery
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
            }));
        }
        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }      
    }
}
