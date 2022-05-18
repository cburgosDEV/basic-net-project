using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginAuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterAuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
